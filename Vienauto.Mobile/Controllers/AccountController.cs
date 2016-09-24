using System.Web.Mvc;
using Vienauto.Core.Extension;
using VienautoMobile.Models.Form;
using VienautoMobile.Models.View;
using System.Collections.Generic;
using Vienauto.Service.Application;
using Vienauto.Core.Extension.Html;
using VienautoMobile.Configuration;

namespace VienautoMobile.Controllers
{
    public class AccountController : MobileController
    {
        private readonly IAccountService _accountService;
        private readonly IAgencyService _agencyService;
        private readonly IOthersService _othersService;

        public AccountController() : this(new AccountService(), new AgencyService(), new OthersService())
        {

        }

        public AccountController(IAccountService accountService, IAgencyService agencyService, IOthersService othersService)
        {
            _accountService = accountService;
            _agencyService = agencyService;
            _othersService = othersService;
        }

        public ActionResult SignIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(AccountFormModel model, string returnUrl)
        {
            ValidateFormModel(model);
            //var errors = ModelState.Values.SelectMany(value => value.Errors).ToList();
            if (ModelState.IsValid)
            {
                var isLoginSuccessful = LoginAction(() => _accountService.AuthenticateUser(model.UserName, model.PassWord), new string[] { "FullName", "UserName" });
                if (isLoginSuccessful)
                    return RedirectToLocal(returnUrl);
                else
                    ModelState.AddModelError("Login", "Đăng nhập thất bại!");
            }
            return View(model);
        }

        public ActionResult Register()
        {
            var registerViewModel = new RegisterFormModel();
            var questions = LoadQuestions();
            registerViewModel.Questions = questions.ToSelectList(q => q.QuestionName, q => q.QuestionId.ToString(), "Chọn câu hỏi");

            var dealerShips = LoadDealerShips();
            registerViewModel.DealerShips = dealerShips.ToSelectList(ds => ds.ManufacturerName, ds => ds.ManufacturerId.ToString(), "Chọn hãng phân phối");

            var locations = LoadLocations();
            registerViewModel.Locations = locations.ToSelectList(l => l.LocationName, l => l.LocationId.ToString(), "Chọn vị trí");

            //registerViewModel.TotalBranches = ConfigSection.GetDropDownList("TotalBranches", "Account");
            //registerViewModel.NumberCarTransactions = ConfigSection.GetDropDownList("NumberCarTransactions", "Account");
            //registerViewModel.CarDistributions = ConfigSection.GetDropDownList("CarDistributions", "Account");
            //registerViewModel.IntroduceCustomer = ConfigSection.GetDropDownList("IntroduceCustomer", "Account");
            //registerViewModel.YourCustomer = ConfigSection.GetDropDownList("YourCustomer", "Account");
            //registerViewModel.HowToKnowUs = ConfigSection.GetDropDownList("HowToKnowUs", "Account");

            return View(registerViewModel);
        }

        [HttpPost]
        public ActionResult Register(RegisterFormModel registerModel)
        {
            return View();
        }

        public ActionResult GetAgencyDealerShip(int dealerShipId)
        {
            var agents = _agencyService.GetAgencyByDealerShip(dealerShipId);
            if (agents.HasErrors)
                return JsonError("Lỗi hiển thị đại lý. Liên hệ admin.", false);
            return JsonSuccess("Hiển thị đại lý thành công.", agents.Target);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOut()
        {
            LogOutAction();
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        private List<QuestionViewModel> LoadQuestions()
        {
            var questionsViewModel = new List<QuestionViewModel>();
            var questionDtos = _othersService.GetAllQuestions();
            if (!questionDtos.HasErrors)
            {
                questionDtos.Target.ForEach(q =>
                {
                    questionsViewModel.Add(new QuestionViewModel
                    {
                        QuestionId = q.QuestionId,
                        QuestionName = q.QuestionName
                    });
                });
            }
            return questionsViewModel;
        }

        private List<DealerShipViewModel> LoadDealerShips()
        {
            var dealerShipsViewModel = new List<DealerShipViewModel>();
            var dealerShipDtos = _agencyService.GetAllDealerShips();
            if (!dealerShipDtos.HasErrors)
            {
                dealerShipDtos.Target.ForEach(ds =>
                {
                    dealerShipsViewModel.Add(new DealerShipViewModel
                    {
                        DealerShipId = ds.DealerShipId,
                        ManufacturerId = ds.ManufacturerId,
                        ManufacturerName = ds.ManufacturerName
                    });
                });
            }
            return dealerShipsViewModel;
        }

        private List<LocationViewModel> LoadLocations()
        {
            var locationsViewModel = new List<LocationViewModel>();
            var locationDtos = _othersService.GetAllLocations();
            if (!locationDtos.HasErrors)
            {
                locationDtos.Target.ForEach(l =>
                {
                    locationsViewModel.Add(new LocationViewModel
                    {
                        LocationId = l.LocationId,
                        LocationName = l.LocationName
                    });
                });
            }
            return locationsViewModel;
        }

        private void ValidateFormModel(AccountFormModel model)
        {
            if (string.IsNullOrEmpty(model.UserName))
                ModelState.AddModelError("UserName", "Chưa nhập email đăng nhập");
            else if (string.IsNullOrEmpty(model.PassWord))
                ModelState.AddModelError("PassWord", "Chưa nhập mật khẩu");
        }
    }
}