using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class ClothingSubPaletteEffect
    {
        public DatReaderWriter.Types.CloSubPalEffect _effect;

        public ClothingSubPaletteEffect(DatReaderWriter.Types.CloSubPalEffect effect)
        {
            _effect = effect;
        }

        public List<TreeNode> BuildTree()
        {
            var icon = new TreeNode($"Icon: {_effect.Icon:X8}", clickable: true);

            var subPalettes = new TreeNode("SubPalettes:");
            foreach (var subPalette in _effect.CloSubPalettes)
            {
                var subPaletteTree = new ClothingSubPalette(subPalette).BuildTree();
                var subPaletteNode = new TreeNode($"{subPaletteTree[1].Name.Replace("Palette Set: ", "")}", clickable: true);
                subPaletteTree.RemoveAt(1);
                subPaletteNode.Items.AddRange(subPaletteTree);
                subPalettes.Items.Add(subPaletteNode);
            }
            return new List<TreeNode>() { icon, subPalettes };
        }
    }
}
