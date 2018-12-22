using MediatR;
using JsonDiffer.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace JsonDiffer.Application.Command
{
    public class SolveCalculusCommandHandler: IRequestHandler<SolveCalculusCommand, double>
    {
        public async Task<double> Handle(SolveCalculusCommand command, CancellationToken cancellationToken)
        {
            Factor f = new Factor(command.Calculus);

            return f.Solve();
        }
    }
}