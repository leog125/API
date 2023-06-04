using API.Models;
using Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionstring = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<Test_MAUContext>(x => x.UseSqlServer(connectionstring));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TestAPI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

});


builder.Services.AddAuthentication(); builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/Security/CreateToken", (Test_MAUContext db, User user) =>
{
    API.Business.Security security = new();
    var result = security.CreateToken(user);
    if (!string.IsNullOrEmpty(result))
        return Results.Ok(result);
    else
        return Results.Unauthorized();
}).WithTags("Security");

app.MapPost("/Property/CreateProperty", (Test_MAUContext db, OCreateProperty property) =>
{
    API.Business.Propertys property_ = new();
    var result = property_.CreateProperty(property);
    if (result == API.Enumn.ECreateProperty.Owner_Not_Exists)
        return Results.NotFound("Owner not exists");
    else
        return Results.Ok(property.Name + " created");

}).WithTags("Property").RequireAuthorization();

app.MapPost("/Property/AddImageProperty", (Test_MAUContext db, OAddImage image) =>
{
    API.Business.Propertys property_ = new();
    var result = property_.AddImageProperty(image);
    if (result == API.Enumn.EAddImageProperty.Property_Not_Exists)
        return Results.NotFound("Owner not exists");
    else if (result == API.Enumn.EAddImageProperty.Property_With_Image)
        return Results.Conflict("Property with image");
    else
        return Results.Ok("Add Image");
}).WithTags("Property").RequireAuthorization();

app.MapPut("/Property/ChangePriceProperty", (Test_MAUContext db, OChangePrice changePrice) =>
{
    API.Business.Propertys property_ = new();
    var result = property_.ChangePriceProperty(changePrice);
    if (result == API.Enumn.EChangePriceProperty.Property_Not_Exists)
        return Results.NotFound("Propertie not exists");
    else
        return Results.Ok("Update price property id: " + changePrice.IdProperty);
}).WithTags("Property").RequireAuthorization();

app.MapPut("/Property/UpdateProperty", (Test_MAUContext db, OProperty property) =>
{
    API.Business.Propertys property_ = new();
    var result = property_.UpdateProperty(property);
    if (result == API.Enumn.EUpdateProperty.Property_Not_Exists)
        return Results.NotFound("Propertie not exists");
    else if (result == API.Enumn.EUpdateProperty.Owner_Not_Exists)
        return Results.NotFound("Owner not exists");
    else
        return Results.Ok("update property id: " + property.IdProperty);
}).WithTags("Property").RequireAuthorization();

app.MapPost("/Property/ListPropertybyFilter", (Test_MAUContext db, OPropertyFilter filter) =>
{
    API.Business.Propertys property_ = new();
    return property_.ListPropertybyFilter(filter);
}).WithTags("Property").RequireAuthorization();


app.Run();

