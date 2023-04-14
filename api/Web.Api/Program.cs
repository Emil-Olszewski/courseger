using Core.Application;
using Infrastructure.Persistence;
using Web.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddApplicationLayer();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();