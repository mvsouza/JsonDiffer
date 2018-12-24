using JsonDiffer.Application.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace JsonDiffer.Infrastructure.API
{
    [Produces("application/json")]
    [Route("v1/diff")]
    public class  DiffController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DiffController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("{id}/left")]
        public async Task<IActionResult> PostLeft([FromQuery] string id, [FromBody]string json)
        {
            return await PostSide(new PushLeftJsonCommand(id, json));
        }
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("{id}/right")]
        public async Task<IActionResult> PostRight([FromQuery] string id, [FromBody]string json)
        {
            return await PostSide(new PushRightJsonCommand(id, json));
        }

        private async Task<IActionResult> PostSide(IRequest command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                var erroMessage = JsonConvert.SerializeObject(new { ex.Message });
                return BadRequest(erroMessage);
            }
        }

    }
}
