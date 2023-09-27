using System.IO;

namespace ACE.DatLoader
{
    /// <summary>
    /// DiskFileInfo_t in the client
    /// </summary>
    public class DatDatabaseHeader : IUnpackable
    {
        public uint FileType { get; set; }
        public uint BlockSize { get; set; }
        public uint FileSize { get; set; }
        public DatDatabaseType DataSet { get; set; }
        public uint DataSubset { get; set; }

        public uint FreeHead { get; set; }
        public uint FreeTail { get; set; }
        public uint FreeCount { get; set; }
        public uint BTree { get; set; }

        public uint NewLRU { get; set; }
        public uint OldLRU { get; set; }
        public bool UseLRU { get; set; }

        public uint MasterMapID { get; set; }

        public uint EnginePackVersion { get; set; }
        public uint GamePackVersion { get; set; }
        public byte[] VersionMajor { get; set; } = new byte[16];
        public uint VersionMinor { get; set; }

        public void Unpack(BinaryReader reader)
        {
            FileType    = reader.ReadUInt32();
            BlockSize   = reader.ReadUInt32();
            FileSize    = reader.ReadUInt32();
            DataSet     = (DatDatabaseType)reader.ReadUInt32();
            DataSubset  = reader.ReadUInt32();

            FreeHead    = reader.ReadUInt32();
            FreeTail    = reader.ReadUInt32();
            FreeCount   = reader.ReadUInt32();
            BTree       = reader.ReadUInt32();

            NewLRU      = reader.ReadUInt32();
            OldLRU      = reader.ReadUInt32();
            UseLRU      = (reader.ReadUInt32() == 1);

            MasterMapID = reader.ReadUInt32();

            EnginePackVersion   = reader.ReadUInt32();
            GamePackVersion     = reader.ReadUInt32();
            VersionMajor        = reader.ReadBytes(16);
            VersionMinor        = reader.ReadUInt32();
        }
    }
}
