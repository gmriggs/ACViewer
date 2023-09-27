using System.IO;

namespace ACE.DatLoader.Entity.AnimationHooks
{
    public class SoundHook : AnimationHook
    {
        public uint Id { get; set; }

        public override void Unpack(BinaryReader reader)
        {
            base.Unpack(reader);

            Id = reader.ReadUInt32();
        }
    }
}
