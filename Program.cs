using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TODOApi;
using TODOApi.Data;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Add Db connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Connect to database
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add repositories 
builder.Services.AddRepositories();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO - pøi deploy zmìnit

var allowedOrigins = Environment.GetEnvironmentVariable("AllowedOrigins")?.Split(',');

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy =>
        {
            if (allowedOrigins != null)
            {
                policy.WithOrigins(allowedOrigins)
                .AllowAnyHeader().
                AllowAnyMethod();
            } else
            {
                policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader().
                AllowAnyMethod();
            }
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();
