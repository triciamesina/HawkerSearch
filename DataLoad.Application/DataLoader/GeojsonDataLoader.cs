using DataLoad.Application.Interfaces;
using DataLoad.Application.Transformer;
using HawkerSearch.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            _logger.LogInformation($"Loading data from {_settings.GeojsonPath}");
            var features = _fileReader.GetFeatures(_settings.GeojsonPath);
            if (!features.Any())
                throw new Exception("File is empty.");

            foreach (var feature in features)
            {
                try
                {
                    var hawker = _transformer.Transform(feature);
                    await _repository.AddAsync(hawker);
                }
                catch (TransformException e)
                {
                    _logger.LogError(e.Message);
                }
            }

            _logger.LogInformation("Data loading complete.");
        }
    }
}
