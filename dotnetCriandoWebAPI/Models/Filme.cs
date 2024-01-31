using System.ComponentModel.DataAnnotations;

namespace dotnetCriandoWebAPI.Models;

public class Filme
{
    [Key]
    public Guid Id { get; set; }
    public required string Titulo { get; set; }
    public required string Genero { get; set; }
    public int Duracao { get; set; }
}