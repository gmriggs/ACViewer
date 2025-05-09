using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class SpellComponentsTable
    {
        public DatReaderWriter.DBObjs.SpellComponentTable _spellComponentsTable;

        public SpellComponentsTable(DatReaderWriter.DBObjs.SpellComponentTable spellComponentsTable)
        {
            _spellComponentsTable = spellComponentsTable;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_spellComponentsTable.Id:X8}");

            foreach (var kvp in _spellComponentsTable.Components)
            {
                var spellComponentNode = new TreeNode($"{kvp.Key}: {kvp.Value.Name}");
                spellComponentNode.Items = new Entity.SpellComponentBase(kvp.Value).BuildTree();

                treeView.Items.Add(spellComponentNode);
            }
            return treeView;
        }
    }
}
