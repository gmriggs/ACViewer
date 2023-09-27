using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ACE.DatLoader.Entity
{
    public class Attribute2ndBase : IUnpackable
    {
        public SkillFormula Formula { get; set; } = new SkillFormula();

        public void Unpack(BinaryReader reader)
        {
            Formula.Unpack(reader);
        }
    }
}
