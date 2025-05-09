using System.Collections.Generic;

namespace ACViewer.Entity.AnimationHooks
{
    public class DefaultScriptPartHook : AnimationHook
    {
        public DefaultScriptPartHook(DatReaderWriter.Types.AnimationHook hook)
            : base(hook)
        {
        }

        public override List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();

            if (_hook is DatReaderWriter.Types.DefaultScriptPartHook _defaultScriptPartHook)
            {
                treeNode.Add(new TreeNode($"PartIndex: {_defaultScriptPartHook.PartIndex}"));
            }
            treeNode.AddRange(base.BuildTree());

            return treeNode;
        }
    }
}
