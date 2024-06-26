using DB;
using DB.Repositories;
using DB.Repositories.Interfaces;
using DB.Services;
using DB.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RESTfulWebServices.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped(typeof(IBaseService<,,>), typeof(BaseService<,,>));
builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddDbContext<MyDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

// Dodanie obs�ugi XML
builder.Services.AddControllers().AddXmlSerializerFormatters();

// Dodanie http context accessor
builder.Services.AddHttpContextAccessor();
// Dodanie action context accessor
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//Middleware do generowania Hateoas
app.UseMiddleware<HateoasMiddleware>();

app.Run("https://localhost:8080");
