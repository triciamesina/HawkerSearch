using System.ComponentModel.DataAnnotations;

namespace HawkerSearch.Web.Models
{
    public class SearchViewModel
    {
        private const string CoordinatesRegex = "(-?\\d+\\.\\d+)+";

        [Required]
        [RegularExpression(CoordinatesRegex, ErrorMessage = "Input must be numeric.")]
        [Range(-90, 90, ErrorMessage = "Invalid value for Latitude.")]
        public double CurrentLatitude { get; set; }

        [Required]
        [RegularExpression(CoordinatesRegex, ErrorMessage = "Input must be numeric.")]
        [Range(-180, 180, ErrorMessage = "Invalid value for Longitude.")]
        public double CurrentLongitude { get; set; }

        public IEnumerable<HawkerViewModel> Results { get; set; }
    }
}
