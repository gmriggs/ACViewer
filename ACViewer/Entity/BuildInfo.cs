using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class BuildInfo
    {
        public DatReaderWriter.Types.BuildingInfo _buildInfo;

        public BuildInfo(DatReaderWriter.Types.BuildingInfo buildInfo)
        {
            _buildInfo = buildInfo;
        }

        public List<TreeNode> BuildTree()
        {
            var modelID = new TreeNode($"ModelID: {_buildInfo.ModelId:X8}", clickable: true);
            var frame = new TreeNode($"Frame: {new Frame(_buildInfo.Frame)}");
            var numLeaves = new TreeNode($"NumLeaves: {_buildInfo.NumLeaves}");

            var portals = new TreeNode("Portals:");
            for (var i = 0; i < _buildInfo.Portals.Count; i++)
            {
                var portal = new TreeNode($"{i}");
                portal.Items.AddRange(new BldPortal(_buildInfo.Portals[i]).BuildTree());

                portals.Items.Add(portal);
            }
            return new List<TreeNode>() { modelID, frame, numLeaves, portals };
        }
    }
}
