using DataLoad.Application.Enumerations;
using DataLoad.Application.Interfaces;
using DataLoad.Application.Transformer;
using Moq;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace DataLoad.UnitTests
{
    public class GeojsonTransformerTests
    {
        private readonly IGeojsonTransformer _sut;
        private readonly Mock<IHawkerMapper> _mockHawkerMapper;

        public GeojsonTransformerTests()
        {
            _mockHawkerMapper = new Mock<IHawkerMapper>();

            _sut = new GeojsonTransformer(_mockHawkerMapper.Object);
        }

        [Fact]
        public void Transform_CallsHawkerMapper()
        {
            // arrange
            var stubFeature = GeojsonTestHelper.GetTestFeature<Feature>(TestConstants.TestFeatureGeoJson);

            // act
            var actual = _sut.Transform(stubFeature);

            // assert
            _mockHawkerMapper.Verify(x => x.CreateHawker(TestConstants.TestFeatureId, It.IsAny<Geometry>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Fact]
        public void GetPropertyValues_ReturnsPropertyValuesDictionary()
        {
            // arrange
            var stubFeature = GeojsonTestHelper.GetTestFeature<Feature>(TestConstants.TestFeatureGeoJson);

            // act
            var actualProperties = _sut.GetAttributeValues(stubFeature);

            // assert
            string actualValue;
            Assert.True(actualProperties.TryGetValue(TestConstants.TestAttributeName, out actualValue));
            Assert.Equal(TestConstants.TestFeatureName, actualValue);
        }

        [Fact]
        public void GetPropertyValues_AndPropertiesNotFound_ReturnsEmptyDictionary()
        {
            // arrange
            var stubFeature = GeojsonTestHelper.GetTestFeature<Feature>(TestConstants.TestFeatureDescriptionEmpty);

            // act
            var actualProperties = _sut.GetAttributeValues(stubFeature);

            // assert
            string actualValue;
            Assert.False(actualProperties.TryGetValue(TestConstants.TestAttributeName, out actualValue));
            Assert.Null(actualValue);
        }

        [Fact]
        public void HasAttribute_AndAttributeFound_ReturnsTrue()
        {
            // arrange
            var propertyName = AttributeNameEnum.NAME.ToString();

            // act
            var result = _sut.HasAttribute(TestConstants.TestFeatureGeoJson, propertyName, out string actualValue);

            // assert
            Assert.True(result);
            Assert.Contains(TestConstants.TestFeatureName, actualValue);
        }

        [Fact]
        public void HasAttribute_AndAttributeNotFound_ReturnsFalse()
        {
            // arrange
            var propertyName = AttributeNameEnum.NAME.ToString();

            // act
            var result = _sut.HasAttribute(TestConstants.TestFeatureDescriptionEmpty, propertyName, out string actualValue);

            // assert
            Assert.False(result);
            Assert.Equal(string.Empty, actualValue);
        }
    }
}
