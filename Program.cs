using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using NotificationService.Messaging;
using NotificationService_Kisen;
using NotificationService_Kisen.Data;
using NotificationService_Kisen.Handlers;
using NotificationService_Kisen.Messaging;
using NotificationService_Kisen.Senders;
using NotificationService_Kisen.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var credentialFilePath = builder.Configuration["Firebase:ServiceAccountPath"];


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Notification Service API",
        Version = "v1"
    });

});

builder.Services.AddDbContext<NotificationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IEventBus, RabbitMqEventBus>();
builder.Services.AddHostedService<EventBusSubscriber>();
builder.Services.AddScoped<AlertCreatedHandler>();
builder.Services.AddHttpClient<ITelegramSender, HttpsTelegramSender>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Bot:BaseUrl"]);
});

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(credentialFilePath)
});

builder.Services.AddSingleton<IPushSender, FirebasePushSender>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification Service API v1");

    });
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
