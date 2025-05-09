using System.Collections.Generic;
using System.Numerics;
using ACE.DatLoader;
using ACE.DatLoader.FileTypes;
using ACE.Server.Physics.Collision;

namespace ACE.Server.Physics.Util
{
    public class Environment
    {
        public DatReaderWriter.DBObjs.EnvCell EnvCell;
        public List<DatReaderWriter.DBObjs.Environment> Environments = new List<DatReaderWriter.DBObjs.Environment>();

        public List<DatReaderWriter.Types.Polygon> Polygons = new List<DatReaderWriter.Types.Polygon>();

        public List<int> PolyOffsets = new List<int>();
        public int TotalVertices;

        public BBox BBox;

        public Environment(DatReaderWriter.DBObjs.EnvCell envCell)
        {
            EnvCell = envCell;
            LoadEnv((uint)0x0D000000 | envCell.EnvironmentId);
        }

        public void LoadEnv(uint envID)
        {
            DatManager.PortalDat.TryReadFileCache(envID, out DatReaderWriter.DBObjs.Environment env);

            var cellOffset = 0;
            foreach (var cell in env.Cells.Values)
            {
                foreach (var poly in cell.Polygons.Values)
                {
                    PolyOffsets.Add(cellOffset);
                    //poly.LoadVertices(cell.VertexArray);  // TODO: fixme
                    Polygons.Add(poly);
                }
                cellOffset += cell.VertexArray.Vertices.Count;
                TotalVertices += cell.VertexArray.Vertices.Count;
            }
            Environments.Add(env);

            BuildBBox();
        }

        public void BuildBBox()
        {
            var origin = EnvCell.Position.Origin;
            var orientation = EnvCell.Position.Orientation;
            var translate = Matrix4x4.CreateTranslation(origin);
            var rotate = Matrix4x4.CreateFromQuaternion(new Quaternion(orientation.X, orientation.Y, orientation.Z, orientation.W));
            BBox = new BBox(Polygons, rotate * translate);
        }
    }
}
