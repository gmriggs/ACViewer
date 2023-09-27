using System.IO;

namespace ACE.DatLoader.Entity
{
    public class EyeStripCG : IUnpackable
    {
        public uint IconImage { get; set; }
        public uint IconImageBald { get; set; }
        public ObjDesc ObjDesc { get; } = new ObjDesc();
        public ObjDesc ObjDescBald { get; } = new ObjDesc();

        public void Unpack(BinaryReader reader)
        {
            IconImage = reader.ReadUInt32();
            IconImageBald = reader.ReadUInt32();

            ObjDesc.Unpack(reader);
            ObjDescBald.Unpack(reader);
        }
    }
}
