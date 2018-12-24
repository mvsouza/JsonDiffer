using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        private Mock<IMediator> _mediator;
        private string _json =  "{\"some\":\"json\"}";
        private Expression<Func<IMediator, Task>> _mediatorSendMessage = m => m.Send(It.IsAny<IRequest>(), It.IsAny<CancellationToken>());
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
            _mediator.Setup(_mediatorSendMessage).Throws(new Exception("Left side already filed."));
            var result = await PostLeft();
            Assert.True(result is BadRequestObjectResult);
            
            result = await PostRight();
            Assert.True(result is BadRequestObjectResult);
        }

        private async Task<IActionResult> PostLeft()
        {
            var controller = new DiffController(_mediator.Object);
            var result = await controller.PostLeft("teste", _json);
            _mediator.Verify(_mediatorSendMessage, Times.Once);
            return result;
        }
        private async Task<IActionResult> PostRight()
        {
            var controller = new DiffController(_mediator.Object);
            var result = await controller.PostRight("teste", _json);
            _mediator.Verify(_mediatorSendMessage, Times.Exactly(2));
            return result;
        }
    }
}
