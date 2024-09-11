using BarberBoss.Application.UseCases.Billings.Delete;
using BarberBoss.Application.UseCases.Billings.GetAll;
using BarberBoss.Application.UseCases.Billings.GetById;
using BarberBoss.Application.UseCases.Billings.Register;
using BarberBoss.Application.UseCases.Billings.Update;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BillingController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseBillingRegisterJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterBillingUseCase useCase,
        [FromBody] RequestBillingJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateBillingUseCase useCase,
        [FromRoute] long id,
        [FromBody] RequestBillingJson request)
    {
        await useCase.Execute(id, request);

        return NoContent();

    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseBillingListJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllBillingUseCase useCase)
    {
        var resonse = await useCase.Execute();
        
        if (resonse.Billings.Count != 0)
        {
            return Ok(resonse);
        }

        return NoContent();
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseBillingJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute] long id,
        [FromServices] IGetBillingByIdUseCase useCase) 
    { 
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] long id,
        [FromServices] IDeleteBillingUseCase useCase)
    {
        await useCase.Execute(id);

        return NoContent();
    }

}
