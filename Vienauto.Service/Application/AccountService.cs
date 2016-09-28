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
        ServiceResult<RegisterDto> SignUpUser(RegisterDto registerDto);
        ServiceResult<RegisterDto> SignUpAgentUser(RegisterDto registerDto);
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
                                      .Where(u => u.UserName == userName && u.PassWord == encodedMd5Password && u.Active == 1)
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

        public ServiceResult<RegisterDto> SignUpUser(RegisterDto registerDto)
        {
            var result = new ServiceResult<RegisterDto>();
            try
            {
                using (var session = Session)
                {
                    var existedUser = Duplicate<User>(x => x.UserName == registerDto.UserName);
                    if (existedUser)
                        result.AddError(ErrorCode.DuplicateUser);

                    var user = new User();
                    user.UserName = registerDto.UserName;
                    user.PassWord = registerDto.PassWord;
                    user.FullName = registerDto.FullName;
                    user.Phone = registerDto.Phone;
                    user.Mobile = registerDto.Mobile;
                    user.Active = registerDto.Active;
                    user.Avatar = registerDto.Avatar;
                    user.NgayGiaNhap = registerDto.JoinDate;
                    user.changesub = registerDto.ChangeSub;
                    user.TienChietKhau = registerDto.Discount;
                    user.Province = Get<Province>(registerDto.ProvinceId);
                    user.ToaDoMap = user.Province?.ToaDoMap2;
                    user.ZoomMap = user.Province?.ZoomMap2.ToString();
                    user.Level = Get<Level>(registerDto.LevelId);
                    user.Question = Get<Question>(registerDto.QuestionId);
                    registerDto.UserId = Create(user);
                    CommitChanges();
                }
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.RegisterAgentUserFail, ex);
            }
            return result;
        }

        public ServiceResult<RegisterDto> SignUpAgentUser(RegisterDto registerDto)
        {
            var result = new ServiceResult<RegisterDto>();
            try
            {
                using (var session = Session)
                {
                    var existedUser = Duplicate<User>(x => x.UserName == registerDto.UserName);
                    if (existedUser)
                        result.AddError(ErrorCode.DuplicateUser);

                    var user = new User();
                    user.UserName = registerDto.UserName;
                    user.PassWord = registerDto.PassWord;
                    user.FullName = registerDto.FullName;
                    user.Phone = registerDto.Phone;
                    user.Mobile = registerDto.Mobile;
                    user.Active = registerDto.Active;
                    user.Avatar = registerDto.Avatar;
                    user.NgayGiaNhap = registerDto.JoinDate;
                    user.changesub = registerDto.ChangeSub;
                    user.TienChietKhau = registerDto.Discount;
                    user.Province = Get<Province>(registerDto.ProvinceId);
                    user.ToaDoMap = user.Province?.ToaDoMap2;
                    user.ZoomMap = user.Province?.ZoomMap2.ToString();
                    user.Level = Get<Level>(registerDto.LevelId);
                    user.Question = Get<Question>(registerDto.QuestionId);
                    registerDto.UserId = Create(user);
                    CommitChanges();

                    if (registerDto.UserId <= 0)
                        result.AddError(ErrorCode.RegisterAgentFail);
                    else
                    {
                        var agent = new Agency();
                        agent.kichhoat = 1;
                        agent.filedinhkem = "";
                        agent.ngaydangki = DateTime.Now;
                        agent.Ten_CTY = registerDto.CompanyName;
                        agent.Diachi_giaodich = registerDto.TransactionAddress;
                        agent.MaSoThue = registerDto.TaxNumber;
                        agent.HoTen_Nguoidaidien = registerDto.DeputyFullName;
                        agent.Dien_thoai = registerDto.AgentPhone;
                        agent.Didong = registerDto.Mobile;
                        agent.Email_kichhoat = registerDto.EmailVerification;
                        agent.Vitri = registerDto.Location;
                        agent.Chinhanh = registerDto.TotalBranches;
                        agent.Soxe_giaodich = registerDto.NumberCarTransaction;
                        agent.Xe_phanphoi = registerDto.CarDistribution;
                        agent.CanTuVanthem = registerDto.NeedConsultMore ? "Có" : "Không";
                        agent.lamsao_cokh = registerDto.IntroduceCustomer;
                        agent.KH_cuaban = registerDto.YourCustomer;
                        agent.Banbietchungtoitudau = registerDto.HowToKnowUs;
                        agent.Banla_TV = registerDto.IsUser ? "rồi" : "chưa";
                        agent.denghi_cungcapdonhan = registerDto.CreateOrders ? "cần" : "không cần";
                        agent.User = Get<User>(registerDto.UserId);
                        CreateOrUpdate(agent);
                    }
                }
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.RegisterAgentUserFail, ex);
            }
            return result;
        }
    }
}
