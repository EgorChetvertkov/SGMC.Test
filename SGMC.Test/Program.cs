using SGMC.Test;
using SGMC.Test.DB;
using SGMC.Test.Application;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services
    .AddPersistence(builder.Configuration)
    .AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();
