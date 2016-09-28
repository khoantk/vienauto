using System.Web.Mvc;
using Vienauto.Service.Dto;
using System.Collections.Generic;

namespace VienautoMobile.Models.Form
{
    public class RegisterFormModel
    {
        public bool IsRegsiterAgent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }               
        public string Answer { get; set; }
        public string HomePhone { get; set; }
        public string Mobile { get; set; }
        public string CompanyName { get; set; }
        public string TransactionAddress { get; set; }
        public string TaxNumber { get; set; }
        public string Deputy { get; set; }
        public string AgentMobile { get; set; }
        public string AgentPhone { get; set; }
        public string AgentEmail { get; set; }
        public bool NeedConsultMore { get; set; }
        public bool IsUser { get; set; }
        public bool CreateOrders { get; set; }
        public bool AgreeWithUs { get; set; }
        public int QuestionId { get; set; }
        public int DealerShipId { get; set; }
        public int AgentId { get; set; }
        public int LocationId { get; set; }
        public int TotalBranchId { get; set; }
        public string NumberCarTransactionId { get; set; }
        public string CarDistributionId { get; set; }
        public string IntroduceCustomerId { get; set; }
        public string YourCustomerId { get; set; }
        public string HowToKnowUsId { get; set; }

        public string Avatar { get; set; }
        public int Point { get; set; }
        public int Parent { get; set; }
        public float Discount { get; set; }
        public string Location { get; set; }
        public string ZoomMap { get; set; }
        public string MapCoordinate { get; set; }
        public int changesub { get; set; }

        public List<SelectListItem> Questions { get; set; }        
        public List<SelectListItem> DealerShips { get; set; }
        public List<SelectListItem> Agents { get; set; }
        public List<SelectListItem> Locations { get; set; }
        public List<SelectListItem> TotalBranches { get; set; }
        public List<SelectListItem> NumberCarTransactions { get; set; }
        public List<SelectListItem> CarDistributions { get; set; }
        public List<SelectListItem> IntroduceCustomer { get; set; }
        public List<SelectListItem> YourCustomer { get; set; }
        public List<SelectListItem> HowToKnowUs { get; set; }
    }

    public static class RegisterFormModelExtension
    {
        public static RegisterDto FromModelToDto(this RegisterFormModel model)
        {
            return new RegisterDto
            {
                UserName = model.Email,
                PassWord = model.Password,
                CompanyName = model.CompanyName,
                TransactionAddress = model.TransactionAddress,
                TaxNumber = model.TaxNumber,
                DeputyFullName = model.Deputy,
                Phone = model.HomePhone,
                Mobile = model.Mobile,
                EmailVerification = model.Email,
                Location = model.LocationId.ToString(),
                TotalBranches = model.TotalBranchId,
                NumberCarTransaction = model.NumberCarTransactionId,
                CarDistribution = model.CarDistributionId,
                NeedConsultMore = model.NeedConsultMore,
                IntroduceCustomer = model.IntroduceCustomerId,
                YourCustomer = model.YourCustomerId,
                HowToKnowUs = model.HowToKnowUsId,
                IsUser = model.IsUser,
                CreateOrders = model.CreateOrders,
                Avatar = string.IsNullOrEmpty(model.Avatar)? "" : model.Avatar,
                Point = string.IsNullOrEmpty(model.Point.ToString())? 0 : model.Point,
                Parent = string.IsNullOrEmpty(model.Parent.ToString()) ? 0 : model.Parent,
                Discount = string.IsNullOrEmpty(model.Discount.ToString()) ? 0 : model.Discount,
                ZoomMap = string.IsNullOrEmpty(model.ZoomMap) ? "" : model.ZoomMap,
                MapCoordinate = string.IsNullOrEmpty(model.MapCoordinate) ? "" : model.MapCoordinate,
                changesub = string.IsNullOrEmpty(model.changesub.ToString()) ? 0 : model.changesub,
                QuestionId = model.QuestionId
            };
        }
    }
}

