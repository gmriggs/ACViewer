using System.Collections.Generic;

namespace ACViewer.Entity.AnimationHooks
{
    public class ReplaceObjectHook : AnimationHook
    {
        public ReplaceObjectHook(DatReaderWriter.Types.AnimationHook hook)
            : base(hook)
        {
        }

        public override List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();

            if (_hook is DatReaderWriter.Types.ReplaceObjectHook _replaceObjectHook)
            {
                var partIdx = new TreeNode($"PartIdx: {_replaceObjectHook.PartIndex}");
                var partID = new TreeNode($"PartID: {_replaceObjectHook.PartId:X8}", clickable: true);

                treeNode.AddRange(new List<TreeNode>() { partIdx, partID });
            }
            treeNode.AddRange(base.BuildTree());

            return treeNode;
        }
    }
}
