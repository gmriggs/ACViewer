using System.IO;

namespace ACE.DatLoader.Entity
{
    public class PhysicsScriptData : IUnpackable
    {
        public double StartTime { get; set; }
        public AnimationHook Hook { get; set; } = new AnimationHook();

        public void Unpack(BinaryReader reader)
        {
            StartTime = reader.ReadDouble();

            Hook = AnimationHook.ReadHook(reader);
        }
    }
}
