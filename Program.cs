using Microsoft.EntityFrameworkCore;
using NotificationService.Messaging;
using NotificationService_Kisen;
using NotificationService_Kisen.Data;
using NotificationService_Kisen.Handlers;
using NotificationService_Kisen.Messaging;
using NotificationService_Kisen.Senders;
using NotificationService_Kisen.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<NotificationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IEventBus, RabbitMqEventBus>();
builder.Services.AddHostedService<EventBusSubscriber>();
builder.Services.AddScoped<AlertCreatedHandler>();
builder.Services.AddTransient<ITelegramSender, ConsoleTelegramSender>();
builder.Services.AddTransient<IPushSender, ConsolePushSender>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
