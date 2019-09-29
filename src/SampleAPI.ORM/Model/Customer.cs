using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleAPI.ORM.Model.Packaging
{
    public class Customer : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CUST_ID { get; set; }
        public string ADDRESS { get; set; }
        public string CITY { get; set; }
        public string CUST_TYPE_CD { get; set; }
        public string FED_ID { get; set; }
        public string POSTAL_CODE { get; set; }
        public string STATE { get; set; }
    }
}
