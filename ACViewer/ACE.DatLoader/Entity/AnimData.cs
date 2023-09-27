using System.IO;

namespace ACE.DatLoader.Entity
{
    public class AnimData : IUnpackable
    {
        public uint AnimId { get; set; }
        public int LowFrame { get; set; }
        public int HighFrame { get; set; }
        /// <summary>
        /// Negative framerates play animation in reverse
        /// </summary>
        public float Framerate { get; set; }

        public void Unpack(BinaryReader reader)
        {
            AnimId      = reader.ReadUInt32();
            LowFrame    = reader.ReadInt32();
            HighFrame   = reader.ReadInt32();
            Framerate   = reader.ReadSingle();
        }

        public override string ToString()
        {
            return $"AnimId: {AnimId:X8}, LowFrame: {LowFrame}, HighFrame: {HighFrame}, FrameRate: {Framerate}";
        }
    }
}
