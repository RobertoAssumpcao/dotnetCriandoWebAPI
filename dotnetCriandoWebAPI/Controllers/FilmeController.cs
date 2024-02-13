using AutoMapper;
using dotnetCriandoWebAPI.Data;
using dotnetCriandoWebAPI.Models;
using dotnetCriandoWebAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetCriandoWebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class FilmeController(ILogger<FilmeController> logger, DataContext dbContext, IMapper mapper)
    : ControllerBase
{
    [HttpPost]
    public IActionResult AdicionarFilme([FromBody] AdicionarFilmeRequest adicionarFilmeRequest)
    {
        try
        {
            logger.LogInformation("Criando filme.");
            var filme = mapper.Map<Filme>(adicionarFilmeRequest);
            dbContext.Filme.Add(filme);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetFilme), new { id = filme.Id }, filme);
        }
        catch (Exception e)
        {
            logger.LogError("Erro " + e.Message + " ao adicionar um filme.");
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }

    [HttpGet]
    public IActionResult GetFilmes([FromQuery] int skip, [FromQuery] int take)
    {
        try
        {
            logger.LogInformation("Verificando se existe filme");
            var filmes = dbContext.Filme.Skip(skip).Take(take).ToList();

            if (filmes.Count == 0)
                return NotFound(new { message = "Filmes não encontrados" });

            return Ok(mapper.Map<IEnumerable<GetFilmeResponse>>(filmes));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetFilme(Guid id)
    {
        try
        {
            logger.LogInformation("Verificando se exite filme");
            var filme = dbContext.Filme.Find(id);

            if (filme == null)
                return NotFound(new { message = "Filme não encontrado" });

            return Ok(mapper.Map<GetFilmeResponse>(filme));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteFilme(Guid id)
    {
        try
        {
            logger.LogInformation("buscando filme filme");
            var filme = dbContext.Filme.Find(id);

            if (filme == null)
                return NotFound(new { message = "Filme não encontrado" });

            logger.LogInformation("Deletando filme.");
            dbContext.Filme.Remove(filme);
            dbContext.SaveChanges();

            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateFilme(Guid id, [FromBody] UpdateFilmeRequest filmeUpdate)
    {
        try
        {
            logger.LogInformation("Buscando filme filme");
            var filme = dbContext.Filme.Find(id);

            if (filme == null)
                return NotFound(new { message = "Filme não encontrado" });

            logger.LogInformation("Editando filme");
            filme.Titulo = filmeUpdate.Titulo;
            filme.Genero = filmeUpdate.Genero;
            filme.Duracao = filmeUpdate.Duracao;

            dbContext.Entry(filme).State = EntityState.Modified;
            dbContext.SaveChanges();

            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }
}