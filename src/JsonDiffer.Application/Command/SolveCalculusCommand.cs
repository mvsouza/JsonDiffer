using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonDiffer.Application.Command
{
    public class SolveCalculusCommand : IRequest<double>
    {
        public string Calculus { get; set; }
        public SolveCalculusCommand(string calculus)
        {
            Calculus = calculus;
        }
    }
}
