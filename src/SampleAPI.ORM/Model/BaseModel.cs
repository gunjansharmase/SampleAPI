using System;

namespace SampleAPI.ORM.Model
{
    public abstract class BaseModel
    {
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public bool Enabled { get; set; }
    }
}
