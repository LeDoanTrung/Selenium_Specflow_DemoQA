using DemoQA.SpecFlow.Service.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Core.Util;

namespace DemoQA.SpecFlow.Service.DataProvider
{
    public class BookDataProvider
    {
        private readonly Dictionary<string, BookDTO> _books;

        public BookDataProvider(string bookFilePath)
        {
            _books = JsonFileUtility.ReadAndParse<Dictionary<string, BookDTO>>(bookFilePath);
        }
        public BookDTO GetBook(string key)
        {
            return _books.ContainsKey(key) ? _books[key] : null;
        }
    }
}
