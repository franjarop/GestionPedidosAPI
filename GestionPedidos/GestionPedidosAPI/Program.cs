using Aplication;
using Aplication.Behaviors;
using Aplication.Commands;
using Aplication.validartors;
using FluentValidation;
using GestionPedidosAPI.Middleware;
using Infraestructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.UseMiddleware<ExceptionMiddleware>(); // 👈 Importante: antes de MapControllers

app.MapControllers();

app.Run();
