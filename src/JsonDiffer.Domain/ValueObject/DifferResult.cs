using System;
using System.Collections.Generic;
using System.Text;
using JsonDiffer.Domain.Interfaces;

namespace JsonDiffer.Domain.ValueObject
{
    public class DifferResult : IDifferResult
    {
        public bool AreEqual { get; }
        public string Id { get; }
        public IEnumerable<Segment> DiffResults { get; }

        public DifferResult(string id, bool areEqual, IEnumerable<Segment> diffResults)
        {
            Id = id;
            AreEqual = areEqual;
            DiffResults = diffResults;
        }
        
    }
}
