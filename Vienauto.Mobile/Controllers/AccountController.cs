using System;
using System.Web.Mvc;
using Vienauto.Service.Dto;
using Vienauto.Core.Extension;
using VienautoMobile.Models.Form;
using VienautoMobile.Models.View;
using System.Collections.Generic;
using Vienauto.Service.Application;
using Vienauto.Core.Extension.Html;
using System.Collections.Specialized;

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

            registerViewModel.TotalBranches = LoadTotalBranches();
            registerViewModel.NumberCarTransactions = LoadNumberCarTransactions();
            registerViewModel.CarDistributions = LoadCarDistributions();
            registerViewModel.IntroduceCustomer = LoadIntroduceCustomer();
            registerViewModel.YourCustomer = LoadYourCustomer();
            registerViewModel.HowToKnowUs = LoadHowToKnowUs();

            return View(registerViewModel);
        }

        public ActionResult GetAgencyDealerShip(int dealerShipId)
        {
            var agency = _agencyService.GetAgencyByDealerShip(dealerShipId);
            if (agency.HasErrors)
                return JsonError("Lỗi hiển thị đại lý. Liên hệ admin.", false);
            return JsonSuccess("Hiển thị đại lý thành công.");
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

        private List<SelectListItem> LoadTotalBranches()
        {
            return LoadDefaultSelectList(new NameValueCollection
            {
                { "2", "2" },
                { "3", "3" },
                { "4", "4" },
                { "5", "5" },
                { "6", "6" },
                { "7", "7" },
                { "8", "8" },
                { ">8", "9" }
            });
        }

        private List<SelectListItem> LoadNumberCarTransactions()
        {
            return LoadDefaultSelectList(new NameValueCollection
            {
                { "<5", "nhỏ hơn 5 chiếc" },
                { "10", "10 chiếc" },
                { "15", "15 chiếc" },
                { "20", "20 chiếc" },
                { "25", "25 chiếc" },
                { "30", "30 chiếc" },
                { ">30", "lớn hơn 30 chiếc" }
            });
        }

        private List<SelectListItem> LoadCarDistributions()
        {
            return LoadDefaultSelectList(new NameValueCollection
            {
                { "Trong nước", "Trong nước" },
                { "Nhập khẩu", "Nhập khẩu" },
                { "Trong nước và nhập khẩu", "Trong nước và nhập khẩu" }
            });
        }

        private List<SelectListItem> LoadIntroduceCustomer()
        {
            return LoadDefaultSelectList(new NameValueCollection
            {
                { "Trong nước", "Trong nước" },
                { "Nhập khẩu", "Nhập khẩu" },
                { "Trong nước và nhập khẩu", "Trong nước và nhập khẩu" }
            });
        }

        private List<SelectListItem> LoadYourCustomer()
        {
            return LoadDefaultSelectList(new NameValueCollection
            {
                { "Tiếp thị quảng cáo", "Tiếp thị quảng cáo" },
                { "Người quen biết", "Người quen biết" },
                { "Cách khác", "Cách khác" }
            });
        }

        private List<SelectListItem> LoadHowToKnowUs()
        {
            return LoadDefaultSelectList(new NameValueCollection
            {
                { "Email quảng cáo", "Email quảng cáo" },
                { "Tiếp thị", "Tiếp thị" },
                { "Truyền hình", "Truyền hình" },
                { "Internet", "Internet" }
            });
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