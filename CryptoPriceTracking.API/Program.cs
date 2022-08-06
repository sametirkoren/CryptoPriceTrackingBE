using CryptoPriceTracking.API.BackgroundJobs;
using CryptoPriceTracking.API.Hubs;
using CryptoPriceTracking.API.IoC;
using CryptoPriceTracking.API.Persistence;
using CryptoPriceTracking.API.Services;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCustomDependencies(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CryptoPriceTrackingContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CryptoPriceTrackingConnection"));
});

builder.Services.AddSignalR();

builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangFireConnection")));
builder.Services.AddHangfireServer();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();

}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("corsapp");

var options = new BackgroundJobServerOptions
{
    SchedulePollingInterval = TimeSpan.FromMilliseconds(30000)
};

app.UseHangfireDashboard("/hangfire");
app.UseHangfireServer(options);

RecurringJob.AddOrUpdate<MessageService>(x => x.GetCryptoInformation(), "*/30 * * * * *");

app.UseAuthorization();


app.MapControllers();
app.MapHub<CryptoPriceTrackingHub>("/cryptoPriceTrackingHub");

app.Run();