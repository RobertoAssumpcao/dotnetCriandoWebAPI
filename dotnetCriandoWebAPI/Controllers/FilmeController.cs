using AutoMapper;
using dotnetCriandoWebAPI.Data;
using dotnetCriandoWebAPI.Models;
using dotnetCriandoWebAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetCriandoWebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class FilmeController : ControllerBase
{
    private readonly DataContext _dbContext;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public FilmeController(ILogger<FilmeController> logger, DataContext dbContext, IMapper mapper)
    {
        _logger = logger;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionarFilme([FromBody] AdicionarFilmeRequest adicionarFilmeRequest)
    {
        try
        {
            _logger.LogInformation("Criando filme.");
            _dbContext.Filmes.Add(_mapper.Map<Filme>(adicionarFilmeRequest));
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

            return Ok(_mapper.Map<IEnumerable<GetFilmeResponse>>(filmes));
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
            _logger.LogInformation("Verificando se exite filme");
            var filme = _dbContext.Filmes.Find(id);

            if (filme == null)
                return NotFound("Filme não encontrado");

            return Ok(_mapper.Map<GetFilmeResponse>(filme));
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

    [HttpPut("{id:guid}")]
    public IActionResult UpdateFilme(Guid id, [FromBody] UpdateFilmeRequest filmeUpdate)
    {
        try
        {
            _logger.LogInformation("Buscando filme filme");
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