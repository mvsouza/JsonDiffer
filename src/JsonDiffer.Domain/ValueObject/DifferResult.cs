using System;
using System.Collections.Generic;
using System.Text;
using JsonDiffer.Domain.Interfaces;

namespace JsonDiffer.Domain.ValueObject
{
    public class DifferResult : IDifferResult
    {
        public bool AreEqual { get; private set; }
        public string Id { get; private set; }
        private ICollection<Segment> _diffResults;
        public IEnumerable<Segment> DiffResults { get { return _diffResults; }}

        public DifferResult(string id, bool areEqual, ICollection<Segment> diffResults)
        {
            Id = id;
            AreEqual = areEqual;
            _diffResults = diffResults;
        }
        
    }
}
