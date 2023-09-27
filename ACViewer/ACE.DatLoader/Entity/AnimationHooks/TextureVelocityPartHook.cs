using System.IO;

namespace ACE.DatLoader.Entity.AnimationHooks
{
    public class TextureVelocityPartHook : AnimationHook
    {
        public uint PartIndex { get; set; }
        public float USpeed { get; set; }
        public float VSpeed { get; set; }

        public override void Unpack(BinaryReader reader)
        {
            base.Unpack(reader);

            PartIndex   = reader.ReadUInt32();
            USpeed      = reader.ReadSingle();
            VSpeed      = reader.ReadSingle();
        }
    }
}
