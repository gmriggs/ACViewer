using System.Collections.Generic;

using ACViewer.Entity;

using DatReaderWriter.Enums;

namespace ACViewer.FileTypes
{
    public class GfxObj
    {
        public DatReaderWriter.DBObjs.GfxObj _gfxObj;

        public GfxObj(DatReaderWriter.DBObjs.GfxObj gfxObj)
        {
            _gfxObj = gfxObj;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_gfxObj.Id:X8}");

            var surfaces = new TreeNode("Surfaces");

            foreach (var surface in _gfxObj.Surfaces)
                surfaces.Items.Add(new TreeNode($"{surface:X8}", clickable: true));

            var vertexArray = new TreeNode("VertexArray");
            foreach (var item in new VertexArray(_gfxObj.VertexArray).BuildTree())
                vertexArray.Items.Add(item);

            treeView.Items.AddRange(new List<TreeNode>() { surfaces, vertexArray });

            if (_gfxObj.Flags.HasFlag(GfxObjFlags.HasPhysics))
            {
                var physicsPolygons = new TreeNode("PhysicsPolygons");
                foreach (var kvp in _gfxObj.PhysicsPolygons)
                {
                    var physicsPolygon = new TreeNode($"{kvp.Key}");
                    physicsPolygon.Items.AddRange(new Polygon(kvp.Value).BuildTree());
                    physicsPolygons.Items.Add(physicsPolygon);
                }

                var physicsBSP = new TreeNode("PhysicsBSP");
                if (_gfxObj.PhysicsBSP != null)
                    physicsBSP.Items.AddRange(new BSPTree(_gfxObj.PhysicsBSP).BuildTree(ACE.Entity.Enum.BSPType.Physics).Items);

                treeView.Items.AddRange(new List<TreeNode>() { physicsPolygons, physicsBSP });
            }

            var sortCenter = new TreeNode($"SortCenter: {_gfxObj.SortCenter}");
            treeView.Items.Add(sortCenter);

            if (_gfxObj.Flags.HasFlag(GfxObjFlags.HasDrawing))
            {
                var polygons = new TreeNode("Polygons");
                foreach (var kvp in _gfxObj.Polygons)
                {
                    var polygon = new TreeNode($"{kvp.Key}");
                    polygon.Items.AddRange(new Polygon(kvp.Value).BuildTree());
                    polygons.Items.Add(polygon);
                }

                var drawingBSP = new TreeNode("DrawingBSP");
                if (_gfxObj.DrawingBSP != null)
                    drawingBSP.Items.AddRange(new BSPTree(_gfxObj.DrawingBSP).BuildTree(ACE.Entity.Enum.BSPType.Drawing).Items);

                treeView.Items.AddRange(new List<TreeNode>() { polygons, drawingBSP });
            }

            if (_gfxObj.Flags.HasFlag(GfxObjFlags.HasDIDDegrade))
            {
                var didDegrade = new TreeNode($"DIDDegrade: {_gfxObj.DIDDegrade:X8}", clickable: true);
                treeView.Items.Add(didDegrade);
            }

            return treeView;
        }
    }
}
