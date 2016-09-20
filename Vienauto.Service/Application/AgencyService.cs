using System;
using Vienauto.Service.Dto;
using Vienauto.Service.Result;
using Vienauto.Entity.Entities;
using System.Collections.Generic;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Vienauto.Service.Application
{
    public interface IAgencyService
    {
        ServiceResult<IList<DealerShipDto>> GetAllDealerShips();
    }

    public class AgencyService : BaseService, IAgencyService
    {
        public AgencyService()
        {
            Session = OpenDefaultSession();
        }

        public ServiceResult<IList<DealerShipDto>> GetAllDealerShips()
        {
            var result = new ServiceResult<IList<DealerShipDto>>();
            try
            {
                DealerShipDto dto = null;
                DealerShip dealerShipAlias = null;
                Manufacturer manufactureAlias = null;

                using (var session = Session)
                {
                    var dealerShipDtos = session.QueryOver<DealerShip>(() => dealerShipAlias)
                                  .JoinAlias(ds => ds.Manufacturer, () => manufactureAlias)
                                  .SelectList(list => list
                                        .SelectGroup(pc => dealerShipAlias.Id).WithAlias(() => dto.DealerShipId)
                                        .SelectGroup(pc => manufactureAlias.Id).WithAlias(() => dto.ManufacturerId)
                                        .SelectGroup(pc => manufactureAlias.Name).WithAlias(() => dto.ManufacturerName))
                                  .TransformUsing(Transformers.AliasToBean<DealerShipDto>())
                                  .List<DealerShipDto>();
                    result.Target = dealerShipDtos;
                }
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.FailToListAllDealerShip, ex);
            }
            return result;
        }
    }
}

