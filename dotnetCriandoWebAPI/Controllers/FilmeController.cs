using dotnetCriandoWebAPI.Data;
using dotnetCriandoWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetCriandoWebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class FilmeController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly DataContext _dbContext;
    
    public FilmeController(ILogger<FilmeController> logger, DataContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    [HttpPost]
    public IActionResult AdicionarFilme([FromBody] Filme request)
    {
        try
        {
            _logger.LogInformation("Criando filme.");
            _dbContext.Filmes.Add(request);
            _dbContext.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }

    [HttpGet]
    public IActionResult GetFilmes([FromQuery] int skip, [FromQuery] int take)
    {
        try
        {
            _logger.LogInformation("Verificando se exite filme");
            var filmes = _dbContext.Filmes.Skip(skip).Take(take).ToList();
            
            if (filmes == null || filmes.Count == 0)
                return NotFound("Filmes não encontrado");
            
            return Ok(filmes);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetFilme(Guid id)
    {
        try
        {
            _logger.LogInformation("Verificando se exite filme");
            var filme = _dbContext.Filmes.Find(id);
            
            if (filme == null)
                return NotFound("Filme não encontrado");
            
            return Ok(filme);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteFilme(Guid id)
    {
        try
        {
            _logger.LogInformation("buscando filme filme");
            var filme = _dbContext.Filmes.Find(id);

            if (filme == null)
                return NotFound("Filme não encontrado");

            _logger.LogInformation("Deletando filme.");
            _dbContext.Filmes.Remove(filme);
            _dbContext.SaveChanges();
        
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateFilme(Guid id, [FromBody] Filme filmeUpdate)
    {
        try
        {
            _logger.LogInformation("buscando filme filme");
            var filme = _dbContext.Filmes.Find(id);

            if (filme == null)
                return NotFound("Filme não encontrado");
        
            _logger.LogInformation("Editando filme");
            filme.Titulo = filmeUpdate.Titulo;
            filme.Genero = filmeUpdate.Genero;
            filme.Duracao = filmeUpdate.Duracao;
        
            _dbContext.Entry(filme).State = EntityState.Modified;
            _dbContext.SaveChanges();
        
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }
} 