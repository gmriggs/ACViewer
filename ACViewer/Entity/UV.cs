namespace ACViewer.Entity
{
    public class UV
    {
        public DatReaderWriter.Types.Vec2Duv _uv;

        public UV(DatReaderWriter.Types.Vec2Duv uv)
        {
            _uv = uv;
        }

        public TreeNode BuildTree()
        {
            return new TreeNode($"U: {_uv.U} V: {_uv.V}");
        }
    }
}
