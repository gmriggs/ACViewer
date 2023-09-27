using System.IO;

namespace ACE.DatLoader.Entity
{
    public class ObjectDesc : IUnpackable
    {
        public uint ObjId { get; set; }
        public Frame BaseLoc { get; } = new Frame();
        public float Freq { get; set; }
        public float DisplaceX { get; set; }
        public float DisplaceY { get; set; }
        public float MinScale { get; set; }
        public float MaxScale { get; set; }
        public float MaxRotation { get; set; }
        public float MinSlope { get; set; }
        public float MaxSlope { get; set; }
        public uint Align { get; set; }
        public uint Orient { get; set; }
        public uint WeenieObj { get; set; }

        public void Unpack(BinaryReader reader)
        {
            ObjId       = reader.ReadUInt32();

            BaseLoc.Unpack(reader);

            Freq        = reader.ReadSingle();

            DisplaceX   = reader.ReadSingle();
            DisplaceY   = reader.ReadSingle();

            MinScale    = reader.ReadSingle();
            MaxScale    = reader.ReadSingle();

            MaxRotation = reader.ReadSingle();

            MinSlope    = reader.ReadSingle();
            MaxSlope    = reader.ReadSingle();

            Align       = reader.ReadUInt32();
            Orient      = reader.ReadUInt32();

            WeenieObj   = reader.ReadUInt32();
        }
    }
}
