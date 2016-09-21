using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vienauto.Service.Dto
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public int QuestionId { get; set; }
        public string AnswerQuestion { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public DateTime JoinDate { get; set; }
        public int LevelId { get; set; }
        public int Active { get; set; }
    }
}
