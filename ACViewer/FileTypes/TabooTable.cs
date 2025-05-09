using System.Linq;

using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class TabooTable
    {
        public DatReaderWriter.DBObjs.TabooTable _tabooTable;

        public TabooTable(DatReaderWriter.DBObjs.TabooTable tabooTable)
        {
            _tabooTable = tabooTable;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_tabooTable.Id:X8}");

            foreach (var kvp in _tabooTable.Entries.OrderBy(i => i.Key))
            {
                var keyNode = new TreeNode(kvp.Key.ToString("X8"));

                var entry = kvp.Value;

                foreach (var bannedPattern in entry.BannedPatterns)
                    keyNode.Items.Add(new TreeNode(bannedPattern));

                treeView.Items.Add(keyNode);
            }
            return treeView;
        }
    }
}
