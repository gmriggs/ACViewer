using System.IO;

namespace ACE.DatLoader.Entity.AnimationHooks
{
    public class CallPESHook : AnimationHook
    {
        public uint PES { get; set; }
        public float Pause { get; set; }

        public override void Unpack(BinaryReader reader)
        {
            base.Unpack(reader);

            PES     = reader.ReadUInt32();
            Pause   = reader.ReadSingle();
        }
    }
}
