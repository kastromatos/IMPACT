using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Tenders
{
    public class TenderFunctions
    {
        private readonly ILogger<TenderFunctions> _logger;

        public TenderFunctions(ILogger<TenderFunctions> logger)
        {
            _logger = logger;
        }

        [Function("TendersList")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tenders")] HttpRequest req)
        {
            _logger.LogInformation("List function triggered.");
            var tenders = HttpService.GetTenders().Result;
            return new OkObjectResult(tenders);
        }
    }
}
