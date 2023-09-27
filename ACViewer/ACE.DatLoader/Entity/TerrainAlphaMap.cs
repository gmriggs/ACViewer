using System.IO;

namespace ACE.DatLoader.Entity
{
    public class TerrainAlphaMap : IUnpackable
    {
        public uint TCode { get; set; }
        public uint TexGID { get; set; }

        public void Unpack(BinaryReader reader)
        {
            TCode   = reader.ReadUInt32();
            TexGID  = reader.ReadUInt32();
        }
    }
}
