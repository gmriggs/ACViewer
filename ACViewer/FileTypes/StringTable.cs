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

            foreach (var kvp in _stringTable.StringTableData)
            {
                var node = new TreeNode($"{kvp.Key:X8}");
                node.Items.AddRange(new StringTableData(kvp.Value).BuildTree());

                stringTableData.Items.Add(node);
            }
            treeView.Items.AddRange(new List<TreeNode>() { language, unknown, stringTableData });
            return treeView;
        }
    }
}
