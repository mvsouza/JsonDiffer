using System;
using System.Collections.Generic;
using System.Text;
using JsonDiffer.Domain.ValueObject;

namespace JsonDiffer.Domain.Interfaces
{
    public interface IDifferResult
    {
        bool AreEqual { get;}
        string Id { get; }
        IEnumerable<Segment> DiffResults { get; }
    }
}
