using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class FaceStripCG
    {
        public DatReaderWriter.Types.FaceStripCG _faceStrip;

        public FaceStripCG(DatReaderWriter.Types.FaceStripCG faceStrip)
        {
            _faceStrip = faceStrip;
        }

        public List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();

            if (_faceStrip.IconId != 0)
            {
                var icon = new TreeNode($"Icon: {_faceStrip.IconId:X8}", clickable: true);
                treeNode.Add(icon);
            }

            var objDesc = new TreeNode("ObjDesc:");
            objDesc.Items.AddRange(new ObjDesc(_faceStrip.ObjDesc).BuildTree());
            treeNode.Add(objDesc);

            return treeNode;
        }
    }
}
