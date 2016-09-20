using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vienauto.Entity.Entities;

namespace Vienauto.Service.Dto
{
    public class DealerShipDto
    {
        public int DealerShipId { get; set; }
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
    }

    public static class DealerShipDtoExtension
    {
        public static DealerShipDto FromEntityToDto(this DealerShip entity)
        {
            return new DealerShipDto
            {
                DealerShipId = entity.Id,
                ManufacturerId = entity.Manufacturer.Id,
                ManufacturerName = entity.Manufacturer.Name
            };
        }

        public static IList<DealerShipDto> FromEntitiesToDtos(this IList<DealerShip> entities)
        {
            var dtos = new List<DealerShipDto>();
            var listEntities = (List<DealerShip>)entities;
            listEntities.ForEach(x =>
            {
                dtos.Add(x.FromEntityToDto());
            });
            return dtos;
        }
    }
}
