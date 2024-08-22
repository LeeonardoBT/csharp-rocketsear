using Microsoft.AspNetCore.Mvc;
using TaskMinder.Application.UseCases.Tasks.Delete;
using TaskMinder.Application.UseCases.Tasks.Get;
using TaskMinder.Application.UseCases.Tasks.GetById;
using TaskMinder.Application.UseCases.Tasks.Register;
using TaskMinder.Application.UseCases.Tasks.StatusUpdate;
using TaskMinder.Application.UseCases.Tasks.Update;
using TaskMinder.Communication.Enums;
using TaskMinder.Communication.Requests;
using TaskMinder.Communication.Responses;

namespace TaskMinder.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseTaskRegisterJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestTaskJson request)
    {
        var response = new RegisterTaskUseCase().Execute(request);

        return Created(string.Empty, response);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult Update([FromRoute] int id, [FromBody] RequestTaskJson request)
    {
        var useCase = new UpdateTaskUseCase();

        useCase.Execute(id, request);

        return NoContent();
    }

    [HttpPatch]
    [Route("{id}/{status}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult StatusUpdate([FromRoute] int id, TaskProgress status)
    {
        var useCase = new StatusUpdateTaskUseCase();

        useCase.Execute(id, status);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult StatusDelete([FromRoute] int id)
    {
        var useCase = new DeleteTaskUseCase();

        useCase.Execute(id);

        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseShortTaskJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAll()
    {
        var response = new GetAllTasksUseCase().Execute();

        if (response.Tasks.Any())
        {
            return Ok(response);
        }

        return NoContent();
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseTaskJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult GetTaskById([FromRoute] int id)
    {
        var response = new GetTaskByIdUseCase().Execute(id);

        return Ok(response);
    }
}
