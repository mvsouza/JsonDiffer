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
        private ICollection<(string offset,string length)> _diffResults;
        public IEnumerable<(string offset, string length)> DiffResults { get { return _diffResults; }}

        public DifferResult(string id, ICollection<(string offset, string length)> diffResults)
        {
            Id = id;
            _diffResults = diffResults;
        }
        
    }
}
