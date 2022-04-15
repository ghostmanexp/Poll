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
    public class PollAPITest
    {
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
        }

        private const string httpAddress = "http://localhost:5014/api/poll/";

        [Test, Order(1)]
        public async Task Insert()
        {
            var newUser = new Poll.ViewModels.PollViewModel
            {
                poll = new Poll.Models.Poll
                {
                    Created = DateTime.Now,
                    Expires = DateTime.Now.AddDays(1),
                    Title = "WHAT IS MY NAME?",
                    Description = "WHAT IS MY REAL NAME?!!?"
                },
                user = new User
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
        public async Task InsertFail()
        {
            var newUser = new Poll.ViewModels.PollViewModel
            {
                poll = new Poll.Models.Poll
                {
                    Created = DateTime.Now,
                    Expires = DateTime.Now.AddDays(1),
                    Title = "WHAT IS MY NAME?",
                    Description = "WHAT IS MY REAL NAME?!!?"
                },
                user = new User
                {
                    UserName = "ghostman",
                    Password = "0"
                }
            };

            var json = JsonSerializer.Serialize(newUser);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(httpAddress, data);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Unauthorized);
        }

        [Test, Order(2)]
        public async Task GetAll()
        {
            var response = await _httpClient.GetAsync(httpAddress);
            Assert.True(response.IsSuccessStatusCode);

            var responseText = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseText);

            var result = JsonSerializer.Deserialize<List<Poll.Models.Poll>>(responseText, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Assert.NotNull(result);
        }

        [Test, Order(3)]
        public async Task GetPoll()
        {
            var response = await _httpClient.GetAsync($"{httpAddress}0");
            Assert.True(response.IsSuccessStatusCode);

            var responseText = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseText);

            var result = JsonSerializer.Deserialize<Poll.Models.Poll>(responseText, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Assert.NotNull(result);
        }

        [Test, Order(4)]
        public async Task GetFromUser()
        {
            var response = await _httpClient.GetAsync($"{httpAddress}GetAllFromUser/0");
            Assert.True(response.IsSuccessStatusCode);

            var responseText = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseText);

            var result = JsonSerializer.Deserialize<List<Poll.Models.Poll>>(responseText, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Assert.NotNull(result);
        }
    }
}