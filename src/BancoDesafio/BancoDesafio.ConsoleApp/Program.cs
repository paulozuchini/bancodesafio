using BancoDesafio.ConsoleApp.Models;
using BancoDesafio.ConsoleApp.Services;

namespace BancoDesafio.ConsoleApp
{

    public class Program
    {
        static void Main(string[] args)
        {
            var opcaoEscolhida = "";

            while (opcaoEscolhida != "Q")
            {
                DisplayOpcoes();

                opcaoEscolhida = Console.ReadLine();

                LerOpcaoEscolhida(opcaoEscolhida);

                BreakLines();
            }
            
            Console.Read();
        }

        public static void DisplayOpcoes()
        {
            Console.WriteLine("Sistema de processamento de liberação de credito");
            Console.WriteLine("Autor: Paulo Hilton Zuchini Silva");

            Console.WriteLine("Digite o numero da opcao desejada");
            Console.WriteLine("1- Listar todos os clientes do estado de SP que tenham mais de 60% das parcelas pagas");
            Console.WriteLine("2- Listar os primeiros 4 clientes que tenham alguma parcela com mais de 05 dias atrasados");
            Console.WriteLine("3- Listar todos os clientes que ja atrasaram em algum momento duas ou mais parcelas em mais de 10 dias e que o valor do financiamento seja maior que R$10.000,00");
            Console.WriteLine("4- Cadastrar pedido de financiamento");
            Console.WriteLine("Q- Sair");
        }

        public static void BreakLines()
        {
            Console.WriteLine("");
            Console.WriteLine("");
        }

        public static void LerOpcaoEscolhida(string opcaoEscolhida)
        {
            LiberacaoDeCreditoService service = new LiberacaoDeCreditoService(new BANCODESAFIOContext());

            switch (opcaoEscolhida.ToUpper())
            {
                case "1":
                    service.MenuOpcaoUm();
                    break;
                case "2":
                    service.MenuOpcaoDois();
                    break;
                case "3":
                    service.MenuOpcaoTres();
                    break;
                case "4":
                    service.MenuOpcaoQuatro();
                    break;
                case "Q":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opcao invalida, favor escolher uma opcao valida do menu");
                    break;
            }
        }
    }
}