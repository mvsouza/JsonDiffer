using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JsonDiffer.Domain.ValueObject;
using JsonDiffer.Infrastructure.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace JsonDiffer.UnitTest.API
{
    public class DiffControllerTests
    {
        private Mock<IMediator> _mediator;
        private string _json =  "{\"some\":\"json\"}";
        private Expression<Func<IMediator, Task>> _mediatorSendRequest = m => m.Send(It.IsAny<IRequest>(), It.IsAny<CancellationToken>());
        private Expression<Func<IMediator, Task<DiffResult>>> _mediatorSendResultRequest = m => m.Send(It.IsAny<IRequest<DiffResult>>(), It.IsAny<CancellationToken>());
        public DiffControllerTests()
        {
            _mediator = new Mock<IMediator>();
        }
        [Fact]
        public async Task Should_send_successfuly_a_command_requestAsync()
        {
            var result = await PostLeft();
            Assert.True(result is OkResult);
            result = await PostRight();
            Assert.True(result is OkResult);
        }
        [Fact]
        public async Task Should_try_to_send_a_command_request_and_handle_a_exceptionAsync()
        {
            _mediator.Setup(_mediatorSendRequest).Throws(new Exception("Left side already filed."));
            var result = await PostLeft();
            Assert.True(result is BadRequestObjectResult);
            
            result = await PostRight();
            Assert.True(result is BadRequestObjectResult);
        }

        private async Task<IActionResult> PostLeft()
        {
            var controller = new DiffController(_mediator.Object);
            var result = await controller.PostLeft("teste", _json);
            _mediator.Verify(_mediatorSendRequest, Times.Once);
            return result;
        }
        private async Task<IActionResult> PostRight()
        {
            var controller = new DiffController(_mediator.Object);
            var result = await controller.Post("teste");
            _mediator.Verify(_mediatorSendRequest, Times.Exactly(2));
            return result;
        }
        [Fact]
        private async Task Should_request_diff()
        {
            var id = "teste";
            var mediatrResult = Task.FromResult(new DiffResult(id, null) { AreEqual = true });
            _mediator.Setup(_mediatorSendResultRequest).Returns(mediatrResult);
            var controller = new DiffController(_mediator.Object);
            var result = await controller.Post(id);
            _mediator.Verify(_mediatorSendResultRequest, Times.Once);
            Assert.True(result is OkObjectResult okResult && 
                okResult.Value is DiffResult diffResult && 
                diffResult.AreEqual);
        }
        [Fact]
        private async Task Should_request_and_handle_Exception()
        {
            var id = "teste";
            var mediatrResult = Task.FromResult(new DiffResult(id, null) { AreEqual = true });
            _mediator.Setup(_mediatorSendResultRequest).Throws(new Exception("Id Not found."));
            var controller = new DiffController(_mediator.Object);
            var result = await controller.Post(id);
            _mediator.Verify(_mediatorSendResultRequest, Times.Once);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
