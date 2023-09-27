using System.ComponentModel;
using System.IO;
using System.Linq;

using ACE.DatLoader.FileTypes;

using ACViewer.PropertyEditor;

namespace ACE.DatLoader.Entity
{
    [TypeConverter(typeof(PropertyExpandableObjectConverter))]
    public class SpellSet : IUnpackable
    {
        // uint key is the total combined item level of all the equipped pieces in the set
        // client calls this m_PieceCount
        public PropertySortedDictionary<uint, SpellSetTiers> SpellSetTiers { get; } = new PropertySortedDictionary<uint, SpellSetTiers>();

        public uint HighestTier = 0;

        [Browsable(false)]
        public PropertySortedDictionary<uint, SpellSetTiers> SpellSetTiersNoGaps { get; } = new PropertySortedDictionary<uint, SpellSetTiers>();

        public void Unpack(BinaryReader reader)
        {
            SpellSetTiers._dict.UnpackPackedHashTable(reader);

            HighestTier = SpellSetTiers._dict.Keys.LastOrDefault();

            SpellSetTiers lastSpellSetTier = null;

            for (uint i = 0; i <= HighestTier; i++)
            {
                if (SpellSetTiers._dict.TryGetValue(i, out var spellSetTiers))
                    lastSpellSetTier = spellSetTiers;

                if (lastSpellSetTier != null)
                    SpellSetTiersNoGaps.Add(i, lastSpellSetTier);
            }
        }
    }
}
