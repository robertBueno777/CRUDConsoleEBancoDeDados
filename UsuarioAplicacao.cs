
public class UsuarioAplicacao
{
    public static void ReceberDadosDoUsuarioECadastrar()
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

    public static void ApagarUsuario()//polish
    {
        System.Console.WriteLine("Informe o nome do usuario...");
        string nome = Console.ReadLine();
        UsuarioServico.ApagarUsuario(nome);
        UsuarioServico.MensagemFinal();
    }

    public static void EditarUsuario()
    {
        System.Console.WriteLine("digite o nome do usuario que sera editado");
        string nome = Console.ReadLine();
        UsuarioServico.ErroNomeNuloOuNaoExistenteNoBanco(nome);
        var usuario = UsuarioRepositorio.BuscarNoBancoPorNome(nome);
        System.Console.WriteLine("escolha o novo nome do usuario...");
        usuario.Nome = Console.ReadLine();
        System.Console.WriteLine("escolha a idade... ");
        usuario.Idade = int.Parse(Console.ReadLine());
        System.Console.WriteLine("escolha o novo cep..");
        usuario.Endereco.Cep = Console.ReadLine();
        System.Console.WriteLine("escolha a nova rua..");
        usuario.Endereco.Rua = Console.ReadLine();
        System.Console.WriteLine("escolha novo número da casa..");
        usuario.Endereco.NumeroDaCasa = Console.ReadLine();
        UsuarioServico.EditarUsuario(usuario);
        UsuarioServico.MensagemFinal();
    }
}