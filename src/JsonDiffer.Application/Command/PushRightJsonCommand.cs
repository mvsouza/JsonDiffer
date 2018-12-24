using MediatR;

namespace JsonDiffer.Application.Command
{
    public class PushRightJsonCommand : IRequest
    {
        public string Id { get; set; }
        public string Json { get; }

        public PushRightJsonCommand(string id, string json)
        {
            Id = id;
            Json = json;
        }
    }
}
