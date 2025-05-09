using DatReaderWriter.DBObjs;

namespace ACE.DatLoader.Extensions
{
    public static class PortalDatabaseExtensions
    {
        public static CharGen CharGen(this PortalDatabaseCache portalDatabase)
        {
            portalDatabase.TryReadFileCache(0x0E000002, out CharGen charGen);
            return charGen;
        }

        public static ExperienceTable ExperienceTable(this PortalDatabaseCache portalDatabase)
        {
            portalDatabase.TryReadFileCache(0x0E000018, out ExperienceTable xpTable);
            return xpTable;
        }
        
        public static Region RegionDesc(this PortalDatabaseCache portalDatabase)
        {
            portalDatabase.TryReadFileCache(0x13000000, out Region region);
            return region;
        }

        public static SkillTable SkillTable(this PortalDatabaseCache portalDatabase)
        {
            portalDatabase.TryReadFileCache(0x0E000004, out SkillTable skillTable);
            return skillTable;
        }

        public static SpellTable SpellTable(this PortalDatabaseCache portalDatabase)
        {
            portalDatabase.TryReadFileCache(0x0E00000E, out SpellTable spellTable);
            return spellTable;
        }

        public static SpellComponentTable SpellComponentTable(this PortalDatabaseCache portalDatabase)
        {
            portalDatabase.TryReadFileCache(0x0E00000F, out SpellComponentTable spellComponentTable);
            return spellComponentTable;
        }

        public static VitalTable VitalTable(this PortalDatabaseCache portalDatabase)
        {
            portalDatabase.TryReadFileCache(0x0E000003, out VitalTable vitalTable);
            return vitalTable;
        }
    }
}
