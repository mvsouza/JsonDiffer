using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JsonDiffer.Domain.Entities;
using JsonDiffer.Domain.ValueObject;
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
        public static TheoryData<string, string, IEnumerable<Segment>> DifferentJsonWithSameSize
        {
            get
            {
                var theoryData = new TheoryData<string, string, IEnumerable<Segment>>();
                theoryData.Add("{\"key\":\"value\"}", "{\"key\":\"tests\"}", new List<Segment>{new Segment(8,5)});
                theoryData.Add("{\"bey\":\"tests\"}", "{\"key\":\"tests\"}", new List<Segment> { new Segment(2, 1) });
                theoryData.Add("{\"bey\":\"value\"}", "{\"key\":\"tests\"}", new List<Segment> { new Segment(2, 1), new Segment(8, 5) });
                return theoryData;
            }
        }
        [Theory]
        [MemberData(nameof(DifferentJsonWithSameSize))]
        public void Should_return_that_the_jsons_they_are_different_and_where_they_are(string left, string right, IEnumerable<Segment> segments)
        {
            var diffJason = new DiffJson(_id) { Left = left, Right = right };
            var differResult = diffJason.Diff();
            Assert.False(differResult.AreEqual);
            Assert.Equal(differResult.DiffResults.Count(),segments.Count());
            foreach (var seg in segments)
            {
                Assert.Contains(differResult.DiffResults, r => r.Equals(seg));
            }
        }
        [Fact]
        public void Equal_objecs_should_have_equal_hashcodes()
        {
            var left = "left";
            var right = "right";
            var firstDiff = new DiffJson(_id) { Left = left, Right = right };
            var secondDiff = new DiffJson(_id) { Left = left, Right = right };
            Assert.Equal(firstDiff, secondDiff);
            Assert.Equal(firstDiff.GetHashCode(), secondDiff.GetHashCode());
        }
    }
}
