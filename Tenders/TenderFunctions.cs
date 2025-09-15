using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Tenders
{
    public class TenderFunctions
    {
        private readonly ILogger<TenderFunctions> _logger;
        private readonly HttpService _httpService;

        public TenderFunctions(HttpService httpService, ILogger<TenderFunctions> logger)
        {
            _logger = logger;
            _httpService = httpService;

        }

        [Function("FirstOrAnyPage")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tenders")] HttpRequest req)
        {
            _logger.LogInformation("Get tenders function triggered.");
            var tenders = Int32.TryParse(req.Query["page"], out int page) 
                                ? _httpService.GetPageOfTenders(page).Result
                                : _httpService.GetPageOfTenders().Result;
            return new OkObjectResult(tenders);
        }

    //[Function("GetById")]
    //public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tenders/{id}")] HttpRequest req, int id)
    //{
    //    _logger.LogInformation("GetById function triggered.");
    //    var tenders = HttpService.GetTenders().Result;
    //    return new OkObjectResult(tenders);
    //}
  }
}
