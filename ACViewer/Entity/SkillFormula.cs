using System.Collections.Generic;

using ACE.Entity.Enum.Properties;

namespace ACViewer.Entity
{
    public class SkillFormula
    {
        public DatReaderWriter.Types.SkillFormula _skillFormula;

        public SkillFormula(DatReaderWriter.Types.SkillFormula skillFormula)
        {
            _skillFormula = skillFormula;
        }

        public List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();
            treeNode.Add(new TreeNode($"AdditiveBonus (W): {_skillFormula.AdditiveBonus}"));
            treeNode.Add(new TreeNode($"Attr1Multiplier (X): {_skillFormula.Attribute1Multiplier}"));
            treeNode.Add(new TreeNode($"Attr2Multiplier (Y): {_skillFormula.Attribute2Multiplier}"));
            treeNode.Add(new TreeNode($"Divisor (Z): {_skillFormula.Divisor}"));
            treeNode.Add(new TreeNode($"Attr1: {(PropertyAttribute)_skillFormula.Attribute1}"));
            treeNode.Add(new TreeNode($"Attr2: {(PropertyAttribute)_skillFormula.Attribute2}"));

            return treeNode;
        }
    }
}
