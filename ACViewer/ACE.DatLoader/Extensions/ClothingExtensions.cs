using ACE.Entity.Enum;

using DatReaderWriter.DBObjs;
using DatReaderWriter.Types;

namespace ACE.DatLoader.Extensions
{
    public static class ClothingExtensions
    {
        public static uint GetIcon(this Clothing clothing, uint palEffectIdx)
        {
            if (clothing.ClothingSubPalEffects.TryGetValue(palEffectIdx, out CloSubPalEffect result))
                return result.Icon;

            return 0;
        }

        /// <summary>
        /// Calculates the ClothingPriority of an item based on the actual coverage. So while an Over-Robe may just be "Chest", we want to know it covers everything but head & arms.
        /// </summary>
        /// <param name="setupId">Defaults to HUMAN_MALE if not set, which is good enough</param>
        /// <returns></returns>
        public static CoverageMask? GetVisualPriority(this Clothing clothing, uint setupId = 0x02000001)
        {
            if (clothing.ClothingBaseEffects.ContainsKey(setupId))
            {
                CoverageMask visualPriority = 0;
                foreach (CloObjectEffect t in clothing.ClothingBaseEffects[setupId].CloObjectEffects)
                    switch (t.Index)
                    {
                        case 0: // HUMAN_ABDOMEN;
                            visualPriority |= CoverageMask.OuterwearAbdomen;
                            break;
                        case 1: // HUMAN_LEFT_UPPER_LEG;
                        case 5: // HUMAN_RIGHT_UPPER_LEG;
                            visualPriority |= CoverageMask.OuterwearUpperLegs;
                            break;
                        case 2: // HUMAN_LEFT_LOWER_LEG;
                        case 6: // HUMAN_RIGHT_LOWER_LEG;
                            visualPriority |= CoverageMask.OuterwearLowerLegs;
                            break;
                        case 3: // HUMAN_LEFT_FOOT;
                        case 4: // HUMAN_LEFT_TOE;
                        case 7: // HUMAN_RIGHT_FOOT;
                        case 8: // HUMAN_RIGHT_TOE;
                            visualPriority |= CoverageMask.Feet;
                            break;
                        case 9: // HUMAN_CHEST;
                            visualPriority |= CoverageMask.OuterwearChest;
                            break;
                        case 10: // HUMAN_LEFT_UPPER_ARM;
                        case 13: // HUMAN_RIGHT_UPPER_ARM;
                            visualPriority |= CoverageMask.OuterwearUpperArms;
                            break;
                        case 11: // HUMAN_LEFT_LOWER_ARM;
                        case 14: // HUMAN_RIGHT_LOWER_ARM;
                            visualPriority |= CoverageMask.OuterwearLowerArms;
                            break;
                        case 12: // HUMAN_LEFT_HAND;
                        case 15: // HUMAN_RIGHT_HAND;
                            visualPriority |= CoverageMask.Hands;
                            break;
                        case 16: // HUMAN_HEAD;
                            visualPriority |= CoverageMask.Head;
                            break;
                        default: // Lots of things we don't care about
                            break;
                    }
                return visualPriority;
            }
            else
                return null;
        }
    }
}
