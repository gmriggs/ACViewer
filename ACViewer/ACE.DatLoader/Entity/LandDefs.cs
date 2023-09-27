using System.Collections.Generic;
using System.IO;

namespace ACE.DatLoader.Entity
{
    public class LandDefs : IUnpackable
    {
        public int NumBlockLength { get; set; }
        public int NumBlockWidth { get; set; }
        public float SquareLength { get; set; }
        public int LBlockLength { get; set; }
        public int VertexPerCell { get; set; }
        public float MaxObjHeight { get; set; }
        public float SkyHeight { get; set; }
        public float RoadWidth { get; set; }

        public List<float> LandHeightTable { get; } = new List<float>();

        public void Unpack(BinaryReader reader)
        {
            NumBlockLength  = reader.ReadInt32();
            NumBlockWidth   = reader.ReadInt32();
            SquareLength    = reader.ReadSingle();
            LBlockLength    = reader.ReadInt32();
            VertexPerCell   = reader.ReadInt32();
            MaxObjHeight    = reader.ReadSingle();
            SkyHeight       = reader.ReadSingle();
            RoadWidth       = reader.ReadSingle();

            for (int i = 0; i < 256; i++)
                LandHeightTable.Add(reader.ReadSingle());
        }
    }
}
