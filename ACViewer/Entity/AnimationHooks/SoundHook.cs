using System.Collections.Generic;

namespace ACViewer.Entity.AnimationHooks
{
    public class SoundHook : AnimationHook
    {
        public SoundHook(DatReaderWriter.Types.AnimationHook hook)
            : base(hook)
        {
        }

        public override List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();

            if (_hook is DatReaderWriter.Types.SoundHook _soundHook)
            {
                treeNode.Add(new TreeNode($"Id: {_soundHook.Id:X8}"));
            }
            treeNode.AddRange(base.BuildTree());

            return treeNode;
        }
    }
}
