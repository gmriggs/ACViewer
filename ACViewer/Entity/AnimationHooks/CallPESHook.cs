using System.Collections.Generic;

namespace ACViewer.Entity.AnimationHooks
{
    public class CallPESHook : AnimationHook
    {
        public CallPESHook(DatReaderWriter.Types.AnimationHook hook)
            : base(hook)
        {
        }

        public override List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();

            if (_hook is DatReaderWriter.Types.CallPESHook _callPESHook)
            {
                treeNode.Add(new TreeNode($"PES: {_callPESHook.PES:X8}", clickable: true));
                treeNode.Add(new TreeNode($"Pause: {_callPESHook.Pause}"));
            }
            treeNode.AddRange(base.BuildTree());

            return treeNode;
        }
    }
}
