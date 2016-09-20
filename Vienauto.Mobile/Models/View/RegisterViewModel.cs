using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace VienautoMobile.Models.View
{
    public class RegisterViewModel
    {
        public List<SelectListItem> Questions { get; set; }
        public List<SelectListItem> DealerShips { get; set; }
        public List<SelectListItem> Locations { get; set; }
        public List<SelectListItem> TotalBranchs { get; set; }
    }
}
