using Microsoft.EntityFrameworkCore.Sqlite;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace ConsoleContexto
{
    public class Program
    {
        static void Main(string[] args)
        {
            int numeroOpcao = 0;
            while (numeroOpcao != 6)
            {
                var usuarioAplicacao = new UsuarioAplicacao();
                usuarioAplicacao.MostrarInterface();
                try
                {
                    numeroOpcao = int.Parse(Console.ReadLine());
                    switch (numeroOpcao)
                    {
                        case 1:
                            usuarioAplicacao.ReceberDadosDoUsuarioECadastrar();
                            break;
                        case 2:
                            usuarioAplicacao.ApagarUsuario();
                            break;
                        case 3:
                            usuarioAplicacao.EditarUsuario();
                            break;
                        case 4:
                            usuarioAplicacao.MostrarUsuario();
                            break;
                        case 5:
                            usuarioAplicacao.MostrarTodosUsuarios();
                            break;
                        case 6:
                            System.Console.WriteLine("saindo....");
                            return;
                        default:
                            System.Console.WriteLine("digite um valor valido, por favor.");
                            break;
                    }
                }
                catch
                {
                    System.Console.WriteLine("erro: insira um caracter valido.");
                    System.Console.WriteLine("pressiona qualquer tecla para tentar novamente....");
                    Console.ReadLine();
                }
            }
            ;
        }
    }
}




