using System.ComponentModel.DataAnnotations;

namespace dotnetCriandoWebAPI.Models.DTO;

public class UpdateCinemaRequest
{
    public required string Nome { get; set; }
    [Range(1, short.MaxValue, ErrorMessage = "Capacidade do cinema inválida.")]
    public int Capacidade { get; set; }
}