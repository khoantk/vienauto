using System;
using Vienauto.Service.Dto;
using Vienauto.Service.Result;
using Vienauto.Entity.Entities;
using System.Collections.Generic;
using Vienauto.Core.Extension.Encode;

namespace Vienauto.Service.Application
{
    public interface IAccountService
    {
        ServiceResult<UserDto> AuthenticateUser(string userName, string passWord);
    }
    
    public class AccountService : BaseService, IAccountService
    {
        public AccountService()
        {
            Session = OpenDefaultSession();
        }

        public ServiceResult<UserDto> AuthenticateUser(string userName, string passWord)
        {
            var result = new ServiceResult<UserDto>();
            try
            {
                var encodedMd5Password = EncodingExtensions.EncodeMD5(passWord);
                using (var session = Session)
                {
                    var user = session.QueryOver<User>()
                                      .Where(u => u.UserName == userName && u.PassWord == passWord && u.Active == 1)
                                      .SingleOrDefault();
                    if (user == null)
                        return new ServiceResult<UserDto>
                        {
                            Errors = new List<Error> { new Error { Code = ErrorCode.LogInFail } }
                        };

                    //Update last login for user
                    user.LastLogin = DateTime.Now;
                    session.SaveOrUpdate(user);
                    session.Flush();

                    var userDto = user.FromEntityToDto();
                    result.Target = userDto;
                }
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.LogInFail, ex);
            }
            return result;
        }

        public ServiceResult<int> SignUpUser(RegisterDto registerDto)
        {
            var result = new ServiceResult<int>();
            try
            {
                using (var session = Session)
                {
                    var user = new User();
                    user.UserName = registerDto.UserName;
                    user.PassWord = registerDto.PassWord;
                    user.Question = Get<Question>(registerDto.QuestionId);
                    user.Answer_Question = registerDto.AnswerQuestion;
                    user.FullName = registerDto.FullName;
                    user.Phone = registerDto.Phone;
                    user.Mobile = registerDto.Mobile;
                    user.NgayGiaNhap = DateTime.Now;
                    user.Level = Get<Level>(registerDto.LevelId = 5);
                    user.Active = registerDto.Active = 1;
                }
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.RegisterFail, ex);
            }
            return result;
        }
    }
}
