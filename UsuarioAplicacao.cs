
public class UsuarioAplicacao
{
    public static void ReceberDadosDoUsuario()
    {
        var usuario = new Usuario();
        System.Console.WriteLine("Informe aqui seu Nome...");
        usuario.Nome = Console.ReadLine();
        System.Console.WriteLine("informe aqui sua idade...");
        usuario.Idade = int.Parse(Console.ReadLine());
        System.Console.WriteLine("Informe o cep..");
        usuario.Endereco.Cep = Console.ReadLine();
        System.Console.WriteLine("informe a rua..");
        usuario.Endereco.Rua = Console.ReadLine();
        System.Console.WriteLine("informe o número da casa...");
        usuario.Endereco.NumeroDaCasa = Console.ReadLine();
        UsuarioServico.CadastrarUsuarios(usuario);
        UsuarioServico.MensagemFinal();
    }
}