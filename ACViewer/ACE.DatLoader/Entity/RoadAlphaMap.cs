using System.IO;

namespace ACE.DatLoader.Entity
{
    public class RoadAlphaMap : IUnpackable
    {
        public uint RCode { get; set; }
        public uint RoadTexGID { get; set; }

        public void Unpack(BinaryReader reader)
        {
            RCode       = reader.ReadUInt32();
            RoadTexGID  = reader.ReadUInt32();
        }
    }
}
