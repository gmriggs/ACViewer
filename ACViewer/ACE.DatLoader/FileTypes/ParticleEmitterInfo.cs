using System;
using System.IO;
using System.Numerics;
using System.Text;
using ACE.Entity.Enum;

namespace ACE.DatLoader.FileTypes
{
    /// <summary>
    /// These are client_portal.dat files starting with 0x32. 
    /// </summary>
    [DatFileType(DatFileType.ParticleEmitter)]
    public class ParticleEmitterInfo : FileType
    {
        public EmitterType EmitterType { get; set; }
        public ParticleType ParticleType { get; set; }
        public uint GfxObjId { get; set; }
        public uint HwGfxObjId { get; set; }
        public double Birthrate { get; set; }
        public int MaxParticles { get; set; }
        public int InitialParticles { get; set; }
        public int TotalParticles { get; set; }
        public double TotalSeconds { get; set; }
        public double Lifespan { get; set; }
        public double LifespanRand { get; set; }
        public Vector3 OffsetDir { get; set; }
        public float MinOffset { get; set; }
        public float MaxOffset { get; set; }
        public Vector3 A { get; set; }
        public float MinA { get; set; }
        public float MaxA { get; set; }
        public Vector3 B { get; set; }
        public float MinB { get; set; }
        public float MaxB { get; set; }
        public Vector3 C { get; set; }
        public float MinC { get; set; }
        public float MaxC { get; set; }
        public float StartScale { get; set; }
        public float FinalScale { get; set; }
        public float ScaleRand { get; set; }
        public float StartTrans { get; set; }
        public float FinalTrans { get; set; }
        public float TransRand { get; set; }
        public int IsParentLocal { get; set; }

        public override void Unpack(BinaryReader reader)
        {
            Id = reader.ReadUInt32();

            /*uint unknown = */reader.ReadUInt32();

            EmitterType  =  (EmitterType)reader.ReadInt32();
            ParticleType = (ParticleType)reader.ReadInt32();

            GfxObjId   = reader.ReadUInt32();
            HwGfxObjId = reader.ReadUInt32();

            Birthrate   = reader.ReadDouble();

            MaxParticles  = reader.ReadInt32();
            InitialParticles = reader.ReadInt32();

            TotalParticles = reader.ReadInt32();

            TotalSeconds  = reader.ReadDouble();

            Lifespan     = reader.ReadDouble();
            LifespanRand = reader.ReadDouble();

            OffsetDir = reader.ReadVector3();      
            MinOffset = reader.ReadSingle();
            MaxOffset = reader.ReadSingle(); 

            A = reader.ReadVector3();
            MinA = reader.ReadSingle();
            MaxA = reader.ReadSingle();

            B = reader.ReadVector3();
            MinB = reader.ReadSingle();
            MaxB = reader.ReadSingle();

            C = reader.ReadVector3();
            MinC = reader.ReadSingle();
            MaxC = reader.ReadSingle();

            StartScale = reader.ReadSingle();
            FinalScale = reader.ReadSingle();
            ScaleRand  = reader.ReadSingle();

            StartTrans = reader.ReadSingle();
            FinalTrans = reader.ReadSingle();
            TransRand  = reader.ReadSingle();

            IsParentLocal = reader.ReadInt32();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("------------------");
            sb.AppendLine($"ID: {Id:X8}");
            sb.AppendLine("EmitterType: " + EmitterType);
            sb.AppendLine("ParticleType: " + ParticleType);
            sb.AppendLine($"GfxObjID: {GfxObjId:X8}");
            sb.AppendLine($"HWGfxObjID: {HwGfxObjId:X8}");
            sb.AppendLine("Birthrate: " + Birthrate);
            sb.AppendLine("MaxParticles: " + MaxParticles);
            sb.AppendLine("InitialParticles: " + InitialParticles);
            sb.AppendLine("TotalParticles: " + TotalParticles);
            sb.AppendLine("TotalSeconds: " + TotalSeconds);
            sb.AppendLine("Lifespan: " + Lifespan);
            sb.AppendLine("LifespanRand: " + LifespanRand);
            sb.AppendLine("OffsetDir: " + OffsetDir);
            sb.AppendLine("MinOffset: " + MinOffset);
            sb.AppendLine("MaxOffset: " + MaxOffset);
            sb.AppendLine("A: " + A);
            sb.AppendLine("MinA: " + MinA);
            sb.AppendLine("MaxA: " + MaxA);
            sb.AppendLine("B: " + B);
            sb.AppendLine("MinB: " + MinB);
            sb.AppendLine("MaxB: " + MaxB);
            sb.AppendLine("C: " + C);
            sb.AppendLine("MinC: " + MinC);
            sb.AppendLine("MaxC: " + MaxC);
            sb.AppendLine("StartScale: " + StartScale);
            sb.AppendLine("FinalScale: " + FinalScale);
            sb.AppendLine("ScaleRand: " + ScaleRand);
            sb.AppendLine("StartTrans: " + StartTrans);
            sb.AppendLine("FinalTrans: " + FinalTrans);
            sb.AppendLine("TransRand: " + TransRand);
            sb.AppendLine("IsParentLocal: " + IsParentLocal);

            return sb.ToString();
        }
    }
}
