using ACE.Entity.Enum;

namespace ACViewer.Entity
{
    public class BSPTree
    {
        public DatReaderWriter.Types.BSPTree _bspTree;

        public BSPTree(DatReaderWriter.Types.BSPTree bspTree)
        {
            _bspTree = bspTree;
        }

        public TreeNode BuildTree(BSPType bspType)
        {
            var root = new TreeNode("Root");
            root.Items.AddRange(new BSPNode(_bspTree.RootNode).BuildTree(bspType));

            return root;
        }
    }
}
