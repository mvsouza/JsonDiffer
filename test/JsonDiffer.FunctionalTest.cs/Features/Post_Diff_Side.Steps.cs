using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using LightBDD.XUnit2;
using JsonDiffer.FunctionalTest.Setup;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;
using static JsonDiffer.FunctionalTest.Setup.JsonDifferScenarioBase;

namespace JsonDiffer.FunctionalTest.Features
{
    public partial class Post_Diff_Side : FeatureFixture
    {
        private IConfigurationRoot _configuration;
        private HttpResponseMessage _response;
        private string _documentOnBase64;
        private string _id;

        public Post_Diff_Side()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());
            _configuration = builder.Build();
        }

        private void Given_a_Json_document_on_base64(string document)
        {
            _documentOnBase64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(document));
        }

        private void Given_a_diff_id(string id)
        {
            _id = id;
        }

        private async void When_the_document_is_posted_as_left_side_diff()
        {
            var content = new StringContent(_documentOnBase64, Encoding.ASCII, "text/json");
            var scenarioBase = new JsonDifferScenarioBase();
            _response = await scenarioBase.CreateServer().CreateClient()
                .PostAsync(Post.DiffLeft(_id), content);
        }


        private async void When_the_document_is_posted_as_right_side_diff()
        {
            var content = new StringContent(_documentOnBase64, Encoding.ASCII, "text/json");
            var scenarioBase = new JsonDifferScenarioBase();
            _response = await scenarioBase.CreateServer().CreateClient()
                .PostAsync(Post.DiffRight(_id), content);
        }
        private void Then_should_receive_bad_request_message(string message)
        {
            _response.EnsureSuccessStatusCode();
            dynamic result = JsonConvert.DeserializeObject<dynamic>(_response.Content.ReadAsStringAsync().Result);
            Assert.False(_response.IsSuccessStatusCode);
            Assert.Equal(message, result.Message);
        }

        private void Then_should_receive_ok_message()
        {
            _response.EnsureSuccessStatusCode();
            Assert.True(_response.IsSuccessStatusCode);
        }
    }
}
