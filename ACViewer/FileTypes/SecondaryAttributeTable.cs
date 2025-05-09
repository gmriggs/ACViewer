using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class SecondaryAttributeTable
    {
        public DatReaderWriter.DBObjs.VitalTable _vitalTable;

        public SecondaryAttributeTable(DatReaderWriter.DBObjs.VitalTable vitalTable)
        {
            _vitalTable = vitalTable;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_vitalTable.Id:X8}");

            var health = new TreeNode($"Health");
            health.Items =  new SkillFormula(_vitalTable.Health).BuildTree();
            treeView.Items.Add(health);

            var stamina = new TreeNode($"Stamina");
            stamina.Items = new SkillFormula(_vitalTable.Stamina).BuildTree();
            treeView.Items.Add(stamina);

            var mana = new TreeNode($"Mana");
            mana.Items = new SkillFormula(_vitalTable.Mana).BuildTree();
            treeView.Items.Add(mana);

            return treeView;
        }
    }
}
