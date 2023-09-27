using System.IO;

namespace ACE.DatLoader.Entity.AnimationHooks
{
    public class ScaleHook : AnimationHook
    {
        public float End { get; set; }
        public float Time { get; set; }

        public override void Unpack(BinaryReader reader)
        {
            base.Unpack(reader);

            End     = reader.ReadSingle();
            Time    = reader.ReadSingle();
        }
    }
}
