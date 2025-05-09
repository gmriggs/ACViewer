using System.Collections.Generic;

using ACE.Entity.Enum;

namespace ACViewer.Entity
{
    public class BSPPortal: BSPNode
    {
        public DatReaderWriter.Types.BSPPortal _bspPortal;

        public BSPPortal(DatReaderWriter.Types.BSPPortal bspPortal)
        {
            _bspPortal = bspPortal;
        }

        public override List<TreeNode> BuildTree(BSPType bspType)
        {
            var type = new TreeNode($"Type: {_bspPortal.Type:X8}");

            var splitter = new TreeNode($"Splitting Plane: {new Plane(_bspPortal.SplittingPlane)}");

            var posNode = new TreeNode("PosNode:");
            if (_bspPortal.PosNode != null)
            {
                if (_bspPortal.PosNode is DatReaderWriter.Types.BSPLeaf leaf)
                    posNode.Items.AddRange(new BSPLeaf(leaf).BuildTree(bspType));
                else if (_bspPortal.PosNode is DatReaderWriter.Types.BSPPortal portal)
                    posNode.Items.AddRange(new BSPPortal(portal).BuildTree(bspType));
                else
                    posNode.Items.AddRange(new BSPNode(_bspPortal.PosNode).BuildTree(bspType));
            }

            var negNode = new TreeNode("NegNode:");
            if (_bspPortal.NegNode != null)
            {
                if (_bspPortal.NegNode is DatReaderWriter.Types.BSPLeaf leaf)
                    negNode.Items.AddRange(new BSPLeaf(leaf).BuildTree(bspType));
                else if (_bspPortal.NegNode is DatReaderWriter.Types.BSPPortal portal)
                    negNode.Items.AddRange(new BSPPortal(portal).BuildTree(bspType));
                else
                    negNode.Items.AddRange(new BSPNode(_bspPortal.NegNode).BuildTree(bspType));
            }

            var treeNode = new List<TreeNode>() { type, splitter, posNode, negNode };

            if (bspType == BSPType.Drawing)
            {
                var sphere = new TreeNode($"Sphere: {new Sphere(_bspPortal.Sphere)}");
                var inPolys = new TreeNode($"InPolys: {string.Join(", ", _bspPortal.InPolys)}");

                var inPortals = new TreeNode("InPortals:");
                foreach (var inPortal in _bspPortal.InPortals)
                    inPortals.Items.Add(new TreeNode(new PortalPoly(inPortal).ToString()));

                treeNode.AddRange(new List<TreeNode>() { sphere, inPolys, inPortals });
            }
            return treeNode;
        }
    }
}
