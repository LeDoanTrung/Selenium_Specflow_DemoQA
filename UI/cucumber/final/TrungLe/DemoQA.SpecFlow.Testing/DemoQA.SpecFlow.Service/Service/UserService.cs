using DemoQA.Service.Models.Request;
using DemoQA.Service.Models.Response;
using DemoQA.SpecFlow.Core.API;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_SpecFlow_Testing.DataObjects;

namespace DemoQA.SpecFlow.Service.Service
{
    public class UserService
    {
        private readonly APIClient _client;

        public UserService(APIClient client)
        {
            _client = client;
        }

        public async Task<RestResponse<UserResponseDTO>> GetUserDetailAsync(string userId, string token)
        {
            return await _client.CreateRequest(String.Format(APIConstant.GetUserDetailEndPoint, userId))
                   .AddHeader("accept", ContentType.Json)
                   .AddHeader("Content-Type", ContentType.Json)
                   .AddAuthorizationHeader(token)
                   .ExecuteGetAsync<UserResponseDTO>();
        }

        public async Task<RestResponse> GenerateTokenAsync(string username, string password)
        {
            return await _client.CreateRequest(APIConstant.GenerateTokenEndPoint)
                .AddHeader("accept", ContentType.Json)
                .AddHeader("Content-Type", ContentType.Json)
                .AddBody(new TokenRequestDTO
                {
                    UserName = username,
                    Password = password
                }, ContentType.Json)
                .ExecutePostAsync();
        }

        public async Task<RestResponse> TryHardGenerateTokenAsync(string username, string password, int loops)
        {
            int count = 0;
            RestResponse result = null;

            while (count < loops)
            {
                result = await this.GenerateTokenAsync(username, password);

                if (result.Content.Contains("User authorized successfully."))
                {
                    return result;
                }

                Thread.Sleep(4000);
                count++;
            }

            throw new Exception("Can not get token from API: " + result.Content);
        }
        
    }
}
