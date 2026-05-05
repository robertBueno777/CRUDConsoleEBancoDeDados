using System.ComponentModel.DataAnnotations.Schema;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public Endereco Endereco { get; set;} = new Endereco();

}