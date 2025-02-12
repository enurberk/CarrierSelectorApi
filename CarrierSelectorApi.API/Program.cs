using CarrierSelectorApi.Business.Abstract;
using CarrierSelectorApi.Business.Concrete;
using CarrierSelectorApi.Business.Jobs;
using CarrierSelectorApi.Business.MappingProfile;
using CarrierSelectorApi.DataAccess.Abstract;
using CarrierSelectorApi.DataAccess.Concrete;
using CarrierSelectorApi.DataAccess.Context;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICarrierRepository, CarrierRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICarrierConfigurationRepository, CarrierConfigurationRepository>();

builder.Services.AddScoped<ICarrierService, CarrierService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICarrierConfigurationService, CarrierConfigurationService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfireServer();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHangfireDashboard();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

RecurringJob.AddOrUpdate<CarrierReportJob>(
    "Generate_Carrier_Report",
   job => job.GenerateCarrierReport(),
   Cron.Hourly);

//RecurringJob.AddOrUpdate<CarrierReportJob>(
//    "Generate_Carrier_Report",
//    job => job.GenerateCarrierReport(),
//    "*/10 * * * *");

app.Run();
