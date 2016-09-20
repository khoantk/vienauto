using System.Collections.Generic;
using Vienauto.Entity.Entities;

namespace Vienauto.Service.Dto
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
    }

    public static class QuestionDtoExtension
    {
        public static QuestionDto FromEntityToDto(this Question entity)
        {
            return new QuestionDto
            {
                QuestionId = entity.Id,
                QuestionName = entity.Name_Question
            };
        }

        public static IList<QuestionDto> FromEntitiesToDtos(this IList<Question> entities)
        {
            var dtos = new List<QuestionDto>();
            var listEntities = (List<Question>)entities;
            listEntities.ForEach(x =>
            {
                dtos.Add(x.FromEntityToDto());
            });
            return dtos;
        }
    }
}
