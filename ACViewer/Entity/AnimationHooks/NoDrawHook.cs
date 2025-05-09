using System.Collections.Generic;

namespace ACViewer.Entity.AnimationHooks
{
    public class NoDrawHook : AnimationHook
    {
        public NoDrawHook(DatReaderWriter.Types.AnimationHook hook)
            : base(hook)
        {
        }

        public override List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();

            if (_hook is DatReaderWriter.Types.NoDrawHook _noDrawHook)
            {
                treeNode.Add(new TreeNode($"NoDraw: {_noDrawHook.NoDraw}"));
            }
            treeNode.AddRange(base.BuildTree());

            return treeNode;
        }
    }
}
