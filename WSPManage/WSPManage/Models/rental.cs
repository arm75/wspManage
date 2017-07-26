using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WSPManage.Models
{
    [Table("dbo.rentals")]
    public class rental : BaseEntity
    {
        [Key]
        public int rentalID { get; set; }

        public int customerID { get; set; }
        public int propertyID { get; set; }

    }
}