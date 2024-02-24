using FluentValidation;
using Logic.TechnicalAssement.App.Models;
using Logic.TechnicalAssement.Core.Commands.CreateLeaveCommand;
using Logic.TechnicalAssement.Core.Commands.DeleteLeaveCommand;
using Logic.TechnicalAssement.Core.Commands.UpdateLeaveCommand;
using Logic.TechnicalAssement.Core.Queries.GetLeaveRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Logic.TechnicalAssement.App.Controllers
{
    public class LeaveController : Controller
    {
        private readonly ILogger<LeaveController> _logger;
        private readonly IMediator _mediator;

        public LeaveController(
            ILogger<LeaveController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/leave")]
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new GetLeaveRequestsRequest(), CancellationToken.None);
            return View("Index", result);
        }

        [HttpPost]
        [Route("leave/")]
        public async Task<IActionResult> CreateLeaveRequest([FromBody] CreateLeaveRequest request)
        {
            try
            {
                var result = await _mediator.Send(request, CancellationToken.None);
                return CreatedAtAction(nameof(CreateLeaveRequest), result.Id);
            }
            catch (ValidationException vEx)
            {
                _logger.LogWarning(vEx, "validation errors exist");
                return new BadRequestObjectResult(vEx.Errors);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        [HttpPut]
        [Route("leave/{id}/update")]
        public async Task<IActionResult> UpdateLeaveRequest(UpdateLeaveRequest request)
        {
            var result = await _mediator.Send(request, CancellationToken.None);

            if (result == null)
            {
                _logger.LogWarning("record with id: {id} not found", request.Id);
            }

            return result == null ? NotFound() : NoContent();
        }

        [HttpDelete]
        [Route("leave/{id}")]
        public async Task<IActionResult> DeleteLEaveRequest(int id)
        {
            _mediator.Send(new DeleteLeaveRequest() { Id = id }, CancellationToken.None);
            return NoContent();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
