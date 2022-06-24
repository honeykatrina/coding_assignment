using UserAccountManagement.TransactionModule.Mappings;
using UserAccountManagement.TransactionModule.Repositories;
using UserAccountManagement.TransactionModule.Services;
using UserAccountManagement.UserModule.Mappings;
using UserAccountManagement.UserModule.Repositories;
using UserAccountManagement.UserModule.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddAutoMapper(new[] { typeof(UserProfile), typeof(TransactionProfile) });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
