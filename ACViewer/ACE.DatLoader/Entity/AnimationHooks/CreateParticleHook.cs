using System.IO;

namespace ACE.DatLoader.Entity.AnimationHooks
{
    public class CreateParticleHook : AnimationHook
    {
        public uint EmitterInfoId { get; set; }
        public uint PartIndex { get; set; }
        public Frame Offset { get; } = new Frame();
        public uint EmitterId { get; set; }

        public override void Unpack(BinaryReader reader)
        {
            base.Unpack(reader);

            EmitterInfoId   = reader.ReadUInt32();
            PartIndex       = reader.ReadUInt32();
            Offset.Unpack(reader);
            EmitterId       = reader.ReadUInt32();
        }
    }
}
