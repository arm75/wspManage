using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WSPManage.Models
{
    public class loanPaymentTypeDropdownList
    {
        public static List<SelectListItem> loanPaymentTypeList = new List<SelectListItem>()
        {
            new SelectListItem() { Text="Loan Payment", Value="LOAN"},
            new SelectListItem() { Text="Loan Payoff", Value="PAYOFF"},
            new SelectListItem() { Text="Principal Payment", Value="PRINCIPAL"},
            new SelectListItem() { Text="Future Loan Payment", Value="FUTURE"},
            new SelectListItem() { Text="Closing Costs", Value="CLOSING"},
            new SelectListItem() { Text="Commission", Value="COMMISS"},
            new SelectListItem() { Text="Court Costs", Value="COURT"},
            new SelectListItem() { Text="Down Payment", Value="DOWNP"},
            new SelectListItem() { Text="Legal Fees", Value="LEGAL"},
            new SelectListItem() { Text="Miscellaneous", Value="MISC"},
            new SelectListItem() { Text="Publication Fees", Value="PUB"},
            new SelectListItem() { Text="Rent Payment", Value="RENT"},
            new SelectListItem() { Text="Returned Check Fees", Value="RCHECK"},
            new SelectListItem() { Text="Security Deposit", Value="SECURITY"},
            new SelectListItem() { Text="Occupancy Fees", Value="OCCUP"}
        };

    }
}