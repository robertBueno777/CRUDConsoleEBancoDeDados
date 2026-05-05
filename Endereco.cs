using System.ComponentModel.DataAnnotations.Schema;
public class Endereco
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string Cep { get; set; } = string.Empty;
    public string Rua { get; set; } = string.Empty;
    public string NumeroDaCasa { get; set; } = string.Empty;

}