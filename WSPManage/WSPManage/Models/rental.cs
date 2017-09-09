using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WSPManage.Models
{
    [Table("dbo.rentals")]
    public class rental : BaseEntity
    {
        [Key,DisplayName("Rental ID")]
        public int rentalID { get; set; }

        [DisplayName("Active?")]
        public bool Active { get; set; }
        
        [DisplayName("Customer")]
        public int customerID { get; set; }
        [DisplayName("Property")]
        public int propertyID { get; set; }

        public string customerIDName { get; set; }
        public string propertyIDName { get; set; }

        [DataType(DataType.Date),DisplayName("Rental Start")]
        public DateTime? RentalStartDate { get; set; }
        [DataType(DataType.Date), DisplayName("Rental End")]
        public DateTime? RentalEndDate { get; set; }

        [DataType(DataType.MultilineText), DisplayName("Rental Notes")]
        public string RentalNotes { get; set; }
        
        [DisplayName("Monthly Rent Amount")]
        public decimal MonthlyRentAmount { get; set; }
        [DisplayName("Late Payment")]
        public decimal LatePaymentAmount { get; set; }

        [DisplayName("Judgement?")]
        public bool Judgement { get; set; }
        [DataType(DataType.Date),DisplayName("Court Date")]
        public DateTime? CourtDate { get; set; }
        [DataType(DataType.Date),DisplayName("Eviction Date")]
        public DateTime? EvictionDate { get; set; }
        [DataType(DataType.Date),DisplayName("R&P Sent")]
        public DateTime? RandPSentDate { get; set; }
        [DataType(DataType.Date),DisplayName("Writ Sent")]
        public DateTime? WritSentDate { get; set; }
        [DataType(DataType.Date),DisplayName("Agree to Vacate")]
        public DateTime? AgreeToVacateDate { get; set; }

        [DisplayName("Court Costs")]
        public decimal CourtCosts { get; set; }
        [DisplayName("Legal Fees")]
        public decimal LegalFees { get; set; }


    }
}