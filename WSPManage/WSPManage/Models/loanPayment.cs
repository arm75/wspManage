using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WSPManage.Models
{
    [Table("dbo.loanPayments")]
    public class loanPayment : BaseEntity
    {
        [Key]
        public int loanPaymentID { get; set; }

        public int customerID { get; set; }
        public int propertyID { get; set; }
        public int loanID { get; set; }
    }
}