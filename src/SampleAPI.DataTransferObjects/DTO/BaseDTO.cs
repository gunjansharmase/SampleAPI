using System;
using System.Collections.Generic;
using System.Text;

namespace SampleAPI.DataTransferObjects.DTO.Packaging
{
    public abstract class BaseDto
    {
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public bool Enabled { get; set; }
    }
}
