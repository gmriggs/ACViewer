using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class PlacementType
    {
        public DatReaderWriter.Types.AnimationFrame _placementType;

        public PlacementType(DatReaderWriter.Types.AnimationFrame placementType)
        {
            _placementType = placementType;
        }

        public List<TreeNode> BuildTree()
        {
            return new AnimationFrame(_placementType).BuildTree();
        }
    }
}
