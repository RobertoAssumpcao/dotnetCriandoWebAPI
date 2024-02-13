using AutoMapper;
using dotnetCriandoWebAPI.Data;
using dotnetCriandoWebAPI.Models;
using dotnetCriandoWebAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetCriandoWebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CinemaController(DataContext dbContext, ILogger<CinemaController> logger, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public IActionResult AdicionarCinema([FromBody] AdicionarCinemaRequest adicionarCinemaRequest)
    {
        try
        {
            logger.LogInformation("Criando cinema");
            var cinema = mapper.Map<Cinema>(adicionarCinemaRequest);
            dbContext.Cinema.Add(cinema);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetCinema), new { id = cinema.Id }, cinema);
        }
        catch (Exception e)
        {
            logger.LogError("Erro " + e.Message + " ao adicionar um cinema.");
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }

    [HttpGet]
    public IActionResult GetCinemas([FromQuery] int skip, [FromQuery] int take)
    {
        try
        {
            logger.LogInformation("Verificando se existe cinema");
            var cinemas = dbContext.Cinema.Skip(skip).Take(take).ToList();

            return cinemas.Count == 0
                ? NotFound(new { message = "Cinemas não encontrados" })
                : Ok(mapper.Map<IEnumerable<GetCinemaResponse>>(cinemas));
        }
        catch (Exception e)
        {
            logger.LogError("Erro " + e.Message + " ao buscar cinemas.");
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetCinema(Guid id)
    {
        try
        {
            logger.LogInformation("Buscando cinema");
            var cinema = dbContext.Cinema.Find(id);

            return cinema == null
                ? NotFound(new { message = "Cinema não encontrado" })
                : Ok(mapper.Map<GetCinemaResponse>(cinema));
        }
        catch (Exception e)
        {
            logger.LogError("Erro " + e.Message + " ao buscar cinema.");
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteCinema(Guid id)
    {
        try
        {
            logger.LogInformation("Buscando cinema");
            var cinema = dbContext.Cinema.Find(id);

            if (cinema == null)
                return NotFound(new { message = "Cinema não encontrado" });

            logger.LogInformation("Deletando cinema");
            dbContext.Cinema.Remove(cinema);
            dbContext.SaveChanges();

            return NoContent();
        }
        catch (Exception e)
        {
            logger.LogError("Erro " + e.Message + " ao deletar cinema.");
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateCinema(Guid id, [FromBody] UpdateCinemaRequest cinemaUpdate)
    {
        try
        {
            logger.LogInformation("Buscando cinema");
            var cinema = dbContext.Cinema.Find(id);

            if (cinema == null)
                return NotFound(new { message = "cinema não encontrado" });

            logger.LogInformation("Atualizando cinema");
            cinema.Nome = cinemaUpdate.Nome;
            cinema.Capacidade = cinemaUpdate.Capacidade;

            dbContext.Entry(cinema).State = EntityState.Modified;
            dbContext.SaveChanges();

            return NoContent();
        }
        catch (Exception e)
        {
            logger.LogError("Erro " + e.Message + " ao atualizar cinema.");
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }
}