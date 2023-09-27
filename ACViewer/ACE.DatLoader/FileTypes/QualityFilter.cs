using System.Collections.Generic;
using System.IO;

namespace ACE.DatLoader.FileTypes
{
    [DatFileType(DatFileType.QualityFilter)]
    public class QualityFilter : FileType
    {
        public List<uint> IntStatFilter { get; set; } = new List<uint>();
        public List<uint> Int64StatFilter { get; set; } = new List<uint>();
        public List<uint> BoolStatFilter { get; set; } = new List<uint>();
        public List<uint> FloatStatFilter { get; set; } = new List<uint>();
        public List<uint> DidStatFilter { get; set; } = new List<uint>();
        public List<uint> IidStatFilter { get; set; } = new List<uint>();
        public List<uint> StringStatFilter { get; set; } = new List<uint>();
        public List<uint> PositionStatFilter { get; set; } = new List<uint>();

        public List<uint> AttributeStatFilter { get; set; } = new List<uint>();
        public List<uint> Attribute2ndStatFilter { get; set; } = new List<uint>();
        public List<uint> SkillStatFilter { get; set; } = new List<uint>();

        public override void Unpack(BinaryReader reader)
        {
            Id = reader.ReadUInt32();
            uint numInt = reader.ReadUInt32();
            uint numInt64 = reader.ReadUInt32();
            uint numBool = reader.ReadUInt32();
            uint numFloat = reader.ReadUInt32();
            uint numDid = reader.ReadUInt32();
            uint numIid = reader.ReadUInt32();
            uint numString = reader.ReadUInt32();
            uint numPosition = reader.ReadUInt32();

            for (var i = 0; i < numInt; i++)
                IntStatFilter.Add(reader.ReadUInt32());
            for (var i = 0; i < numInt64; i++)
                Int64StatFilter.Add(reader.ReadUInt32());
            for (var i = 0; i < numBool; i++)
                BoolStatFilter.Add(reader.ReadUInt32());
            for (var i = 0; i < numFloat; i++)
                FloatStatFilter.Add(reader.ReadUInt32());
            for (var i = 0; i < numDid; i++)
                DidStatFilter.Add(reader.ReadUInt32());
            for (var i = 0; i < numIid; i++)
                IidStatFilter.Add(reader.ReadUInt32());
            for (var i = 0; i < numString; i++)
                StringStatFilter.Add(reader.ReadUInt32());
            for (var i = 0; i < numPosition; i++)
                PositionStatFilter.Add(reader.ReadUInt32());

            uint numAttribute = reader.ReadUInt32();
            uint numAttribute2nd = reader.ReadUInt32();
            uint numSkill = reader.ReadUInt32();

            for (var i = 0; i < numAttribute; i++)
                AttributeStatFilter.Add(reader.ReadUInt32());
            for (var i = 0; i < numAttribute2nd; i++)
                Attribute2ndStatFilter.Add(reader.ReadUInt32());
            for (var i = 0; i < numSkill; i++)
                SkillStatFilter.Add(reader.ReadUInt32());
        }
    }
}
