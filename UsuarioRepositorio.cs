public class UsuarioRepositorio
{
    public static void SalvarUsuarioNoBanco(Usuario usuario)
    {
        var db = new BancoContexto();
        db.Add(usuario);
        db.SaveChanges();
    }
}