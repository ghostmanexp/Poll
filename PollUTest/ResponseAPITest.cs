using Models.ViewModels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PollUTest
{
    [TestFixture]
    public class ResponseAPITest
    {
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
        }

        private const string httpAddress = "http://localhost:5014/api/pollresponse/";

        [Test, Order(1)]
        public async Task Insert()
        {
            var newUser = new PollResponseViewModel
            {
                poll = new Poll.Models.Poll
                {
                    Id = 0
                },
                question = new Poll.Models.Question
                {
                    Id = 0
                },
                respose = new Poll.Models.PollRespose
                {
                    Response = "MARCO"
                },
                user = new Poll.Models.User
                {
                    UserName = "ghostman",
                    Password = "123"
                }
            };

            var json = JsonSerializer.Serialize(newUser);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(httpAddress, data);
            Assert.IsTrue(response.IsSuccessStatusCode);

            Assert.Pass();
        }

        [Test, Order(2)]
        public async Task GetAll()
        {
            var response = await _httpClient.GetAsync("http://localhost:5014/api/question/");
            Assert.True(response.IsSuccessStatusCode);

            var responseText = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseText);

            var result = JsonSerializer.Deserialize<List<Poll.Models.Question>>(responseText, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Assert.NotNull(result);
        }
    }
}