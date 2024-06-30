using DemoQA.Service.Models.Request;
using DemoQA.Service.Models.Response;
using DemoQA.SpecFlow.Core.API;
using DemoQA.SpecFlow.Service;
using RestSharp;


namespace DemoQA.Service.Services
{
    public class BookService
    {
        private readonly APIClient _client;

        public BookService(APIClient client)
        {
            _client = client;
        }

        public async Task<RestResponse> DeleteBookFromCollectionAsync(string userId, string isbn, string token)
        {
            return await _client.CreateRequest(APIConstant.DeleteBookEndPoint)
                   .AddHeader("accept", ContentType.Json)
                   .AddHeader("Content-Type", ContentType.Json)
                   .AddAuthorizationHeader(token)
                   .AddBody(new BookRequestDTO
                   {
                       Isbn = isbn,
                       UserId = userId
                   }, ContentType.Json)
                   .ExecuteDeleteAsync();
        }

        public async Task<RestResponse<AddBookResponseDTO>> AddBookToCollectionAsync(string userId, string isbn, string token)
        {
            return await _client.CreateRequest(APIConstant.AddBookEndPoint)
                   .AddHeader("accept", ContentType.Json)
                   .AddHeader("Content-Type", ContentType.Json)
                   .AddAuthorizationHeader(token)
                   .AddBody(new AddBookRequestDTO
                   {
                       UserId = userId,
                       CollectionOfIsbns = new List<CollectionOfIsbn>
                       {
                           new CollectionOfIsbn
                           {
                               Isbn = isbn
                           }
                       }
                   }, ContentType.Json)
                   .ExecutePostAsync<AddBookResponseDTO>();
        }



        
    }
}
