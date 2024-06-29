using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DemoQA_Selenium.DataObjects
{
    public class SearchKeyword
    {
        [JsonProperty("keyword")]
        public string Keyword { get; set; }
    }
}
