using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Media;
using System.Windows;
using System.Windows.Controls;

using ACE.DatLoader;
using ACE.DatLoader.Extensions;
using ACE.DatLoader.FileTypes;

using ACViewer.Data;
using ACViewer.Enum;
using ACViewer.FileTypes;

namespace ACViewer.View
{
    /// <summary>
    /// Interaction logic for FileType.xaml
    /// </summary>
    public partial class FileExplorer : UserControl, INotifyPropertyChanged
    {
        public static MainWindow MainWindow => MainWindow.Instance;
        public static FileInfo FileInfo => FileInfo.Instance;
        public static MotionList MotionList => MotionList.Instance;
        public static ClothingTableList ClothingTableList => ClothingTableList.Instance;

        public static GameView GameView => GameView.Instance;
        public static ModelViewer ModelViewer => ModelViewer.Instance;
        public static TextureViewer TextureViewer => TextureViewer.Instance;

        public static List<Entity.FileType> FileTypes { get; set; }

        private List<string> _fileIDs { get; set; }

        public bool PortalMode { get; set; } = true;

        public uint Selected_FileID { get; set; }
        public static FileExplorer Instance { get; set; }

        public bool TeleportMode { get; set; }

        public History History { get; set; }

        public List<string> FileIDs
        {
            get => _fileIDs;
            set
            {
                _fileIDs = value;
                NotifyPropertyChanged("FileIDs");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public FileExplorer()
        {
            InitializeComponent();
            Instance = this;

            FileTypes = new List<Entity.FileType>()
            {
                new Entity.FileType(0xFFFF, "Landblock", typeof(DatReaderWriter.DBObjs.LandBlock)),
                new Entity.FileType(0xFFFE, "LandblockInfo", typeof(DatReaderWriter.DBObjs.LandBlockInfo)),
                new Entity.FileType(0x100, "EnvCell", typeof(DatReaderWriter.DBObjs.EnvCell)),
                new Entity.FileType(0x01, "GfxObj", typeof(DatReaderWriter.DBObjs.GfxObj)),
                new Entity.FileType(0x02, "Setup", typeof(DatReaderWriter.DBObjs.Setup)),
                new Entity.FileType(0x03, "Animation", typeof(DatReaderWriter.DBObjs.Animation)),
                new Entity.FileType(0x04, "Palette", typeof(DatReaderWriter.DBObjs.Palette)),
                new Entity.FileType(0x05, "SurfaceTexture", typeof(DatReaderWriter.DBObjs.SurfaceTexture)),
                new Entity.FileType(0x06, "Textures", typeof(DatReaderWriter.DBObjs.RenderSurface)),
                new Entity.FileType(0x08, "Surface", typeof(DatReaderWriter.DBObjs.Surface)),
                new Entity.FileType(0x09, "MotionTable", typeof(DatReaderWriter.DBObjs.MotionTable)),
                new Entity.FileType(0x0A, "Sound", typeof(DatReaderWriter.DBObjs.Wave)),
                new Entity.FileType(0x0D, "Environment", typeof(DatReaderWriter.DBObjs.Environment)),
                new Entity.FileType(0x0E000002, "CharGen", typeof(DatReaderWriter.DBObjs.CharGen)),
                new Entity.FileType(0x0E000003, "VitalTable", typeof(DatReaderWriter.DBObjs.VitalTable)),
                new Entity.FileType(0x0E000004, "SkillTable", typeof(DatReaderWriter.DBObjs.SkillTable)),
                new Entity.FileType(0x0E000007, "ChatPoseTable", typeof(DatReaderWriter.DBObjs.ChatPoseTable)),
                new Entity.FileType(0x0E00000D, "GeneratorTable", typeof(DatReaderWriter.DBObjs.ObjectHierarchy)),
                new Entity.FileType(0x0E00000E, "SpellTable", typeof(DatReaderWriter.DBObjs.SpellTable)),
                new Entity.FileType(0x0E00000F, "SpellComponents", typeof(DatReaderWriter.DBObjs.SpellComponentTable)),
                new Entity.FileType(0x0E000018, "XpTable", typeof(DatReaderWriter.DBObjs.ExperienceTable)),
                new Entity.FileType(0x0E00001A, "BadData", typeof(DatReaderWriter.DBObjs.BadDataTable)),
                new Entity.FileType(0x0E00001D, "ContractTable", typeof(DatReaderWriter.DBObjs.ContractTable)),
                //new Entity.FileType(0x0E00001E, "TabooTable", typeof(DatReaderWriter.DBObjs.TabooTable)),
                new Entity.FileType(0x0E000020, "NameFilters", typeof(DatReaderWriter.DBObjs.NameFilterTable)),
                new Entity.FileType(0x0F, "PaletteSet", typeof(DatReaderWriter.DBObjs.PaletteSet)),
                new Entity.FileType(0x10, "Clothing", typeof(DatReaderWriter.DBObjs.Clothing)),
                new Entity.FileType(0x11, "DegradeInfo", typeof(DatReaderWriter.DBObjs.GfxObjDegradeInfo)),
                new Entity.FileType(0x12, "Scene", typeof(DatReaderWriter.DBObjs.Scene)),
                new Entity.FileType(0x13, "Region", typeof(DatReaderWriter.DBObjs.Region)),
                new Entity.FileType(0x20, "SoundTable", typeof(DatReaderWriter.DBObjs.SoundTable)),
                new Entity.FileType(0x22, "Enums", typeof(DatReaderWriter.DBObjs.EnumMapper)),
                new Entity.FileType(0x23, "StringTable", typeof(DatReaderWriter.DBObjs.StringTable)),
                new Entity.FileType(0x25, "DIDs", typeof(DatReaderWriter.DBObjs.DataIdMapper)),
                new Entity.FileType(0x27, "DualDIDs", typeof(DatReaderWriter.DBObjs.DualDataIdMapper)),
                new Entity.FileType(0x30, "CombatTable", typeof(DatReaderWriter.DBObjs.CombatTable)),
                new Entity.FileType(0x32, "EmitterInfo", typeof(DatReaderWriter.DBObjs.ParticleEmitter)),
                new Entity.FileType(0x33, "PhysicsScript", typeof(DatReaderWriter.DBObjs.PhysicsScript)),
                new Entity.FileType(0x34, "PhysicsScriptTable", typeof(DatReaderWriter.DBObjs.PhysicsScriptTable)),
            };

            DIDTables.Load();
            LootArmorList.Load();

            History = new History();

            DataContext = this;
        }

        private void FileType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatManager.CellDat == null || DatManager.PortalDat == null)
                return;

            var selected = (Entity.FileType)FileType.SelectedItem;

            if (selected == null) return;

            PortalMode = selected.ID != 0xFFFF && selected.ID != 0xFFFE && selected.ID != 0x100;

            switch (selected.ID)
            {
                case 0x10: // ClothingTable
                    ScriptList.Instance.Visibility = Visibility.Hidden;
                    ClothingTableList.Instance.Visibility = Visibility.Visible;
                    MotionList.Instance.Visibility = Visibility.Hidden;
                    MainWindow.menuMain.miVirindiColorTool.Visibility = Visibility.Visible;
                    break;
                case 0x34: // PhysicsScriptTable
                    ScriptList.Instance.Visibility = Visibility.Hidden;
                    ClothingTableList.Instance.Visibility = Visibility.Hidden;
                    MotionList.Instance.Visibility = Visibility.Visible;
                    MainWindow.menuMain.miVirindiColorTool.Visibility = Visibility.Collapsed;
                    break;
                default:
                    ScriptList.Instance.Visibility = Visibility.Hidden;
                    ClothingTableList.Instance.Visibility = Visibility.Hidden;
                    MotionList.Instance.Visibility = Visibility.Visible;
                    MainWindow.menuMain.miVirindiColorTool.Visibility = Visibility.Collapsed;
                    break;
            }

            // strings
            if (selected.ID == 0x23)
            {
                var minRange = selected.ID << 24;
                var maxRange = minRange | 0xFFFFFF;
                FileIDs = DatManager.LanguageDat.Tree.GetFilesInRange(minRange, maxRange).OrderBy(i => i.Id).Select(i => i.Id.ToString("X8")).ToList();
            }
            // portal files
            else if (selected.ID <= 0x34)
            {
                var minRange = selected.ID << 24;
                var maxRange = minRange | 0xFFFFFF;
                FileIDs = DatManager.PortalDat.Tree.GetFilesInRange(minRange, maxRange).OrderBy(i => i.Id).Select(i => i.Id.ToString("X8")).ToList();
            }

            // landblock
            else if (selected.ID == 0xFFFF)
            {
                FileIDs = DatManager.CellDat.Tree.GetFilesInRange(uint.MinValue, uint.MaxValue).Where(i => (i.Id & 0xFFFF) == selected.ID).OrderBy(i => i.Id).Select(i => i.Id.ToString("X8")).ToList();
                MapViewer.Instance.Init();
            }

            // landblock info
            else if (selected.ID == 0xFFFE)
            {
                FileIDs = DatManager.CellDat.Tree.GetFilesInRange(uint.MinValue, uint.MaxValue).Where(i => (i.Id & 0xFFFF) == selected.ID).OrderBy(i => i.Id).Select(i => i.Id.ToString("X8")).ToList();
                MapViewer.Instance.Init();
            }
            // envcell
            else if (selected.ID == 0x100)
                FileIDs = DatManager.CellDat.Tree.GetFilesInRange(uint.MinValue, uint.MaxValue).Where(i => (i.Id & 0xFFFF) >= selected.ID && (i.Id & 0xFFFF) < 0xFFFE).OrderBy(i => i.Id).Select(i => i.Id.ToString("X8")).ToList();

            // other
            else
                FileIDs = DatManager.PortalDat.Tree.HasFile(selected.ID) ? new List<string>() { selected.ID.ToString("X8") } : null;

            MainWindow.Status.WriteLine($"{selected.Name}s: {FileIDs.Count:N0}");
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Files_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fileID = Convert.ToUInt32((string)Files.SelectedItem, 16);
            if (fileID == 0) return;

            Selected_FileID = fileID;
            History.Add(fileID);

            if (PortalMode)
                ReadPortalFile(fileID);
            else
                ReadCellFile(fileID);
        }

