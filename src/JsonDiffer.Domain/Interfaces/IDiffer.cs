using System;
using System.Collections.Generic;
using System.Text;

namespace JsonDiffer.Domain.Interfaces
{
    public interface IDiffer
    {
        string Left { set; get; }
        string Right { set; get; }
        string Id { get; }

        IDifferResult Diff();
    }
}
