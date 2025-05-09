using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class NameFilterLanguageData
    {
        public DatReaderWriter.Types.NameFilterLanguageData _nameFilterLanguageData;

        public NameFilterLanguageData(DatReaderWriter.Types.NameFilterLanguageData nameFilterLanguageData)
        {
            _nameFilterLanguageData = nameFilterLanguageData;
        }

        public List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();

            treeNode.Add(new TreeNode($"MaximumVowelsInARow: {_nameFilterLanguageData.MaximumVowelsInARow}"));
            treeNode.Add(new TreeNode($"FirstNCharactersMustHaveAVowel: {_nameFilterLanguageData.FirstNCharactersMustHaveAVowel}"));
            treeNode.Add(new TreeNode($"VowelContainingSubstringLength: {_nameFilterLanguageData.VowelContainingSubstringLength}"));
            treeNode.Add(new TreeNode($"ExtraAllowedCharacters: {_nameFilterLanguageData.ExtraAllowedCharacters}"));

            var compoundLetterGroups = new TreeNode($"CompoundLetterGrounds");

            foreach (var compoundLetterGroup in _nameFilterLanguageData.CompoundLetterGroups)
                compoundLetterGroups.Items.Add(new TreeNode(compoundLetterGroup));

            treeNode.Add(compoundLetterGroups);

            return treeNode;
        }
    }
}
