using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System;
using TaskManagerAPI.ServiceDependencies;
using TaskManagerAPI.EntityRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"])),
    };
    options.IncludeErrorDetails = true;
    options.UseSecurityTokenValidators = true;

    options.Events = new JwtBearerEvents   //log çektiðim kýsým
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault();
            Console.WriteLine($"Raw Authorization header: {token}");

            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                context.Token = token.Substring("Bearer ".Length);
                Console.WriteLine($"Processed token: {context.Token}");
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    };   //log JWT
});
//builder.Services.AddAuthorization();
builder.Services.AddAuthorization(options =>
{
    // Sadece admin kullanýcýlarý için politika
    options.AddPolicy("admin", policy => policy.RequireClaim("Role", "admin"));

    // Admin veya SecondClass kullanýcýlarý için politika
    options.AddPolicy("AdminOrSecondClass", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "Role" && (c.Value == "admin" || c.Value == "secondclass"))
        ));

    // Sadece thirdclass kullanýcýlarý için politika
    options.AddPolicy("thirdclass", policy => policy.RequireClaim("Role", "thirdclass"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddServiceDependencies();   //DI Container'a ekledim

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagerDatabase")));

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
