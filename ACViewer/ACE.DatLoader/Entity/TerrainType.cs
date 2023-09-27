using System.Collections.Generic;
using System.IO;

namespace ACE.DatLoader.Entity
{
    public class TerrainType : IUnpackable
    {
        public string TerrainName { get; set; }
        public uint TerrainColor { get; set; }
        public List<uint> SceneTypes { get; } = new List<uint>();

        public void Unpack(BinaryReader reader)
        {
            TerrainName = reader.ReadPString();
            reader.AlignBoundary();

            TerrainColor = reader.ReadUInt32();

            SceneTypes.Unpack(reader);
        }
    }
}
