// See https://aka.ms/new-console-template for more information
using DataLoad.Application.DataLoader;
using DataLoad.Application.Interfaces;
using DataLoad.Application.Mapper;
using DataLoad.Application.Transformer;
using HawkerSearch.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services
            .AddTransient<IGeojsonDataLoader, GeojsonDataLoader>()
            .AddTransient<IGeojsonTransformer, GeojsonTransformer>()
            .AddTransient<IFileReader, FileReader>()
            .AddTransient<IHawkerMapper, HawkerMapper>()
            .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
            .AddScoped<IHawkerRepository, HawkerRepository>()
            .AddDbContext<HawkerContext>(options =>
                options.UseSqlServer(config.Get<AppSettings>().ConnectionString, x => x.UseNetTopologySuite()))
            .AddOptions()
            .Configure<AppSettings>(config))
    .Build();

var serviceProvider = host.Services.CreateScope().ServiceProvider;
var dataLoader = serviceProvider.GetService<IGeojsonDataLoader>();

await dataLoader.LoadData();