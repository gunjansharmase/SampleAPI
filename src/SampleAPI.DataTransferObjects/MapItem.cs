using System;
using System.Collections.Generic;
using System.Text;
using SampleAPI.DataTransferObjects.Enum;

namespace SampleAPI.DataTransferObjects
{
    public class MapItem
    {
        public Type Type { get; private set; }
        public DataRetrieveTypeEnum DataRetrieveType { get; private set; }
        public string PropertyName { get; private set; }

        public MapItem(Type type, DataRetrieveTypeEnum dataRetrieveType, string propertyName)
        {
            Type = type;
            DataRetrieveType = dataRetrieveType;
            PropertyName = propertyName;
        }
    }
}
