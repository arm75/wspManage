using System;
// using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// using System.Linq;
// using System.Web;
// using System.Web.Mvc;

namespace WSPManage.Models
{
    [Table("dbo.properties")]
    public class property : BaseEntity
    {
        [Key]
        public int propertyID { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required, DisplayName("Street #")]
        public string PhysicalStreetNumber { get; set; }

        [DisplayName("Direction")]
        public string PhysicalStreetDir { get; set; }

        [Required, DisplayName("Street Name")]
        public string PhysicalStreetName { get; set; }

        [DisplayName("Address")]
        public string PhysicalAddress { get; set; }
        [DisplayName("Unit")]
        public string PhysicalUnit { get; set; }
        [Required, DisplayName("City")]
        public string PhysicalCity { get; set; }

        [Required, DisplayName("State")]
        public string PhysicalState { get; set; }
        [Required, DisplayName("Zip Code")]
        public string PhysicalZipcode { get; set; }
        [Required, DisplayName("County")]
        public string PhysicalCounty { get; set; }

        [Required, DisplayName("Original Cost")]
        public decimal OriginalCost { get; set; }
        [Required, DisplayName("Acquire Date"), DataType(DataType.Date)]
        public DateTime AcquireDate { get; set; }
        [Required, DisplayName("Market Value")]
        public decimal MarketValue { get; set; }
        [Required, DisplayName("Rental Unit")]
        public bool RentalUnit { get; set; }
        [Required, DisplayName("Property Source")]
        public string PropertySource { get; set; }
        [Required, DisplayName("Legal Avail")]
        public bool LegallyAvailable { get; set; }
        [Required, DisplayName("Phys Avail")]
        public bool PhysicallyAvailable { get; set; }
        [Required, DisplayName("WSP Owned")]
        public bool WSPOwned { get; set; }

        [DisplayName("Insurance Required")]
        public bool InsuranceRequired { get; set; }
        [DisplayName("Insurance Carrier")]
        public string InsuranceCarrier { get; set; }
        [DisplayName("Insurance Policy")]
        public string InsurancePolicy { get; set; }
        [Required, DisplayName("Insurance Policy Expiration"), DataType(DataType.Date)]
        public DateTime InsurancePolicyExpiration { get; set; }
        [Required, DisplayName("WSP Liability Date Added"), DataType(DataType.Date)]
        public DateTime WSPLiabilityDateAdded { get; set; }

        [DisplayName("Lender")]
        public string Lender { get; set; }
        [DisplayName("Loan Number")]
        public string LoanNumber { get; set; }
        [DisplayName("WSP Balance Owed")]
        public decimal WSPBalanceOwed { get; set; }
        [DisplayName("Tax ID Number")]
        public string TaxIDNumber { get; set; }
        [DisplayName("Last Year Paid")]
        public string LastYearPaid { get; set; }
              
        // The FIRST way I built the select list, by creating a new instance of a model property.
        // public IEnumerable<SelectListItem> businessentitySelectList { get; set; }
                      
    }
}