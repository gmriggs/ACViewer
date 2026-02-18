using System;
using System.Collections.Generic;
using System.Numerics;

using Microsoft.Xna.Framework.Graphics;

namespace ACViewer.Model
{
    public class Polygon
    {
        public GfxObj GfxObj { get; set; }

        public ACE.DatLoader.Entity.Polygon _polygon { get; set; }

        public Dictionary<Tuple<ushort, ushort>, ushort> UVLookup { get; set; }

        public Texture2D Texture { get; set; }

        public List<ushort> Indices { get; set; }

        public IndexBuffer IndexBuffer { get; set; }

        public Polygon(GfxObj gfxObj, ACE.DatLoader.Entity.Polygon polygon)
        {
            GfxObj = gfxObj;
            _polygon = polygon;

            UVLookup = GfxObj.UVLookup;

            BuildIndices();
            //BuildIndexBuffer();

            Texture = gfxObj.Textures[polygon.PosSurface];
        }

        public Polygon(ACE.DatLoader.Entity.Polygon polygon, Dictionary<Tuple<ushort, ushort>, ushort> uvLookup)
        {
            _polygon = polygon;
            UVLookup = uvLookup;

            BuildIndices();

            BuildIndexBuffer();
        }

        public void BuildIndices()
        {
            Indices = new List<ushort>();

            ushort firstIdx = 0;
            ushort lastIdx = 0;

            for (var i = 0; i < _polygon.VertexIds.Count; i++)
            {
                var vertID = _polygon.VertexIds[i];
                ushort uvIdx = 0;
                if (_polygon.PosUVIndices != null && i < _polygon.PosUVIndices.Count)
                {
                    uvIdx = _polygon.PosUVIndices[i];
                }
                var key = new Tuple<ushort, ushort>((ushort)vertID, uvIdx);
                if (!UVLookup.TryGetValue(key, out var idx))
                {
                    Console.WriteLine($"Couldn't find UV for {GfxObj._gfxObj.Id:X8} key {key}");
                    continue;
                }
                if (i == 0)
                {
                    firstIdx = idx;
                }
                else if (i > 2)
                {
                    // make triangle fan
                    Indices.Add(firstIdx);
                    Indices.Add(lastIdx);
                }
                lastIdx = idx;
                Indices.Add(idx);
            }

            //Console.WriteLine($"Poly verts: {_polygon.VertexIds.Count} ({IndexArray.Count})");
        }

        public void BuildIndexBuffer()
        {
            IndexBuffer = new IndexBuffer(GfxObj.GraphicsDevice, typeof(short), Indices.Count, BufferUsage.WriteOnly);
            IndexBuffer.SetData(Indices.ToArray());
        }

        private Vector3 CalculateNormal()
        {
            var v0 = _polygon.Vertices[0].Origin;

            for (int i = 1; i < _polygon.Vertices.Count - 1; i++)
            {
                var v1 = _polygon.Vertices[i].Origin;
                var v2 = _polygon.Vertices[i + 1].Origin;

                var edge1 = v1 - v0;
                var edge2 = v2 - v0;

                var normal = Vector3.Cross(edge1, edge2);

                if (normal.LengthSquared() >= 0.0001f)
                    return Vector3.Normalize(normal);
            }

            return Vector3.Zero;
        }

        private Vector3 CalculatePolyCenter()
        {
            Vector3 center = Vector3.Zero;

            foreach (var vertex in _polygon.Vertices)
                center += vertex.Origin;

            return center / _polygon.Vertices.Count;
        }

        private VertexPositionColor[] _cachedNormalVerts;
        private const float normalScale = 0.25f;

        public VertexPositionColor[] GetNormalLineVerts()
        {
            if (_cachedNormalVerts != null)
                return _cachedNormalVerts;

            var centerPoint = CalculatePolyCenter();
            var normal = CalculateNormal();
            var endPoint = centerPoint + (normal * normalScale);

            _cachedNormalVerts =
            [
                new VertexPositionColor(centerPoint.ToXna(), Microsoft.Xna.Framework.Color.White),
                new VertexPositionColor(endPoint.ToXna(), Microsoft.Xna.Framework.Color.White)
            ];

            return _cachedNormalVerts;
        }
    }
}
