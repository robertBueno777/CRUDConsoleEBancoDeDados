
using Microsoft.EntityFrameworkCore;

public class UsuarioRepositorio
{
    public static void SalvarNovoUsuarioNoBanco(Usuario usuario)
    {
        var db = new BancoContexto();
        db.Add(usuario);
        db.SaveChanges();
    }
    public static void ApagarUsuario(Usuario usuario)
    {
        var db = new BancoContexto();
        db.Remove(usuario);
        db.SaveChanges();
    }
    public static Usuario BuscarNoBancoPorNome(string nome)
    {
        var db = new BancoContexto();
        var usuario = db.Usuarios.Include(u => u.Endereco).FirstOrDefault(u => u.Nome == nome);
        if (usuario == null)
        {
            usuario = db.Usuarios.Include(u => u.Endereco).Where(u => u.Nome.ToLower() == nome).FirstOrDefault(u => u.Nome.ToLower() == nome);
        }
        return usuario;
    }
    public static void EditarUsuario(Usuario usuario)
    {
        var db = new BancoContexto();
        db.Update(usuario);
        db.SaveChanges();
    }
    public static List<Usuario> ListaUsuarios()
    {
        var db = new BancoContexto();
        var listaUsuarios = db.Usuarios.ToList();
        return listaUsuarios;
    }
}