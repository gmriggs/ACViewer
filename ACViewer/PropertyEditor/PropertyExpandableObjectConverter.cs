using System;
using System.ComponentModel;
using System.Globalization;

namespace ACViewer.PropertyEditor
{
    public class PropertyExpandableObjectConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
        {
            return null;
        }
    }
}
