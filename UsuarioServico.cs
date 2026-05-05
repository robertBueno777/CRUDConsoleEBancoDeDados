using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using ConsoleContexto;



public class UsuarioServico : BancoContexto
{
    public static void CadastrarUsuarios(Usuario usuario)//polish
    {
        if(ValidarUsuario(usuario) == false) 
            return;
        UsuarioRepositorio.SalvarUsuarioNoBanco(usuario);
        System.Console.WriteLine("usuario cadastrado com sucesso.");
    }
    public static bool ValidarUsuario(Usuario usuario)
    {
        if (string.IsNullOrEmpty(usuario.Nome))
        {
            System.Console.WriteLine($"ERRO: Campo nome nulo ou vazio");
            return false;
        }
        if(UsuarioServico.UsuarioJaExiste(usuario.Nome))
        {
            System.Console.WriteLine($"ERRO: Ja existe um usuario com esse nome");
            return false;
        }
        if (usuario.Idade <= 0 || usuario.Idade > 120)
        {
            System.Console.WriteLine($"ERRO: coloque uma idade real");
            return false;
        }
        if (string.IsNullOrEmpty(usuario.Endereco.Cep))
        {
            System.Console.WriteLine($"ERRO: Campo Cep nulo ou vazio");
            return false;
        }
        if (string.IsNullOrEmpty(usuario.Endereco.Rua))
        {
            System.Console.WriteLine($"ERRO: Campo Rua nulo ou vazio");
            return false;
        }
        if (string.IsNullOrEmpty(usuario.Endereco.NumeroDaCasa))
        {
            System.Console.WriteLine($"ERRO: Campo Numero da casa nulo ou vazio");
            return false;
        }
        System.Console.WriteLine("Usuário validado.");
        return true;
    }
    // public static void MensagemValidacaoUsuario()
    // {
    //     string erroNomeVazioOuNulo = "ERRO: Campo nome nulo ou vazio";
    //     string erroUsuarioNomeJaExistente = "ERRO: Ja existe um usuario com esse nome";
    //     string idadeIrreal ="ERRO: coloque uma idade real";
    //     string erroCepNuloOuVazio = "ERRO: Campo Cep nulo ou vazio";
    //     string ruaNulaOuVazia = "ERRO: Campo Rua nulo ou vazio";
    //     string numeroCasaNulaOuVazia ="ERRO: Campo Numero da casa nulo ou vazio";
    // }

    public static void MensagemFinal()
    {
        System.Console.WriteLine("--------------------------------------------");
        System.Console.WriteLine("aperte qualquer tecla para voltar ao menu...");
        System.Console.WriteLine("--------------------------------------------");
        Console.ReadLine();
    }

