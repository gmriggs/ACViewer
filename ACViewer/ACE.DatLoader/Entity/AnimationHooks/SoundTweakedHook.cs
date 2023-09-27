using System.IO;

namespace ACE.DatLoader.Entity.AnimationHooks
{
    public class SoundTweakedHook : AnimationHook
    {
        public uint SoundID { get; set; }
        public float Priority { get; set; }
        public float Probability { get; set; }
        public float Volume { get; set; }

        public override void Unpack(BinaryReader reader)
        {
            base.Unpack(reader);

            SoundID = reader.ReadUInt32();
            Priority = reader.ReadSingle();
            Probability = reader.ReadSingle();
            Volume = reader.ReadSingle();
        }
    }
}
