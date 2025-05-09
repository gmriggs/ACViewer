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

            treeNode.Add(new TreeNode($"Attr1: {(PropertyAttribute)_skillFormula.Attribute1}"));
            treeNode.Add(new TreeNode($"Attr2: {(PropertyAttribute)_skillFormula.Attribute2}"));
            treeNode.Add(new TreeNode($"W: {_skillFormula.Unknown}"));
            treeNode.Add(new TreeNode($"X: {_skillFormula.HasSecondAttribute}"));
            treeNode.Add(new TreeNode($"Y: {_skillFormula.UseFormula}"));
            treeNode.Add(new TreeNode($"Z (divisor): {_skillFormula.Divisor}"));

            return treeNode;
        }
    }
}
