﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierSelectorApi.Entities.Entities
{
    public class Order : BaseEntity
    {
        public int OrderId { get; set; }
        public int CarrierId { get; set; }
        public int OrderDesi { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderCarrierCost { get; set; }
        public virtual Carrier Carrier { get; set; }
    }
}
