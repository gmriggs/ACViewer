using System;

using DatReaderWriter.Types;

namespace ACE.DatLoader.Extensions
{
    public static class SexCGExtensions
    {
        // Eyes
        public static uint GetEyeTexture(this SexCG sex, uint eyesStrip, bool isBald)
        {
            ObjDesc eyes;
            if (isBald)
                eyes = sex.EyeStrips[Convert.ToInt32(eyesStrip)].BaldObjDesc;
            else
                eyes = sex.EyeStrips[Convert.ToInt32(eyesStrip)].ObjDesc;
            return eyes.TextureChanges[0].NewTexture;
        }
        public static uint GetDefaultEyeTexture(this SexCG sex, uint eyesStrip, bool isBald)
        {
            ObjDesc eyes;
            if (isBald)
                eyes = sex.EyeStrips[Convert.ToInt32(eyesStrip)].BaldObjDesc;
            else
                eyes = sex.EyeStrips[Convert.ToInt32(eyesStrip)].ObjDesc;
            return eyes.TextureChanges[0].OldTexture;
        }

        // Nose
        public static uint GetNoseTexture(this SexCG sex, uint noseStrip)
        {
            ObjDesc nose = sex.NoseStrips[Convert.ToInt32(noseStrip)].ObjDesc;
            return nose.TextureChanges[0].NewTexture;
        }
        public static uint GetDefaultNoseTexture(this SexCG sex, uint noseStrip)
        {
            ObjDesc nose = sex.NoseStrips[Convert.ToInt32(noseStrip)].ObjDesc;
            return nose.TextureChanges[0].OldTexture;
        }

        // Mouth
        public static uint GetMouthTexture(this SexCG sex, uint mouthStrip)
        {
            ObjDesc mouth = sex.MouthStrips[Convert.ToInt32(mouthStrip)].ObjDesc;
            return mouth.TextureChanges[0].NewTexture;
        }
        public static uint GetDefaultMouthTexture(this SexCG sex, uint mouthStrip)
        {
            ObjDesc mouth = sex.MouthStrips[Convert.ToInt32(mouthStrip)].ObjDesc;
            return mouth.TextureChanges[0].OldTexture;
        }

        // Hair (Head)
        public static uint? GetHeadObject(this SexCG sex, uint hairStyle)
        {
            HairStyleCG hairstyle = sex.HairStyles[Convert.ToInt32(hairStyle)];

            // Gear Knights, both Olthoi types have multiple anim part changes.
            if (hairstyle.ObjDesc.AnimPartChanges.Count == 1)
                return hairstyle.ObjDesc.AnimPartChanges[0].PartId;
            else
                return null;
        }
        public static uint? GetHairTexture(this SexCG sex, uint hairStyle)
        {
            HairStyleCG hairstyle = sex.HairStyles[Convert.ToInt32(hairStyle)];

            // OlthoiAcid has no TextureChanges
            if (hairstyle.ObjDesc.TextureChanges.Count > 0)
                return hairstyle.ObjDesc.TextureChanges[0].NewTexture;
            else
                return null;

        }
        public static uint? GetDefaultHairTexture(this SexCG sex, uint hairStyle)
        {
            HairStyleCG hairstyle = sex.HairStyles[Convert.ToInt32(hairStyle)];

            // OlthoiAcid has no TextureChanges
            if (hairstyle.ObjDesc.TextureChanges.Count > 0)
                return hairstyle.ObjDesc.TextureChanges[0].OldTexture;
            else
                return null;
        }

        // Headgear
        public static uint GetHeadgearWeenie(this SexCG sex, uint headgearStyle)
        {
            return sex.Headgears[Convert.ToInt32(headgearStyle)].WeenieDefault;
        }
        public static uint GetHeadgearClothingTable(this SexCG sex, uint headgearStyle)
        {
            return sex.Headgears[Convert.ToInt32(headgearStyle)].ClothingTable;
        }

        // Shirt
        public static uint GetShirtWeenie(this SexCG sex, uint shirtStyle)
        {
            return sex.Shirts[Convert.ToInt32(shirtStyle)].WeenieDefault;
        }
        public static uint GetShirtClothingTable(this SexCG sex, uint shirtStyle)
        {
            return sex.Shirts[Convert.ToInt32(shirtStyle)].ClothingTable;
        }

        // Pants
        public static uint GetPantsWeenie(this SexCG sex, uint pantsStyle)
        {
            return sex.Pants[Convert.ToInt32(pantsStyle)].WeenieDefault;
        }
        public static uint GetPantsClothingTable(this SexCG sex, uint pantsStyle)
        {
            return sex.Pants[Convert.ToInt32(pantsStyle)].ClothingTable;
        }

        // Footwear
        public static uint GetFootwearWeenie(this SexCG sex, uint footwearStyle)
        {
            return sex.Footwear[Convert.ToInt32(footwearStyle)].WeenieDefault;
        }
        public static uint GetFootwearClothingTable(this SexCG sex, uint footwearStyle)
        {
            return sex.Footwear[Convert.ToInt32(footwearStyle)].ClothingTable;
        }
    }
}
