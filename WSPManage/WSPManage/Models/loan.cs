using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// using System.Linq;
// using System.Web;

namespace WSPManage.Models
{
    [Table("dbo.loans")]
    public class loan : BaseEntity
    {
        [Key, DisplayName("Loan ID")]
        public int loanID { get; set; }

        [DisplayName("Active?")]
        public bool Active { get; set; }
        [DisplayName("Loan Only?")]
        public bool LoanOnly { get; set; }

        [DisplayName("Customer")]
        public int customerID { get; set; }
        [DisplayName("Property")]
        public int propertyID { get; set; }

        public string customerIDName { get; set; }
        public string propertyIDName { get; set; }

        [DisplayName("Sale Price"), DataType(DataType.Currency)]
        public decimal SalePrice { get; set; }

        [DisplayName("Down Payment"), DataType(DataType.Currency)]
        public decimal DownPayment { get; set; }

        [DataType(DataType.Date)]
        public DateTime ContractDate { get; set; }

        [DisplayName("Closing Date"), DataType(DataType.Date)]
        public DateTime ClosingDate { get; set; }

        [DisplayName("Loan Amount"), DataType(DataType.Currency)]
        public decimal LoanAmount { get; set; }

        // , DisplayFormat(DataFormatString = "{0:0.########}", ApplyFormatInEditMode = true)

        [Range(0, 99.99999999), DisplayFormat(DataFormatString = "{0:0.##########}", ApplyFormatInEditMode = true), DisplayName("Interest Rate")]
        public decimal InterestRate { get; set; }

        [DisplayName("Loan Periods")]
        public int Period { get; set; }
        [DisplayName("Loan Payment"), DataType(DataType.Currency)]
        public decimal Payment { get; set; }

        [DataType(DataType.Date)]
        public DateTime FirstPaymentDate { get; set; }

        [DisplayName("Value to Calc")]
        public string ValueToCalc { get; set; }

        [DataType(DataType.MultilineText), DisplayName("Loan Notes")]
        public string LoanNotes { get; set; }

        public bool ActionInProgress { get; set; }
        public string ActionInProgressCounty { get; set; }
        public DateTime? TenDayDate { get; set; }
        public DateTime? NoticeOfPub { get; set; }
        public DateTime? FirstPubDate { get; set; }
        public bool Bankruptcy { get; set; }
        public DateTime? SaleDate { get; set; }
        public DateTime? AgreeToCont { get; set; }
        public DateTime? NewSaleDate { get; set; }
        public bool Judgement { get; set; }
        public DateTime? TwentyDayDate { get; set; }
        public DateTime? UdrpSentOut { get; set; }
        public DateTime? CourtDate { get; set; }
        public DateTime? AgreeToVacate { get; set; }
        public DateTime? WritDate { get; set; }
        public DateTime? EvictionDate { get; set; }

        
        public static int addTwo(int testParam)
        {
            testParam = testParam + 2;

            return testParam;
        }
        

        public decimal calculateMonthlyLoanPayment(
            decimal LoanAmount,
            decimal InterestRate,
            int Period )
        {
            this.LoanAmount = LoanAmount;
            this.InterestRate = InterestRate;
            this.Period = Period;
            
            double MonthlyInterestAsDouble = Convert.ToDouble(this.InterestRate) / 12;
            var OnePlusI = 1 + MonthlyInterestAsDouble;
            double DiscountRateAsDouble = ((Math.Pow((OnePlusI), (this.Period)) - 1) / (MonthlyInterestAsDouble * (Math.Pow((OnePlusI), (this.Period)))));
            decimal DiscountRate = Convert.ToDecimal(DiscountRateAsDouble);
            decimal MonthlyPayment = Math.Round((this.LoanAmount / DiscountRate), 2);

            return MonthlyPayment;
        }
        

