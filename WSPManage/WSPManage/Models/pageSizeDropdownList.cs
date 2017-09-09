// using System;
using System.Collections.Generic;
// using System.Linq;
// using System.Web;
using System.Web.Mvc;

namespace WSPManage.Models
{
    public class pageSizeDropdownList
    {
        public static List<SelectListItem> pageSizeList = new List<SelectListItem>()
        {
            new SelectListItem() { Text="1", Value="1"},
            new SelectListItem() { Text="2", Value="2"},
            new SelectListItem() { Text="3", Value="3"},
            new SelectListItem() { Text="4", Value="4"},
            new SelectListItem() { Text="5", Value="5"}
        };
    }
}