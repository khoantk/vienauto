using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vienauto.Entity.Entities;

namespace Vienauto.Entity.Maps
{
    public class DealerShipMap : ClassMapping<DealerShip>
    {
        public DealerShipMap()
        {
            Schema("hdt");
            Table("Hang_Phanphoi");
            Lazy(true);
            Id(x => x.Id, map =>
            {
                map.Column("Id_Hang_PhanPhoi");
                map.Generator(Generators.Identity);
            });
            ManyToOne(x => x.User, map => map.Column("Id_DaiLy"));
            ManyToOne(x => x.Manufacturer, map => map.Column("Id_Manufacturer"));
        }
    }
}
