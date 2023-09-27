using System.ComponentModel;
using System.IO;

using ACE.Entity.Enum;

using ACViewer.PropertyEditor;

namespace ACE.DatLoader.FileTypes
{
    [TypeConverter(typeof(PropertyExpandableObjectConverter))]
    public class SpellSetTiers : IUnpackable
    {
        /// <summary>
        /// A list of spell ids that are active in the spell set tier
        /// </summary>
        public PropertyList<SpellId> Spells { get; } = new PropertyList<SpellId>();

        public void Unpack(BinaryReader reader)
        {
            Spells._list.Unpack(reader);
        }
    }
}
