using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using JsonDiffer.Domain.Interfaces;
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
        private Mock<IDifferResult> _differResult;
        private string _json =  "{\"some\":\"json\"}";
        private string _id = "teste";
        private Expression<Func<IMediator, Task>> _mediatorSendRequest = m => m.Send(It.IsAny<IRequest>(), It.IsAny<CancellationToken>());
        private Expression<Func<IMediator, Task<IDifferResult>>> _mediatorSendResultRequest = m => m.Send(It.IsAny<IRequest<IDifferResult>>(), It.IsAny<CancellationToken>());

        public DiffControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _differResult = new Mock<IDifferResult>();
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
            var result = await controller.PostLeft(_id, _json);
            _mediator.Verify(_mediatorSendRequest, Times.Once);
            return result;
        }
        private async Task<IActionResult> PostRight()
        {
            var controller = new DiffController(_mediator.Object);
            var result = await controller.PostRight(_id,_json);
            _mediator.Verify(_mediatorSendRequest, Times.Exactly(2));
            return result;
        }
        [Fact]
        private async Task Should_request_diff()
        {
            _differResult.Setup(d => d.AreEqual).Returns(true);
            _differResult.Setup(d => d.Id).Returns(_id);
            var mediatrResult = Task.FromResult(_differResult.Object);
            _mediator.Setup(_mediatorSendResultRequest).Returns(mediatrResult);
            var controller = new DiffController(_mediator.Object);
            var result = await controller.Post(_id);
            _mediator.Verify(_mediatorSendResultRequest, Times.Once);
            Assert.True(result is OkObjectResult okResult && 
                okResult.Value is IDifferResult diffResult && 
                diffResult.AreEqual);
        }
        [Fact]
        private async Task Should_request_and_handle_Exception()
        {
            _mediator.Setup(_mediatorSendResultRequest).Throws(new Exception("Id Not found."));
            var controller = new DiffController(_mediator.Object);
            var result = await controller.Post(_id);
            _mediator.Verify(_mediatorSendResultRequest, Times.Once);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
