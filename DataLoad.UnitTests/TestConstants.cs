using HawkerSearch.Domain;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace DataLoad.UnitTests
{
    internal static class TestConstants
    {
        public const string TestFeatureGeoJson = @"{
            ""type"": ""Feature"",
            ""properties"": {
              ""Name"": ""kml_4"",
              ""Description"": ""<center><table><tr><th colspan='2' align='center'><em>Attributes</em></th></tr><tr bgcolor=\""#E3E3F3\""> <th>ADDRESSBLOCKHOUSENUMBER</th> <td>49A</td> </tr><tr bgcolor=\""\""> <th>LATITUDE</th> <td></td> </tr><tr bgcolor=\""#E3E3F3\""> <th>EST_ORIGINAL_COMPLETION_DATE</th> <td>1957</td> </tr><tr bgcolor=\""\""> <th>STATUS</th> <td>Existing</td> </tr><tr bgcolor=\""#E3E3F3\""> <th>CLEANINGSTARTDATE</th> <td></td> </tr><tr bgcolor=\""\""> <th>ADDRESSUNITNUMBER</th> <td></td> </tr><tr bgcolor=\""#E3E3F3\""> <th>ADDRESSFLOORNUMBER</th> <td></td> </tr><tr bgcolor=\""\""> <th>NO_OF_FOOD_STALLS</th> <td></td> </tr><tr bgcolor=\""#E3E3F3\""> <th>HYPERLINK</th> <td></td> </tr><tr bgcolor=\""\""> <th>REGION</th> <td></td> </tr><tr bgcolor=\""#E3E3F3\""> <th>APPROXIMATE_GFA</th> <td>2448.2</td> </tr><tr bgcolor=\""\""> <th>LONGITUDE</th> <td></td> </tr><tr bgcolor=\""#E3E3F3\""> <th>INFO_ON_CO_LOCATORS</th> <td></td> </tr><tr bgcolor=\""\""> <th>NO_OF_MARKET_STALLS</th> <td></td> </tr><tr bgcolor=\""#E3E3F3\""> <th>AWARDED_DATE</th> <td></td> </tr><tr bgcolor=\""\""> <th>LANDYADDRESSPOINT</th> <td>38356.53</td> </tr><tr bgcolor=\""#E3E3F3\""> <th>CLEANINGENDDATE</th> <td></td> </tr><tr bgcolor=\""\""> <th>PHOTOURL</th> <td>http://www.nea.gov.sg/images/default-source/Hawker-Centres-Division/resize_1262156095817.jpg</td> </tr><tr bgcolor=\""#E3E3F3\""> <th>DESCRIPTION</th> <td>HUP Rebuilding</td> </tr><tr bgcolor=\""\""> <th>NAME</th> <td>Serangoon Garden Market</td> </tr><tr bgcolor=\""#E3E3F3\""> <th>ADDRESSTYPE</th> <td>I</td> </tr><tr bgcolor=\""\""> <th>RNR_STATUS</th> <td></td> </tr><tr bgcolor=\""#E3E3F3\""> <th>ADDRESSBUILDINGNAME</th> <td></td> </tr><tr bgcolor=\""\""> <th>HUP_COMPLETION_DATE</th> <td>26/9/2002</td> </tr><tr bgcolor=\""#E3E3F3\""> <th>LANDXADDRESSPOINT</th> <td>31719.13</td> </tr><tr bgcolor=\""\""> <th>ADDRESSSTREETNAME</th> <td>Serangoon Garden Way</td> </tr><tr bgcolor=\""#E3E3F3\""> <th>ADDRESSPOSTALCODE</th> <td>555945</td> </tr><tr bgcolor=\""\""> <th>DESCRIPTION_MYENV</th> <td></td> </tr><tr bgcolor=\""#E3E3F3\""> <th>IMPLEMENTATION_DATE</th> <td></td> </tr><tr bgcolor=\""\""> <th>ADDRESS_MYENV</th> <td>49A, Serangoon Garden Way, Singapore 555945</td> </tr><tr bgcolor=\""#E3E3F3\""> <th>INC_CRC</th> <td>F3EA4B58784B2F78</td> </tr><tr bgcolor=\""\""> <th>FMEL_UPD_D</th> <td>20210330151704</td> </tr></table></center>""
            },
            ""geometry"": {
              ""type"": ""Point"",
              ""coordinates"": [103.866737484645995, 1.3631571201113, 0.0]
            }
          }";
        public const string TestFeatureId = "kml_4";
        public const string TestAttributeName = "NAME";
        public const string TestFeatureName = "Serangoon Garden Market";
        public const string TestFeatureDescriptionEmpty = @"{
            ""type"": ""Feature"",
            ""properties"": {
              ""Name"": ""kml_4"",
              ""Description"": ""<center><table><tr><th colspan='2' align='center'><em>Attributes</em></th></tr><tr bgcolor=\""#E3E3F3\""></tr></table></center>""
            },
            ""geometry"": {
              ""type"": ""Point"",
              ""coordinates"": [0, 0, 0.0]
            }
          }";
        public static Hawker TestHawker = new Hawker
        {
            Id = "kml_4",
            Name = "Serangoon Garden Market",
            Coordinate = new Point(103.866737484645995, 1.3631571201113),
            PostalAddress = "555945",
            Street = "Serangoon Garden Way",
            BlockNumber = "49A",
            PhotoUrl = "http://www.nea.gov.sg/images/default-source/Hawker-Centres-Division/resize_1262156095817.jpg"
        };
        public static Feature TestFeature = new Feature();
    }
}
