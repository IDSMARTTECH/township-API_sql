using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Township_API.Data;
using Township_API.Services; 
using EventFlow.Configuration.Serialization;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var port = builder.Configuration.GetValue<int>("AllowedPort");
        var URL = builder.Configuration.GetValue<string>("AllowedURL");
        var allowedOrigin = $"http://{URL}:{port}";

        //allowedOrigin = port;

        //// Add services to the container.
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularDevClient",
                  policy =>
                  {
                      policy.WithOrigins(allowedOrigin)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                  });
        });

       

        //configure database
        builder.Services.AddDbContext<AppDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Configure Authentication
        //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    .AddJwtBearer(options =>
        //    {
        //        options.RequireHttpsMetadata = false;
        //        options.SaveToken = true;
        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateLifetime = true,
        //            ValidateIssuerSigningKey = true,
        //            ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //            ValidAudience = builder.Configuration["Jwt:Audience"],
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        //        };
        //    });

        builder.Services.AddControllers() 
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.IncludeFields = true;
        });


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Use CORS policy
        app.UseCors("AllowAngularDevClient");




        // Configure the HTTP request pipeline.
        if (1 == 1 || app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}