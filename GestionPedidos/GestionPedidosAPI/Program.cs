using Aplication;
using Aplication.Behaviors;
using Aplication.Commands;
using Aplication.validartors;
using Domain.Dtos;
using FluentValidation;
using GestionPedidosAPI.Middleware;
using Infraestructure;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Gestión de Pedidos",
        Version = "v1",
        Description = "API que permite gestionar pedidos, consultar su historial y controlar estados.",
    });

    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Autenticación JWT usando el esquema Bearer. Ejemplo: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
    });
});

// Inyección de dependencias
builder.Services.AddApplication();
builder.Services.AddInfraestructure(builder.Configuration);


builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreatePedidoCommand).Assembly));

builder.Services.AddValidatorsFromAssemblyContaining<CreatePedidoCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdatePedidoCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetPedidoByIdQuerieValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<DeletePedidoCommandValidator>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Settings")["SecretKey"]))
        };
    });

builder.Services.Configure<SettingsRequestDto>(builder.Configuration.GetSection("Settings")); 
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
   
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();


app.MapControllers();

app.Run();
