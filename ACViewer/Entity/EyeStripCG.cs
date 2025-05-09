using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class EyeStripCG
    {
        public DatReaderWriter.Types.EyeStripCG _eyeStrip;

        public EyeStripCG(DatReaderWriter.Types.EyeStripCG eyeStrip)
        {
            _eyeStrip = eyeStrip;
        }

        public List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();

            if (_eyeStrip.IconId != 0)
            {
                var iconImage = new TreeNode($"Icon: {_eyeStrip.IconId:X8}", clickable: true);
                treeNode.Add(iconImage);
            }

            if (_eyeStrip.BaldIconId != 0)
            {
                var iconImageBald = new TreeNode($"Bald Icon: {_eyeStrip.BaldIconId:X8}", clickable: true);
                treeNode.Add(iconImageBald);
            }

            var objDesc = new TreeNode($"ObjDesc:");
            objDesc.Items.AddRange(new ObjDesc(_eyeStrip.ObjDesc).BuildTree());
            treeNode.Add(objDesc);

            var objDescBald = new TreeNode($"ObjDescBald:");
            objDescBald.Items.AddRange(new ObjDesc(_eyeStrip.BaldObjDesc).BuildTree());
            treeNode.Add(objDescBald);

            return treeNode;
        }
    }
}
