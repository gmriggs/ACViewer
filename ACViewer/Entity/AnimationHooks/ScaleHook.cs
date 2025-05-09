using System.Collections.Generic;

namespace ACViewer.Entity.AnimationHooks
{
    public class ScaleHook : AnimationHook
    {
        public ScaleHook(DatReaderWriter.Types.AnimationHook hook)
            : base(hook)
        {
        }

        public override List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();

            if (_hook is DatReaderWriter.Types.ScaleHook _scaleHook)
            {
                treeNode.Add(new TreeNode($"End: {_scaleHook.End}"));
                treeNode.Add(new TreeNode($"Time: {_scaleHook.Time}"));
            }
            treeNode.AddRange(base.BuildTree());

            return treeNode;
        }
    }
}
