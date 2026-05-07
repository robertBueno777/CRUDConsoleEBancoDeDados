
using Microsoft.EntityFrameworkCore;

public class UsuarioRepositorio
{
    public void SalvarNovoUsuarioNoBanco(Usuario usuario)
    {
        var db = new BancoContexto();
        db.Add(usuario);
        db.SaveChanges();
    }
    public void ApagarUsuario(Usuario usuario)
    {
        var db = new BancoContexto();
        db.Remove(usuario);
        db.SaveChanges();
    }
    public Usuario BuscarNoBancoPorNome(string nome)
    {
        var db = new BancoContexto();
        var usuario = db.Usuarios.Include(u => u.Endereco).FirstOrDefault(u => u.Nome == nome);
        if (usuario == null)
        {
            usuario = db.Usuarios.Include(u => u.Endereco).Where(u => u.Nome.ToLower() == nome).FirstOrDefault(u => u.Nome.ToLower() == nome);
        }
        return usuario;
    }
    public void EditarUsuario(Usuario usuario)
    {
        var db = new BancoContexto();
        db.Update(usuario);
        db.SaveChanges();
    }
    public List<Usuario> ListaUsuarios()
    {
        var db = new BancoContexto();
        var listaUsuarios = db.Usuarios.ToList();
        return listaUsuarios;
    }
}