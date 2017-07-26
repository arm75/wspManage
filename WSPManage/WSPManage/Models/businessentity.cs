using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WSPManage.Models
{
    [Table("dbo.businessEntities")]
    public class businessEntity
    {
        [Key]
        public int businessID { get; set; }

        public string businessName { get; set; }
        public bool isSelected { get; set; }

    }
}