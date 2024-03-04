using Microsoft.Extensions.DependencyInjection;
using NoteLinqApp.Infrastructure.Databases.InMemory;
using Meteors.AspNetCore.Domain.ConfigureServices;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using NoteLinqApp.Infrastructure.Models;
using Meteors.AspNetCore.Service.DependencyInjection;
using Meteors.AspNetCore.Infrastructure.ModelEntity.Interface;
using Meteors.AspNetCore.Service.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



builder.Services.AddMrDbContext<NoteLinqAppInMemoryDbContext>(
(options) =>
{
    //usesqlserver
    options.UseInMemoryDatabase("NoteLinqAppDB");
});




builder.Services.AddIdentity<Account, IdentityRole<Guid>>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<NoteLinqAppInMemoryDbContext>().AddDefaultTokenProviders().AddRoles<IdentityRole<Guid>>();



// for more: https://github.com/MhozaifaA/Meteors.DependencyInjection.AutoService
builder.Services.AddAutoService(BoundedContextRepositoriesSecurityAssembly.Assembly);
	

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{

    //options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:IssuerAudience"],
        ValidAudience = builder.Configuration["Jwt:IssuerAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
    };
});


builder.Services.AddMrSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMrSwagger();
}


app.MapGet("/", () => "hello");


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseCors(b =>
{

    var org = builder.Configuration.GetSection("WithOrigins").Get<string[]>();
    if (org.Contains("*"))
        b.AllowAnyHeader().AllowAnyMethod()
    .SetIsOriginAllowed((host) => true).AllowCredentials();
    else
        b.AllowAnyOrigin().WithOrigins(org).AllowAnyMethod()
               .AllowAnyHeader().AllowCredentials();
    //  b.Build().SupportsCredentials = true;
}
);

app.UseAuthorization();

app.MapControllers();


app.UseDbContextSeed<NoteLinqAppInMemoryDbContext>(async (context, provider) =>
{
    await context.AccountsSeedAsync(provider);

    app.DisposeDbContextSeed();
});


app.Run();
