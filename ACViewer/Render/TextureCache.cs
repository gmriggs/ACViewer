using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;

using Microsoft.Xna.Framework.Graphics;

using ACE.DatLoader;
using ACE.DatLoader.Extensions;

using ACViewer.Model;
using ACViewer.View;

using DatReaderWriter.DBObjs;
using DatReaderWriter.Enums;

namespace ACViewer.Render
{
    public class TextureCache
    {
        public static GraphicsDevice GraphicsDevice => GameView.Instance.GraphicsDevice;
        public static SpriteBatch SpriteBatch => GameView.Instance.SpriteBatch;

        public static Dictionary<TextureChanges, Texture2D> Textures { get; set; }

        public static List<Texture2D> Uncached { get; set; }

        public static bool UseMipMaps { get; set; }

        static TextureCache()
        {
            Init();
        }

        public static void Init(bool dispose = true)
        {
            if (dispose)
            {
                if (Textures != null)
                {
                    foreach (var texture in Textures.Values)
                        texture.Dispose();
                }
                
                if (Uncached != null)
                {
                    foreach (var texture in Uncached)
                        texture.Dispose();
                }
            }

            GfxObjCache.Init();
            SetupCache.Init();

            Textures = new Dictionary<TextureChanges, Texture2D>();
            Uncached = new List<Texture2D>();
        }

