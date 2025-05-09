using System.Collections.Generic;

using ACE.Entity.Enum;

namespace ACViewer.Entity
{
    public class BSPNode
    {
        public DatReaderWriter.Types.BSPNode _bspNode;

        public BSPNode() { }

        public BSPNode(DatReaderWriter.Types.BSPNode bspNode)
        {
            _bspNode = bspNode;
        }

        public virtual List<TreeNode> BuildTree(BSPType bspType)
        {
            var type = new TreeNode($"Type: {_bspNode.Type:X8}");

            var splitter = new TreeNode("Splitting Plane: ");
            if (_bspNode.SplittingPlane != null)
                splitter.Name += new Plane(_bspNode.SplittingPlane).ToString();

            var treeNode = new List<TreeNode>() { type, splitter };

            var posNode = new TreeNode("PosNode:");
            if (_bspNode.PosNode != null)
            {
                if (_bspNode.PosNode is DatReaderWriter.Types.BSPLeaf leaf)
                    posNode.Items.AddRange(new BSPLeaf(leaf).BuildTree(bspType));
                else if (_bspNode.PosNode is DatReaderWriter.Types.BSPPortal portal)
                    posNode.Items.AddRange(new BSPPortal(portal).BuildTree(bspType));
                else
                    posNode.Items.AddRange(new BSPNode(_bspNode.PosNode).BuildTree(bspType));

                treeNode.Add(posNode);
            }

            var negNode = new TreeNode("NegNode:");
            if (_bspNode.NegNode != null)
            {
                if (_bspNode.NegNode is DatReaderWriter.Types.BSPLeaf leaf)
                    negNode.Items.AddRange(new BSPLeaf(leaf).BuildTree(bspType));
                else if (_bspNode.NegNode is DatReaderWriter.Types.BSPPortal portal)
                    negNode.Items.AddRange(new BSPPortal(portal).BuildTree(bspType));
                else
                    negNode.Items.AddRange(new BSPNode(_bspNode.NegNode).BuildTree(bspType));

                treeNode.Add(negNode);
            }

            if (bspType == BSPType.Cell)
                return treeNode;

            var sphere = new TreeNode($"Sphere: {new Sphere(_bspNode.Sphere)}");
            treeNode.Add(sphere);

            if (bspType == BSPType.Physics)
                return treeNode;

            var inPolys = new TreeNode($"InPolys: {string.Join(", ", _bspNode.InPolys)}");
            treeNode.Add(inPolys);

            return treeNode;
        }
    }
}
