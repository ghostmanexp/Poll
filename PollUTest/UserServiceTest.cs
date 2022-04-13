using NUnit.Framework;
using Poll;
using Poll.Interfaces;
using Poll.Models;
using Poll.Services;
using Poll.ViewModels;
using SQLite;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PollUTest
{
    [TestFixture]
    public class UserServiceTest
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
            //var newUser = new UserViewModel
            //{
            //    role = new Role { Id = 1, Name = "Adm" },
            //    user = new User { Id = 2, UserName = "ghostman", Password = "123", RoleId = 1 }
            //};

            //var response = await _httpClient.PostAsync(JsonSerializer.Serialize(newUser), HttpContent. "http://localhost:5014/api/user/");
            //Assert.IsFalse(response.IsSuccessStatusCode);

            //var responseText = await response.Content.ReadAsStringAsync();
            //Assert.IsNotEmpty(responseText);

            //var result = JsonSerializer.Deserialize<List<Poll.Models.Role>>(responseText, new JsonSerializerOptions()
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //});
            //Assert.NotNull(result);
            //Assert.IsNotNull(lst);

            Assert.Pass();
        }
    }
}