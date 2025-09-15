using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenders
{
    public class Tenders
    {
        public Tenders() => TendersList = new List<Tender>();

        [JsonProperty("data")]
        public List<Tender> TendersList { get; set; }

        public string page_number { get; set; }
    }

    public class Tender
    {
        public string id { get; set; }
        public string date { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string awarded_value_eur { get; set; }
        public List<Awarded> awarded { get; set; }
    }

    public class Awarded
    {
        public string date { get; set; }
        public string suppliers_id { get; set; }
        public List<Suppliers> suppliers { get; set; } 
    }

    public class Suppliers
    {
        public string id { get; set; }
        public string name { get; set; }
    }

}
