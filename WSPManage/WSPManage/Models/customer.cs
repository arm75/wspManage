using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WSPManage.Models
{
    [Table("dbo.customers")]
    public class customer
    {
        public int ID { get; set; }
        public bool Active { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public string EIN { get; set; }
        public string ContractName { get; set; }
        public string AlternateName { get; set; }
        public string MailingAddress { get; set; }
        public string MailingCity { get; set; }
        public string MailingState { get; set; }
        public string MailingZipcode { get; set; }
        public string PhysicalAddress { get; set; }
        public string PhysicalCity { get; set; }
        public string PhysicalState { get; set; }
        public string PhysicalZipcode { get; set; }
        public string HomePhoneNumber { get; set; }
        public string WorkPhoneNumber { get; set; }
        public string CellPhoneNumber { get; set; }
        public string AlternateNumber { get; set; }
        public string Notes { get; set; }
    }
}