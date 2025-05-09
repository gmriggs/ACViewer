using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class LandSurf
    {
        public DatReaderWriter.Types.LandSurf _landSurf;

        public LandSurf(DatReaderWriter.Types.LandSurf landSurf)
        {
            _landSurf = landSurf;
        }

        public List<TreeNode> BuildTree()
        {
            // type always 0?
            return new TexMerge(_landSurf.TexMerge).BuildTree();
        }
    }
}
