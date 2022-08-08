using DataLoad.Application.Interfaces;
using DataLoad.Application.Transformer;
using HawkerSearch.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace DataLoad.Application.DataLoader
{
    public class GeojsonDataLoader : IGeojsonDataLoader
    {
        protected readonly AppSettings _settings;
        protected readonly IFileReader _fileReader;
        protected readonly IGeojsonTransformer _transformer;
        protected readonly IHawkerRepository _repository;
        protected readonly ILogger<GeojsonDataLoader> _logger;

        public GeojsonDataLoader(IOptions<AppSettings> settings,
            IFileReader fileReader,
            IGeojsonTransformer transformer,
            IHawkerRepository repository,
            ILogger<GeojsonDataLoader> logger)
        {
            _settings = settings.Value;
            _fileReader = fileReader;
            _transformer = transformer;
            _repository = repository;
            _logger = logger;
        }

        public async Task LoadData()
        {
            var timer = new Stopwatch();
            timer.Start();

            _logger.LogInformation($"Loading data from {_settings.GeojsonPath}.");
            var features = _fileReader.GetFeatures(_settings.GeojsonPath);
            if (!features.Any())
                throw new Exception("File is empty.");

            ConcurrentBag<Hawker> hawkers = new ConcurrentBag<Hawker>();
            Parallel.ForEach(features, (feature) =>
            {
                try
                {
                    var hawker = _transformer.Transform(feature);
                    hawkers.Add(hawker);
                }
                catch (TransformException e)
                {
                    _logger.LogError(e.Message);
                }
            });

            if (!hawkers.Any())
                throw new Exception("No data to insert.");

            await _repository.BulkAddAsync(hawkers);

            timer.Stop();
            _logger.LogInformation($"Data loading complete. Time Elapsed : {timer.Elapsed}");
        }
    }
}
