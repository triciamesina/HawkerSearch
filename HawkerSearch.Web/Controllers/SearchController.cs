using HawkerSearch.Web.Interfaces;
using HawkerSearch.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HawkerSearch.Web.Controllers
{
    public class SearchController : Controller
    {
        private const string ResultsSessionKey = "Results";
        private const string CurrentLatitudeSessionKey = "CurrentLatitude";
        private const string CurrentLongitudeSessionKey = "CurrentLongitude";

        private readonly ILogger<SearchController> _logger;
        private readonly ILocationService _locationService;
        private readonly ISessionManager _sessionManager;

        public SearchController(ILogger<SearchController> logger,
            ILocationService locationService,
            ISessionManager sessionManager)
        {
            _logger = logger;
            _locationService = locationService;
            _sessionManager = sessionManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ClearSearchSessionItems();
            return View(new SearchViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchViewModel viewModel)
        {
            var result = await _locationService.GetNearestHawkers(longitude: viewModel.CurrentLongitude, latitude: viewModel.CurrentLatitude);
            _sessionManager.SetObject(ResultsSessionKey, result);
            _sessionManager.SetObject(CurrentLongitudeSessionKey, viewModel.CurrentLongitude);
            _sessionManager.SetObject(CurrentLatitudeSessionKey, viewModel.CurrentLatitude);
            return RedirectToAction("Results");
        }

        [HttpGet]
        public IActionResult Results()
        {
            var results = _sessionManager.GetObject<IEnumerable<HawkerViewModel>>(ResultsSessionKey);
            if (results == null || !results.Any())
                return RedirectToAction("Index");

            var viewModel = new SearchViewModel
            {
                Results = results,
                CurrentLongitude = _sessionManager.GetObject<double>(CurrentLongitudeSessionKey),
                CurrentLatitude = _sessionManager.GetObject<double>(CurrentLatitudeSessionKey)
            };
            return View(viewModel);
        }

        private void ClearSearchSessionItems()
        {
            _sessionManager.RemoveObject(ResultsSessionKey);
            _sessionManager.RemoveObject(CurrentLongitudeSessionKey);
            _sessionManager.RemoveObject(CurrentLatitudeSessionKey);
        }
    }
}