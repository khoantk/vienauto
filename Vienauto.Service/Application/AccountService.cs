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
                    var newUser = new User
                    {
                        UserName = registerDto.UserName,
                        PassWord = registerDto.PassWord,
                        FullName = registerDto.FullName,
                        Phone = registerDto.Phone,
                        Mobile = registerDto.Mobile,
                        Active = registerDto.Active,
                        NgayGiaNhap = registerDto.JoinDate,
                        Level = Get<Level>(registerDto.LevelId),
                        Question = Get<Question>(registerDto.QuestionId)
                    };
                    registerDto.UserId = Create(newUser);
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
                    var newUser = new User
                    {
                        UserName = registerDto.UserName,
                        PassWord = registerDto.PassWord,
                        FullName = registerDto.FullName,
                        Phone = registerDto.Phone,
                        Mobile = registerDto.Mobile,
                        Active = registerDto.Active,
                        NgayGiaNhap = registerDto.JoinDate,
                        Level = Get<Level>(registerDto.LevelId),
                        Question = Get<Question>(registerDto.QuestionId)
                    };
                    registerDto.UserId = Create(newUser);
                    CommitChanges();

                    var agent = new Agency
                    {
                        kichhoat = 1,
                        filedinhkem = "",
                        ngaydangki = DateTime.Now,
                        Ten_CTY = registerDto.CompanyName,
                        Diachi_giaodich = registerDto.TransactionAddress,
                        MaSoThue = registerDto.TaxNumber,
                        HoTen_Nguoidaidien = registerDto.DeputyFullName,
                        Dien_thoai = registerDto.AgentPhone,
                        Didong = registerDto.Mobile,
                        Email_kichhoat = registerDto.EmailVerification,
                        Vitri = registerDto.Location,
                        Chinhanh = registerDto.TotalBranches,
                        Soxe_giaodich = registerDto.NumberCarTransaction,
                        Xe_phanphoi = registerDto.CarDistribution,
                        CanTuVanthem = registerDto.NeedConsultMore ? "Có" : "Không",
                        lamsao_cokh = registerDto.IntroduceCustomer,
                        KH_cuaban = registerDto.YourCustomer,
                        Banbietchungtoitudau = registerDto.HowToKnowUs,
                        Banla_TV = registerDto.IsUser ? "rồi" : "chưa",
                        denghi_cungcapdonhan = registerDto.CreateOrders ? "cần" : "không cần",
                        User = Get<User>(registerDto.UserId)
                    };
                    CreateOrUpdate(agent);
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
