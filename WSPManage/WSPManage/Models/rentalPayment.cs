using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WSPManage.Models
{
    [Table("dbo.rentalPayments")]
    public class rentalPayment : BaseEntity
    {
        [Key]
        public int rentalPaymentID { get; set; }

        public int customerID { get; set; }
        public int propertyID { get; set; }
        public int rentalID { get; set; }

    }
}