using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenders
{
  internal class Tender
  {
    public string id { get; set; }
    public string date { get; set; }
    public string title { get; set; }
    public string category { get; set; }
  }

  internal class Tenders
  {
    public Tenders() => TendersList = new List<Tender>();

    [JsonProperty("data")]
    public List<Tender> TendersList { get; set; }

    public string page_number { get; set; }
  }
}
