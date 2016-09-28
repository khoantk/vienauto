using System.Web.Mvc;
using Vienauto.Service.Dto;
using Vienauto.Service.Result;
using Vienauto.Core.Extension;
using VienautoMobile.Models.Form;
using VienautoMobile.Models.View;
using System.Collections.Generic;
using Vienauto.Service.Application;
using Vienauto.Core.Extension.Html;
using Vienauto.Mobile.Configuration;

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
            var registerModel = new RegisterFormModel();
            registerModel.QuestionId = 0;
            registerModel.DealerShipId = 0;
            registerModel.AgentId = 0;
            registerModel.LocationId = 0;
            registerModel.TotalBranchId = 2;
            registerModel.NumberCarTransactionId = "nhỏ hơn 5 chiếc";
            registerModel.CarDistributionId = "Trong nước";
            registerModel.IntroduceCustomerId = "Trong nước";
            registerModel.YourCustomerId = "Tiếp thị quảng cáo";
            registerModel.HowToKnowUsId = "Email quảng cáo";
            LoadDropdownListRegisterModel(registerModel);
            return View(registerModel);
        }

        [HttpPost]
        public ActionResult Register(RegisterFormModel registerModel)
        {
            ValidateForm(registerModel);
            if (ModelState.IsValid)
            {
                var result = new ServiceResult<RegisterDto>();
                var registerDto = registerModel.FromModelToDto();

                if (registerModel.IsRegsiterAgent)
                    result = _accountService.SignUpAgentUser(registerDto);
                else
                    result = _accountService.SignUpUser(registerDto);

                if (result.HasErrors)
                {
                    ViewBag.ErrorMessage = result.Errors; 
                    LoadDropdownListRegisterModel(registerModel);
                    return View(registerModel);
                }

                return RedirectToAction("Index", "Home");
            }
            LoadDropdownListRegisterModel(registerModel);
            return View(registerModel);
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

        private void ValidateForm(RegisterFormModel model)
        {
            if (string.IsNullOrEmpty(model.FirstName))
                ModelState.AddModelError("FirstName", "Chưa nhập họ và chữ lót.");
            else if (string.IsNullOrEmpty(model.LastName))
                ModelState.AddModelError("PassWord", "Chưa nhập tên.");
            else if (string.IsNullOrEmpty(model.Email))
                ModelState.AddModelError("Email", "Chưa nhập email.");
            else if (string.IsNullOrEmpty(model.Password))
                ModelState.AddModelError("PassWord", "Chưa nhập mật khẩu.");
            else if (string.IsNullOrEmpty(model.ConfirmPassword))
                ModelState.AddModelError("ConfirmPassword", "Chưa nhập xác nhận mật khẩu.");
            else if (string.Equals(model.ConfirmPassword, model.Password))
                ModelState.AddModelError("IsEquals", "Mật khẩu xác nhận không trùng khớp.");
            else if (int.Equals(model.QuestionId, 0))
                ModelState.AddModelError("QuestionId", "Chưa chọn câu hỏi bí mật.");
            else if (string.IsNullOrEmpty(model.Answer))
                ModelState.AddModelError("Answer", "Chưa nhập câu trả lời. Nó rất cần thiết khi khôi phục mật khẩu.");
            else if (!model.AgreeWithUs)
                ModelState.AddModelError("AgreeWithUs", "Vui lòng chọn đồng ý với các điều khoản của chúng tôi.");
            else if (model.IsRegsiterAgent)
            {
                if (string.IsNullOrEmpty(model.CompanyName))
                    ModelState.AddModelError("CompanyName", "Chưa nhập tên công ty.");
                if (string.IsNullOrEmpty(model.TransactionAddress))
                    ModelState.AddModelError("TransactionAddress", "Chưa nhập địa chỉ giao dịch.");
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        private void LoadDropdownListRegisterModel(RegisterFormModel registerFormModel)
        {
            var questions = LoadQuestions();
            registerFormModel.Questions = questions.ToSelectList(q => q.QuestionName, q => q.QuestionId.ToString(), "Chọn một câu hỏi và trả lời");

            var dealerShips = LoadDealerShips();
            registerFormModel.DealerShips = dealerShips.ToSelectList(ds => ds.ManufacturerName, ds => ds.ManufacturerId.ToString(), "Chọn hãng phân phối");

            registerFormModel.Agents = LoadAgentsDealership(registerFormModel.DealerShipId);

            var locations = LoadLocations();
            registerFormModel.Locations = locations.ToSelectList(l => l.LocationName, l => l.LocationId.ToString(), "Chọn vị trí");

            registerFormModel.TotalBranches = ConfigSection.GetDropDownList("TotalBranches", "Account");
            registerFormModel.NumberCarTransactions = ConfigSection.GetDropDownList("NumberCarTransactions", "Account");
            registerFormModel.CarDistributions = ConfigSection.GetDropDownList("CarDistributions", "Account");
            registerFormModel.IntroduceCustomer = ConfigSection.GetDropDownList("IntroduceCustomer", "Account");
            registerFormModel.YourCustomer = ConfigSection.GetDropDownList("YourCustomer", "Account");
            registerFormModel.HowToKnowUs = ConfigSection.GetDropDownList("HowToKnowUs", "Account");
        }

        private List<SelectListItem> LoadAgentsDealership(int dealerShipId)
        {
            var selectList = new List<SelectListItem>();
            var agentDtos = _agencyService.GetAgencyByDealerShip(dealerShipId);
            if (!agentDtos.HasErrors)
                agentDtos.Target.ForEach(agent => 
                { selectList.Add(new SelectListItem { Text = agent.FullName, Value = agent.AgencyId.ToString() }); });
            return selectList;
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