using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class RoadAlphaMap
    {
        public DatReaderWriter.Types.RoadAlphaMap _roadAlphaMap;

        public RoadAlphaMap(DatReaderWriter.Types.RoadAlphaMap roadAlphaMap)
        {
            _roadAlphaMap = roadAlphaMap;
        }

        public List<TreeNode> BuildTree()
        {
            var roadCode = new TreeNode($"RoadCode: {_roadAlphaMap.RCode}");
            var roadTexGID = new TreeNode($"RoadTexGID: {_roadAlphaMap.TexGID:X8}", clickable: true);

            return new List<TreeNode>() { roadCode, roadTexGID };
        }

        public override string ToString()
        {
            return $"RoadCode: {_roadAlphaMap.RCode}, RoadTexGID: {_roadAlphaMap.TexGID:X8}";
        }
    }
}
