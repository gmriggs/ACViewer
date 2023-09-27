using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

namespace ACViewer.PropertyEditor
{
    [TypeConverter(typeof(PropertyExpandableObjectConverter))]
    [EditorAttribute(typeof(PropertyCollectionEditor), typeof(UITypeEditor))]
    public class PropertyList<T> : ICustomTypeDescriptor
    {
        public List<T> _list;

        public PropertyList()
        {
            _list = new List<T>();
        }

        public PropertyList(List<T> list)
        {
            _list = list;
        }

        public void Add(T item)
        {
            _list.Add(item);
        }

        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        public T this[int idx]
        {
            get
            {
                return _list[idx];
            }
            set
            {
                _list[idx] = value;
            }
        }
        
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public System.ComponentModel.PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            //return TypeDescriptor.GetEditor(this, editorBaseType, true);
            return null;
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            var descs = new PropertyDescriptorCollection(null);

            for (int i = 0; i < _list.Count; i++)
                descs.Add(new PropertyDescriptor(this, i));

            return descs;
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public object GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd)
        {
            return this;
        }

        // ---------------

        public class PropertyDescriptor : System.ComponentModel.PropertyDescriptor
        {
            private PropertyList<T> list = null;
            private int idx = -1;

            public PropertyDescriptor(PropertyList<T> _list, int _idx) :
                base(_idx.ToString(), null)
            {
                list = _list;
                idx = _idx;
            }

            public override AttributeCollection Attributes => new AttributeCollection(null);

            public override bool CanResetValue(object component)
            {
                return true;
            }

            public override Type ComponentType => list.GetType();

            public override string DisplayName
            {
                get
                {
                    return idx.ToString();
                }
            }

            public override string Description
            {
                get
                {
                    return list[idx].ToString();
                }
            }

            public override object GetValue(object component)
            {
                return list[idx];
            }

            public override bool IsReadOnly => false;

            public override string Name
            {
                get
                {
                    return idx.ToString();
                }
            }

            public override Type PropertyType => list[idx].GetType();

            public override void ResetValue(object component)
            {
            }

            public override bool ShouldSerializeValue(object component)
            {
                return true;
            }

            public override void SetValue(object component, object value)
            {
                list[idx] = (T)value;
            }
        }
    }
}
