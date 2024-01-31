using System.ComponentModel.DataAnnotations;

namespace dotnetCriandoWebAPI.Models.DTO;

public class FilmeRequest
{
    [Required(ErrorMessage = "Campo titulo é obrigatorio.")]
    public required string Titulo { get; set; }

    [Required(ErrorMessage = "Campo genero é obrigatorio.")]
    [MaxLength(60)]
    public required string Genero { get; set; }

    [Required(ErrorMessage = "Campo duração é obrigatorio.")]
    public int Duracao { get; set; }
}