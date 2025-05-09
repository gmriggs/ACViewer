using System.Collections.Generic;

using ACE.Entity.Enum;

namespace ACViewer.Entity
{
    public class SkillCG
    {
        public DatReaderWriter.Types.SkillCG _skill;

        public SkillCG(DatReaderWriter.Types.SkillCG skill)
        {
            _skill = skill;
        }

        public List<TreeNode> BuildTree()
        {
            var skill = new TreeNode($"Skill: {(Skill)_skill.Id}");
            var normalCost = new TreeNode($"Normal Cost: {_skill.NormalCost}");
            var primaryCost = new TreeNode($"Primary Cost: {_skill.PrimaryCost}");

            return new List<TreeNode>() { skill, normalCost, primaryCost };
        }
    }
}
