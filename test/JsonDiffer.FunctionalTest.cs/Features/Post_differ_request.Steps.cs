using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using JsonDiffer.Domain.Entities;
using JsonDiffer.FunctionalTest.Setup;
using LightBDD.XUnit2;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;
using static JsonDiffer.FunctionalTest.Setup.JsonDifferScenarioBase;

namespace JsonDiffer.FunctionalTest.Features
{
    public partial class Post_differ_request : FeatureFixture
    {
        private string _id;
        private string _left;
        private string _right;
        public DiffJson DiffJsonRequested {
            get => new DiffJson(_id) { Left = _left, Right = _right };
        }
        private TestServer _service;
        private ICollection<DiffJson> _collection;
        private HttpResponseMessage _response;


        private void Given_a_diff_id(string id)
        {
            _id = id;
            var scenarioBase = new JsonDifferScenarioBase();
            var webHostBuilder = BuildWebHost();
            _service = scenarioBase.CreateServer(webHostBuilder);
            _collection = _service.Host.Services.GetService<ICollection<DiffJson>>();
        }
        private void Then_should_receive_a_message_that_they_are_equal()
        {
            dynamic result = JsonConvert.DeserializeObject<dynamic>(_response.Content.ReadAsStringAsync().Result);
            Assert.True((bool)result.areEqual);
        }
        private void Then_should_receive_message_that_are_from_different()
        {
            dynamic result = JsonConvert.DeserializeObject<dynamic>(_response.Content.ReadAsStringAsync().Result);
            Assert.False((bool)result.areEqual);
        }
        private void Then_should_specify_the_offset_and_length_of_diff(int offset, int length)
        {
            dynamic result = JsonConvert.DeserializeObject<dynamic>(_response.Content.ReadAsStringAsync().Result);
            var diffResults = (IEnumerable<dynamic>)result.diffResults;
            Assert.Single(diffResults);
            Assert.Contains(diffResults, r => r.offset==offset&&r.length==length);
        }
        private void Then_should_ba_receive_a_ok_message()
        {
            Assert.True(_response.IsSuccessStatusCode);
        }

        private async void When_diff_is_requested()
        {
            _collection.Add(DiffJsonRequested);
            var content = new StringContent("", Encoding.ASCII, "text/json");
            _response = await _service.CreateClient()
                .PostAsync(Post.Diff(_id), content);
        }

        private void Given_a_posted_right_side(string leftJson)
        {
            _left = leftJson;
        }

        private void Given_a_posted_left_side(string rightJson)
        {
            _right = rightJson;
        }
    }
}
