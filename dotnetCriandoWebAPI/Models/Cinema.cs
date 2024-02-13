using System.ComponentModel.DataAnnotations;

namespace dotnetCriandoWebAPI.Models;

public class Cinema
{
    [Key] public Guid Id { get; set; }

    public string Nome { get; set; }
    public int Capacidade { get; set; }
}