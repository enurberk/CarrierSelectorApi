using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierSelectorApi.Entities.DTOs.CarrierDTOs
{
    public class CarrierCreateDTO
    {
        public string CarrierName { get; set; } = string.Empty;
        public bool CarrierIsActive { get; set; }
        public decimal CarrierPlusDesiCost { get; set; }
    }
}
