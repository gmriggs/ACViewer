namespace ACViewer.Entity
{
    public class Generator
    {
        public DatReaderWriter.Types.ObjHierarchyNode _generator;

        public Generator(DatReaderWriter.Types.ObjHierarchyNode generator)
        {
            _generator = generator;
        }

        public TreeNode BuildTree()
        {
            var heading = _generator.WCID != 0 ? $"{_generator.WCID} - {_generator.MenuName}" : _generator.MenuName;

            var treeNode = new TreeNode(heading);
            foreach (var item in _generator.Children)
                treeNode.Items.Add(new Generator(item).BuildTree());

            return treeNode;
        }
    }
}
