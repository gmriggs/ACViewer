using System.IO;

namespace ACE.DatLoader.Entity.AnimationHooks
{
    public class SetLightHook : AnimationHook
    {
        public int LightsOn { get; set; }

        public override void Unpack(BinaryReader reader)
        {
            base.Unpack(reader);

            LightsOn = reader.ReadInt32();
        }
    }
}
