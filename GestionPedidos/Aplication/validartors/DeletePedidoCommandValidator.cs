using Aplication.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.validartors
{
    public class DeletePedidoCommandValidator : AbstractValidator<DeletePedidoCommand>
    {
        public DeletePedidoCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("El campo Id no puedo quedar vacio")
                .GreaterThan(0).WithMessage("El campo Id debe ser mayor a 0");
        }
    }
}
