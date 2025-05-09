using System.Collections.Generic;
using System.Linq;

using ACE.Entity.Enum;

namespace ACViewer.Entity
{
    public class SexCG
    {
        public DatReaderWriter.Types.SexCG _sex;

        public SexCG(DatReaderWriter.Types.SexCG sex)
        {
            _sex = sex;
        }

        public List<TreeNode> BuildTree()
        {
            var name = new TreeNode($"Name: {_sex.Name}");
            var scale = new TreeNode($"Scale: {_sex.Scale}%");
            var setup = new TreeNode($"Setup: {_sex.SetupId:X8}", clickable: true);
            var soundTable = new TreeNode($"SoundTable: {_sex.SoundTable:X8}", clickable: true);
            var icon = new TreeNode($"Icon: {_sex.IconId:X8}", clickable: true);
            var basePalette = new TreeNode($"Base Palette: {_sex.BasePalette:X8}", clickable: true);
            var skinPaletteSet = new TreeNode($"Skin Palette Set: {_sex.SkinPalSet:X8}", clickable: true);
            var physicsTable = new TreeNode($"Physics Table: {_sex.PhysicsTable:X8}", clickable: true);
            var motionTable = new TreeNode($"Motion Table: {_sex.MotionTable:X8}", clickable: true);
            var combatTable = new TreeNode($"Combat Table: {_sex.CombatTable:X8}", clickable: true);
            var baseObjDesc = new TreeNode("ObjDesc:");
            baseObjDesc.Items.AddRange(new ObjDesc(_sex.BaseObjDesc).BuildTree());

            var hairColors = new TreeNode($"Hair Colors:");
            foreach (var hairColor in _sex.HairColors)
                hairColors.Items.Add(new TreeNode($"{hairColor:X8}", clickable: true));

            var hairStyles = new TreeNode($"Hair Styles:");
            for (var i = 0; i < _sex.HairStyles.Count; i++)
            {
                var hairStyle = new TreeNode($"{i}");
                hairStyle.Items.AddRange(new HairStyleCG(_sex.HairStyles[i]).BuildTree());
                hairStyles.Items.Add(hairStyle);
            }

            var eyeColors = new TreeNode($"Eye Colors:");
            foreach (var eyeColor in _sex.EyeColors)
                eyeColors.Items.Add(new TreeNode($"{eyeColor:X8}", clickable: true));

            var eyeStrips = new TreeNode($"Eye Strips:");
            for (var i = 0; i < _sex.EyeStrips.Count; i++)
            {
                var eyeStrip = new TreeNode($"{i}");
                eyeStrip.Items.AddRange(new EyeStripCG(_sex.EyeStrips[i]).BuildTree());
                eyeStrips.Items.Add(eyeStrip);
            }

            var noseStrips = new TreeNode($"Nose Strips:");
            for (var i = 0; i < _sex.NoseStrips.Count; i++)
            {
                var noseStrip = new TreeNode($"{i}");
                noseStrip.Items.AddRange(new FaceStripCG(_sex.NoseStrips[i]).BuildTree());
                noseStrips.Items.Add(noseStrip);
            }

            var mouthStrips = new TreeNode($"Mouth Strips:");
            for (var i = 0; i < _sex.MouthStrips.Count; i++)
            {
                var mouthStrip = new TreeNode($"{i}");
                mouthStrip.Items.AddRange(new FaceStripCG(_sex.MouthStrips[i]).BuildTree());
                mouthStrips.Items.Add(mouthStrip);
            }

            var headgear = new TreeNode($"Headgear:");
            for (var i = 0; i < _sex.Headgears.Count; i++)
            {
                var headgearNode = new TreeNode($"{i}");
                headgearNode.Items.AddRange(new GearCG(_sex.Headgears[i]).BuildTree());
                headgear.Items.Add(headgearNode);
            }

            var shirts = new TreeNode($"Shirt:");
            for (var i = 0; i < _sex.Shirts.Count; i++)
            {
                var shirt = new TreeNode($"{i}");
                shirt.Items.AddRange(new GearCG(_sex.Shirts[i]).BuildTree());
                shirts.Items.Add(shirt);
            }

            var pants = new TreeNode($"Pants:");
            for (var i = 0; i < _sex.Pants.Count; i++)
            {
                var pantsNode = new TreeNode($"{i}");
                pantsNode.Items.AddRange(new GearCG(_sex.Pants[i]).BuildTree());
                pants.Items.Add(pantsNode);
            }

            var footwear = new TreeNode($"Footwear:");
            for (var i = 0; i < _sex.Footwear.Count; i++)
            {
                var footwearNode = new TreeNode($"{i}");
                footwearNode.Items.AddRange(new GearCG(_sex.Footwear[i]).BuildTree());
                footwear.Items.Add(footwearNode);
            }

            var clothingColors = new TreeNode($"Clothing Colors:");

            foreach (var clothingColor in _sex.ClothingColors.OrderBy(i => i))
                clothingColors.Items.Add(new TreeNode($"{clothingColor} - {(PaletteTemplate)clothingColor}"));

            return new List<TreeNode>() { name, scale, setup, soundTable, icon, basePalette, skinPaletteSet, physicsTable, motionTable, combatTable,
                baseObjDesc, hairColors, hairStyles, eyeColors, eyeStrips, noseStrips, mouthStrips, headgear, shirts, pants, footwear, clothingColors };
        }
    }
}
