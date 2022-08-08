using DataLoad.Application.DataLoader;
using DataLoad.Application.Interfaces;
using DataLoad.Application.Transformer;
using DataLoad.UnitTests;
using DataLoad.UnitTests.Attributes;
using HawkerSearch.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NetTopologySuite.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HawkerSearch.UnitTests.DataLoader
{
    public class GeojsonDataLoaderTests
    {
        private readonly Mock<IOptions<AppSettings>> _mockAppSettings;
        private readonly Mock<IFileReader> _mockFileReader;
        private readonly Mock<IGeojsonTransformer> _mockTransformer;
        private readonly Mock<IHawkerRepository> _mockRepository;
        private readonly Mock<ILogger<GeojsonDataLoader>> _mockLogger;
        private IGeojsonDataLoader _sut;

        public GeojsonDataLoaderTests()
        {
            _mockAppSettings = new Mock<IOptions<AppSettings>>();
            _mockFileReader = new Mock<IFileReader>();
            _mockTransformer = new Mock<IGeojsonTransformer>();
            _mockRepository = new Mock<IHawkerRepository>();
            _mockLogger = new Mock<ILogger<GeojsonDataLoader>>();
        }

        [Theory]
        [AutoMoqData]
        public async void LoadData_CallsFileReader(string stubGeojsonPath)
        {
            // arrange
            var stubFeatures = new FeatureCollection();
            stubFeatures.Add(new Feature());
            _mockFileReader.Setup(x => x.GetFeatures(It.IsAny<string>())).Returns(stubFeatures);
            _mockAppSettings.SetupGet(x => x.Value).Returns(new AppSettings { GeojsonPath = stubGeojsonPath });
            _sut = new GeojsonDataLoader(_mockAppSettings.Object,
                _mockFileReader.Object,
                _mockTransformer.Object,
                _mockRepository.Object,
                _mockLogger.Object);

            // act
            await _sut.LoadData();

            // assert
            _mockFileReader.Verify(x => x.GetFeatures(stubGeojsonPath), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async void LoadData_AndFeaturesEmpty_ThrowsException(string stubGeojsonPath)
        {
            // arrange
            var stubFeatures = new FeatureCollection();
            _mockFileReader.Setup(x => x.GetFeatures(It.IsAny<string>())).Returns(stubFeatures);
            _mockAppSettings.SetupGet(x => x.Value).Returns(new AppSettings { GeojsonPath = stubGeojsonPath });
            _sut = new GeojsonDataLoader(_mockAppSettings.Object,
                _mockFileReader.Object,
                _mockTransformer.Object,
                _mockRepository.Object,
                _mockLogger.Object);

            // act - assert
            await Assert.ThrowsAsync<Exception>(async () => { await _sut.LoadData(); });
        }

        [Theory]
        [AutoMoqData]
        public async void LoadData_AndFeaturesEmpty_DoesNotCallRepository(string stubGeojsonPath)
        {
            // arrange
            var stubFeatures = new FeatureCollection();
            _mockFileReader.Setup(x => x.GetFeatures(It.IsAny<string>())).Returns(stubFeatures);
            _mockAppSettings.SetupGet(x => x.Value).Returns(new AppSettings { GeojsonPath = stubGeojsonPath });
            _sut = new GeojsonDataLoader(_mockAppSettings.Object,
                _mockFileReader.Object,
                _mockTransformer.Object,
                _mockRepository.Object,
                _mockLogger.Object);

            // act
            try
            {
                await _sut.LoadData();
            }
            catch { }

            // assert
            _mockRepository.Verify(x => x.BulkAddAsync(It.IsAny<IEnumerable<Hawker>>()), Times.Never);
        }

        [Theory]
        [AutoMoqData]
        public async void LoadData_CallsTransformer(string stubGeojsonPath)
        {
            // arrange
            var stubFeatures = new FeatureCollection();
            var stubFeature = new Feature();
            stubFeatures.Add(stubFeature);
            _mockFileReader.Setup(x => x.GetFeatures(It.IsAny<string>())).Returns(stubFeatures);
            _mockAppSettings.SetupGet(x => x.Value).Returns(new AppSettings { GeojsonPath = stubGeojsonPath });
            _sut = new GeojsonDataLoader(_mockAppSettings.Object,
                _mockFileReader.Object,
                _mockTransformer.Object,
                _mockRepository.Object,
                _mockLogger.Object);

            // act
            await _sut.LoadData();

            // assert
            _mockTransformer.Verify(x => x.Transform(stubFeature), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async void LoadData_CallsRepository(string stubGeojsonPath)
        {
            // arrange
            var stubFeatures = new FeatureCollection();
            var stubFeature = new Feature();
            stubFeatures.Add(stubFeature);
            _mockFileReader.Setup(x => x.GetFeatures(It.IsAny<string>())).Returns(stubFeatures);
            _mockAppSettings.SetupGet(x => x.Value).Returns(new AppSettings { GeojsonPath = stubGeojsonPath });
            _mockTransformer.Setup(x => x.Transform(It.IsAny<Feature>())).Returns(TestConstants.TestHawker);
            _sut = new GeojsonDataLoader(_mockAppSettings.Object,
                _mockFileReader.Object,
                _mockTransformer.Object,
                _mockRepository.Object,
                _mockLogger.Object);

            // act
            await _sut.LoadData();

            // assert
            _mockRepository.Verify(x => x.BulkAddAsync(It.Is<IEnumerable<Hawker>>(x => x.Contains(TestConstants.TestHawker))), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async void LoadData_AndTransformerReturnsEmpty_ThrowsException(string stubGeojsonPath)
        {
            // arrange
            var stubFeatures = new FeatureCollection();
            _mockFileReader.Setup(x => x.GetFeatures(It.IsAny<string>())).Returns(stubFeatures);
            _mockAppSettings.SetupGet(x => x.Value).Returns(new AppSettings { GeojsonPath = stubGeojsonPath });
            _mockTransformer.Setup(x => x.Transform(It.IsAny<Feature>())).Throws(new TransformException("Error"));
            _sut = new GeojsonDataLoader(_mockAppSettings.Object,
                _mockFileReader.Object,
                _mockTransformer.Object,
                _mockRepository.Object,
                _mockLogger.Object);

            // act - assert
            await Assert.ThrowsAsync<Exception>(async () => { await _sut.LoadData(); });
        }

        [Theory]
        [AutoMoqData]
        public async void LoadData_AndTransformerReturnsEmpty_DoesNotCallRepository(string stubGeojsonPath)
        {
            // arrange
            var stubFeatures = new FeatureCollection();
            _mockFileReader.Setup(x => x.GetFeatures(It.IsAny<string>())).Returns(stubFeatures);
            _mockAppSettings.SetupGet(x => x.Value).Returns(new AppSettings { GeojsonPath = stubGeojsonPath });
            _mockTransformer.Setup(x => x.Transform(It.IsAny<Feature>())).Throws(new TransformException("Error"));
            _sut = new GeojsonDataLoader(_mockAppSettings.Object,
                _mockFileReader.Object,
                _mockTransformer.Object,
                _mockRepository.Object,
                _mockLogger.Object);

            // act
            try
            {
                await _sut.LoadData();
            }
            catch { }

            // assert
            _mockRepository.Verify(x => x.BulkAddAsync(It.IsAny<IEnumerable<Hawker>>()), Times.Never);
        }

    }
}
