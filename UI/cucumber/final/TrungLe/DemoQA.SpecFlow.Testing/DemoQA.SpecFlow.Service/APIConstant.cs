using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.SpecFlow.Service
{
    public class APIConstant
    {
        //BookStore Endpoint
        public const string AddBookEndPoint = "/BookStore/v1/Books";
        public const string DeleteBookEndPoint = "/BookStore/v1/Book";

        //Account Endpoint
        public const string GetUserDetailEndPoint = "/Account/v1/User/{0}";
        public const string GenerateTokenEndPoint = "/Account/v1/GenerateToken";
        public const string AuthorizedUserEndPoint = "/Account/v1/Authorized";
    }
}
