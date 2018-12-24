using System;
using System.Collections.Generic;
using System.Text;
using JsonDiffer.Domain.Entities;
using Xunit;

namespace JsonDiffer.UnitTest.Domain
{
    public class DiffJsonTests
    {
        [Fact]
        public void Should_return_that_the_jsons_they_are_equal()
        {
            var id = "teste";
            var json = "{\"some\":\"json\"}";
            var diffJason = new DiffJson(id) { Left =json, Right=json };
            var differResult = diffJason.Diff();
            Assert.True(differResult.AreEqual);
        }
    }
}
