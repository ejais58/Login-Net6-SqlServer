using AplicationCore.interfaces;
using AplicationCore.interfaces.IService;
using Autofac;
using Infraestructura.Dao;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApplication1.Service;
using Microsoft.AspNetCore.Hosting;
using AplicationCore.interfaces.IDao;

var builder = WebApplication.CreateBuilder(args);

var containerBuilder = new ContainerBuilder();

var config = builder.Configuration;




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4000",
                                              "http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});
//configuracion Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Configuración de la seguridad con JWT
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "JWT Authorization header using the Bearer scheme."
        

    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    };
    c.AddSecurityRequirement(securityRequirement);
});

//inyeccion de dependecia de interfaces
builder.Services.AddScoped<IUsersDao, UsersDao>();
builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddScoped<ITurnosDao, TurnosDao>();
builder.Services.AddScoped<ITurnosService, TurnosService>();

builder.Services.AddScoped<IMascotasDao, MascotasDao>();

//configuracion de autenticacion
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
