using FitnessAppAPI.Models;
using FitnessAppAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseSetting>(builder.Configuration.GetSection("FitnessAppDatabase"));
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<MembershipsService>();
builder.Services.AddSingleton<UserMembershipsService>();
builder.Services.AddSingleton<CheckInsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
