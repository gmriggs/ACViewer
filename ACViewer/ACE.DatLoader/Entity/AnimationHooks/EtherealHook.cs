using System.IO;

namespace ACE.DatLoader.Entity.AnimationHooks
{
    public class EtherealHook : AnimationHook
    {
        public int Ethereal { get; set; }

        public override void Unpack(BinaryReader reader)
        {
            base.Unpack(reader);

            Ethereal = reader.ReadInt32();
        }
    }
}
