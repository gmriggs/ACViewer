using System.IO;

using DatReaderWriter.Options;

namespace ACE.DatLoader
{
    /// <summary>
    /// Mimic the ACE.DatLoader interface
    /// </summary>
    public static class DatManager
    {
        public static CellDatabaseCache CellDat { get; private set; }

        public static PortalDatabaseCache PortalDat { get; private set; }
        public static PortalDatabaseCache HighResDat { get; private set; }
        public static LocalDatabaseCache LanguageDat { get; private set; }

        public static void Initialize(string datFileDirectory, bool keepOpen = false, bool loadCell = true)
        {
            var datDir = Path.GetFullPath(Path.Combine(datFileDirectory));

            if (loadCell)
                CellDat = new CellDatabaseCache(Path.Combine(datDir, "client_cell_1.dat"), DatAccessType.ReadWrite);

            PortalDat = new PortalDatabaseCache(Path.Combine(datDir, "client_portal.dat"), DatAccessType.ReadWrite);
            HighResDat = new PortalDatabaseCache(Path.Combine(datDir, "client_highres.dat"), DatAccessType.ReadWrite);
            LanguageDat = new LocalDatabaseCache(Path.Combine(datDir, "client_local_English.dat"), DatAccessType.ReadWrite);
        }
    }
}

namespace ACE.DatLoader.FileTypes { }
namespace ACE.DatLoader.Entity { }
