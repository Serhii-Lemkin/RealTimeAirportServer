using Airport.data;
using Airport.Hubs;
using Airport.Repository.Class;
using Airport.Repository.Interface;
using Airport.Services.Class;
using Airport.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
 builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AirportDataContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SqlLight")));

builder.Services.AddSingleton<IAirportLogic, AirportLogic>();
builder.Services.AddSingleton<IControllTower, ControllTower>();
builder.Services.AddSingleton<IStationRepository, Stationrepository>();
builder.Services.AddSingleton<IPlaneRepository, PlaneRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.MapHub<AirportHub>("/airport");

app.Run();
