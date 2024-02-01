namespace dotnetCriandoWebAPI.Models.DTO;

public class UpdateFilmeRequest
{
    public required string Titulo { get; set; }
    public required string Genero { get; set; }
    public int Duracao { get; set; }
}