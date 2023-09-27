using System.IO;

namespace ACE.DatLoader.Entity.AnimationHooks
{
    public class TextureVelocityHook : AnimationHook
    {
        public float USpeed { get; set; }
        public float VSpeed { get; set; }

        public override void Unpack(BinaryReader reader)
        {
            base.Unpack(reader);

            USpeed = reader.ReadSingle();
            VSpeed = reader.ReadSingle();
        }
    }
}
