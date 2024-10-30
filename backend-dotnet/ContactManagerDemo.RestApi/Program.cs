using System.Text;
using ContactManagerDemo.Application.Authentication;
using ContactManagerDemo.Application.Queries.Contacts;
using ContactManagerDemo.Application.Services;
using ContactManagerDemo.Infrastructure.DataContext;
using ContactManagerDemo.Infrastructure.Seeds;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("/api/auth/token", UriKind.Relative),
                Scopes = new Dictionary<string, string>
                {
                    { "api1", "Access to my API" }
                }
            }
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                },
                Scheme = "oauth2",
                Name = "oauth2",
                In = ParameterLocation.Header
            },
            new List<string> { "api" }
        }
    });
});

builder.Services.AddDbContext<AppDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDataConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure());
});


builder.Services.AddApplicationServices();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetContactsQuery).Assembly));

var origins = builder.Configuration.GetValue<string>("AllowOrigins")!.Split(";");

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

var tokenKey = builder.Configuration.GetValue<string>("Token:Key");

if (tokenKey == null)
    throw new Exception("Token key not found in configuration");

var key = Encoding.ASCII.GetBytes(tokenKey);
builder.Services.AddAuthentication(
        auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    )
    .AddJwtBearer(
        options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Append("Token-Expired", "true");
                    }

                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    context.Response.OnStarting(async () =>
                    {
                        if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() ==
                            typeof(SecurityTokenExpiredException))
                        {
                            context.Response.StatusCode = 401;
                            await context.Response.WriteAsync("Token has expired");
                        }
                    });

                    return Task.CompletedTask;
                }
            };
        }
    );


builder.Services.AddScoped<IAuthenticationManager>(options =>
    new AuthenticationManager(tokenKey, builder.Configuration, options));


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

if (builder.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}


app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();