using System;
using System.Collections.Generic;
using System.Text;
using JsonDiffer.Domain.Entities;
using Xunit;

namespace JsonDiffer.UnitTest.Domain
{
    public class DiffJsonTests
    {
        private string _id;

        public DiffJsonTests()
        {
            _id = "teste";
        }
        [Fact]
        public void Should_return_that_the_jsons_they_are_equal()
        {
            var json = "{\"some\":\"json\"}";
            var diffJason = new DiffJson(_id) { Left =json, Right=json };
            var differResult = diffJason.Diff();
            Assert.True(differResult.AreEqual);
            Assert.Equal(_id,differResult.Id);
        }
        [Fact]
        public void Should_return_that_the_jsons_are_different()
        {
            var diffJason = new DiffJson(_id) { Left = "{\"key\":\"value\"}", Right = "{\"key\":\"test\"}" };
            var differResult = diffJason.Diff();
            Assert.False(differResult.AreEqual);
        }
        [Fact]
        public void Should_return_that_the_jsons_they_are_different_and_where_they_are()
        {
            var offset = 8;
            var length = 5;
            var diffJason = new DiffJson(_id) { Left = "{\"key\":\"value\"}", Right = "{\"key\":\"tests\"}" };
            var differResult = diffJason.Diff();
            Assert.False(differResult.AreEqual);
            Assert.Contains(differResult.DiffResults, r => r.Offset == offset && r.Length == length);
        }
    }
}
