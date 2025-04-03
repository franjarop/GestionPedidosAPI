using Aplication.Commands;
using Aplication.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.validartors
{
    public class GetPedidoByIdQuerieValidator : AbstractValidator<GetPedidoByIdQuerie>
    {
        public GetPedidoByIdQuerieValidator()
        {
            RuleFor(x => x.Id)
               .GreaterThan(0).WithMessage("El campo Id debe ser mayor a 0");
        }
    }
}