        private static Texture2D LoadTexture(uint textureID, bool useDummy = false, DatReaderWriter.DBObjs.Surface surface = null, PaletteChanges paletteChanges = null)
        {
            //Console.WriteLine($"--> TextureCache.LoadTexture({textureID:X8})");

            if (textureID >> 24 == 0x04)
                return LoadPalette(textureID);
            else if (textureID >> 24 == 0x0F)
                return LoadPaletteSet(textureID);

            var isClipMap = surface != null && surface.Type.HasFlag(SurfaceType.Base1ClipMap);

            MainWindow.Instance.Status.WriteLine($"Loading texture {textureID:X8}");

            if (!DatManager.PortalDat.TryReadFileCache(textureID, out RenderSurface texture) && DatManager.HighResDat != null)
                DatManager.HighResDat.TryReadFileCache(textureID, out texture);

            if (texture.SourceData == null)
            {
                Console.WriteLine($"TextureCache.LoadTexture({textureID:X8}) - couldn't find texture in portal or highres");
                var fallback = new Texture2D(GameView.Instance.GraphicsDevice, 2, 2, false, SurfaceFormat.Color);
                var color = new Microsoft.Xna.Framework.Color[4];
                color[0] = Microsoft.Xna.Framework.Color.Magenta;
                color[1] = Microsoft.Xna.Framework.Color.Black;
                color[2] = Microsoft.Xna.Framework.Color.Black;
                color[3] = Microsoft.Xna.Framework.Color.Magenta;
                fallback.SetDataAsync(color);
                return fallback;
            }

            var surfaceFormat = SurfaceFormat.Color;
            switch (texture.Format)
            {
                case DatReaderWriter.Enums.PixelFormat.PFID_DXT1:
                    surfaceFormat = SurfaceFormat.Dxt1;
                    break;
                case DatReaderWriter.Enums.PixelFormat.PFID_DXT3:
                    surfaceFormat = SurfaceFormat.Dxt3;
                    break;
                case DatReaderWriter.Enums.PixelFormat.PFID_DXT5:
                    //if (!isClipMap)
                    surfaceFormat = SurfaceFormat.Dxt5;
                    break;
                case DatReaderWriter.Enums.PixelFormat.PFID_CUSTOM_LSCAPE_ALPHA:
                case DatReaderWriter.Enums.PixelFormat.PFID_A8:
                case DatReaderWriter.Enums.PixelFormat.PFID_P8:    // indexed color
                    surfaceFormat = SurfaceFormat.Alpha8;
                    break;
            }

            var width = texture.Width;
            var height = texture.Height;

            var data = new byte[texture.SourceData.Length];
            Array.Copy(texture.SourceData, data, data.Length);  // fixme: multiple rgb reversals

            if (surfaceFormat == SurfaceFormat.Color)
            {
                switch (texture.Format)
                {
                    case DatReaderWriter.Enums.PixelFormat.PFID_R8G8B8:
                    case DatReaderWriter.Enums.PixelFormat.PFID_CUSTOM_LSCAPE_R8G8B8:
                        data = AddAlpha(data);
                        break;
                    case DatReaderWriter.Enums.PixelFormat.PFID_INDEX16:
                        data = IndexToColor(texture, isClipMap, paletteChanges);
                        break;
                    case DatReaderWriter.Enums.PixelFormat.PFID_CUSTOM_RAW_JPEG:
                    case DatReaderWriter.Enums.PixelFormat.PFID_R5G6B5:
                    case DatReaderWriter.Enums.PixelFormat.PFID_A4R4G4B4:
                    //case SurfacePixelFormat.PFID_DXT5:
                        var bitmap = texture.GetBitmap();
                        if (texture.Format == DatReaderWriter.Enums.PixelFormat.PFID_CUSTOM_RAW_JPEG)
                            SwapRedAndBlueChannels(bitmap);
                        var _tex = GetTexture2DFromBitmap(GameView.Instance.GraphicsDevice, bitmap);
                        //if (isClipMap)
                            //AdjustClip(_tex);
                        return _tex;

                    case DatReaderWriter.Enums.PixelFormat.PFID_A8R8G8B8:
                        ConvertToBGRA(data);
                        if (surface != null && surface.Translucency > 0)
                            PremultiplyAlpha(data, 1.0f - surface.Translucency);
                        break;
                }
            }

            Texture2D tex = null;
            if (useDummy)
            {
                if (surfaceFormat == SurfaceFormat.Alpha8)
                {
                    tex = new Texture2D(GameView.Instance.GraphicsDevice, 1, 1, false, SurfaceFormat.Alpha8);
                    var alpha = new byte[1];
                    alpha[0] = 255;
                    tex.SetDataAsync(alpha);
                }
                else
                {
                    tex = new Texture2D(GameView.Instance.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                    var color = new Microsoft.Xna.Framework.Color[1];
                    color[0].A = data[3];
                    color[0].R = data[2];
                    color[0].G = data[1];
                    color[0].B = data[0];
                    tex.SetDataAsync(color);
                }
            }
            else
            {
                if (UseMipMaps && surfaceFormat == SurfaceFormat.Color && texture.Width == texture.Height)
                    tex = GenerateMipMaps(data, texture.Width);
                else
                {
                    tex = new Texture2D(GameView.Instance.GraphicsDevice, texture.Width, texture.Height, false, surfaceFormat);
                    tex.SetDataAsync(data);
                }
            }
            return tex;
        }

        private static Texture2D LoadPalette(uint paletteID)
        {
            var colors = LoadPaletteColors(paletteID, out var width, out var height);

            var texture = new Texture2D(GameView.Instance.GraphicsDevice, width, height, false, SurfaceFormat.Color);
            texture.SetDataAsync(colors);

            return texture;
        }

        private static List<Microsoft.Xna.Framework.Color> Padding = Enumerable.Repeat(Microsoft.Xna.Framework.Color.Black, 384).ToList();

        private static Texture2D LoadPaletteSet(uint paletteSetID)
        {
            DatManager.PortalDat.TryReadFileCache(paletteSetID, out DatReaderWriter.DBObjs.PaletteSet paletteSet);

            var numPalettes = paletteSet.Palettes.Count;

            var colors = new List<Microsoft.Xna.Framework.Color>();

            for (var i = 0; i < paletteSet.Palettes.Count; i++)
            {
                var paletteID = paletteSet.Palettes[i];

                colors.AddRange(LoadPaletteColors(paletteID, out var width, out var height).ToList());
                if (i < paletteSet.Palettes.Count - 1)
                    colors.AddRange(Padding);
            }
            var setWidth = 64;
            var setHeight = colors.Count / setWidth;

            var texture = new Texture2D(GameView.Instance.GraphicsDevice, setWidth, setHeight, false, SurfaceFormat.Color);
            texture.SetDataAsync(colors.ToArray());

            return texture;
        }

        private static readonly int PaletteWidth = 64;

        private static Microsoft.Xna.Framework.Color[] LoadPaletteColors(uint paletteID, out int width, out int height)
        {
            DatManager.PortalDat.TryReadFileCache(paletteID, out DatReaderWriter.DBObjs.Palette palette);

            var numColors = palette.Colors.Count;

            width = Math.Min(numColors, PaletteWidth);
            height = (int)Math.Ceiling((float)numColors / PaletteWidth);

            var colors = new Microsoft.Xna.Framework.Color[width * height];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var colorNum = x + y * PaletteWidth;

                    if (colorNum >= numColors)
                    {
                        colors[colorNum] = new Microsoft.Xna.Framework.Color(0, 0, 0, 0);
                        continue;
                    }

                    var c = palette.Colors[colorNum];

                    var color = new Microsoft.Xna.Framework.Color();
                    color.A = c.Alpha;
                    color.R = c.Red;
                    color.G = c.Green;
                    color.B = c.Blue;

                    colors[colorNum] = color;
                }
            }
            return colors;
        }

