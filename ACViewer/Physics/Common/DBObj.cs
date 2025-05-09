using System;

using ACE.DatLoader;
using ACE.DatLoader.FileTypes;
using DatReaderWriter.DBObjs;

namespace ACE.Server.Physics.Common
{
    public class DBObj
    {
        public static Object Get(QualifiedDataID qualifiedDID)
        {
            // TODO: map to ACE datloaders
            // return static or mutable?

            if (qualifiedDID.Type == 1)
                return GetCellLandblock(qualifiedDID.ID);

            if (qualifiedDID.Type == 2)
                return GetLandblockInfo(qualifiedDID.ID);

            if (qualifiedDID.Type == 3)
                return GetEnvCell(qualifiedDID.ID);

            if (qualifiedDID.Type == 6)
                return GetGfxObj(qualifiedDID.ID);

            if (qualifiedDID.Type == 7)
                return GetSetup(qualifiedDID.ID);

            if (qualifiedDID.Type == 8)
                return GetAnimation(qualifiedDID.ID);

            if (qualifiedDID.Type == 11)
                return GetSurfaceTexture(qualifiedDID.ID);

            if (qualifiedDID.Type == 16)
                return GetEnvironment(qualifiedDID.ID);

            if (qualifiedDID.Type == 42)
                return GetParticleEmitterInfo(qualifiedDID.ID);

            return -1;
        }

        /// <summary>
        /// QualifiedDID Type 1
        /// </summary>
        public static LandBlock GetCellLandblock(uint id)
        {
            DatManager.CellDat.TryReadFileCache(id, out LandBlock landBlock);
            return landBlock;
        }

        /// <summary>
        /// QualifiedDID Type 2
        /// </summary>
        public static LandBlockInfo GetLandblockInfo(uint id)
        {
            DatManager.CellDat.TryReadFileCache(id, out LandBlockInfo lbInfo);
            return lbInfo;
        }

        /// <summary>
        /// QualifiedDID Type 3
        /// </summary>
        public static EnvCell GetEnvCell(uint id)
        {
            DatManager.CellDat.TryReadFileCache(id, out DatReaderWriter.DBObjs.EnvCell envCell);
            return new EnvCell(envCell);
        }

        /// <summary>
        /// QualifiedDID Type 6
        /// </summary>
        public static GfxObj GetGfxObj(uint id)
        {
            DatManager.PortalDat.TryReadFileCache(id, out GfxObj gfxObj);
            return gfxObj;
        }

        /// <summary>
        /// QualifiedDID Type 7
        /// </summary>
        public static DatReaderWriter.DBObjs.Setup GetSetup(uint id)
        {
            DatManager.PortalDat.TryReadFileCache(id, out DatReaderWriter.DBObjs.Setup setup);
            return setup;
        }

        /// <summary>
        /// QualifiedDID Type 8
        /// </summary>
        public static DatReaderWriter.DBObjs.Animation GetAnimation(uint id)
        {
            DatManager.PortalDat.TryReadFileCache(id, out DatReaderWriter.DBObjs.Animation anim);
            return anim;
        }

        /// <summary>
        /// QualifiedDID Type 11
        /// </summary>
        public static SurfaceTexture GetSurfaceTexture(uint id)
        {
            DatManager.PortalDat.TryReadFileCache(id, out SurfaceTexture st);
            return st;
        }

        /// <summary>
        /// QualifiedDID Type 16
        /// </summary>
        public static DatReaderWriter.DBObjs.Environment GetEnvironment(uint id)
        {
            DatManager.PortalDat.TryReadFileCache(id, out DatReaderWriter.DBObjs.Environment env);
            return env;
        }

        /// <summary>
        /// QualifiedDID Type 42
        /// </summary>
        public static DatReaderWriter.DBObjs.ParticleEmitter GetParticleEmitterInfo(uint id)
        {
            DatManager.PortalDat.TryReadFileCache(id, out DatReaderWriter.DBObjs.ParticleEmitter emitter);
            return emitter;
        }
    }
}
