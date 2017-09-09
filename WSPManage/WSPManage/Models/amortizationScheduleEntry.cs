using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSPManage.Models
{
    public class amortizationScheduleEntry
    {
        public DateTime PaymentDate { get; set; }
        public int PaymentNumber { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal? PrincipalBalance { get; set; }
    }
}