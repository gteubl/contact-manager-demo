using AutoMapper;
using ContactManagerDemo.Application.Mappers;
using ContactManagerDemo.Application.Queries.Contacts;
using ContactManagerDemo.Infrastructure.DataContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactManagerDemo.Tests;

public abstract class TestBase
{
    protected readonly IConfigurationRoot Config;
    protected readonly AppDataContext AppDataContext;
    protected readonly IServiceProvider ServiceProvider;
    protected readonly IMapper Mapper;
    protected readonly IMediator Mediator;

    protected TestBase()
    {
        // Config
        Config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .AddEnvironmentVariables()
            .Build();

        // DB
        var appDataConnectionString = Config.GetConnectionString("SqlDataConnection");
        var optionsBuilder = new DbContextOptionsBuilder<AppDataContext>();
        AppDataContext = new AppDataContext(optionsBuilder.UseSqlServer(appDataConnectionString).Options);

        // Mapper
        var mapperConfig = DtoToEntityMappers.GetMapperConfig();
        Mapper = mapperConfig.CreateMapper();
        
        // ServiceProvider
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IConfiguration>(Config);
        serviceCollection.AddSingleton(Mapper);
        serviceCollection.AddScoped(_ => AppDataContext);
        
        // Mediator
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetContactsQuery).Assembly));
        
        
        ServiceProvider = serviceCollection.BuildServiceProvider();
        
        Mediator = ServiceProvider.GetRequiredService<IMediator>();
    }
    
    [OneTimeSetUp]
    public void TestMapperConfiguration()
    {
        Mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }


    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        AppDataContext.Dispose();
        if (ServiceProvider is IDisposable disposableServiceProvider)
        {
            disposableServiceProvider.Dispose();
        }
        
        if(Config is IDisposable disposableConfig)
        {
            disposableConfig.Dispose();
        }
    }
    
}