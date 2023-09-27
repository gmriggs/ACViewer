using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

namespace ACViewer.PropertyEditor
{
    [TypeConverter(typeof(PropertyExpandableObjectConverter))]
    [EditorAttribute(typeof(PropertyCollectionEditor), typeof(UITypeEditor))]
    public class PropertyDictionary<T, U> : ICustomTypeDescriptor
    {
        public Dictionary<T, U> _dict;

        public PropertyDictionary()
        {
            _dict = new Dictionary<T, U>();
        }
        
        public PropertyDictionary(Dictionary<T, U> dict)
        {
            _dict = dict;
        }

        public void Add(T key, U val)
        {
            _dict.Add(key, val);
        }

        public bool Remove(T key)
        {
            return _dict.Remove(key);
        }

        public U this[T key]
        {
            get
            {
                return _dict[key];
            }
            set
            {
                _dict[key] = value;
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

            foreach (var key in _dict.Keys)
                descs.Add(new PropertyDescriptor(this, key));

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
            private PropertyDictionary<T, U> dict = null;
            private T key;

            public PropertyDescriptor(PropertyDictionary<T, U> _dict, T _key) :
                base(_key.ToString(), null)
            {
                dict = _dict;
                key = _key;
            }

            public override AttributeCollection Attributes => new AttributeCollection(null);

            public override bool CanResetValue(object component)
            {
                return true;
            }

            public override Type ComponentType => dict.GetType();

            public override string DisplayName
            {
                get
                {
                    return key.ToString();
                }
            }

            public override string Description
            {
                get
                {
                    return dict[key].ToString();
                }
            }

            public override object GetValue(object component)
            {
                return dict[key];
            }

            public override bool IsReadOnly => false;

            public override string Name
            {
                get
                {
                    return key.ToString();
                }
            }

            public override Type PropertyType => dict[key].GetType();

            public override void ResetValue(object component)
            {
            }

            public override bool ShouldSerializeValue(object component)
            {
                return true;
            }

            public override void SetValue(object component, object value)
            {
                dict[key] = (U)value;
            }
        }
    }
}
