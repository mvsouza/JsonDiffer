using System;
using System.Collections.Generic;
using System.Text;
using JsonDiffer.Domain.ValueObject;
using Xunit;

namespace JsonDiffer.UnitTest.Domain
{
    public class SegmentTest
    {
        [Fact]
        public void Equal_objecs_should_have_equal_hashcodes()
        {
            var offset = 3;
            var length = 5;
            var firstSeg = new Segment(offset, length);
            var seconfSeg = new Segment(offset, length);
            Assert.Equal(firstSeg,seconfSeg);
            Assert.Equal(firstSeg.GetHashCode(), seconfSeg.GetHashCode());
        }
    }
}
