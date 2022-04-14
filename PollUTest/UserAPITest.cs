using NUnit.Framework;
using Poll.Models;
using Poll.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PollUTest
{
    [TestFixture]
    public class UserAPITest
    {
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
        }

        [Test]
        public async Task Insert()
        {
            var newUser = new UserViewModel
            {
                role = new Role { Id = 0, Name = "Adm" },
                user = new User { Id = 0, UserName = "ghostman", Password = "123", RoleId = 0 }
            };

            var json = JsonSerializer.Serialize(newUser);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://localhost:5014/api/user/";
            var response = await _httpClient.PostAsync(url, data);
            Assert.IsTrue(response.IsSuccessStatusCode);

            Assert.Pass();
        }

        [Test]
        public async Task GetAll()
        {
            var response = await _httpClient.GetAsync("http://localhost:5014/api/user/");
            Assert.True(response.IsSuccessStatusCode);

            var responseText = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseText);

            var result = JsonSerializer.Deserialize<List<Poll.Models.User>>(responseText, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Assert.NotNull(result);
        }

        [Test]
        public async Task Get()
        {
            var response = await _httpClient.GetAsync("http://localhost:5014/api/user/0");
            Assert.True(response.IsSuccessStatusCode);

            var responseText = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseText);

            var result = JsonSerializer.Deserialize<Poll.Models.User>(responseText, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Assert.NotNull(result);
        }
    }
}