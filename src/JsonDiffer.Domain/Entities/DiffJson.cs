using System;
using System.Collections.Generic;
using System.Linq;
using JsonDiffer.Domain.Interfaces;
using JsonDiffer.Domain.ValueObject;

namespace JsonDiffer.Domain.Entities
{
    public class DiffJson : IDiffer
    {
        public DiffJson(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DiffJson diff
                && diff.Id == Id
                && String.Equals(diff.Left, Left)
                && String.Equals(diff.Right, Right);
        }
        public override int GetHashCode()
        {
            return (Id + Left + Right).GetHashCode();
        }
        public IDifferResult Diff()
        {
            var areEqual = Left == Right;
            ICollection<Segment> differences = null;
            if (!areEqual && Left.Length == Right.Length)
                differences = FindDifferences().ToList();
            return new DifferResult(Id,areEqual, differences);
        }

        private IEnumerable<Segment> FindDifferences()
        {
            for (int i = 0, offset = 0, length = 0; i < Left.Length; i++)
            {
                bool areDifferent = Left[i] != Right[i];
                bool isCountingADifference = length > 0;
                if (areDifferent && isCountingADifference)
                    length++;
                if (areDifferent && !isCountingADifference)
                {
                    offset = i;
                    length = 1;
                }
                if (!areDifferent && isCountingADifference)
                {
                    yield return new Segment(offset, length);
                    length = 0;
                }

            }
        }
    }
}
