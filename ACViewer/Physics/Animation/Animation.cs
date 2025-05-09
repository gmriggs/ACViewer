using System.Collections.Generic;

using DatReaderWriter.Types;

namespace ACE.Server.Physics.Animation
{
    public class Animation
    {
        public uint ID;
        public List<AFrame> PosFrames;
        public List<AnimationFrame> PartFrames;
        public bool HasHooks;
        public uint NumParts;
        public uint NumFrames;

        public Animation() { }

        public Animation(DatReaderWriter.DBObjs.Animation anim)
        {
            ID = anim.Id;
            HasHooks = false;   // comes from bitfield?
            NumParts = anim.NumParts;
            NumFrames = (uint)anim.PosFrames.Count;
            PartFrames = anim.PartFrames;
            PosFrames = new List<AFrame>();
            foreach (var posFrame in anim.PosFrames)
                PosFrames.Add(new AFrame(posFrame));
        }
    }
}
