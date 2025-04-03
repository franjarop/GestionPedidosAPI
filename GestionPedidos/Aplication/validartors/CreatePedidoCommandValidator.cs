using Aplication.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.validartors
{
    public class CreatePedidoCommandValidator : AbstractValidator<CreatePedidoCommand>
    {
        public CreatePedidoCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotNull().WithMessage("El campo CustomerId no puedo quedar vacio")
                .GreaterThan(0).WithMessage("El campo CustomerId debe ser mayor a 0");
            RuleFor(x => x.TotalAmount)
                .NotNull().WithMessage("El campo TotalAmount no puedo quedar vacio")
                .GreaterThan(0).WithMessage("El campo TotalAmount debe ser mayor a 0");
        }
    }
}
