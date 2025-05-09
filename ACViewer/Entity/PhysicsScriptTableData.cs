using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class PhysicsScriptTableData
    {
        public DatReaderWriter.Types.PhysicsScriptTableData _data;

        public PhysicsScriptTableData(DatReaderWriter.Types.PhysicsScriptTableData data)
        {
            _data = data;
        }

        public List<TreeNode> BuildTree()
        {
            var scriptTable = new TreeNode("ScriptMods:");

            foreach (var scriptMod in _data.Scripts)
                scriptTable.Items.Add(new TreeNode($"{new ScriptMod(scriptMod)}", clickable: true));

            return scriptTable.Items;
        }
    }
}
