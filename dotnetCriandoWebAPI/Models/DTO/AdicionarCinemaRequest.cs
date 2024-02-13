using System.ComponentModel.DataAnnotations;

namespace dotnetCriandoWebAPI.Models.DTO;

public class AdicionarCinemaRequest
{
    [Required(ErrorMessage = "Campo nome é obrigatório.")]
    public required string Nome { get; set; }

    [Range(1, short.MaxValue, ErrorMessage = "Capacidade do cinema inválida.")]
    public required int Capacidade { get; set; }
}