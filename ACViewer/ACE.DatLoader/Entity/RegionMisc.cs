using System.IO;

namespace ACE.DatLoader.Entity
{
    public class RegionMisc : IUnpackable
    {
        public uint Version { get; set; }
        public uint GameMapID { get; set; }
        public uint AutotestMapId { get; set; }
        public uint AutotestMapSize { get; set; }
        public uint ClearCellId { get; set; }
        public uint ClearMonsterId { get; set; }

        public void Unpack(BinaryReader reader)
        {
            Version         = reader.ReadUInt32();
            GameMapID       = reader.ReadUInt32();
            AutotestMapId   = reader.ReadUInt32();
            AutotestMapSize = reader.ReadUInt32();
            ClearCellId     = reader.ReadUInt32();
            ClearMonsterId  = reader.ReadUInt32();
        }
    }
}
