using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WSPManage.Models
{
    public class physicalStreetDirDropdownList
    {
        public static List<SelectListItem> physicalStreetDirList = new List<SelectListItem>()
        {
            new SelectListItem() { Text="North", Value="N"},
            new SelectListItem() { Text="South", Value="S"},
            new SelectListItem() { Text="East", Value="E"},
            new SelectListItem() { Text="West", Value="W"}
        };
    }
}