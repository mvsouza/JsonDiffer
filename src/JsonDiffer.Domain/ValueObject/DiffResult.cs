using System;
using System.Collections.Generic;
using System.Text;

namespace JsonDiffer.Domain.ValueObject
{
    public class DiffResult
    {
        public bool AreEqual { get; set; }
        private string _id;
        private ICollection<(string offset,string length)> _diffResults;

        public DiffResult(string id, ICollection<(string offset, string length)> diffResults)
        {
            _id = id;
            _diffResults = diffResults;
        }
    }
}
