using System.Collections.Generic;

namespace ACViewer.Entity.AnimationHooks
{
    public class LuminousPartHook : AnimationHook
    {
        public LuminousPartHook(DatReaderWriter.Types.AnimationHook hook)
            : base(hook)
        {
        }

        public override List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();

            if (_hook is DatReaderWriter.Types.LuminousPartHook _luminousPartHook)
            {
                treeNode.Add(new TreeNode($"Part: {_luminousPartHook.PartIndex}"));
                treeNode.Add(new TreeNode($"Start: {_luminousPartHook.Start}"));
                treeNode.Add(new TreeNode($"End: {_luminousPartHook.End}"));
                treeNode.Add(new TreeNode($"Time: {_luminousPartHook.Time}"));
            }
            treeNode.AddRange(base.BuildTree());

            return treeNode;
        }
    }
}
