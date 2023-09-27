using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace ACViewer.PropertyEditor
{
    public class PropertyCollectionEditor : CollectionEditor
    {
        public PropertyCollectionEditor(Type type) : base(type)
        {
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.None;
        }
    }
}
