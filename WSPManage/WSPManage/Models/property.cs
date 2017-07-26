using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WSPManage.Models
{
    [Table("dbo.properties")]
    public class property : BaseEntity
    {
        [Key]
        public int propertyID { get; set; }

        public bool Active { get; set; }
        public string PhysicalAddress { get; set; }
        public string PhysicalUnit { get; set; }
        public string PhysicalCity { get; set; }
        public string PhysicalState { get; set; }
        public string PhysicalZipcode { get; set; }
        public string PhysicalCounty { get; set; }
        public decimal OriginalCost { get; set; }

        [DataType(DataType.Date)]
        public DateTime AcquireDate { get; set; }

        public decimal MarketValue { get; set; }
        public bool RentalUnit { get; set; }

        public string PropertySource { get; set; }
        
        public bool LegallyAvailable { get; set; }
        public bool PhysicallyAvailable { get; set; }
        public bool WSPOwned { get; set; }
        public bool InsuranceRequired { get; set; }
        public string InsuranceCarrier { get; set; }
        public string InsurancePolicy { get; set; }

        [DataType(DataType.Date)]
        public DateTime InsurancePolicyExpiration { get; set; }

        [DataType(DataType.Date)]
        public DateTime WSPLiabilityDateAdded { get; set; }

        public string Lender { get; set; }
        public string LoanNumber { get; set; }
        public decimal WSPBalanceOwed { get; set; }
        public string TaxIDNumber { get; set; }
        public string LastYearPaid { get; set; }
                
        public IEnumerable<SelectListItem> businessentitySelectList { get; set; }
                      
    }
}