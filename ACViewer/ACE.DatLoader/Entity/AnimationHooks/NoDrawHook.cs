using System.IO;

namespace ACE.DatLoader.Entity.AnimationHooks
{
    public class NoDrawHook : AnimationHook
    {
        public uint NoDraw { get; set; }

        public override void Unpack(BinaryReader reader)
        {
            base.Unpack(reader);

            NoDraw = reader.ReadUInt32();
        }
    }
}
