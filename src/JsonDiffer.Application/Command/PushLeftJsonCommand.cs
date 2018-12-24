using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonDiffer.Application.Command
{
    public class PushLeftJsonCommand : IRequest
    {
        public string Id { get; set; }
        public string Json { get; }

        public PushLeftJsonCommand(string id, string json)
        {
            Id = id;
            Json = json;
        }
    }
}
