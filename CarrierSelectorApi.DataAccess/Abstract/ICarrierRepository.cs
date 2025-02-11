using CarrierSelectorApi.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierSelectorApi.DataAccess.Abstract
{
    public interface ICarrierRepository : IGenericRepository<Carrier>
    {
        //Task<IEnumerable<Carrier>> GetActiveCarriersAsync();
    }
}
