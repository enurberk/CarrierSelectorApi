using CarrierSelectorApi.DataAccess.Context;
using CarrierSelectorApi.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierSelectorApi.Business.Jobs
{
    public class CarrierReportJob
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CarrierReportJob> _logger;

        public CarrierReportJob(ApplicationDbContext context, ILogger<CarrierReportJob> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task GenerateCarrierReport()
        {
            _logger.LogInformation("CarrierReport job started at {Time}", DateTime.UtcNow);

            var orderGroups = await _context.Orders
                .GroupBy(o => new { o.CarrierId, o.OrderDate.Date })
                .Select(group => new
                {
                    CarrierId = group.Key.CarrierId,
                    ReportDate = group.Key.Date,
                    TotalCost = group.Sum(o => o.OrderCarrierCost)
                })
                .ToListAsync();

            foreach (var report in orderGroups)
            {
                var carrierReport = new CarrierReport
                {
                    CarrierId = report.CarrierId,
                    CarrierCost = report.TotalCost,
                    CarrierReportDate = report.ReportDate
                };

                _context.CarrierReports.Add(carrierReport);
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("CarrierReport job completed successfully at {Time}", DateTime.UtcNow);
        }
    }
}
