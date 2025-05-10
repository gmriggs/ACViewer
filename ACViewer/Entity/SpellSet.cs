using System.Collections.Generic;
using System.Linq;

namespace ACViewer.Entity
{
    public class SpellSet
    {
        public DatReaderWriter.Types.SpellSet _spellSet;

        public SpellSet(DatReaderWriter.Types.SpellSet spellSet)
        {
            _spellSet = spellSet;
        }

        public List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();

            var spellSetTiers = new TreeNode($"SpellSetTiers");

            foreach (var kvp in _spellSet.SpellSetTiers)
            {
                var spellSetTier = new TreeNode(kvp.Key.ToString());
                spellSetTier.Items = new SpellSetTier(kvp.Value).BuildTree();
                
                spellSetTiers.Items.Add(spellSetTier);
            }
            treeNode.Add(spellSetTiers);
            //treeNode.Add(new TreeNode($"HighestTier: {_spellSet.SpellSetTiers.Keys.LastOrDefault()}"));
            //return treeNode;
            return spellSetTiers.Items;
        }
    }
}
