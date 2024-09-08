using Doctorly.Api.Endpoints;
using Doctorly.Data.Repository;
using Doctorly.Data.UseCases.Attendees;
using Doctorly.Data.UseCases.Events;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DoctorlyDbContext>(db => db.UseInMemoryDatabase("DoctorlyDb"));
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IAttendeeService, AttendeeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.RegisterAllEndPoints();

app.Run();
