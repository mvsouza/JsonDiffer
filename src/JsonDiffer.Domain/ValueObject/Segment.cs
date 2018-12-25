using System;
using System.Collections.Generic;
using System.Text;

namespace JsonDiffer.Domain.ValueObject
{
    public class Segment
    {
        public int Offset { get; }
        public int Length { get; }
        public Segment(int offset, int length)
        {
            Offset = offset;
            Length = length;
        }
    }
}
