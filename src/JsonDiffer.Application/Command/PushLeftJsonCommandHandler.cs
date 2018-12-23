using MediatR;
using JsonDiffer.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace JsonDiffer.Application.Command
{
    public class PushLeftJsonCommandHandler: IRequestHandler<PushLeftJsonCommand>
    {
        public async Task Handle(PushLeftJsonCommand command, CancellationToken cancellationToken)
        {
            Factor f = new Factor(command.Calculus);

            f.Solve();
        }
    }
}
