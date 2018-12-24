using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using JsonDiffer.Application.Command;

namespace JsonDiffer.Application.Validation
{
    public class PushRightJsonCommandValidation : AbstractValidator<PushRightJsonCommand>
    {
        public PushRightJsonCommandValidation()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("\"Id\" field should'nt be empty.");
            RuleFor(command => command.Json).NotEmpty().WithMessage("\"Json\" field should'nt be empty.");
        }
    }
}
