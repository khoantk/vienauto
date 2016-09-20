using System;
using Vienauto.Service.Dto;
using Vienauto.Service.Result;
using Vienauto.Entity.Entities;
using System.Collections.Generic;

namespace Vienauto.Service.Application
{
    public interface IOthersService
    {
        ServiceResult<IList<QuestionDto>> GetAllQuestions();
        ServiceResult<IList<LocationDto>> GetAllLocations();
    }

    public class OthersService : BaseService, IOthersService
    {
        public OthersService()
        {
            Session = OpenDefaultSession();
        }

        public ServiceResult<IList<QuestionDto>> GetAllQuestions()
        {
            var result = new ServiceResult<IList<QuestionDto>>();
            try
            {
                using (var session = Session)
                {
                    var questions = session.QueryOver<Question>().OrderBy(q => q.Name_Question).Asc.List();
                    if (questions == null)
                        return new ServiceResult<IList<QuestionDto>>
                        {
                            Errors = new List<Error> { new Error { Code = ErrorCode.FailToListAllQuestion } }
                        };

                    var questionDtos = questions.FromEntitiesToDtos();
                    result.Target = questionDtos;
                }
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.Exception, ex);
            }
            return result;
        }

        public ServiceResult<IList<LocationDto>> GetAllLocations()
        {
            var result = new ServiceResult<IList<LocationDto>>();
            try
            {
                using (var session = Session)
                {
                    var locations = session.QueryOver<Location>().OrderBy(l => l.Name_Location).Asc.List();
                    if (locations == null)
                        return new ServiceResult<IList<LocationDto>>
                        {
                            Errors = new List<Error> { new Error { Code = ErrorCode.FailToListLocation } }
                        };

                    var locationDtos = locations.FromEntitiesToDtos();
                    result.Target = locationDtos;
                }
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.Exception, ex);
            }
            return result;
        }
    }
}
