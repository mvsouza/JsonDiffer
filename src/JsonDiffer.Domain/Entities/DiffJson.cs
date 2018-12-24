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
                && diff.Left == Left
                && diff.Right == Right;
        }

        public IDifferResult Diff()
        {
            var areEqual = Left == Right;
            return new DifferResult(Id,areEqual,null);
        }
    }
}