    //passar só o usuario e excluir o endereco por cascata
    public static void ApagarUsuarioESalvar(Usuario usuario)//polish
    {
        var db = new BancoContexto();
        db.Remove(usuario);
        db.SaveChanges();
        System.Console.WriteLine("Exclusão concluida.");
    }
    public void UsuarioEEnderecoASerEditado(string nome)
    {   
        var usuarioEditado = BuscarNoBancoPorNome(nome);
        System.Console.WriteLine("escolha o novo nome do usuario...");
        usuarioEditado.Nome = Console.ReadLine();
        if (string.IsNullOrEmpty(usuarioEditado.Nome) || UsuarioJaExiste(usuarioEditado.Nome))
            return;
        System.Console.WriteLine("escolha a idade... ");
        usuarioEditado.Idade = int.Parse(Console.ReadLine());
        if (usuarioEditado.Idade <= 0 )
            return;
        System.Console.WriteLine("escolha o novo cep..");
        usuarioEditado.Endereco.Cep = Console.ReadLine();
        if (string.IsNullOrEmpty(usuarioEditado.Endereco.Cep))
            return;
        System.Console.WriteLine("escolha a nova rua..");
        usuarioEditado.Endereco.Rua = Console.ReadLine();
        if (string.IsNullOrEmpty(usuarioEditado.Endereco.Rua))
            return;
        System.Console.WriteLine("escolha novo número da casa..");
        usuarioEditado.Endereco.NumeroDaCasa = Console.ReadLine();
        if (string.IsNullOrEmpty(usuarioEditado.Endereco.NumeroDaCasa))
            return;
        var db = new BancoContexto();
        db.Update(usuarioEditado);
        db.SaveChanges();
        System.Console.WriteLine("Edição concluida...");
    }
    public static void MostrarUsuario()
    {
        var exibirInfo = new ExibicaoUsuario();
        System.Console.WriteLine("digite o nome do usuario desejado....");
        string nome = Console.ReadLine();      
        var usuario = BuscarNoBancoPorNome(nome);
        if(usuario == null)
        {
            System.Console.WriteLine("digite um nome válido, por favor.");
            return; 
        }
        exibirInfo.ExibirInformacoes(usuario);
        MensagemFinal();
    }
    public static void MostrarTodosUsuarios()
    {
        var db = new BancoContexto();
        var exibirInfo = new ExibicaoListaUsuario();
        var listaUsuarios = db.Usuarios.ToList();
        foreach (var usuario in listaUsuarios)
        {
            exibirInfo.ExibirInformacoes(usuario);
        }
        MensagemFinal();
    }
    public static bool UsuarioJaExiste(string nomeUsuario)
    {
        return new BancoContexto().Usuarios.Any(u => u.Nome.Equals(nomeUsuario));
    }
    public interface IInterfaceExibir
    {
        public void ExibirInformacoes(Usuario usuario);
    }
    public class ExibicaoListaUsuario : IInterfaceExibir
    {
        public void ExibirInformacoes(Usuario usuario)
        {
            var novoUsuario = BuscarNoBancoPorNome(usuario.Nome);
            System.Console.WriteLine($"ID: {novoUsuario.Id}");
            System.Console.WriteLine($"NOME: {novoUsuario.Nome}");
            System.Console.WriteLine($"IDADE: {novoUsuario.Idade}");
            if(VerificarSePossuiEndereço(usuario) == false)
                return;
            System.Console.WriteLine($"CEP: {novoUsuario.Endereco.Cep}");
            System.Console.WriteLine($"RUA: {novoUsuario.Endereco.Rua}");
            System.Console.WriteLine($"NUMERO DA CASA: {novoUsuario.Endereco.NumeroDaCasa}");
            System.Console.WriteLine("===========================");
        }
    }
    public static bool VerificarSePossuiEndereço(Usuario usuario)
    {
        var usuarioASerBuscado = BuscarNoBancoPorNome(usuario.Nome);
        if(string.IsNullOrEmpty(usuarioASerBuscado.Endereco.Cep) == true)
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
            if(VerificarSePossuiEndereço(usuario) == false)
                return;
            System.Console.WriteLine($"CEP: {usuario.Endereco.Cep}");
            System.Console.WriteLine($"RUA: {usuario.Endereco.Rua}");
            System.Console.WriteLine($"NUMERO DA CASA: {usuario.Endereco.NumeroDaCasa}");
            System.Console.WriteLine("===========================");
        }
    }
    public static void EditarUsuario()
        {
            var metodos = new Metodos();
            System.Console.WriteLine("digite o nome do usuario que sera editado");
            string nomeUsuarioASerEditado = Console.ReadLine();
            if (string.IsNullOrEmpty(nomeUsuarioASerEditado) || VerificarSeNomeExisteNoBanco(nomeUsuarioASerEditado) == false)
                return;
            metodos.UsuarioEEnderecoASerEditado(nomeUsuarioASerEditado);
            MensagemFinal();
        }
        public static bool VerificarSeNomeExisteNoBanco(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                System.Console.WriteLine("nome nulo não cadastrado no banco.");
                System.Console.WriteLine("pressiona qualquer tecla para tentar novamente....");
                System.Console.ReadLine();
                return false;
            }
            else if (UsuarioJaExiste(nome) == false)
            {
                System.Console.WriteLine("nome não encontrado no banco.");
                System.Console.WriteLine("pressiona qualquer tecla para tentar novamente....");
                Console.ReadLine();
                return false;
            }
            return true;
        }
        public static void CadastroUsuario()
        {
            var metodos = new Metodos();
            metodos.CadastrarUsuarios();
            MensagemFinal();
        }
        public static void ApagarUsuario()
        {
            System.Console.WriteLine("Informe o nome do usuario...");
            string nome = Console.ReadLine();
            if (VerificarSeNomeExisteNoBanco(nome) == false)
                return;
            var usuarioASerApagado = BuscarNoBancoPorNome(nome);
            ApagarUsuarioESalvar(usuarioASerApagado);
            MensagemFinal();
        }
        public static void MostrarInterface()
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

        public static Usuario BuscarNoBancoPorNome(string nome)
        {
            var db = new BancoContexto();
            var usuario = db.Usuarios.Include(u => u.Endereco).FirstOrDefault(u => u.Nome == nome);
            if(usuario == null)
            {
                usuario = db.Usuarios.Include(u => u.Endereco).Where(u => u.Nome.ToLower() == nome).FirstOrDefault( u => u.Nome.ToLower() == nome);
            }
            return usuario;
        }

}