        public List<amortizationScheduleEntry> createAmortizationSchedule (
            DateTime FirstPaymentDate,
            decimal SalePrice,
            decimal DownPayment,
            decimal LoanAmount,
            decimal InterestRate, 
            int Period )
        {
            // Get all of the variables passed to the function.
            this.FirstPaymentDate = FirstPaymentDate;
            this.SalePrice = SalePrice;
            this.DownPayment = DownPayment;
            this.LoanAmount = LoanAmount;
            this.InterestRate = InterestRate;
            this.Period = Period;

            // this is the date of each payment. set the first line to the first payment date.
            DateTime PaymentDate = this.FirstPaymentDate;
            
            //get initial period interest payment
            decimal YearlyInterestTotal = this.LoanAmount * this.InterestRate; //the amount of interest paid
            decimal DailyInterestTotal = YearlyInterestTotal / 360;
            decimal PeriodInterestPayment = DailyInterestTotal * 30;
            PeriodInterestPayment = Math.Round(PeriodInterestPayment, 2);

            // get period payment amount (doesn't change)
            decimal PeriodPayment = calculateMonthlyLoanPayment(this.LoanAmount, this.InterestRate, this.Period);
            
            //get initial period principal payment
            decimal PeriodPrincipalPayment = PeriodPayment - PeriodInterestPayment;

            // set the initial line principal balance (LoanAmount - PeriodPrincipalPayment)
            decimal PrincipalBalance = Math.Round((this.LoanAmount - PeriodPrincipalPayment), 2);

            // variables for counting the column totals
            decimal PaymentAmountTotal = 0;
            decimal PrincipalTotal = 0;
            decimal InterestTotal = 0;


            // initialize the list
            List<amortizationScheduleEntry> thisAmortizationScheduleList = new List<amortizationScheduleEntry>();
            
            for (int PaymentNumber = 1; PaymentNumber <= this.Period; PaymentNumber++)
            {
                PaymentAmountTotal = PaymentAmountTotal + PeriodPayment;
                PrincipalTotal = PrincipalTotal + PeriodPrincipalPayment;
                InterestTotal = InterestTotal + PeriodInterestPayment;

                // create this entry
                amortizationScheduleEntry thisEntry = new amortizationScheduleEntry();
                
                // THIS ENTRY
                // set all values for this list entry
                thisEntry.PaymentDate = PaymentDate;
                thisEntry.PaymentNumber = PaymentNumber;
                thisEntry.PaymentAmount = PeriodPayment;
                thisEntry.Principal = PeriodPrincipalPayment; 
                thisEntry.Interest = PeriodInterestPayment;
                thisEntry.PrincipalBalance = PrincipalBalance;
                // add this list entry to the list
                thisAmortizationScheduleList.Add(thisEntry);

                // SETUP THE NEXT ENTRY
                // increment PaymentDate to the next month, for the next line
                PaymentDate = PaymentDate.AddMonths(1);
                
                // set the NEXT period's interest payment
                YearlyInterestTotal = PrincipalBalance * this.InterestRate;
                DailyInterestTotal = YearlyInterestTotal / 360;
                PeriodInterestPayment = DailyInterestTotal * 30;
                PeriodInterestPayment = Math.Round(PeriodInterestPayment, 2);
                
                // set the NEXT period's principal payment
                PeriodPrincipalPayment = PeriodPayment - PeriodInterestPayment;

                //if the principal remaining on the last line, is less than the principal payment,
                //then we'll pay THAT much principal on the last line
                if (PrincipalBalance < PeriodPayment)
                {
                    PeriodPrincipalPayment = PrincipalBalance;
                    PeriodPayment = PeriodPrincipalPayment + PeriodInterestPayment;
                    PrincipalBalance = PrincipalBalance - PeriodPrincipalPayment;
                }
                else
                {
                    PrincipalBalance = PrincipalBalance - PeriodPrincipalPayment;
                }

            }

            //create an entry for the totals
            amortizationScheduleEntry lastEntry = new amortizationScheduleEntry();
            lastEntry.PaymentDate = PaymentDate;
            lastEntry.PaymentNumber = 0;
            lastEntry.PaymentAmount = PaymentAmountTotal;
            lastEntry.Principal = PrincipalTotal;
            lastEntry.Interest = InterestTotal;
            lastEntry.PrincipalBalance = null;
            // add this list entry to the list
            thisAmortizationScheduleList.Add(lastEntry);



            return (thisAmortizationScheduleList);
        }



    }
}