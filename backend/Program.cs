using Microsoft.Extensions.Azure;
using UserAccountManagement.Shared.Models;
using UserAccountManagement.Shared.ServiceBusServices;
using UserAccountManagement.Transactions.Mappings;
using UserAccountManagement.Transactions.Repositories;
using UserAccountManagement.Transactions.Services;
using UserAccountManagement.Users.Mappings;
using UserAccountManagement.Users.Repositories;
using UserAccountManagement.Users.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
builder.Services.AddSingleton<IProcessDataService<CreateTransaction>, ProcessCreateTransactionMessageService>();
builder.Services.AddSingleton<IMessageSender, TransactionMessageSenderService>();
builder.Services.AddSingleton<IServiceBusSenderService, ServiceBusSenderService>();
builder.Services.AddHostedService<TransactionMessageProcessorHostedService>();

builder.Services.Configure<QueueSettings>(builder.Configuration.GetSection("QueueConfiguration"));

builder.Services.AddAutoMapper(new[] { typeof(UserProfile), typeof(TransactionProfile) });
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder
        .AddServiceBusClient(builder.Configuration.GetConnectionString("ServiceBus"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
