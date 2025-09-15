using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Tenders
{
    public class HttpService
    {
        private static readonly HttpClient client = new HttpClient();
        private const int MAX_PAGE = 100;

        static HttpService()
        {
            client.BaseAddress = new Uri("https://tenders.guru/api/pl/");
        }

        private readonly IMemoryCache _memoryCache;

        public HttpService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<Tenders> GetPageOfTenders(int page = 1)
        {
            if (page < 1 || page >= MAX_PAGE) return new Tenders();

            string cacheKey = $"Page_{page}";
            if (!_memoryCache.TryGetValue(cacheKey, out Tenders tenders))
            {
                HttpResponseMessage response = client.GetAsync($"tenders?page={page}").Result;
                string jsonBody = await response.Content.ReadAsStringAsync();
                tenders = JsonConvert.DeserializeObject<Tenders>(jsonBody);

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20),
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };

                _memoryCache.Set(cacheKey, tenders, cacheOptions);
            }

            return tenders;
        }

        public async Task<Tender> GetTenderById (int id)
        {
            Tender tender = new Tender();
            for (int page = 1; page < MAX_PAGE; page++)
            {
                var tenders = await GetPageOfTenders(page);
                tender = tenders.TendersList.FirstOrDefault(x => x.id == id);
                if (tender == null) { continue; } else { return tender; }
            }
            return tender;
        }
    }
}
