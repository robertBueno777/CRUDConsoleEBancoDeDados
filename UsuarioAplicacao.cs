
public class UsuarioAplicacao
{
    public void ReceberDadosDoUsuarioECadastrar()
    {
        var usuario = new Usuario();
        System.Console.WriteLine("Informe aqui seu Nome...");
        usuario.Nome = Console.ReadLine();
        System.Console.WriteLine("informe aqui sua idade...");
        int numero;
        while (!int.TryParse(Console.ReadLine(), out numero))
        {
            System.Console.WriteLine("ERRO: coloque somente numeros de 0 a 100");
            System.Console.WriteLine("Tente novamente");
        }
        usuario.Idade = numero;
        System.Console.WriteLine("Informe o cep..");
        usuario.Endereco.Cep = Console.ReadLine();
        System.Console.WriteLine("informe a rua..");
        usuario.Endereco.Rua = Console.ReadLine();
        System.Console.WriteLine("informe o número da casa...");
        usuario.Endereco.NumeroDaCasa = Console.ReadLine();
        UsuarioServico usuarioServico = new UsuarioServico();
        if (!usuarioServico.CadastrarUsuarios(usuario))
            System.Console.WriteLine(string.Join("\n", usuarioServico.MensagensErros));
        else
            System.Console.WriteLine("usuario cadastrado com sucesso.");
        MensagemFinal();
    }
    public void ApagarUsuario()
    {
        UsuarioServico usuarioServico = new UsuarioServico();
        System.Console.WriteLine("Informe o nome do usuario...");
        string nome = Console.ReadLine();
        if (usuarioServico.ErroNomeNuloOuNaoExistenteNoBanco(nome) == false)
        {
            System.Console.WriteLine("ERRO: nome nulo ou não existente no banco.");
            return;
        }
        usuarioServico.ApagarUsuario(nome);
        System.Console.WriteLine("Exclusão feita com sucesso!");
        MensagemFinal();
    }
    public void EditarUsuario()
    {
        System.Console.WriteLine("digite o nome do usuario que sera editado");
        string nome = Console.ReadLine();
        UsuarioServico usuarioServico = new UsuarioServico();
        if (usuarioServico.ErroNomeNuloOuNaoExistenteNoBanco(nome) == false)
        {
            System.Console.WriteLine("ERRO: nome nulo ou não existente no banco.");
            return;
        }
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
        if (usuarioServico.UsuarioEhValido(usuario) == false)
            return;
        usuarioServico.EditarUsuario(usuario);
        System.Console.WriteLine("Edição concluida...");
        MensagemFinal();
    }
    public void MostrarUsuario()
    {
        System.Console.WriteLine("digite o nome do usuario desejado....");
        string nome = Console.ReadLine();
        UsuarioServico usuarioServico = new UsuarioServico();
        if (usuarioServico.ErroNomeNuloOuNaoExistenteNoBanco(nome))
        {
            System.Console.WriteLine("ERRO: nome nulo ou não existente no banco.");
            return;
        }
        usuarioServico.MostrarUsuario(nome);
        MensagemFinal();
    }
    public void MostrarTodosUsuarios()
    {
        UsuarioServico usuarioServico = new UsuarioServico();
        usuarioServico.MostrarTodosUsuarios();
        MensagemFinal();
    }
    public void MensagemFinal()
    {
        System.Console.WriteLine("--------------------------------------------");
        System.Console.WriteLine("aperte qualquer tecla para voltar ao menu...");
        System.Console.WriteLine("--------------------------------------------");
        Console.ReadLine();
    }
    public void MostrarInterface()
    {
        System.Console.WriteLine("===============================");
        System.Console.WriteLine("1. cadastrar usuario ==========");
        System.Console.WriteLine("2. Apagar usuario =============");
        System.Console.WriteLine("3. Editar Usuario =============");
        System.Console.WriteLine("4. Mostrar Usuario ============");
        System.Console.WriteLine("5. Mostrar todos os usuarios ==");
        System.Console.WriteLine("6. Sair =======================");
        System.Console.WriteLine("===============================");
        System.Console.WriteLine("digite a opção desejada....");
    }
    public interface IInterfaceExibir
    {
        public void ExibirInformacoes(Usuario usuario);
    }
    public class ExibicaoListaUsuario : IInterfaceExibir
    {
        public void ExibirInformacoes(Usuario usuario)
        {
            var novoUsuario = UsuarioRepositorio.BuscarNoBancoPorNome(usuario.Nome);
            System.Console.WriteLine($"ID: {novoUsuario.Id}");
            System.Console.WriteLine($"NOME: {novoUsuario.Nome}");
            System.Console.WriteLine($"IDADE: {novoUsuario.Idade}");
            if (VerificarSePossuiEndereço(usuario) == false)
                return;
            System.Console.WriteLine($"CEP: {novoUsuario.Endereco.Cep}");
            System.Console.WriteLine($"RUA: {novoUsuario.Endereco.Rua}");
            System.Console.WriteLine($"NUMERO DA CASA: {novoUsuario.Endereco.NumeroDaCasa}");
            System.Console.WriteLine("===========================");
        }
    }
    public static bool VerificarSePossuiEndereço(Usuario usuario)
    {
        var usuarioASerBuscado = UsuarioRepositorio.BuscarNoBancoPorNome(usuario.Nome);
        if (string.IsNullOrEmpty(usuarioASerBuscado.Endereco.Cep) == true)
        {
            System.Console.WriteLine($"CEP: CEP NAO ENCONTRADO.");
            System.Console.WriteLine($"RUA: RUA NAO ENCONTRADA");
            System.Console.WriteLine($"NUMERO DA CASA: NUMERO DA CASA NAO ENCONTRADA");
            System.Console.WriteLine("===========================");
            return false;
        }
        else
            return true;
    }
    public class ExibicaoUsuario : IInterfaceExibir
    {
        public void ExibirInformacoes(Usuario usuario)
        {
            System.Console.WriteLine("===========================");
            System.Console.WriteLine($"ID: {usuario.Id}");
            System.Console.WriteLine($"NOME: {usuario.Nome}");
            System.Console.WriteLine($"IDADE: {usuario.Idade}");
            if (VerificarSePossuiEndereço(usuario) == false)
                return;
            System.Console.WriteLine($"CEP: {usuario.Endereco.Cep}");
            System.Console.WriteLine($"RUA: {usuario.Endereco.Rua}");
            System.Console.WriteLine($"NUMERO DA CASA: {usuario.Endereco.NumeroDaCasa}");
            System.Console.WriteLine("===========================");
        }
    }
}