using System.IO;

namespace ACE.DatLoader.Entity
{
    public class SpellComponentBase : IUnpackable
    {
        public string Name { get; set; }
        public uint Category { get; set; }
        public uint Icon { get; set; }
        public uint Type { get; set; }
        public uint Gesture { get; set; }
        public float Time { get; set; }
        public string Text { get; set; }
        public float CDM { get; set; } // Unsure what this is

        public void Unpack(BinaryReader reader)
        {
            Name        = reader.ReadObfuscatedString();
            reader.AlignBoundary();
            Category    = reader.ReadUInt32();
            Icon        = reader.ReadUInt32();
            Type        = reader.ReadUInt32();
            Gesture     = reader.ReadUInt32();
            Time        = reader.ReadSingle();
            Text        = reader.ReadObfuscatedString();
            reader.AlignBoundary();
            CDM         = reader.ReadSingle();
        }
    }
}
