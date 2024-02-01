namespace dotnetCriandoWebAPI.Models.DTO;

public class GetFilmeResponse
{
    public Guid Id { get; set; }
    public required string Titulo { get; set; }
    public required string Genero { get; set; }
    public int Duracao { get; set; }
}