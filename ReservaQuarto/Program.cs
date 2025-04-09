using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.NetworkInformation;

namespace ReservaQuarto
{
    class Program
    {
        static List<Quarto> quartos = new List<Quarto>();
        static List<Reserva> reservas = new List<Reserva>();

        static void Main(string[] args)
        {
            InicializarQuartos();

            bool executando = true;

            while (executando)
            {
                Console.Clear();
                Console.WriteLine("=== Sistema de Reserva de Quartos ===");
                Console.WriteLine("1. Reservar Quarto");
                Console.WriteLine("2. Consultar Disponibilidade");
                Console.WriteLine("3. Alterar status do quarto");
                Console.WriteLine("4. Sair");
                Console.WriteLine("Escolha uma opção:");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        FazerReserva();
                        break;
                    case "2":
                        ConsultarDisponibilidade();
                        break;
                    case "3":
                        AlterarStatusQuarto();
                        break;
                    case "4":
                        executando = false;
                        Console.WriteLine("Saindo do sistema. Obrigada por usar o gerenciador de quartos! :)");
                        break;
                    default:
                        Console.WriteLine("Opção inválda. Tente novamente");
                        break;
                }

                if (executando)
                {
                    Console.WriteLine("\nPressione ENTER para continuar");
                }
            }
        }

        static void InicializarQuartos()
        {
            quartos.Add(new Quarto { Numero = 101, Tipo = TipoQuarto.Single, PrecoPorNoite = 150, Status = StatusQuarto.Disponivel });
            quartos.Add(new Quarto { Numero = 102, Tipo = TipoQuarto.Duplo, PrecoPorNoite = 220, Status = StatusQuarto.EmManutencao });
            quartos.Add(new Quarto { Numero = 103, Tipo = TipoQuarto.Suite, PrecoPorNoite = 350, Status = StatusQuarto.Disponivel });
        }

        static void FazerReserva()
        {
            Console.Clear();
            Console.WriteLine("=== Reservar Quarto ===");

            var QuartosDisponiveis = quartos.FindAll(q => q.Status == StatusQuarto.Disponivel);

            if (QuartosDisponiveis.Count == 0)
            {
                Console.WriteLine("Nenhum quarto disponível no momento.");
                return;
            }

            Console.WriteLine("Quartos disponíveis:");
            foreach (var quarto in QuartosDisponiveis)
            {
                Console.WriteLine($"Número: {quarto.Numero}, Tipo: {quarto.Tipo}, Preço por noite: {quarto.PrecoPorNoite}, Status {quarto.Status}");
            }

            Console.WriteLine("Digite o número do quarto que deseja reservar:");
            if (!int.TryParse(Console.ReadLine(), out int numeroQuarto))
            {
                Console.WriteLine("Número de quarto inválido. Tente novamente.");
                return;
            }

            var quartoSelecionado = quartos.Find(q => q.Numero == numeroQuarto);

            if(quartoSelecionado == null)
            {
                Console.WriteLine("Quarto não encontrato.");
                return;
            }

            if (quartoSelecionado.Status != StatusQuarto.Disponivel)
            {
                Console.WriteLine("Quarto indisponível");
                return;
            }

            Console.WriteLine("Nome do hóspede:");
            string nome = Console.ReadLine();

            Console.WriteLine("Documento:");
            string documento = Console.ReadLine();

            Console.WriteLine("Data de Check-in (dd/mm/yyyy): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime checkIn))
            {
                Console.WriteLine("Data inválida.");
                return;
            }

            Console.WriteLine("Data de Check-Out (dd/mmm/yyyy): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime checkOut))
            {
                Console.WriteLine("Data inválida.");
                return;
            }

            if (checkOut <= checkIn)
            {
                Console.WriteLine("Data de Check-Out deve ser maior que a data de Check-In.");
                return;
            }

            int numeroNoites = (checkOut - checkIn).Days;
            decimal valorTotal = quartoSelecionado.PrecoPorNoite * numeroNoites;

            if (numeroNoites > 5)
            {
                valorTotal *= 0.9m;
            }

            reservas.Add(new Reserva
            {
                Quarto = quartoSelecionado,
                NomeHospede = nome,
                Documento = documento,
                Checkin = checkIn,
                Checkout = checkOut,
                CustoTotal = valorTotal
            });

            quartoSelecionado.Status = StatusQuarto.Ocupado;

            Console.WriteLine($"\nReserva realizada com sucesso!");
            Console.WriteLine($"Número de noites: {numeroNoites}");
            Console.WriteLine($"Valor total: R${valorTotal:F2}");
        }

        static void ConsultarDisponibilidade()
        {
            Console.Clear();
            Console.WriteLine("=== Consultar Disponibilidade ===");

            foreach (var quarto in quartos)
            {
                Console.WriteLine($"Número: {quarto.Numero}");
                Console.WriteLine($"Tipo: {quarto.Tipo}");
                Console.WriteLine($"Preço por noite: {quarto.PrecoPorNoite:F2}");
                Console.WriteLine($"Status {quarto.Status}");
                Console.WriteLine(new string('-', 30));
            }

            Console.WriteLine("Pressione ENTER para voltar ao menu principal.");
            Console.ReadLine();
        }

        static void AlterarStatusQuarto()
        {
            Console.Clear();
            Console.WriteLine(" === Alterar Status do Quarto ===\n");

            foreach (var q in quartos)
            {
                Console.WriteLine($"Número: {q.Numero} | Tipo: {q.Tipo} | Status atual: {q.Status}");
            }

            Console.WriteLine("\nDigite o número do quarto que deseja alterar o status:");
            if (!int.TryParse(Console.ReadLine(), out int numeroQuarto))
            {
                Console.WriteLine("Número inválido");
                return;
            }

            var quarto = quartos.Find(q => q.Numero == numeroQuarto);

            if (quarto == null)
            {
                Console.WriteLine("Quarto não encontrado.");
                return;
            }

            Console.WriteLine("Escolha o novo status:");
            Console.WriteLine("1. Disponível");
            Console.WriteLine("2. Ocupado");
            Console.WriteLine("3. Em Manutenção");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    quarto.Status = StatusQuarto.Disponivel;
                    Console.WriteLine("Status alterado para Disponível.");
                    break;
                case "2":
                    quarto.Status = StatusQuarto.Ocupado;
                    Console.WriteLine("Status alterado para Ocupado.");
                    break;
                case "3":
                    quarto.Status = StatusQuarto.EmManutencao;
                    Console.WriteLine("Status alterado para Em Manutenção.");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}