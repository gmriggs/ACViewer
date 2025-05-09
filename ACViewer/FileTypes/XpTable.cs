using System.Collections.Generic;

using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class XpTable
    {
        public DatReaderWriter.DBObjs.ExperienceTable _xpTable;

        public XpTable(DatReaderWriter.DBObjs.ExperienceTable xpTable)
        {
            _xpTable = xpTable;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_xpTable.Id:X8}");

            var attributeXpList = new TreeNode("AttributeXpList");

            for (var i = 0; i < _xpTable.Attributes.Length; i++)
            {
                var attributeXpNode = new TreeNode($"{i}: {_xpTable.Attributes[i]:N0}");
                attributeXpList.Items.Add(attributeXpNode);
            }

            var vitalXpList = new TreeNode("VitalXpList");

            for (var i = 0; i < _xpTable.Vitals.Length; i++)
            {
                var vitalXpNode = new TreeNode($"{i}: {_xpTable.Vitals[i]:N0}");
                vitalXpList.Items.Add(vitalXpNode);
            }

            var trainedSkillXpList = new TreeNode("TrainedSkillXpList");

            for (var i = 0; i < _xpTable.TrainedSkills.Length; i++)
            {
                var trainedSkillXpNode = new TreeNode($"{i}: {_xpTable.TrainedSkills[i]:N0}");
                trainedSkillXpList.Items.Add(trainedSkillXpNode);
            }

            var specializedSkillXpList = new TreeNode("SpecializedSkillXpList");

            for (var i = 0; i < _xpTable.SpecializedSkills.Length; i++)
            {
                var specializedSkillXpNode = new TreeNode($"{i}: {_xpTable.SpecializedSkills[i]:N0}");
                specializedSkillXpList.Items.Add(specializedSkillXpNode);
            }

            var characterLevelXpList = new TreeNode("CharacterLevelXpList");

            for (var i = 0; i < _xpTable.Levels.Length; i++)
            {
                var characterLevelXpNode = new TreeNode($"{i}: {_xpTable.Levels[i]:N0}");
                characterLevelXpList.Items.Add(characterLevelXpNode);
            }

            var characterLevelSkillCreditList = new TreeNode("CharacterLevelSkillCreditList");

            for (var i = 0; i < _xpTable.SkillCredits.Length; i++)
            {
                var characterLevelSkillCreditNode = new TreeNode($"{i}: {_xpTable.SkillCredits[i]:N0}");
                characterLevelSkillCreditList.Items.Add(characterLevelSkillCreditNode);
            }

            treeView.Items.AddRange(new List<TreeNode>() { attributeXpList, vitalXpList, trainedSkillXpList, specializedSkillXpList, characterLevelXpList, characterLevelSkillCreditList });

            return treeView;
        }
    }
}
