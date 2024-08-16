using Microsoft.AspNetCore.Mvc;
using MyLibrary.Communication.Requests;
using MyLibrary.Entities;

namespace MyLibrary.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var response = new List<Book>()
        {
            new Book("1984", "George Orwell", "Distopia, Ficção Científica", 190.90, 10),
            new Book("O Senhor dos Anéis: A Sociedade do Anel", "J.R.R. Tolkien", "Fantasia, Aventura", 130.50, 9),
            new Book("Orgulho e Preconceito", "Jane Austen", "Romance, Literatura Clássica", 55.99, 6),
        };
        
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] RequestCreateBookJson request)
    {
        var book = new Book(request.Title, request.Author, request.Gender, request.Price, request.Stock);

        return Created(string.Empty, book);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Update([FromRoute] int id, [FromBody] RequestUpdateBookJson request)
    {
        var book = new Book(id, request.Price, request.Stock);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete([FromRoute] int id)
    {
        return NoContent();
    }
}
