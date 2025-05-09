using System.Collections.Generic;

namespace ACViewer.Entity.AnimationHooks
{
    public class StopParticleHook : AnimationHook
    {
        public StopParticleHook(DatReaderWriter.Types.AnimationHook hook)
            : base(hook)
        {
        }

        public override List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();

            if (_hook is DatReaderWriter.Types.StopParticleHook _stopParticleHook)
            {
                treeNode.Add(new TreeNode($"EmitterId: {_stopParticleHook.EmitterId}"));
            }
            treeNode.AddRange(base.BuildTree());

            return treeNode;
        }
    }
}
