using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonDiffer.Application.Command
{
    public class PushLeftJsonCommand : IRequest
    {
        public string Calculus { get; set; }
        public PushLeftJsonCommand(string id, string json)
        {
            Calculus = id;
        }
    }
}
