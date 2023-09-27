using System.IO;

namespace ACE.DatLoader.Entity
{
    public class Contract : IUnpackable
    {
        public uint Version { get; set; }
        public uint ContractId { get; set; }
        public string ContractName { get; set; }

        public string Description { get; set; }
        public string DescriptionProgress { get; set; }

        public string NameNPCStart { get; set; }
        public string NameNPCEnd { get; set; }

        public string QuestflagStamped { get; set; }
        public string QuestflagStarted { get; set; }
        public string QuestflagFinished { get; set; }
        public string QuestflagProgress { get; set; }
        public string QuestflagTimer { get; set; }
        public string QuestflagRepeatTime { get; set; }

        public Position LocationNPCStart { get; } = new Position();
        public Position LocationNPCEnd { get; } = new Position();
        public Position LocationQuestArea { get; } = new Position();

        public void Unpack(BinaryReader reader)
        {
            Version = reader.ReadUInt32();
            ContractId = reader.ReadUInt32();
            ContractName = reader.ReadPString();
            reader.AlignBoundary();

            Description = reader.ReadPString();
            reader.AlignBoundary();
            DescriptionProgress = reader.ReadPString();
            reader.AlignBoundary();

            NameNPCStart = reader.ReadPString();
            reader.AlignBoundary();
            NameNPCEnd = reader.ReadPString();
            reader.AlignBoundary();

            QuestflagStamped = reader.ReadPString();
            reader.AlignBoundary();
            QuestflagStarted = reader.ReadPString();
            reader.AlignBoundary();
            QuestflagFinished = reader.ReadPString();
            reader.AlignBoundary();
            QuestflagProgress = reader.ReadPString();
            reader.AlignBoundary();
            QuestflagTimer = reader.ReadPString();
            reader.AlignBoundary();
            QuestflagRepeatTime = reader.ReadPString();
            reader.AlignBoundary();

            LocationNPCStart.Unpack(reader);
            LocationNPCEnd.Unpack(reader);
            LocationQuestArea.Unpack(reader);
        }
    }
}
