using System.Collections.Generic;

using DatReaderWriter.Types;

namespace ACE.DatLoader.Extensions
{
    public static class PolygonExtensions
    {
        public static void LoadVertices(this Polygon p, VertexArray vertexArray)
        {
            /*p.Vertices = new List<SWVertex>();

            foreach (var id in VertexIds)
                Vertices.Add(vertexArray.Vertices[(ushort)id]);*/
        }

        public static List<SWVertex> Vertices(this Polygon p, VertexArray vertexArray)
        {
            var vertices = new List<SWVertex>();

            foreach (var id in p.VertexIds)
                vertices.Add(vertexArray.Vertices[(ushort)id]);

            return vertices;
        }
    }
}
