using System.IO;

namespace ACE.DatLoader.Entity
{
    public class AmbientSoundDesc : IUnpackable
    {
        public uint SType { get; set; }
        public float Volume { get; set; }
        public float BaseChance { get; set; }
        public float MinRate { get; set; }
        public float MaxRate { get; set; }

        public void Unpack(BinaryReader reader)
        {
            SType       = reader.ReadUInt32();
            Volume      = reader.ReadSingle();
            BaseChance  = reader.ReadSingle();
            MinRate     = reader.ReadSingle();
            MaxRate     = reader.ReadSingle();
        }

        public bool IsContinuous => (BaseChance == 0);
    }
}
