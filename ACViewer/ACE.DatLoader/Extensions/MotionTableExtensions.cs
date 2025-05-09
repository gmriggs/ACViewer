using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;

using ACE.Entity;

using DatReaderWriter.DBObjs;
using DatReaderWriter.Enums;
using DatReaderWriter.Types;

namespace ACE.DatLoader.Extensions
{
    public static class MotionTableExtensions
    {
        /// <summary>
        /// Gets the default style for the requested MotionStance
        /// </summary>
        /// <returns>The default style or MotionCommand.Invalid if not found</returns>
        private static MotionCommand GetDefaultMotion(this MotionTable motionTable, MotionCommand style)
        {
            if (motionTable.StyleDefaults.ContainsKey(style))
                return motionTable.StyleDefaults[style];

            return MotionCommand.Invalid;
        }

        public static float GetAnimationLength(this MotionTable motionTable, MotionCommand motion)
        {
            var defaultStance = motionTable.DefaultStyle;
            var defaultMotion = motionTable.GetDefaultMotion(defaultStance);

            return motionTable.GetAnimationLength(defaultStance, motion, defaultMotion);
        }

        public static float GetAnimationLength(this MotionTable motionTable, MotionCommand stance, MotionCommand motion, MotionCommand? currentMotion = null)
        {
            if (currentMotion == null)
                currentMotion = motionTable.GetDefaultMotion(stance);

            return motionTable.GetAnimationLength(stance, motion, currentMotion.Value);
        }

        public static float GetCycleLength(this MotionTable motionTable, MotionCommand stance, MotionCommand motion)
        {
            int key = (int)stance << 16 | (int)motion & 0xFFFFF;

            motionTable.Cycles.TryGetValue(key, out var motionData);

            if (motionData == null)
                return 0.0f;

            var length = 0.0f;
            foreach (var anim in motionData.Anims)
                length += motionTable.GetAnimationLength(anim);

            return length;
        }

        private static readonly ConcurrentDictionary<AttackFrameParams, List<(float time, AttackHook attackHook)>> attackFrameCache = new ConcurrentDictionary<AttackFrameParams, List<(float time, AttackHook attackHook)>>();

        public static List<(float time, AttackHook attackHook)> GetAttackFrames(this MotionTable motionTable, uint motionTableId, MotionCommand stance, MotionCommand motion)
        {
            // could also do uint, and then a packed ulong, but would be more complicated maybe?
            var attackFrameParams = new AttackFrameParams(motionTableId, (ACE.Entity.Enum.MotionStance)stance, (ACE.Entity.Enum.MotionCommand)motion);
            if (attackFrameCache.TryGetValue(attackFrameParams, out var attackFrames))
                return attackFrames;

            var defaultMotion = motionTable.GetDefaultMotion(stance);

            var animData = motionTable.GetAnimData(stance, motion, defaultMotion);

            var frameNums = new List<int>();
            var attackHooks = new List<AttackHook>();
            var totalFrames = 0;

            foreach (var anim in animData)
            {
                DatManager.PortalDat.TryReadFileCache(anim.AnimId, out Animation animation);

                foreach (var frame in animation.PartFrames)
                {
                    foreach (var hook in frame.Hooks)
                    {
                        if (hook is AttackHook attackHook)
                        {
                            frameNums.Add(totalFrames);
                            attackHooks.Add(attackHook);
                        }
                    }
                    totalFrames++;
                }
            }
            attackFrames = new List<(float time, AttackHook attackHook)>();
            for (var i = 0; i < frameNums.Count; i++)
                attackFrames.Add(((float)frameNums[i] / totalFrames, attackHooks[i]));    // div 0?

            attackFrameCache.TryAdd(attackFrameParams, attackFrames);

            return attackFrames;
        }

        public static List<DatReaderWriter.Types.AnimData> GetAnimData(this MotionTable motionTable, MotionCommand stance, MotionCommand motion, MotionCommand currentMotion)
        {
            var animData = new List<DatReaderWriter.Types.AnimData>();

            int motionHash = (int)stance << 16 | (int)currentMotion & 0xFFFFF;

            motionTable.Links.TryGetValue(motionHash, out var link);
            if (link == null) return animData;

            link.MotionData.TryGetValue((int)motion, out var motionData);
            if (motionData == null)
            {
                motionHash = (int)stance << 16;
                motionTable.Links.TryGetValue(motionHash, out link);
                if (link == null) return animData;
                link.MotionData.TryGetValue((int)motion, out motionData);
                if (motionData == null) return animData;
            }
            return motionData.Anims;
        }