        private static Texture2D GetTexture2DFromBitmap(GraphicsDevice device, Bitmap bitmap)
        {
            Texture2D tex = new Texture2D(device, bitmap.Width, bitmap.Height, false, SurfaceFormat.Color);

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

            int bufferSize = data.Height * data.Stride;

            // create data buffer 
            byte[] bytes = new byte[bufferSize];

            // copy bitmap data into buffer
            Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);

            // copy our buffer to the texture
            tex.SetDataAsync(bytes);

            // unlock the bitmap data
            bitmap.UnlockBits(data);

            return tex;
        }

        private static byte[] AddAlpha(byte[] rgb)
        {
            var rgba = new byte[rgb.Length + rgb.Length / 3];

            var idx = 0;
            for (var i = 0; i < rgb.Length; i += 3)
            {
                rgba[idx++] = rgb[i + 2];
                rgba[idx++] = rgb[i + 1];
                rgba[idx++] = rgb[i];
                rgba[idx++] = 255;
            }
            return rgba;
        }

        private static void ConvertToBGRA(byte[] rgba)
        {
            for (var i = 0; i < rgba.Length; i += 4)
            {
                var tmp = rgba[i];
                rgba[i] = rgba[i + 2];
                rgba[i + 2] = tmp;
            }
        }

        private static void PremultiplyAlpha(byte[] bgra, float alpha)
        {
            for (var i = 3; i < bgra.Length; i += 4)
                bgra[i] = (byte)(bgra[i] * alpha);
        }

        private static byte[] IndexToColor(RenderSurface texture, bool isClipMap = false, PaletteChanges paletteChanges = null)
        {
            var colors = GetColors(texture);
            
            DatManager.PortalDat.TryReadFileCache(texture.DefaultPaletteId, out DatReaderWriter.DBObjs.Palette palette);

            // Make a copy of the Palette Colors, so we don't inadvertently save them back to the dat File Cache
            var paletteColors = palette.Colors.ToList();

            // Apply any custom palette colors, if any, to our loaded palette (note, this may be all of them!)
            if (paletteChanges != null)
            {
                for (var i = 0; i < paletteChanges.CloSubPalettes.Count; i++)
                {
                    var subpalette = paletteChanges.CloSubPalettes[i];

                    DatManager.PortalDat.TryReadFileCache(paletteChanges.PaletteIds[i], out DatReaderWriter.DBObjs.Palette newPalette);

                    foreach (var range in subpalette.Ranges)
                    {
                        var offset = (int)range.Offset;
                        var numColors = (int)range.NumColors;

                        for (var j = 0; j < numColors; j++)
                            paletteColors[j + offset] = newPalette.Colors[j + offset];
                    }
                }
            }

            var output = new byte[texture.Width * texture.Height * 4];

            for (int i = 0; i < texture.Height; i++)
            {
                for (int j = 0; j < texture.Width; j++)
                {
                    int idx = (i * texture.Width) + j;
                    var color = colors[idx];
                    var paletteColor = paletteColors[color];

                    byte a = paletteColor.Alpha;
                    byte r = paletteColor.Red;
                    byte g = paletteColor.Green;
                    byte b = paletteColor.Blue;

                    if (isClipMap && color < 8)
                        r = g = b = a = 0;

                    output[idx * 4] = r;
                    output[idx * 4 + 1] = g;
                    output[idx * 4 + 2] = b;
                    output[idx * 4 + 3] = a;
                }
            }
            return output;
        }

        private static List<int> GetColors(RenderSurface texture)
        {
            var colors = new List<int>();
            using (BinaryReader reader = new BinaryReader(new MemoryStream(texture.SourceData)))
            {
                for (uint y = 0; y < texture.Height; y++)
                    for (uint x = 0; x < texture.Width; x++)
                        colors.Add(reader.ReadInt16());
            }
            return colors;
        }

        public static Texture2D Get(uint surfaceID, uint surfaceTextureID, PaletteChanges paletteChanges)
        {
            if (surfaceID >> 24 != 0x8)
                return Get(surfaceID);
            
            DatManager.PortalDat.TryReadFileCache(surfaceID, out DatReaderWriter.DBObjs.Surface surface);

            if (surface.ColorValue != null)
            {
                // swatch
                var swatch = new Texture2D(GameView.Instance.GraphicsDevice, 1, 1);
                var a = surface.ColorValue.Alpha;
                var r = surface.ColorValue.Red;
                var g = surface.ColorValue.Green;
                var b = surface.ColorValue.Blue;
                a = 0;
                swatch.SetDataAsync(new Microsoft.Xna.Framework.Color[] { new Microsoft.Xna.Framework.Color(r, g, b, a) });
                return swatch;
            }

            DatManager.PortalDat.TryReadFileCache(surfaceTextureID, out DatReaderWriter.DBObjs.SurfaceTexture surfaceTexture);

            return GetTexture(surfaceTexture.Textures[0], surface, paletteChanges);
        }
        
        // 0x08 - Surface - contains a 0x05 SurfaceTexture, along with additional type info (clipmask)
        // 0x05 - SurfaceTexture - contains a list of 0x06 textures
        // 0x06 - Texture - image format and data

        public static Texture2D Get(uint fileID, Dictionary<uint, uint> textureChanges = null, PaletteChanges paletteChanges = null, bool useCache = true)
        {
            //Console.WriteLine($"TextureCache.Get({fileID:X8})");
            
            if (fileID >> 24 == 0x01)
            {
                // gfxobj
                DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.GfxObj gfxObj);

                var surfaceID = gfxObj.Surfaces[0];
                DatManager.PortalDat.TryReadFileCache(surfaceID, out DatReaderWriter.DBObjs.Surface surface);

                if (surface.ColorValue != null)
                {
                    // swatch
                    var swatch = new Texture2D(GameView.Instance.GraphicsDevice, 1, 1);
                    var a = surface.ColorValue.Alpha;
                    var r = surface.ColorValue.Red;
                    var g = surface.ColorValue.Green;
                    var b = surface.ColorValue.Blue;
                    swatch.SetDataAsync(new Microsoft.Xna.Framework.Color[] { new Microsoft.Xna.Framework.Color(r, g, b, a) });
                    return swatch;
                }

                var textureId = surface.OrigTextureId;

                if (textureChanges != null && textureChanges.TryGetValue(textureId, out var newTextureId))
                    textureId = newTextureId;

                DatManager.PortalDat.TryReadFileCache(textureId, out DatReaderWriter.DBObjs.SurfaceTexture surfaceTexture);

                return GetTexture(surfaceTexture.Textures[0], surface);
            }
            else if (fileID >> 24 == 0x04)
            {
                // palette
                return GetTexture(fileID);
            }
            else if (fileID >> 24 == 0x0F)
            {
                // palette set
                return GetTexture(fileID);
            }

            else if (fileID >> 24 == 0x05)
            {
                // surface texture
                DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.SurfaceTexture surfaceTexture);

                return GetTexture(surfaceTexture.Textures[0], null, paletteChanges, useCache);
            }
            else if (fileID >> 24 == 0x06)
            {
                // texture
                return GetTexture(fileID, null, paletteChanges, useCache);
            }

            else if (fileID >> 24 == 0x08)
            {
                // surface
                DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.Surface surface);
                //Console.WriteLine($"Loading swatch {fileID:X8}");

                if (surface.ColorValue != null)
                {
                    // swatch
                    var swatch = new Texture2D(GameView.Instance.GraphicsDevice, 1, 1);
                    var a = surface.ColorValue.Alpha;
                    var r = surface.ColorValue.Red;
                    var g = surface.ColorValue.Green;
                    var b = surface.ColorValue.Blue;

                    if (surface.Translucency > 0)
                        a = (byte)(a * (1.0f - surface.Translucency));

                    swatch.SetDataAsync(new Microsoft.Xna.Framework.Color[] { new Microsoft.Xna.Framework.Color(r, g, b, a) });
                    return swatch;
                }

                var textureId = surface.OrigTextureId;

                if (textureChanges != null && textureChanges.TryGetValue(textureId, out var newTextureId))
                    textureId = newTextureId;

                DatManager.PortalDat.TryReadFileCache(textureId, out DatReaderWriter.DBObjs.SurfaceTexture surfaceTexture);

                return GetTexture(surfaceTexture.Textures[0], surface, paletteChanges, useCache);
            }
            return null;
        }

        private static Texture2D GetTexture(uint textureID, DatReaderWriter.DBObjs.Surface surface = null, PaletteChanges paletteChanges = null, bool useCache = true)
        {
            //Console.WriteLine($"-> GetTexture({textureID:X8})");

            var texturePalette = new TextureChanges(textureID, surface?.Translucency ?? 0.0f, paletteChanges);

            if (useCache && Textures.TryGetValue(texturePalette, out var cached))
                return cached;

            var texture = LoadTexture(textureID, false, surface, paletteChanges);

            if (useCache)
                Textures.Add(texturePalette, texture);
            else
                Uncached.Add(texture);

            return texture;
        }

        public static Texture2D GenerateMipMaps(byte[] data, int size)
        {
            var source = new Texture2D(GraphicsDevice, size, size, false, SurfaceFormat.Color);
            source.SetDataAsync(data);

            var texture = new Texture2D(GraphicsDevice, size, size, true, SurfaceFormat.Color);
            texture.SetDataAsync(0, null, data, 0, data.Length);

            var miplevel = new List<byte[]>();
            var mipsize = size / 2;
            while (true)
            {
                var mipmap = GenerateMipMap(source, mipsize);
                var mipdata = new byte[mipsize * mipsize * 4];
                mipmap.GetData(mipdata);
                miplevel.Add(mipdata);
                if (mipsize > 1)
                    mipsize /= 2;
                else
                    break;
            };

            for (var i = 0; i < miplevel.Count; i++)
            {
                texture.SetDataAsync(i + 1, null, miplevel[i], 0, miplevel[i].Length);
            }
            return texture;
        }

        public static RenderTarget2D GenerateMipMap(Texture2D source, int size)
        {
            RenderTarget2D renderTarget = null;

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                renderTarget = new RenderTarget2D(GraphicsDevice, size, size);

                GraphicsDevice.SetRenderTarget(renderTarget);

                SpriteBatch.Begin();
                SpriteBatch.Draw(source, new Microsoft.Xna.Framework.Rectangle(0, 0, size, size), Microsoft.Xna.Framework.Color.White);
                SpriteBatch.End();

                GraphicsDevice.SetRenderTarget(null);
            }));

            return renderTarget;
        }

        public static uint GetSurfaceTextureID(uint surfaceID, Dictionary<uint, uint> textureChanges = null)
        {
            if (surfaceID >> 24 == 0x5 && textureChanges != null && textureChanges.TryGetValue(surfaceID, out var newSurfaceTextureId))
                return newSurfaceTextureId;
            
            if (surfaceID >> 24 != 0x8)
                return surfaceID;
            
            DatManager.PortalDat.TryReadFileCache(surfaceID, out DatReaderWriter.DBObjs.Surface surface);

            if (surface.OrigTextureId == 0)
                return surfaceID;

            var surfaceTextureId = surface.OrigTextureId;
            
            if (textureChanges != null && textureChanges.TryGetValue(surfaceTextureId, out newSurfaceTextureId))
                surfaceTextureId = newSurfaceTextureId;

            return surfaceTextureId;
        }

        /// <summary>
        /// For SurfacePixelFormat.PFID_CUSTOM_RAW_JPEG, rendering only?
        /// </summary>
        private static Bitmap SwapRedAndBlueChannels(Bitmap bitmap)
        {
            for (var y = 0; y < bitmap.Height; y++)
            {
                for (var x = 0; x < bitmap.Width; x++)
                {
                    var color = bitmap.GetPixel(x, y);
                    bitmap.SetPixel(x, y, Color.FromArgb(color.B, color.G, color.R));
                }
            }
            return bitmap;
        }
    }
}
