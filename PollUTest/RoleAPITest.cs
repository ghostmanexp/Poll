using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PollUTest
{
    public class RoleAPITest
    {
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
        }

        [Test]
        public async Task Test1()
        {
            var response = await _httpClient.GetAsync("http://localhost:5014/api/roles/");
            Assert.True(response.IsSuccessStatusCode);

            var responseText = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseText);

            var result = JsonSerializer.Deserialize<List<Poll.Models.Role>>(responseText, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Assert.NotNull(result);
        }

        [Test]
        public async Task Test2()
        {
            var response = await _httpClient.GetAsync("http://localhost:5014/api/roles/0");
            Assert.True(response.IsSuccessStatusCode);

            var responseText = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseText);

            var result = JsonSerializer.Deserialize<Poll.Models.Role>(responseText, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Assert.NotNull(result);
        }
    }
}
