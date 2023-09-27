using System.IO;

namespace ACE.DatLoader.Entity
{
    public class HairStyleCG : IUnpackable
    {
        public uint IconImage { get; set; }
        public bool Bald { get; set; }
        public uint AlternateSetup { get; set; }
        public ObjDesc ObjDesc { get; } = new ObjDesc();

        public void Unpack(BinaryReader reader)
        {
            IconImage       = reader.ReadUInt32();
            Bald            = (reader.ReadByte() == 1);
            AlternateSetup  = reader.ReadUInt32();

            ObjDesc.Unpack(reader);
        }
    }
}
