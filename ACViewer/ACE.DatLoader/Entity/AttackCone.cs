using System.IO;

namespace ACE.DatLoader.Entity
{
    public class AttackCone : IUnpackable
    {
        public uint PartIndex { get; set; }
        
        // these Left and Right are technically Vec2D types
        public float LeftX { get; set; }
        public float LeftY { get; set; }

        public float RightX { get; set; }
        public float RightY { get; set; }

        public float Radius { get; set; }
        public float Height { get; set; }

        public void Unpack(BinaryReader reader)
        {
            PartIndex   = reader.ReadUInt32();

            LeftX       = reader.ReadSingle();
            LeftY       = reader.ReadSingle();

            RightX      = reader.ReadSingle();
            RightY      = reader.ReadSingle();
            
            Radius      = reader.ReadSingle();
            Height      = reader.ReadSingle();
        }
    }
}
