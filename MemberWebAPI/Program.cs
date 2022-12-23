using MemberWebAPI.DataAccess;
using MemberWebAPI.DbContexts;
using MemberWebAPI.GraphQL;
using MemberWebAPI.Interface;
using MemberWebAPI.Shared;
using MemberWebAPI.Shared.Interface;
using MemberWebAPI.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services
    .AddPooledDbContextFactory<EldPlatContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("EldPlatContext"))
    );

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IAuthLogin, AuthLoginDataAccessLayer>();
builder.Services.AddScoped<IUtility, Utilitys>();


//Token相關設定
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var tokenSettings = builder.Configuration.GetSection("TokenSettings").Get<TokenSettings>();
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,

            ValidIssuer = tokenSettings.Issuer,
            ValidAudience = tokenSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key)),
        };
    });

//使用者權限
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("roles-policy", policy =>
    {
        policy.RequireRole(new string[] { "admin", "super-admin" });
    });
});

builder.Services.AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType<QueryResolver>()
    .AddMutationType<MutationResolver>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.Run();
