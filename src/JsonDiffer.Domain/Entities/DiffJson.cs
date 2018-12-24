namespace JsonDiffer.Domain.Entities
{
    public class DiffJson
    {
        public DiffJson(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public string Left { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DiffJson diff 
                && diff.Id == Id 
                && diff.Left == Left;
        }

    }
}
