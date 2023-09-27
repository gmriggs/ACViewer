using System.IO;

namespace ACE.DatLoader.Entity
{
    public class Season : IUnpackable
    {
        public uint StartDate { get; set; }
        public string Name { get; set; }

        public void Unpack(BinaryReader reader)
        {
            StartDate = reader.ReadUInt32();
            Name = reader.ReadPString();
            reader.AlignBoundary();
        }
    }
}
