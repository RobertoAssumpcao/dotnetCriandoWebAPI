namespace dotnetCriandoWebAPI.Models.DTO;

public class GetCinemaResponse
{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public int Capacidade { get; set; }
}