using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class VertexArray
    {
        public DatReaderWriter.Types.VertexArray _vertexArray;

        public VertexArray(DatReaderWriter.Types.VertexArray vertexArray)
        {
            _vertexArray = vertexArray;
        }

        public List<TreeNode> BuildTree()
        {
            var vertexType = new TreeNode($"VertexType: {_vertexArray.VertexType}");

            var vertices = new TreeNode("Vertices");

            foreach (var kvp in _vertexArray.Vertices)
            {
                var vertex = new TreeNode($"{kvp.Key}");

                foreach (var item in new Vertex(kvp.Value).BuildTree())
                    vertex.Items.Add(item);

                vertices.Items.Add(vertex);
            }
            return new List<TreeNode>() { vertexType, vertices };
        }
    }
}
