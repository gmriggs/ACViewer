using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class Position
    {
        public DatReaderWriter.Types.Position _position;

        public Position(DatReaderWriter.Types.Position position)
        {
            _position = position;
        }

        public List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();

            if (_position.CellId != 0)
            {
                var objCellID = new TreeNode($"ObjCellID: {_position.CellId:X8}", clickable: true);
                treeNode.Add(objCellID);
            }

            // frame inlined
            if (!_position.Frame.Origin.IsZeroEpsilon())
            {
                var origin = new TreeNode($"Origin: {_position.Frame.Origin}");
                treeNode.Add(origin);
            }

            if (!_position.Frame.Orientation.IsIdentity)
            {
                var orientation = new TreeNode($"Orientation: {_position.Frame.Orientation}");
                treeNode.Add(orientation);
            }
            return treeNode;
        }
    }
}
