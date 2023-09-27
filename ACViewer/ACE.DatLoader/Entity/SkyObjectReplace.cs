using System.IO;

namespace ACE.DatLoader.Entity
{
    public class SkyObjectReplace : IUnpackable
    {
        public uint ObjectIndex { get; set; }
        public uint GFXObjId { get; set; }
        public float Rotate { get; set; }
        public float Transparent { get; set; }
        public float Luminosity { get; set; }
        public float MaxBright { get; set; }

        public void Unpack(BinaryReader reader)
        {
            ObjectIndex = reader.ReadUInt32();
            GFXObjId    = reader.ReadUInt32();
            Rotate      = reader.ReadSingle();
            Transparent = reader.ReadSingle();
            Luminosity  = reader.ReadSingle();
            MaxBright   = reader.ReadSingle();

            reader.AlignBoundary();
        }
    }
}
