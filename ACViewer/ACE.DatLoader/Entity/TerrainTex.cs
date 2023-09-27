using System.IO;

namespace ACE.DatLoader.Entity
{
    public class TerrainTex : IUnpackable
    {
        public uint TexGID { get; set; }
        public uint TexTiling { get; set; }
        public uint MaxVertBright { get; set; }
        public uint MinVertBright { get; set; }
        public uint MaxVertSaturate { get; set; }
        public uint MinVertSaturate { get; set; }
        public uint MaxVertHue { get; set; }
        public uint MinVertHue { get; set; }
        public uint DetailTexTiling { get; set; }
        public uint DetailTexGID { get; set; }

        public void Unpack(BinaryReader reader)
        {
            TexGID          = reader.ReadUInt32();
            TexTiling       = reader.ReadUInt32();
            MaxVertBright   = reader.ReadUInt32();
            MinVertBright   = reader.ReadUInt32();
            MaxVertSaturate = reader.ReadUInt32();
            MinVertSaturate = reader.ReadUInt32();
            MaxVertHue      = reader.ReadUInt32();
            MinVertHue      = reader.ReadUInt32();
            DetailTexTiling = reader.ReadUInt32();
            DetailTexGID    = reader.ReadUInt32();
        }
    }
}
