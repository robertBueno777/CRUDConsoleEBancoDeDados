using Microsoft.EntityFrameworkCore;
//aqui em cima to falando q to usando o entityframeworkcore
public class BancoContexto : DbContext
{
    //to criando a classe banco contexto que herda de dbcontext, usaremos o banco contexto para conectar com o nosso banco
    public DbSet<Usuario> Usuarios {get; set;}
    public DbSet<Endereco> Enderecos {get; set;}
    
    //aqui a gente ta falando basicamente : db set, to criando um banco de usuario chamado usuarios
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        //aqui em cima a gente ta usando um metodo ja escrido no dbContext pra escolher onde vamos salvar e como vamos salvar nosso banco
        options.UseSqlite("Data Source=./../../../meubanco.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
   
    }


};
