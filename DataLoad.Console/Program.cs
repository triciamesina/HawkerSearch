// See https://aka.ms/new-console-template for more information
using DataLoad.Application.DataLoader;
using DataLoad.Application.Interfaces;
using DataLoad.Application.Mapper;
using DataLoad.Application.Transformer;
using HawkerSearch.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;MultipleActiveResultSets=true", x => x.UseNetTopologySuite())))
    .Build();

var serviceProvider = host.Services.CreateScope().ServiceProvider;
var dataLoader = serviceProvider.GetService<IGeojsonDataLoader>();

await dataLoader.LoadData();