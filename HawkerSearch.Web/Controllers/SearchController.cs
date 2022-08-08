using HawkerSearch.Web.Interfaces;
using HawkerSearch.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HawkerSearch.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly ILocationService _locationService;

        public SearchController(ILogger<SearchController> logger,
            ILocationService locationService)
        {
            _logger = logger;
            _locationService = locationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ModelState.Clear();
            return View(new SearchViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchViewModel viewModel)
        {
            var result = await _locationService.GetNearestHawkers(longitude: viewModel.CurrentLongitude, latitude: viewModel.CurrentLatitude);
            viewModel.Results = result;
            viewModel.ShowResults = true;
            return View(viewModel);
        }
    }
}