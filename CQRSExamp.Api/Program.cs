using CQRSExamp.Api.Middlewares;
using CQRSExamp.Application;
using CQRSExamp.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(
        opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
    .AddJwtBearer(
        opt =>
        {
            var clockSkew = builder.Configuration["CQRSToken:ClockSkew"];

            var issuers = new List<string>
            {
                builder.Configuration["CQRSToken:Issuer"]
            };

            var audiences = new List<string>
            {
                builder.Configuration["CQRSToken:Audience"]
            };

            SecurityKey accessTokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["CQRSToken:AccessTokenKey"]));

            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuers = issuers,

                ValidateAudience = true,
                ValidAudiences = audiences,

                ValidateIssuerSigningKey = true,
                IssuerSigningKeys = new List<SecurityKey> { accessTokenKey },

                ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(clockSkew))
            };

        });


builder.Services.AddApplicationService();
builder.Services.AddPersistenceService(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandleMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