        public static float GetAnimationLength(this MotionTable motionTable, MotionCommand stance, MotionCommand motion, MotionCommand currentMotion)
        {
            var animData = motionTable.GetAnimData(stance, motion, currentMotion);

            var length = 0.0f;
            foreach (var anim in animData)
                length += motionTable.GetAnimationLength(anim);

            return length;
        }

        public static float GetAnimationLength(this MotionTable motionTable, DatReaderWriter.Types.AnimData anim)
        {
            var highFrame = anim.HighFrame;

            // get the maximum # of animation frames
            DatManager.PortalDat.TryReadFileCache(anim.AnimId, out Animation animation);

            if (anim.HighFrame == -1)
                highFrame = animation.PosFrames.Count;

            if (highFrame > animation.PosFrames.Count)
            {
                // magic windup for level 6 spells appears to be the only animation w/ bugged data
                //Console.WriteLine($"MotionTable.GetAnimationLength({anim}): highFrame({highFrame}) > animation.NumFrames({animation.NumFrames})");
                highFrame = animation.PosFrames.Count;
            }

            var numFrames = highFrame - anim.LowFrame;

            return numFrames / Math.Abs(anim.Framerate); // framerates can be negative, which tells the client to play in reverse
        }

        public static ACE.Entity.Position GetAnimationFinalPositionFromStart(this MotionTable motionTable, ACE.Entity.Position position, float objScale, MotionCommand motion)
        {
            var defaultStyle = motionTable.DefaultStyle;

            // get the default motion for the default
            MotionCommand defaultMotion = motionTable.GetDefaultMotion(defaultStyle);
            return motionTable.GetAnimationFinalPositionFromStart(position, objScale, defaultMotion, defaultStyle, motion);
        }

        public static ACE.Entity.Position GetAnimationFinalPositionFromStart(this MotionTable motionTable, ACE.Entity.Position position, float objScale, MotionCommand currentMotionState, MotionCommand style, MotionCommand motion)
        {
            float length = 0; // init our length var...will return as 0 if not found

            ACE.Entity.Position finalPosition = new ACE.Entity.Position();

            int motionHash = ((int)currentMotionState & 0xFFFFFF) | ((int)style << 16);

            if (motionTable.Links.ContainsKey(motionHash))
            {
                Dictionary<int, MotionData> links = motionTable.Links[motionHash].MotionData;

                if (links.ContainsKey((int)motion))
                {
                    // loop through all that animations to get our total count
                    for (int i = 0; i < links[(int)motion].Anims.Count; i++)
                    {
                        DatReaderWriter.Types.AnimData anim = links[(int)motion].Anims[i];

                        int numFrames;

                        // check if the animation is set to play the whole thing, in which case we need to get the numbers of frames in the raw animation
                        if ((anim.LowFrame == 0) && (anim.HighFrame == -1))
                        {
                            DatManager.PortalDat.TryReadFileCache(anim.AnimId, out Animation animation);
                            numFrames = animation.PosFrames.Count;

                            if (animation.PosFrames.Count > 0)
                            {
                                finalPosition = position;
                                var origin = new Vector3(position.PositionX, position.PositionY, position.PositionZ);
                                var orientation = new Quaternion(position.RotationX, position.RotationY, position.RotationZ, position.RotationW);
                                foreach (var posFrame in animation.PosFrames)
                                {
                                    origin += Vector3.Transform(posFrame.Origin, orientation) * objScale;

                                    orientation *= posFrame.Orientation;
                                    orientation = Quaternion.Normalize(orientation);
                                }

                                finalPosition.PositionX = origin.X;
                                finalPosition.PositionY = origin.Y;
                                finalPosition.PositionZ = origin.Z;

                                finalPosition.RotationW = orientation.W;
                                finalPosition.RotationX = orientation.X;
                                finalPosition.RotationY = orientation.Y;
                                finalPosition.RotationZ = orientation.Z;
                            }
                            else
                                return position;
                        }
                        else
                            numFrames = anim.HighFrame - anim.LowFrame;

                        length += numFrames / Math.Abs(anim.Framerate); // Framerates can be negative, which tells the client to play in reverse
                    }
                }
            }

            return finalPosition;
        }
    }
}
