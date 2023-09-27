using System.IO;

namespace ACE.DatLoader.Entity
{
    public class TimeOfDay : IUnpackable
    {
        public float Start { get; set; }
        public bool IsNight { get; set; }
        public string Name { get; set; }

        public void Unpack(BinaryReader reader)
        {
            Start   = reader.ReadSingle();
            IsNight = (reader.ReadUInt32() == 1);
            Name    = reader.ReadPString();
            reader.AlignBoundary();
        }
    }
}
