using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WSPManage.Models
{
    [Table("dbo.loans")]
    public class loan : BaseEntity
    {
        [Key]
        public int loanID { get; set; }

        public bool Active { get; set; }
        public bool LoanOnly { get; set; }

        public int customerID { get; set; }
        public int propertyID { get; set; }

        public decimal SalePrice { get; set; }
        public decimal DownPayment { get; set; }

        [DataType(DataType.Date)]
        public DateTime ContractDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ClosingDate { get; set; }

        public decimal LoanAmount { get; set; }

        public decimal InterestRate { get; set; }
        public int Period { get; set; }
        public decimal Payment { get; set; }

        [DataType(DataType.Date)]
        public DateTime FirstPaymentDate { get; set; }

        public string ValueToCalc { get; set; }
        public string LoanNotes { get; set; }
        
        

        public static int addTwo(int testParam)
        {
            testParam = testParam + 2;

            return testParam;
        }

        public List<amortizationScheduleEntry> createAmortizationSchedule (
            DateTime FirstPaymentDate,
            decimal SalePrice,
            decimal DownPayment,
            decimal LoanAmount,
            decimal InterestRate, 
            int Period)
        {
            this.FirstPaymentDate = FirstPaymentDate;
            this.SalePrice = SalePrice;
            this.DownPayment = DownPayment;
            this.LoanAmount = LoanAmount;
            this.InterestRate = InterestRate;
            this.Period = Period;

            DateTime PaymentDate;
            PaymentDate = this.FirstPaymentDate;
            
            //get initial period interest payment
            decimal YearlyInterestTotal = this.LoanAmount * this.InterestRate;
            decimal DailyInterestTotal = YearlyInterestTotal / 360;
            decimal PeriodInterestPayment = DailyInterestTotal * 30;
            PeriodInterestPayment = Math.Round(PeriodInterestPayment, 2);

            //get period payment amount (doesn't change)
            decimal TotalLoanInterst = PeriodInterestPayment * this.Period;
            decimal TotalLoanPayedAmount = this.LoanAmount + TotalLoanInterst;
            decimal PeriodPayment = TotalLoanPayedAmount / this.Period;
            PeriodPayment = Math.Round(PeriodPayment, 2);

            //get initial period principal payment
            decimal PeriodPrincipalPayment = PeriodPayment - PeriodInterestPayment;

            // set the initial principal balance
            decimal PrincipalBalance = Math.Round(this.LoanAmount,2);

            // initialize the list
            List<amortizationScheduleEntry> thisAmortizationScheduleList = new List<amortizationScheduleEntry>();
            
            for (int PaymentNumber = 1; PaymentNumber <= this.Period; PaymentNumber++)
            {
                // create this entry
                amortizationScheduleEntry thisEntry = new amortizationScheduleEntry();

                // set all values for this list entry
                thisEntry.PaymentDate = PaymentDate;
                thisEntry.PaymentNumber = PaymentNumber;
                thisEntry.PaymentAmount = PeriodPayment;
                thisEntry.Principal = PeriodPrincipalPayment; 
                thisEntry.Interest = PeriodInterestPayment;
                thisEntry.PrincipalBalance = PrincipalBalance;

                // add this list entry to the list
                thisAmortizationScheduleList.Add(thisEntry);

                // subtact the payment from the PrincipalBalance
                PrincipalBalance = PrincipalBalance - PeriodPrincipalPayment;

                // set the NEXT period's interest payment
                YearlyInterestTotal = PrincipalBalance * this.InterestRate;
                DailyInterestTotal = YearlyInterestTotal / 360;
                PeriodInterestPayment = DailyInterestTotal * 30;
                PeriodInterestPayment = Math.Round(PeriodInterestPayment, 2);

                // set the NEXT period's principal payment
                PeriodPrincipalPayment = PeriodPayment - PeriodInterestPayment;

                // increment PaymentDate to the next month
                PaymentDate = PaymentDate.AddMonths(1);
            }

            return (thisAmortizationScheduleList);
        }



    }
}