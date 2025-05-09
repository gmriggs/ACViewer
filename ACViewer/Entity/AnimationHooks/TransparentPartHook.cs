using System.Collections.Generic;

namespace ACViewer.Entity.AnimationHooks
{
    public class TransparentPartHook : AnimationHook
    {
        public TransparentPartHook(DatReaderWriter.Types.AnimationHook hook)
            : base(hook)
        {
        }

        public override List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();

            if (_hook is DatReaderWriter.Types.TransparentPartHook _transparentPartHook)
            {
                treeNode.Add(new TreeNode($"Part: {_transparentPartHook.PartIndex}"));
                treeNode.Add(new TreeNode($"Start: {_transparentPartHook.Start}"));
                treeNode.Add(new TreeNode($"End: {_transparentPartHook.End}"));
                treeNode.Add(new TreeNode($"Time: {_transparentPartHook.Time}"));
            }
            treeNode.AddRange(base.BuildTree());

            return treeNode;
        }
    }
}
