using EmployeeApplication.Data;
using EmployeeApplication.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connstring = builder.Configuration.GetConnectionString("LocalDB");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.GetConnectionString("LocalDB");
builder.Services.AddSingleton<DataConnectionProvider>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUI", policy =>
    {
        policy.WithOrigins("https://localhost:7123")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowUI");
app.UseAuthorization();

app.MapControllers();

app.Run();
