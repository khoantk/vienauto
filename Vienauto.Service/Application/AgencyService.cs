using System;
using NHibernate.Transform;
using Vienauto.Service.Dto;
using Vienauto.Service.Result;
using Vienauto.Entity.Entities;
using System.Collections.Generic;
using NHibernate.Criterion;

namespace Vienauto.Service.Application
{
    public interface IAgencyService
    {
        ServiceResult<IList<DealerShipDto>> GetAllDealerShips();
        ServiceResult<IList<AgencyDto>> GetAgencyByDealerShip(int manufacturerId);
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

        public ServiceResult<IList<AgencyDto>> GetAgencyByDealerShip(int manufacturerId)
        {
            var result = new ServiceResult<IList<AgencyDto>>();
            try
            {
                AgencyDto dto = null;
                User userAlias = null;
                DealerShip dealerShipAlias = null;
                //raise error Session is closed
                //check Session state
                if (!Session.IsOpen)
                    Session = OpenDefaultSession();
                using (var session = Session)
                {
                    //has exception
                    //Invalid column name 'Id_Users'.
                    //table hdt.Hang_Phanphoi does not exists Id_Users.Instead of Id_Hang_PhanPhoi
                    //SqlString: SELECT useralias1_.Id_Users as y0_, useralias1_.FullName as y1_ FROM hdt.Hang_Phanphoi this_ inner join hdt.Users useralias1_ on this_.Id_Users=useralias1_.Id_Users WHERE this_.Id_Manufacturer = @p0 GROUP BY useralias1_.Id_Users, useralias1_.FullName
                    var agencyDtos = session.QueryOver<DealerShip>(() => dealerShipAlias)
                                  .JoinAlias(ds => ds.User, () => userAlias)
                                  .Where(d => d.Manufacturer.Id == manufacturerId)
                                  .SelectList(list => list
                                        .SelectGroup(pc => userAlias.Id).WithAlias(() => dto.AgencyId)
                                        .SelectGroup(pc => userAlias.FullName).WithAlias(() => dto.FullName))
                                  .TransformUsing(Transformers.AliasToBean<AgencyDto>())
                                  .List<AgencyDto>();
                    result.Target = agencyDtos;
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

