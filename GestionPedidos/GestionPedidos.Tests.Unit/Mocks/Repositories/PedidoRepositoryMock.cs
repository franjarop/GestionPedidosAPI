using Aplication.Exceptions;
using Aplication.Interfaces.infraestrcuture;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidos.Tests.Unit.Mocks.Repositories
{
    public static class PedidoRepositoryMock
    {
        public static Mock<IPedidoRepository> MockAddPedido()
        {
            var mock = new Mock<IPedidoRepository>();

            mock.Setup(r => r.AddPedido(It.IsAny<Pedido>()))
                .ReturnsAsync(42);

            return mock;
        }

        public static Mock<IPedidoRepository> MockWithError(string errorMessage)
        {
            var mock = new Mock<IPedidoRepository>();
            mock
                .Setup(r => r.AddPedido(It.IsAny<Pedido>()))
                .ThrowsAsync(new RepositoryException(errorMessage));

            return mock;
        }
    }
}
