using System.Collections.Generic;

namespace ACViewer.Entity.AnimationHooks
{
    public class SetLightHook : AnimationHook
    {
        public SetLightHook(DatReaderWriter.Types.AnimationHook hook)
            : base(hook)
        {
        }

        public override List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();

            if (_hook is DatReaderWriter.Types.SetLightHook _setLightHook)
            {
                treeNode.Add(new TreeNode($"LightsOn: {_setLightHook.LightsOn}"));
            }
            treeNode.AddRange(base.BuildTree());

            return treeNode;
        }
    }
}
