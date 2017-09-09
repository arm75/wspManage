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

        public string PaymentType { get; set; }

        public string Payor { get; set; }

        public decimal PaymentAmount { get; set; }

        public decimal PaymentLateFeeAmount { get; set; }

        public decimal PaymentInterestAmount { get; set; }

        public decimal PaymentPrincipalAmount { get; set; }
        
        public DateTime PaymentDate { get; set; }

        public DateTime? PostedDate { get; set; }

        public DateTime? DepositDate { get; set; }
        
        public string PaymentMethod { get; set; }

        public string CheckNumber { get; set; }

        public string PaymentDescription { get; set; }

        public string PaymentNotes { get; set; }


    }
}