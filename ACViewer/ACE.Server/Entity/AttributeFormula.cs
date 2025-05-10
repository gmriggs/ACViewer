using ACE.Common.Extensions;
using ACE.DatLoader;
using ACE.DatLoader.Extensions;
using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Server.WorldObjects;

namespace ACE.Server.Entity
{
    /// <summary>
    /// Calculates the amount to add to a creature's current skills and vitals,
    /// based on their primary attribute current values
    /// </summary>
    public static class AttributeFormula
    {
        /// <summary>
        /// Returns the amount to add to a creature's current skill,
        /// based on their primary attribute current values
        /// </summary>
        public static uint GetFormula(Creature creature, Skill skill, bool current = true)
        {
            var skillTable = DatManager.PortalDat.SkillTable();

            if (!skillTable.Skills.TryGetValue((DatReaderWriter.Enums.SkillId)skill, out var skillBase))
                return 0;

            return GetFormula(creature, skillBase.Formula, current);
        }

        /// <summary>
        /// Returns the amount to add to a creature's current vital,
        /// based on their primary attribute current values
        /// </summary>
        public static uint GetFormula(Creature creature, PropertyAttribute2nd vital, bool current = true)
        {
            var vitalTable = DatManager.PortalDat.VitalTable();

            switch (vital)
            {
                case PropertyAttribute2nd.MaxHealth:
                    return GetFormula(creature, vitalTable.Health, current);
                case PropertyAttribute2nd.MaxStamina:
                    return GetFormula(creature, vitalTable.Stamina, current);
                case PropertyAttribute2nd.MaxMana:
                    return GetFormula(creature, vitalTable.Mana, current);
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Applies a SkillFormula from the portal.dat,
        /// using the primary attributes for a creature
        /// </summary>
        public static uint GetFormula(Creature creature, DatReaderWriter.Types.SkillFormula formula, bool current = true)
        {
            if (formula.Attribute1Multiplier == 0) return 0;

            var attr1 = (PropertyAttribute)formula.Attribute1;
            var attr2 = (PropertyAttribute)formula.Attribute2;
            var divisor = formula.Divisor;

            var total = current ? creature.Attributes[attr1].Current : creature.Attributes[attr1].Base;
            if (attr2 != PropertyAttribute.Undef)
                total += current ? creature.Attributes[attr2].Current : creature.Attributes[attr2].Base;

            if (divisor != 1)
                total = (uint)((float)total / divisor).Round();

            return total;
        }
    }
}
