using MediatR;
using JsonDiffer.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using JsonDiffer.Domain;

namespace JsonDiffer.Application.Command
{
    public class PushLeftJsonCommandHandler: IRequestHandler<PushLeftJsonCommand>
    {
        private IDiffRepository _diffRepository;
        public PushLeftJsonCommandHandler(IDiffRepository diffRepository){
            _diffRepository = diffRepository;
        }
        public async Task Handle(PushLeftJsonCommand command, CancellationToken cancellationToken)
        {

            var diff = _diffRepository.GetById(command.Id);
            if (diff != null)
            {
                if (!string.IsNullOrEmpty(diff.Left))
                    return;
                diff.Left = command.Json;
                _diffRepository.Update(diff);
            }
            else
            {
                _diffRepository.Add(new DiffJson(command.Id) { Left = command.Json });
            }
        }
    }
}
