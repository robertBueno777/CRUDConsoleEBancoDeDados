using System.Linq;

public class UsuarioRepositorio
{        
    public static void SalvarMudancas()
    {
        var db = new BancoContexto();
        db.SaveChanges();
    }

    public static void SalvarNovoUsuarioNoBanco(Usuario usuario)
    {
        var db = new BancoContexto();
        db.Add(usuario);
        SalvarMudancas();
    }
    public static void ApagarUsuario(Usuario usuario)
    {
        var db = new BancoContexto();
        db.Remove(usuario);
        SalvarMudancas();
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