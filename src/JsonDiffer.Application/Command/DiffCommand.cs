using JsonDiffer.Domain.Interfaces;
using MediatR;

namespace JsonDiffer.Application.Command
{
    public class DiffCommand : IRequest<IDifferResult>
    {
        public string Id { private set; get; }

        public DiffCommand(string id)
        {
            Id = id;
        }
    }
}
