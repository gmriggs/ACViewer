using System.IO;

namespace ACE.DatLoader.Entity
{
    public class GearCG : IUnpackable
    {
        public string Name { get; set; }
        public uint ClothingTable { get; set; }
        public uint WeenieDefault { get; set; }

        public void Unpack(BinaryReader reader)
        {
            Name            = reader.ReadString();
            ClothingTable   = reader.ReadUInt32();
            WeenieDefault   = reader.ReadUInt32();
        }
    }
}
