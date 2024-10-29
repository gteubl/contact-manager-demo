using ContactManagerDemo.Application.Queries.Contacts;
using ContactManagerDemo.Application.Services;
using ContactManagerDemo.Infrastructure.DataContext;
using ContactManagerDemo.Infrastructure.Seeds;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDataConnection"), sqlOptions => sqlOptions.EnableRetryOnFailure());
});



builder.Services.AddApplicationServices();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetContactsQuery).Assembly));

var origins = builder.Configuration.GetValue<string>("AllowOrigins").Split(";");

builder.Services.AddCors(
    options =>
    {
        options.AddDefaultPolicy(
            corsPolicyBuilder => corsPolicyBuilder.WithOrigins(origins)
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("Content-Disposition")
                .WithExposedHeaders("Token-Expired")
        );
    }
);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = false;
    options.SerializerOptions.PropertyNamingPolicy = null;
    options.SerializerOptions.WriteIndented = true;
});


var app = builder.Build();

var runMigrations = builder.Configuration.GetValue<bool>("AppStartupConfig:RunMigrations");

if (runMigrations)
{
    Console.WriteLine("Migrating database...");
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDataContext>();
    context.Database.Migrate();
    Console.WriteLine("Database migrated successfully");
    
    var seedData = builder.Configuration.GetValue<bool>("AppStartupConfig:SeedTestData");
    
    if (seedData)
    {
        Console.WriteLine("Seeding database with test data...");
        context.SeedTestData();
        Console.WriteLine("Database seeded successfully");
    }
    else
    {
     
        Console.WriteLine("Database seeding skipped");
    }
    
}


if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
