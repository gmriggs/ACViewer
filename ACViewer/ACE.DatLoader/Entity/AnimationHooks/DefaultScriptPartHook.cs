using System.IO;

namespace ACE.DatLoader.Entity.AnimationHooks
{
    public class DefaultScriptPartHook : AnimationHook
    {
        public uint PartIndex { get; set; }

        public override void Unpack(BinaryReader reader)
        {
            base.Unpack(reader);

            PartIndex = reader.ReadUInt32();
        }
    }
}
