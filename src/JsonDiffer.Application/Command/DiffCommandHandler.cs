using System;
using System.Threading;
using System.Threading.Tasks;
using JsonDiffer.Domain;
using JsonDiffer.Domain.Interfaces;
using JsonDiffer.Domain.ValueObject;
using MediatR;

namespace JsonDiffer.Application.Command
{
    public class DiffCommandHandler : IRequestHandler<DiffCommand, IDifferResult>
    {
        private readonly IDiffRepository _repository;

        public DiffCommandHandler(IDiffRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDifferResult> Handle(DiffCommand command, CancellationToken cancellationToken)
        {
            return _repository.GetById(command.Id).Diff();
        }
    }
}
