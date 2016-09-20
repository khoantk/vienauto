namespace Vienauto.Entity.Entities
{
    public class DealerShip : Entity
    {
        public virtual User User { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
    }
}

