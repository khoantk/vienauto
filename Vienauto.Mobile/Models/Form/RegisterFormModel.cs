using System.Web.Mvc;
using System.Collections.Generic;

namespace VienautoMobile.Models.Form
{
    public class RegisterFormModel
    {        
        public List<SelectListItem> Questions { get; set; }        
        public List<SelectListItem> DealerShips { get; set; }
        public List<SelectListItem> Locations { get; set; }
        public List<SelectListItem> TotalBranches { get; set; }
        public List<SelectListItem> NumberCarTransactions { get; set; }
        public List<SelectListItem> CarDistributions { get; set; }
        public List<SelectListItem> IntroduceCustomer { get; set; }
        public List<SelectListItem> YourCustomer { get; set; }
        public List<SelectListItem> HowToKnowUs { get; set; }
    }
        
}
