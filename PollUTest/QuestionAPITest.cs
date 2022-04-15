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
    public class QuestionAPITest
    {
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
        }

        [Test, Order(1)]
        public async Task Insert()
        {
            var newUser = new Question
            {
                Text = "QUESTION 01",
                QuestionType = Poll.Enums.TypesOfQuestion.QuestionTypes.SingleLineTextBox.ToString(),
                Options = "BATATINHA;123"
            };

            var json = JsonSerializer.Serialize(newUser);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://localhost:5014/api/question/";
            var response = await _httpClient.PostAsync(url, data);
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