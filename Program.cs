using Login_Register.Interface;
using Login_Register.Mapping;
using Login_Register.Model;
using Login_Register.Process;
using Login_Register.Repository;
using Login_Registor.Model;
using Login_Registor.Process;
using Login_Registor.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers().AddJsonOptions(options =>{options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;});
        builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSettings"));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<EmailProcess>();
        builder.Services.AddScoped<FIleUploadProcess>();
        builder.Services.AddScoped<LoginProcess>();
        builder.Services.AddScoped<User>();
        builder.Services.AddScoped<IProduct<Product>,ProductRepository>();
        builder.Services.AddMemoryCache();
        builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        builder.Services.AddAuthentication(a => { a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; }).AddJwtBearer(a => { a.RequireHttpsMetadata = false; a.TokenValidationParameters = new() { IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ConfigProvider.EncryptionKey)), ValidateIssuer = false, ValidateLifetime = true, ClockSkew = TimeSpan.Zero, ValidateAudience = false }; });
        builder.Services.AddAuthorization(options =>{ options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));});
        builder.Services.AddSwaggerGen(s => { s.AddSecurityDefinition("Bearer", new() { Name = "Authorization", In = ParameterLocation.Header, Type = SecuritySchemeType.ApiKey, Description = "Enter JWT with Bearer into field" }); s.AddSecurityRequirement(new OpenApiSecurityRequirement { { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() } }); });
        builder.Services.AddAutoMapper(typeof(MappingStudent));
        Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day).CreateLogger();
        builder.Host.UseSerilog();
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        //app.UseAuthentication().UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
