using System.IO;

namespace ACE.DatLoader.Entity
{
    public class SkyObject : IUnpackable
    {
        public float BeginTime { get; set; }
        public float EndTime { get; set; }
        public float BeginAngle { get; set; }
        public float EndAngle { get; set; }
        public float TexVelocityX { get; set; }
        public float TexVelocityY { get; set; }
        public float TexVelocityZ { get; } = 0;
        public uint DefaultGFXObjectId { get; set; }
        public uint DefaultPESObjectId { get; set; }
        public uint Properties { get; set; }

        public void Unpack(BinaryReader reader)
        {
            BeginTime           = reader.ReadSingle();
            EndTime             = reader.ReadSingle();
            BeginAngle          = reader.ReadSingle();
            EndAngle            = reader.ReadSingle();
            TexVelocityX        = reader.ReadSingle();
            TexVelocityY        = reader.ReadSingle();
            DefaultGFXObjectId  = reader.ReadUInt32();
            DefaultPESObjectId  = reader.ReadUInt32();
            Properties          = reader.ReadUInt32();

            reader.AlignBoundary();
        }
    }
}
