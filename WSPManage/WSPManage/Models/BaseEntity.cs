using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WSPManage.Models
{
    public class BaseEntity
    {
        [DataType(DataType.Date)]
        public DateTime? DateCreated { get; set; }

        public string UserCreated { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateModified { get; set; }

        public string UserModified { get; set; }
    }
}