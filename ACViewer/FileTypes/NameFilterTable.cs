using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class NameFilterTable
    {
        public DatReaderWriter.DBObjs.NameFilterTable _nameFilterTable;

        public NameFilterTable(DatReaderWriter.DBObjs.NameFilterTable nameFilterTable)
        {
            _nameFilterTable = nameFilterTable;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_nameFilterTable.Id:X8}");

            foreach (var kvp in _nameFilterTable.LanguageData)
            {
                var node = new TreeNode($"{kvp.Key}");
                node.Items = new NameFilterLanguageData(kvp.Value).BuildTree();

                treeView.Items.Add(node);
            }
            return treeView;
        }
    }
}
