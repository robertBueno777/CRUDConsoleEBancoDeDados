
using Microsoft.EntityFrameworkCore.Query.Internal;

public class UsuarioServico
{
    public List<string> MensagensErros { get; set; }
    public UsuarioServico()
    {
        MensagensErros = new List<string>();
    }
    public bool CadastrarUsuarios(Usuario usuario)
    {
        var usuarioRepositorio = new UsuarioRepositorio();
        if (UsuarioEhValido(usuario) == false)
            return false;
        usuarioRepositorio.SalvarNovoUsuarioNoBanco(usuario);
        return true;
    }
    public UsuarioRepositorio ConexaoRepositorio()
    {
        var usuarioRepositorio = new UsuarioRepositorio();
        return usuarioRepositorio;
    }
    public bool UsuarioEhValido(Usuario usuario)
    {
        if (string.IsNullOrEmpty(usuario.Nome.Trim()))
            MensagensErros.Add("ERRO: Campo nome nulo ou vazio");
        if (UsuarioJaExiste(usuario.Nome))
            MensagensErros.Add($"ERRO: Ja existe um usuario com esse nome");
        if (usuario.Idade <= 0 || usuario.Idade > 100)
            MensagensErros.Add($"ERRO: coloque uma idade real");
        if (string.IsNullOrEmpty(usuario.Endereco.Cep))
            MensagensErros.Add($"ERRO: Campo Cep nulo ou vazio");
        if (string.IsNullOrEmpty(usuario.Endereco.Rua))
            MensagensErros.Add($"ERRO: Campo Rua nulo ou vazio");
        if (string.IsNullOrEmpty(usuario.Endereco.NumeroDaCasa))
            MensagensErros.Add($"ERRO: Campo Numero da casa nulo ou vazio");
        return !MensagensErros.Any();
    }
    public bool ErroNomeNuloOuNaoExistenteNoBanco(string nome)
    {
        if (string.IsNullOrEmpty(nome) || UsuarioJaExiste(nome) == false)
        {
            return false;
        }
        return true;
    }
    public Usuario RetornarUsuarioDoBanco(string nome)
    {
        var usuarioRepositorio = new UsuarioRepositorio();
        var usuario = usuarioRepositorio.BuscarNoBancoPorNome(nome);
        return usuario;
    }
    public void ApagarUsuario(string nome)
    {  
        var usuarioRepositorio = new UsuarioRepositorio();
        var usuarioASerApagado = usuarioRepositorio.BuscarNoBancoPorNome(nome);
        usuarioRepositorio.ApagarUsuario(usuarioASerApagado);
    }
    public Usuario MostrarUsuario(string nome)
    {
        var usuarioRepositorio = new UsuarioRepositorio();
        var usuario = usuarioRepositorio.BuscarNoBancoPorNome(nome);
        return usuario;
    }
    public List<Usuario> MostrarTodosUsuarios()
    {
        var usuarioRepositorio = new UsuarioRepositorio();
        List<Usuario> listaUsuarios = usuarioRepositorio.ListaUsuarios();
        return listaUsuarios;
    }
    public bool UsuarioJaExiste(string nomeUsuario)//estudar mais esse metodo
    {
        return new BancoContexto().Usuarios.Any(u => u.Nome.Equals(nomeUsuario)) || new BancoContexto().Usuarios.Any(u => u.Nome.ToLower().Equals(nomeUsuario));
    }
    public void EditarUsuario(Usuario usuario)
    {
        var usuarioRepositorio = new UsuarioRepositorio();
        usuarioRepositorio.EditarUsuario(usuario);
    }
}



