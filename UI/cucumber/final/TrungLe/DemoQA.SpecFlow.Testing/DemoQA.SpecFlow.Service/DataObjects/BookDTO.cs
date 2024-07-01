using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.SpecFlow.Service.DataObjects
{
    public class BookDTO
    { 
        [JsonProperty("isbn")]
        public string ISBN { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }     
    }
}
