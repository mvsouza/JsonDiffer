using System.Threading;
using System.Threading.Tasks;
using JsonDiffer.Domain;
using JsonDiffer.Domain.Entities;
using MediatR;

namespace JsonDiffer.Application.Command
{
    public class PushRightJsonCommandHandler : IRequestHandler<PushRightJsonCommand>
    {
        private IDiffRepository _diffRepository;
        public PushRightJsonCommandHandler(IDiffRepository diffRepository)
        {
            _diffRepository = diffRepository;
        }
        public async Task Handle(PushRightJsonCommand command, CancellationToken cancellationToken)
        {

            var diff = _diffRepository.GetById(command.Id);
            if (diff != null)
            {
                if (!string.IsNullOrEmpty(diff.Right))
                    return;
                diff.Right = command.Json;
                _diffRepository.Update(diff);
            }
            else
            {
                _diffRepository.Add(new DiffJson(command.Id) { Right = command.Json });
            }
        }
    }
}
