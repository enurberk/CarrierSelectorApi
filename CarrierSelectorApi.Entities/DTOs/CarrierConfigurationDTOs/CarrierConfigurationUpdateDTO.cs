using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierSelectorApi.Entities.DTOs.CarrierConfigurationDTOs
{
    public class CarrierConfigurationUpdateDTO
    {
        public int CarrierConfigurationId { get; set; }
        public int CarrierId { get; set; }
        public int CarrierMinDesi { get; set; }
        public int CarrierMaxDesi { get; set; }
        public decimal CarrierCost { get; set; }
    }
}
