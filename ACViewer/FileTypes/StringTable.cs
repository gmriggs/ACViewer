using System.Collections.Generic;

using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class StringTable
    {
        public DatReaderWriter.DBObjs.StringTable _stringTable;

        public StringTable(DatReaderWriter.DBObjs.StringTable stringTable)
        {
            _stringTable = stringTable;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_stringTable.Id:X8}");

            var language = new TreeNode($"Language: {_stringTable.Language}");

            var unknown = new TreeNode($"Unknown: {_stringTable.Unknown}");

            var stringTableData = new TreeNode($"String Tables:");

            for (uint i = 0; i < _stringTable.StringTableData.Count; i++)
            {
                var tree = new StringTableData(_stringTable.StringTableData[i]).BuildTree();
                var node = new TreeNode($"{tree[0].Name}");
                tree.RemoveAt(0);
                node.Items.AddRange(tree);

                stringTableData.Items.Add(node);
            }
            treeView.Items.AddRange(new List<TreeNode>() { language, unknown, stringTableData });
            return treeView;
        }
    }
}
