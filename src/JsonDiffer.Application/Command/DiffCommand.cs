using System;
using System.Collections.Generic;
using System.Text;
using JsonDiffer.Domain.ValueObject;
using MediatR;

namespace JsonDiffer.Application.Command
{
    public class DiffCommand : IRequest<DiffResult>
    {
        private string _id;

        public DiffCommand(string id)
        {
            this._id = id;
        }
    }
}
