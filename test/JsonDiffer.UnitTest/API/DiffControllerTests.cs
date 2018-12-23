using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JsonDiffer.Infrastructure.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace JsonDiffer.UnitTest.API
{
    public class DiffControllerTests
    {
        [Fact]
        public async Task Should_send_successfuly_a_command_requestAsync()
        {
            var mediator = new Mock<IMediator>();
            var controller = new DiffController(mediator.Object);
            var result = await controller.PostLeft("teste", "{\"some\":\"json\"}");
            mediator.Verify(m => m.Send(It.IsAny<IRequest>(),It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(result is OkResult);
        }
        [Fact]
        public async Task Should_try_to_send_a_command_request_and_handle_a_exceptionAsync()
        {
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<IRequest>(), It.IsAny<CancellationToken>())).Throws(new Exception("Left side already filed."));
            var controller = new DiffController(mediator.Object);
            var result = await controller.PostLeft("teste", "{\"some\":\"json\"}");
            mediator.Verify(m => m.Send(It.IsAny<IRequest>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
