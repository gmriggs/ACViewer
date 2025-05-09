using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

using ACE.DatLoader;

namespace ACViewer.Render
{
    public class R_Environment
    {
        public DatReaderWriter.DBObjs.Environment _env { get; set; }

        public Dictionary<uint, R_CellStruct> R_CellStructs { get; set; }

        public R_Environment(uint envID)
        {
            // caching?
            DatManager.PortalDat.TryReadFileCache(envID, out DatReaderWriter.DBObjs.Environment e);
            _env = e;

            BuildEnv();
        }

        public void BuildEnv()
        {
            R_CellStructs = new Dictionary<uint, R_CellStruct>();

            foreach (var kvp in _env.Cells)
                R_CellStructs.Add(kvp.Key, new R_CellStruct(kvp.Value));
        }

        public void Draw(uint? cellStructId = null, List<Texture2D> textures = null)
        {
            if (cellStructId != null)
            {
                // draw EnvCell
                if (R_CellStructs.TryGetValue(cellStructId.Value, out var cellStruct))
                    cellStruct.Draw(textures);
            }
            else
            {
                // draw all the possible cell structs
                foreach (var cellStruct in R_CellStructs.Values)
                    cellStruct.Draw(textures);
            }
        }
    }
}
