using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vienauto.Service.Dto
{
    public class RegisterDto : AgentUserDto 
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public int QuestionId { get; set; }
        public string AnswerQuestion { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public DateTime JoinDate { get; set; }
        public int LevelId { get; set; }
        public int ProvinceId { get; set; }
        public int ChangeSub { get; set; }
        public float Discount { get; set; }
        public string Avatar { get; set; }
        public int Active { get; set; }
        //
        public string Avatar { get; set; }
        public int Point { get; set; }
        public int Parent { get; set; }
        public float Discount { get; set; }        
        public string ZoomMap { get; set; }
        public string MapCoordinate { get; set; }
        public int changesub { get; set; }
    }

    public class AgentUserDto
    {
        public string CompanyName { get; set; }
        public string TransactionAddress { get; set; }
        public string TaxNumber { get; set; }
        public string DeputyFullName { get; set; }
        public string AgentPhone { get; set; }
        public string AgentMobile { get; set; }
        public string EmailVerification { get; set; }
        public string Location { get; set; }
        public int TotalBranches { get; set; }
        public string NumberCarTransaction { get; set; }
        public string CarDistribution { get; set; }
        public bool NeedConsultMore { get; set; }
        public string IntroduceCustomer { get; set; }
        public string YourCustomer { get; set; }
        public string HowToKnowUs { get; set; }
        public bool IsUser { get; set; }
        public bool CreateOrders { get; set; }
    }
}
