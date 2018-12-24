using System;
using System.Collections.Generic;
using System.Text;

namespace JsonDiffer.Domain.Interfaces
{
    public interface IDifferResult
    {
        bool AreEqual { get;}
        string Id { get; }
        IEnumerable<(string offset, string length)> DiffResults { get; }
    }
}
