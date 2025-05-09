using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class Stab
    {
        public DatReaderWriter.Types.Stab _stab;

        public Stab(DatReaderWriter.Types.Stab stab)
        {
            _stab = stab;
        }

        public List<TreeNode> BuildTree()
        {
            var id = new TreeNode($"ID: {_stab.Id:X8}", clickable: true);
            var frame = new TreeNode($"Frame: {new Frame(_stab.Frame)}");

            return new List<TreeNode>() { id, frame };
        }

        public override string ToString()
        {
            return $"ID: {_stab.Id:X8}, Frame: {new Frame(_stab.Frame)}";
        }
    }
}
