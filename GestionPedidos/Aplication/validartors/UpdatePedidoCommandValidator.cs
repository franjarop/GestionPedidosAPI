using Aplication.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.validartors
{
    public class UpdatePedidoCommandValidator : AbstractValidator<UpdatePedidoCommand>
    {
        public UpdatePedidoCommandValidator()
        {
            RuleFor(x => x.id)
           .NotNull().WithMessage("El campo CustomerId no puedo quedar vacio")
           .GreaterThan(0).WithMessage("El campo Id debe ser mayor a 0");

            RuleFor(x => x.statusId)
                .IsInEnum().WithMessage("El statusId registrado no es valido");
        }
    }
}
