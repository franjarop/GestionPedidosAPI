using Aplication.Commands;
using Aplication.validartors;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidos.Tests.Unit.validators
{
    public class CreatePedidoCommandValidatorTests
    {
        private readonly CreatePedidoCommandValidator _validator = new();

        [Fact]
        public void validate_CustomerIsZero_shouldFail()
        {
            var command = new CreatePedidoCommand { CustomerId = 0, TotalAmount = 100 };
            var validator = new CreatePedidoCommandValidator();

            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "CustomerId");

        }

        [Fact]
        public void validate_CustomerIsGreatherThanZero_shouldSuceed()
        {
            var command = new CreatePedidoCommand { CustomerId = 100, TotalAmount = 500 };
            var validator = new CreatePedidoCommandValidator();

            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();

        }

        [Fact]
        public void validate_TotalAmountIsZero_shouldFail()
        {
            var command = new CreatePedidoCommand { CustomerId = 1, TotalAmount = 0 };
            var validator = new CreatePedidoCommandValidator();

            var result = validator.Validate(command);
            
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "TotalAmount");
            
        }
        [Fact]
        public void validate_TotalAmountIsGreaterThanZero_shouldSuceed()
        {
            var command = new CreatePedidoCommand { CustomerId = 1, TotalAmount = 100 };
            var validator = new CreatePedidoCommandValidator();

            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();

        }
    }
}
