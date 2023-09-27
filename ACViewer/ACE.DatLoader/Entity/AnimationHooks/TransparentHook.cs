using System.IO;

namespace ACE.DatLoader.Entity.AnimationHooks
{
    public class TransparentHook : AnimationHook
    {
        public float Start { get; set; }
        public float End { get; set; }
        public float Time { get; set; }

        public override void Unpack(BinaryReader reader)
        {
            base.Unpack(reader);

            Start   = reader.ReadSingle();
            End     = reader.ReadSingle();
            Time    = reader.ReadSingle();
        }
    }
}
