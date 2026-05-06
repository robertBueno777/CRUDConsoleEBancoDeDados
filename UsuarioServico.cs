
public class UsuarioServico
{
    public List<string> MensagensErros { get; set; }
    public UsuarioServico()
    {
        MensagensErros = new List<string>();
    }
    public bool CadastrarUsuarios(Usuario usuario)
    {
        if (UsuarioEhValido(usuario) == false)
            return false;
        UsuarioRepositorio.SalvarNovoUsuarioNoBanco(usuario);
        return true;
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
    public void ApagarUsuario(string nome)
    {
        var usuarioASerApagado = UsuarioRepositorio.BuscarNoBancoPorNome(nome);
        UsuarioRepositorio.ApagarUsuario(usuarioASerApagado);
    }
    public void MostrarUsuario(string nome)
    {
        var exibirInfo = new UsuarioAplicacao.ExibicaoUsuario();
        var usuario = UsuarioRepositorio.BuscarNoBancoPorNome(nome);
        exibirInfo.ExibirInformacoes(usuario);
    }
    public void MostrarTodosUsuarios()
    {
        var exibirInfo = new UsuarioAplicacao.ExibicaoListaUsuario();
        foreach (var usuario in UsuarioRepositorio.ListaUsuarios())
        {
            exibirInfo.ExibirInformacoes(usuario);
        }
    }
    public bool UsuarioJaExiste(string nomeUsuario)//estudar mais esse metodo
    {
        return new BancoContexto().Usuarios.Any(u => u.Nome.Equals(nomeUsuario)) || new BancoContexto().Usuarios.Any(u => u.Nome.ToLower().Equals(nomeUsuario));
    }
    public void EditarUsuario(Usuario usuario)
    {
        UsuarioRepositorio.EditarUsuario(usuario);
    }
}



