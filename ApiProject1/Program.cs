using System.Text;
using BusinessProject1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ModelProject1;
using RepoProject1;
namespace ApiProject1;

using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;

public class Program
{
    private static void Main(string[] args)
    {   
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
    
    
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        //removed this line to add the stuff under testing
        //builder.Services.AddSwaggerGen();

        //thanks to Code with Julian's youtube for the tutorial ".NET 6 Web API Authentication | Minimal API & Swagger (CRUD)" on how to add authentication to the api
#region Testing
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:5117",
        ValidAudience = "https://localhost:5117",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"))
    };
});
builder.Services.AddAuthorization();


        #endregion

        builder.Services.AddScoped<IBusinessLayerClassAuthUserLogin, BusinessLayerClassAuthUserLogin>();
        builder.Services.AddScoped<IBusinessLayerClassGetUserReimbursements, BusinessLayerClassGetUserReimbursements>();
        builder.Services.AddScoped<IBusinessLayerClassManagerGetAllReimbursements, BusinessLayerClassManagerGetAllReimbursements>();
        builder.Services.AddScoped<IBusinessLayerClassManagerUpdateReimbursement, BusinessLayerClassManagerUpdateReimbursement>();
        builder.Services.AddScoped<IBusinessLayerClassNewUser, BusinessLayerClassNewUser>();
        builder.Services.AddScoped<IBusinessLayerClassReimbursementRequest, BusinessLayerClassReimbursementRequest>();
        builder.Services.AddScoped<IBusinessLayerClassUpdateUserInformation, BusinessLayerClassUpdateUserInformation>();

        builder.Services.AddScoped<IRepoClassAuthUserLogin, RepoClassAuthUserLogin>();
        builder.Services.AddScoped<IRepoClassGetUserReimbursements, RepoClassGetUserReimbursements>();
        builder.Services.AddScoped<IRepoClassManagerGetAllReimbursements, RepoClassManagerGetAllReimbursements>();
        builder.Services.AddScoped<IRepoClassManagerUpdateReimbursement, RepoClassManagerUpdateReimbursement>();
        builder.Services.AddScoped<IRepoClassNewUser, RepoClassNewUser>();
        builder.Services.AddScoped<IRepoClassReimbursementRequest, RepoClassReimbursementRequest>();
        builder.Services.AddScoped<IRepoClassUpdateUserInformation, RepoClassUpdateUserInformation>();

        //builder.Services.AddScoped< , >;

        //this makes enums show up as strings instead of numbers
        builder.Services
    .AddControllers()
    .AddJsonOptions(options => 
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        //added this line to add the stuff under testing
        app.UseAuthentication();

        app.MapControllers();

        app.Run();
    }
}