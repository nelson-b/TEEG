using FluentValidation.AspNetCore;
using Guest.API.Validation;
using Guest.BAL.Handler;
using Guest.BAL.Interface;
using Guest.BAL.Repositories;
using Infrastructure;
using Serilog;
using TestAPI_TEE.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddGuestValidator>())
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdateGuestValidation>());
builder.Services.AddMediatR(_=> _.RegisterServicesFromAssemblies(typeof(AddGuestCommandHandler).Assembly));
builder.Services.AddTransient<IGuestRepository, GuestRepository>();
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Logging.AddSerilog(logger);

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
