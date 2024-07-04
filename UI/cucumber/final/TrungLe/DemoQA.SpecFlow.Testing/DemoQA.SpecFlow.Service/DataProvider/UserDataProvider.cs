using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Core.Util;
using TMS_SpecFlow_Testing.DataObjects;

namespace DemoQA.SpecFlow.Service.DataProvider
{
    public class UserDataProvider
    {
        private readonly Dictionary<string, AccountDTO> _accounts;

        public UserDataProvider(string accountFilePath)
        {
            _accounts = JsonFileUtility.ReadAndParse<Dictionary<string, AccountDTO>>(accountFilePath);
        }

        public AccountDTO GetAccount(string key)
        {
            return _accounts.ContainsKey(key) ? _accounts[key] : null;
        }
    }
}
