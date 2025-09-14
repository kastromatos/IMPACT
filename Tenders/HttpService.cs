using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tenders
{
  internal class HttpService
  {
    private static readonly HttpClient client = new HttpClient();

    static HttpService()
    {
      client.BaseAddress = new Uri("https://tenders.guru/api/pl/");
    }

    public static async Task<Tenders> GetTenders()
    {
      HttpResponseMessage response = client.GetAsync("tenders").Result;
      string jsonBody = await response.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<Tenders>(jsonBody);
    }
  }
}
