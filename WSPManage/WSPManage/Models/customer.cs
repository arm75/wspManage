using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WSPManage.Models
{
    [Table("dbo.customers")]
    public class customer : BaseEntity
    {
        [Key]
        public int customerID { get; set; }

        public bool Active { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string SSN { get; set; }
        public string EIN { get; set; }
        [DisplayName("Contract Name")]
        public string ContractName { get; set; }
        [DisplayName("Alt Name")]
        public string AlternateName { get; set; }

        [DisplayName("Address")]
        public string MailingAddress { get; set; }
        [DisplayName("City")]
        public string MailingCity { get; set; }
        [DisplayName("State")]
        public string MailingState { get; set; }
        [DisplayName("ZipCode")]
        public string MailingZipcode { get; set; }

        // public string PhysicalAddress { get; set; }
        // public string PhysicalCity { get; set; }
        // public string PhysicalState { get; set; }
        // public string PhysicalZipcode { get; set; }

        [DisplayName("Home")]
        public string HomePhoneNumber { get; set; }
        [DisplayName("Work")]
        public string WorkPhoneNumber { get; set; }
        [DisplayName("Cell")]
        public string CellPhoneNumber { get; set; }
        [DisplayName("Alt")]
        public string AlternateNumber { get; set; }
        [DisplayName("Customer Notes")]
        public string Notes { get; set; }
                
    }
}