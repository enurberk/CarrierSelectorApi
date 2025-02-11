using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierSelectorApi.Entities.Entities
{
    public class Carrier : BaseEntity
    {
        public int CarrierId { get; set; }
        public int CarrierConfigurationId { get; set; }
        public string CarrierName { get; set; }
        public bool CarrierIsActive { get; set; }
        public int CarrierPlusDesiCost { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<CarrierConfiguration> CarrierConfigurations { get; set; }
    }
}
