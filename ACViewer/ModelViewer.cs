﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Framework.WpfInterop.Input;

using ACE.DatLoader;
using ACE.DatLoader.FileTypes;
using ACE.Entity.Enum;
using ACE.Server.Physics.Animation;

using ACViewer.Model;
using ACViewer.Render;
using ACViewer.View;

namespace ACViewer
{
    public enum ModelType
    {
        Setup,
        EnvCell,
        Environment,
    };

    public class ModelViewer
    {
        public static MainWindow MainWindow => MainWindow.Instance;
        public static GameView GameView => GameView.Instance;

        public static ModelViewer Instance { get; set; }

        public SetupInstance Setup;
        public R_EnvCell EnvCell;
        public R_Environment Environment;

        public static GraphicsDevice GraphicsDevice => GameView.GraphicsDevice;
        public Render.Render Render => GameView.Render;
        public static Effect Effect => ACViewer.Render.Render.Effect;
        public static Camera Camera => GameView.Camera;

        public ViewObject ViewObject { get; set; }

        public bool GfxObjMode = false;

        public WpfKeyboard Keyboard => GameView._keyboard;

        public KeyboardState PrevKeyboardState;

        public ModelType ModelType;

        public ModelViewer()
        {
            Instance = this;
        }

        public void LoadModel(uint id)
        {
            TextureCache.Init();

            // can be either a gfxobj or setup id
            // if gfxobj, create a simple setup
            MainWindow.Status.WriteLine($"Loading {id:X8}");
            GfxObjMode = id >> 24 == 0x01;

            Setup = new SetupInstance(id);
            InitObject(id);

            Render.Camera.InitModel(Setup.Setup.BoundingBox);

            ModelType = ModelType.Setup;
        }

        public void LoadModel(uint id, ClothingTable clothingBase, uint palTemplate, float shade)
        {
            TextureCache.Init();

            // can be either a gfxobj or setup id
            // if gfxobj, create a simple setup
            GfxObjMode = false;

            // create the objDesc, describing the "Changed" items to the base setup.
            // We won't bother loading the palette stuff, we'll just create that dictionary directly
            FileTypes.ObjDesc objDesc = new FileTypes.ObjDesc();

            var cbe = clothingBase.ClothingBaseEffects[id].CloObjectEffects;

            foreach (var objEffect in cbe)
            {
                ACE.DatLoader.Entity.AnimationPartChange apChange = new ACE.DatLoader.Entity.AnimationPartChange();
                apChange.PartID = objEffect.ModelId;
                apChange.PartIndex = (byte)objEffect.Index;

                objDesc.AnimPartChanges.Add(apChange.PartIndex, apChange);

                foreach(var texEffect in objEffect.CloTextureEffects)
                {
                    ACE.DatLoader.Entity.TextureMapChange tmChange = new ACE.DatLoader.Entity.TextureMapChange();
                    tmChange.PartIndex = apChange.PartIndex;
                    tmChange.OldTexture = texEffect.OldTexture;
                    tmChange.NewTexture = texEffect.NewTexture;

                    if (!objDesc.TextureChanges.TryGetValue(tmChange.PartIndex, out var tmChanges))
                    {
                        tmChanges = new List<ACE.DatLoader.Entity.TextureMapChange>();
                        objDesc.TextureChanges.Add(tmChange.PartIndex, tmChanges);
                    }
                    tmChanges.Add(tmChange);
                }
            }

            // To hold our Custom Palette (palette swaps)
            Dictionary<int, uint> customPaletteColors = new Dictionary<int, uint>();

            // Load all the custom palette colors...
            var subPalEffect = clothingBase.ClothingSubPalEffects[palTemplate];
            foreach (var subPals in subPalEffect.CloSubPalettes)
            {
                var palSet = DatManager.PortalDat.ReadFromDat<PaletteSet>(subPals.PaletteSet);
                uint palId = palSet.GetPaletteID(shade);
                // Load our palette dictated by the shade in the palset
                var palette = DatManager.PortalDat.ReadFromDat<Palette>(palId);
                foreach (var range in subPals.Ranges)
                {
                    int offset = (int)(range.Offset);
                    int numColors = (int)(range.NumColors);
                    // add the appropriate colors to our custom palette
                    for (int i = 0; i < numColors; i++)
                        customPaletteColors.Add(i + offset, palette.Colors[i + offset]);
                }
            }

            Setup = new SetupInstance(id, objDesc, customPaletteColors);

            if (ViewObject == null || ViewObject.PhysicsObj.PartArray.Setup._dat.Id != id)
            {
                InitObject(id);

                Render.Camera.InitModel(Setup.Setup.BoundingBox);
            }

            ModelType = ModelType.Setup;

            MainWindow.Status.WriteLine($"Loading {id:X8} with ClothingBase {clothingBase.Id:X8}, PaletteTemplate {palTemplate}, and Shade {shade}");
        }

