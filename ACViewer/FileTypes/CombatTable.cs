using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class CombatTable
    {
        public DatReaderWriter.DBObjs.CombatTable _combatTable;

        public CombatTable(DatReaderWriter.DBObjs.CombatTable combatTable)
        {
            _combatTable = combatTable;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_combatTable.Id:X8}");

            var maneuvers = new TreeNode("Maneuvers:");

            for (var i = 0; i < _combatTable.CombatManeuvers.Count; i++)
            {
                var maneuver = new TreeNode($"{i}");
                maneuver.Items.AddRange(new CombatManeuver(_combatTable.CombatManeuvers[i]).BuildTree());

                maneuvers.Items.Add(maneuver);
            }
            treeView.Items.AddRange(maneuvers.Items);

            return treeView;
        }
    }
}
