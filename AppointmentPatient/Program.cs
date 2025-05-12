using AnimalClinicAPI.Service.Appointment;
using AnimalClinicAPI.Service.Doctor;
using AnimalClinicAPI.Service.Patient;
using AnimalClinicAPI.Service.ServiceD;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();

builder.Services.AddControllers();
builder.Services.AddScoped<AppointmentInterface, AppointmentService>();
builder.Services.AddScoped<PatientInterface, PatientService>();
builder.Services.AddScoped<DoctorInterface, DoctorService>();
builder.Services.AddScoped<ServiceInterface, ServiceD>();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
