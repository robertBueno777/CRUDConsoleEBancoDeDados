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
            var metodos = new Metodos();
            int numeroOpcao = 0;
            List<int> opcoes = new List<int>() { 1, 2, 3, 4, 5, 6 };
            while (numeroOpcao != 6)
            {
                Metodos.MostrarInterface();
                try
                {
                    numeroOpcao = int.Parse(Console.ReadLine());
                    switch (numeroOpcao)
                    {
                        case 1:
                            UsuarioAplicacao.ReceberDadosDoUsuario();
                            break;
                        case 2:
                            Metodos.ApagarUsuario();
                            break;
                        case 3:
                            Metodos.EditarUsuario();
                            break;
                        case 4:
                            Metodos.MostrarUsuario();
                            break;
                        case 5:
                            Metodos.MostrarTodosUsuarios();
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




