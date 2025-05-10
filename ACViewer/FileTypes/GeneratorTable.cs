using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class GeneratorTable
    {
        public DatReaderWriter.DBObjs.ObjectHierarchy _generatorTable;

        public GeneratorTable(DatReaderWriter.DBObjs.ObjectHierarchy generatorTable)
        {
            _generatorTable = generatorTable;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_generatorTable.Id:X8}");

            treeView.Items = new Generator(_generatorTable.RootNode).BuildTree().Items;

            return treeView;
        }
    }
}