        public void ReadCellFile(uint fileID)
        {
            switch (fileID & 0xFFFF)
            {
                case 0xFFFF:
                    DatManager.CellDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.LandBlock landblock);
                    FileInfo.SetInfo(new FileTypes.CellLandblock(landblock).BuildTree());

                    GameView.ViewMode = ViewMode.World;
                    GameView.WorldViewer = new WorldViewer();
                    GameView.WorldViewer.LoadLandblock(fileID);

                    break;
                case 0xFFFE:
                    DatManager.CellDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.LandBlockInfo landblockInfo);
                    FileInfo.SetInfo(new FileTypes.LandblockInfo(landblockInfo).BuildTree());

                    GameView.ViewMode = ViewMode.World;
                    GameView.WorldViewer = new WorldViewer();
                    GameView.WorldViewer.LoadLandblock(fileID | 0xFFFF);

                    break;
                /* >= 0x100 && < 0xFFEE */
                default:
                    DatManager.CellDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.EnvCell envCell);
                    FileInfo.SetInfo(new FileTypes.EnvCell(envCell).BuildTree());
                    GameView.ViewMode = ViewMode.Model;
                    ModelViewer.LoadEnvCell(fileID);
                    break;
            }
        }

        public static readonly float PaletteScale = 12.0f;

        public void ReadPortalFile(uint fileID)
        {
            switch (fileID >> 24)
            {
                case 0x01:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.GfxObj gfxObj);
                    FileInfo.SetInfo(new FileTypes.GfxObj(gfxObj).BuildTree());
                    GameView.ViewMode = ViewMode.Model;
                    ModelViewer.LoadModel(fileID);
                    break;
                case 0x02:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.Setup setup);
                    FileInfo.SetInfo(new Setup(setup).BuildTree());
                    GameView.ViewMode = ViewMode.Model;
                    ModelViewer.LoadModel(fileID);
                    MotionList.OnClickSetup(fileID);
                    break;
                case 0x03:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.Animation anim);
                    FileInfo.SetInfo(new FileTypes.Animation(anim).BuildTree());
                    break;
                case 0x04:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.Palette palette);
                    FileInfo.SetInfo(new FileTypes.Palette(palette).BuildTree());
                    GameView.ViewMode = ViewMode.Texture;
                    TextureViewer.LoadTexture(fileID);
                    if (TextureViewer.CurScale < PaletteScale)
                        TextureViewer.SetScale(PaletteScale);
                    break;
                case 0x05:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.SurfaceTexture surfaceTexture);
                    FileInfo.SetInfo(new FileTypes.SurfaceTexture(surfaceTexture).BuildTree());
                    GameView.ViewMode = ViewMode.Texture;
                    TextureViewer.LoadTexture(fileID);
                    break;
                case 0x06:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.RenderSurface texture);
                    FileInfo.SetInfo(new FileTypes.Texture(texture).BuildTree());
                    GameView.ViewMode = ViewMode.Texture;
                    TextureViewer.LoadTexture(fileID);
                    break;
                case 0x08:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.Surface surface);
                    FileInfo.SetInfo(new FileTypes.Surface(surface).BuildTree(fileID));
                    GameView.ViewMode = ViewMode.Texture;
                    TextureViewer.LoadTexture(fileID);
                    break;
                case 0x09:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.MotionTable motionTable);
                    FileInfo.SetInfo(new FileTypes.MotionTable(motionTable).BuildTree());
                    break;
                case 0x0A:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.Wave sound);
                    FileInfo.SetInfo(new Sound(sound).BuildTree(fileID));
                    using (var stream = new MemoryStream())
                    {
                        sound.ReadData(stream);
                        var soundPlayer = new SoundPlayer(stream);
                        stream.Seek(0, SeekOrigin.Begin);
                        try
                        {
                            soundPlayer.Play();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        stream.Close();
                    }
                    break;
                case 0x0D:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.Environment environment);
                    FileInfo.SetInfo(new FileTypes.Environment(environment).BuildTree());
                    GameView.ViewMode = ViewMode.Model;
                    ModelViewer.LoadEnvironment(fileID);
                    break;
                case 0x0E:
                    if (fileID == 0x0E000002)
                    {
                        DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.CharGen charGen);
                        FileInfo.SetInfo(new FileTypes.CharGen(charGen).BuildTree());
                    }
                    else if (fileID == 0x0E000003)
                    {
                        DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.VitalTable vitalTable);
                        FileInfo.SetInfo(new FileTypes.SecondaryAttributeTable(vitalTable).BuildTree());
                    }
                    else if (fileID == 0x0E000004)
                    {
                        DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.SkillTable skillTable);
                        FileInfo.SetInfo(new FileTypes.SkillTable(skillTable).BuildTree());
                    }
                    else if (fileID == 0x0E000007)
                    {
                        DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.ChatPoseTable chatPoseTable);
                        FileInfo.SetInfo(new FileTypes.ChatPoseTable(chatPoseTable).BuildTree());
                    }
                    else if (fileID == 0x0E00000D)
                    {
                        DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.ObjectHierarchy generatorTable);
                        FileInfo.SetInfo(new FileTypes.GeneratorTable(generatorTable).BuildTree());
                    }
                    else if (fileID == 0x0E00000E)
                    {
                        DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.SpellTable spellTable);
                        FileInfo.SetInfo(new FileTypes.SpellTable(spellTable).BuildTree());
                    }
                    else if (fileID == 0x0E00000F)
                    {
                        DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.SpellComponentTable spellComponentsTable);
                        FileInfo.SetInfo(new FileTypes.SpellComponentsTable(spellComponentsTable).BuildTree());
                    }
                    else if (fileID == 0x0E000018)
                    {
                        DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.ExperienceTable xpTable);
                        FileInfo.SetInfo(new FileTypes.XpTable(xpTable).BuildTree());
                    }
                    else if (fileID == 0x0E00001A)
                    {
                        DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.BadDataTable badData);
                        FileInfo.SetInfo(new FileTypes.BadData(badData).BuildTree());
                    }
                    else if (fileID == 0x0E00001D)
                    {
                        DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.ContractTable contractTable);
                        FileInfo.SetInfo(new FileTypes.ContractTable(contractTable).BuildTree());
                    }
                    else if (fileID == 0x0E00001E)
                    {
                        DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.TabooTable tabooTable);
                        FileInfo.SetInfo(new FileTypes.TabooTable(tabooTable).BuildTree());
                    }
                    else if (fileID == 0x0E000020)
                    {
                        DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.NameFilterTable nameFilterTable);
                        FileInfo.SetInfo(new FileTypes.NameFilterTable(nameFilterTable).BuildTree());
                    }
                    break;
                case 0x0F:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.PaletteSet paletteSet);
                    FileInfo.SetInfo(new FileTypes.PaletteSet(paletteSet).BuildTree());
                    GameView.ViewMode = ViewMode.Texture;
                    TextureViewer.LoadTexture(fileID);
                    if (TextureViewer.CurScale < PaletteScale / 2.0f)
                        TextureViewer.SetScale(PaletteScale / 2.0f);
                    break;
                case 0x10:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.Clothing clothing);
                    FileInfo.SetInfo(new FileTypes.ClothingTable(clothing).BuildTree());
                    GameView.ViewMode = ViewMode.Model;
                    ClothingTableList.OnClickClothingBase(clothing, fileID);
                    break;
                case 0x11:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.GfxObjDegradeInfo degradeInfo);
                    FileInfo.SetInfo(new DegradeInfo(degradeInfo).BuildTree());
                    break;
                case 0x12:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.Scene scene);
                    FileInfo.SetInfo(new FileTypes.Scene(scene).BuildTree());
                    break;
                case 0x13:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.Region region);
                    FileInfo.SetInfo(new Region(region).BuildTree());
                    break;
                case 0x20:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.SoundTable soundTable);
                    FileInfo.SetInfo(new FileTypes.SoundTable(soundTable).BuildTree());
                    break;
                case 0x22:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.EnumMapper enumMapper);
                    FileInfo.SetInfo(new FileTypes.EnumMapper(enumMapper).BuildTree());
                    break;
                case 0x23:
                    DatManager.LanguageDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.StringTable stringTable);
                    FileInfo.SetInfo(new FileTypes.StringTable(stringTable).BuildTree());
                    break;
                case 0x25:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.DataIdMapper didMapper);
                    FileInfo.SetInfo(new FileTypes.DidMapper(didMapper).BuildTree());
                    break;
                case 0x27:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.DualDataIdMapper dualDidMapper);
                    FileInfo.SetInfo(new FileTypes.DualDidMapper(dualDidMapper).BuildTree());
                    break;
                case 0x30:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.CombatTable combatTable);
                    FileInfo.SetInfo(new CombatTable(combatTable).BuildTree());
                    break;
                case 0x32:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.ParticleEmitter emitterInfo);
                    FileInfo.SetInfo(new FileTypes.ParticleEmitterInfo(emitterInfo).BuildTree());
                    ParticleViewer.Instance.InitEmitter(fileID, 1.0f);
                    break;
                case 0x33:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.PhysicsScript playScript);
                    FileInfo.SetInfo(new FileTypes.PhysicsScript(playScript).BuildTree());
                    ParticleViewer.Instance.InitEmitter(fileID, 1.0f);
                    break;
                case 0x34:
                    DatManager.PortalDat.TryReadFileCache(fileID, out DatReaderWriter.DBObjs.PhysicsScriptTable pScriptTable);
                    FileInfo.SetInfo(new FileTypes.PhysicsScriptTable(pScriptTable).BuildTree());
                    ScriptList.Instance.ScriptTable_OnClick(fileID);
                    break;
            }
        }
    }
}