        public void LoadEnvironment(uint envID)
        {
            ViewObject = null;
            Environment = new R_Environment(envID);

            ModelType = ModelType.Environment;
        }

        public void LoadEnvCell(uint envCellID)
        {
            var envCell = new ACE.Server.Physics.Common.EnvCell(DatManager.CellDat.ReadFromDat<EnvCell>(envCellID));
            envCell.Pos = new ACE.Server.Physics.Common.Position();
            EnvCell = new R_EnvCell(envCell);

            ModelType = ModelType.EnvCell;
        }

        public void LoadScript(uint scriptID)
        {
            var createParticleHooks = ParticleViewer.Instance.GetCreateParticleHooks(scriptID, 1.0f);

            ViewObject.PhysicsObj.destroy_particle_manager();
            
            foreach (var createParticleHook in createParticleHooks)
            {
                ViewObject.PhysicsObj.create_particle_emitter(createParticleHook.EmitterInfoId, (int)createParticleHook.PartIndex, new AFrame(createParticleHook.Offset), (int)createParticleHook.EmitterId);
            }
        }

        public void InitObject(uint setupID)
        {
            ViewObject = new ViewObject(setupID);

            if (Setup.Setup._setup.DefaultScript != 0)
                LoadScript(Setup.Setup._setup.DefaultScript);
        }

        public void DoStance(MotionStance stance)
        {
            if (ViewObject != null)
                ViewObject.DoStance(stance);
        }

        public void DoMotion(MotionCommand motionCommand)
        {
            if (ViewObject != null)
                ViewObject.DoMotion(motionCommand);
        }

        public static int PolyIdx = -1;

        public void Update(GameTime time)
        {
            if (ViewObject != null)
                ViewObject.Update(time);

            if (Camera != null)
                Camera.Update(time);
        }

        public void Draw(GameTime time)
        {
            Effect.Parameters["xWorld"].SetValue(Matrix.Identity);
            Effect.Parameters["xView"].SetValue(Camera.ViewMatrix);
            Effect.Parameters["xProjection"].SetValue(Camera.ProjectionMatrix);
            Effect.Parameters["xOpacity"].SetValue(1.0f);
            Effect.CurrentTechnique = Effect.Techniques["TexturedNoShading"];

            switch (ModelType)
            {
                case ModelType.Setup:
                    DrawModel();
                    break;
                case ModelType.Environment:
                    DrawEnvironment();
                    break;
                case ModelType.EnvCell:
                    DrawEnvCell();
                    break;
            }
        }

        public void DrawModel()
        {
            if (Setup == null) return;

            Setup.Draw(PolyIdx);

            if (ViewObject.PhysicsObj.ParticleManager != null)
                ParticleViewer.Instance.DrawParticles(ViewObject.PhysicsObj);
        }

        public void DrawEnvironment()
        {
            Effect.CurrentTechnique = Effect.Techniques["ColoredNoShading"];
            GraphicsDevice.Clear(new Color(48, 48, 48));

            if (Environment != null)
                Environment.Draw();
        }

        public void DrawEnvCell()
        {
            if (EnvCell != null)
                EnvCell.Draw(Matrix.Identity);
        }
    }
}

