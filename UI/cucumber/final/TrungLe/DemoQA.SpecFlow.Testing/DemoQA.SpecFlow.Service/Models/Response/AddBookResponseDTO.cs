using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Service.Models.Response
{
    public class BookAdded
    {
        [JsonProperty("isbn")]
        public string Isbn { get; set; }
    }

    public class AddBookResponseDTO
    {
        [JsonProperty("books")]
        public List<BookAdded> Books { get; set; }
    }
}
