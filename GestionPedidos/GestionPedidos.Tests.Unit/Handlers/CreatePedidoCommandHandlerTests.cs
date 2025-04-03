using Aplication.Commands;
using Aplication.EventHandler;
using Aplication.Exceptions;
using Aplication.Interfaces.infraestrcuture;
using Domain.Models;
using FluentAssertions;
using GestionPedidos.Tests.Unit.Mocks.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidos.Tests.Unit.Handlers
{
    public class CreatePedidoCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnPedidoId_WhenPedidoIsCreated()
        {
            var command = new CreatePedidoCommand
            {
                CustomerId = 1,
                TotalAmount = 500
            };

            var mockRepo = PedidoRepositoryMock.MockAddPedido();
            var handler = new CreatePedidoCommandHandler(mockRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(42);
            mockRepo.Verify(r => r.AddPedido(It.IsAny<Pedido>()), Times.Once);

        }

        [Fact]
        public async Task Handle_ShouldThrowRepositoryException_WhenRepositoryFails()
        {
            String mensaje = "Error al guardar en la base de datos";
            var command = new CreatePedidoCommand
            {
                CustomerId = 1,
                TotalAmount = 100
            };

            var mockRepo = PedidoRepositoryMock.MockWithError(mensaje);
            var handler = new CreatePedidoCommandHandler(mockRepo.Object);
            var act = async () => await handler.Handle(command, default);

            
            await act.Should()
                .ThrowAsync<RepositoryException>()
                .WithMessage(mensaje);

            mockRepo.Verify(r => r.AddPedido(It.IsAny<Pedido>()), Times.Once);
        }

    }
}
