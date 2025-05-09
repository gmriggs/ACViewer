using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class SubPalette
    {
        public DatReaderWriter.Types.SubPalette _subPalette;

        public SubPalette(DatReaderWriter.Types.SubPalette subPalette)
        {
            _subPalette = subPalette;
        }

        public List<TreeNode> BuildTree()
        {
            var subID = new TreeNode($"SubID: {_subPalette.SubId:X8}", clickable: true);
            var offset = new TreeNode($"Offset: {_subPalette.Offset}");
            var numColors = new TreeNode($"NumColors: {_subPalette.NumColors}");

            return new List<TreeNode>() { subID, offset, numColors };
        }
    }
}
