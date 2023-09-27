using System.IO;

namespace ACE.DatLoader.Entity
{
    public class GfxObjInfo : IUnpackable
    {
        public uint Id { get; set; }
        public uint DegradeMode { get; set; }
        public float MinDist { get; set; }
        public float IdealDist { get; set; }
        public float MaxDist { get; set; }

        public void Unpack(BinaryReader reader)
        {
            Id          = reader.ReadUInt32();
            DegradeMode = reader.ReadUInt32();
            MinDist     = reader.ReadSingle();
            IdealDist   = reader.ReadSingle();
            MaxDist     = reader.ReadSingle();
        }
    }
}
