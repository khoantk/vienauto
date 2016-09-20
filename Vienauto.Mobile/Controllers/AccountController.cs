using System.Web.Mvc;
using System.Collections.Generic;
using VienautoMobile.Models.Form;
using Vienauto.Service.Application;
using Vienauto.Core.Extension.Html;
using VienautoMobile.Models.View;
using Vienauto.Service.Dto;
using System;
using Vienauto.Core.Extension;
using Vienauto.Core.IO;
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
            var registerViewModel = new RegisterViewModel();
            var questions = LoadQuestions();
            registerViewModel.Questions = questions.ToSelectList(
                    q => { return q.QuestionName; },
                    q => { return q.QuestionId.ToString(); },
                    "Chọn câu hỏi");

            var dealerShips = LoadDealerShips();
            registerViewModel.DealerShips = dealerShips.ToSelectList(
                    ds => { return ds.ManufacturerName; },
                    ds => { return ds.ManufacturerId.ToString(); },
                    "Chọn hãng phân phối");

            var locations = LoadLocations();
            registerViewModel.Locations = locations.ToSelectList(
                    l => { return l.LocationName; },
                    l => { return l.LocationId.ToString(); },
                    "Chọn vị trí");

            var totalBranches = new NameValueCollection
            {
                { "2", "2" },
                { "3", "3" },
                { "4", "4" },
                { "5", "5" },
                { "6", "6" },
                { "7", "7" },
                { "8", "8" },
                { ">8", "9" }
            };
            registerViewModel.TotalBranchs = LoadDefaultSelectList(totalBranches);
            return View(registerViewModel);
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
                questionsViewModel.ToSelectList(
                    q => { return q.QuestionName; },
                    q => { return q.QuestionId.ToString(); },
                    "Chọn câu hỏi");
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