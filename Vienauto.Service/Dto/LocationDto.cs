using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vienauto.Entity.Entities;

namespace Vienauto.Service.Dto
{
    public class LocationDto
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }

    public static class LocationDtoExtension
    {
        public static LocationDto FromEntityToDto(this Location entity)
        {
            return new LocationDto
            {
                LocationId = entity.Id,
                LocationName = entity.Name_Location
            };
        }

        public static IList<LocationDto> FromEntitiesToDtos(this IList<Location> entities)
        {
            var dtos = new List<LocationDto>();
            var listEntities = (List<Location>)entities;
            listEntities.ForEach(x =>
            {
                dtos.Add(x.FromEntityToDto());
            });
            return dtos;
        }
    }
}
