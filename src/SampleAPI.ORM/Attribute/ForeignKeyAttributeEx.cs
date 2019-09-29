using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleAPI.ORM.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKeyAttributeEx : ForeignKeyAttribute
    {
        protected readonly Type _referenceType;
        protected readonly bool _isNullable;

        public ForeignKeyAttributeEx(string name, Type referenceType) : this(name, referenceType, false)
        {
        }

        public ForeignKeyAttributeEx(string name, Type referenceType, bool isNullable) : base(name)
        {
            _referenceType = referenceType;
            _isNullable = isNullable;
        }

        public Type ReferenceType => _referenceType;

        public bool IsNullable => _isNullable;
    }
}
