using System;
using System.Collections.Generic;
using System.Globalization;
using ConversorFusoHorario;

class Program
{
    static List<AgendaEntrada> agenda = new List<AgendaEntrada>();
    static IConversorHora conversor = new ConversorHora();

    static List<string> fusosSugeridos = new List<string>
    {
        "UTC",
        "E. South America Standard Time", // Windows São Paulo
        "Pacific Standard Time",
        "GMT Standard Time",
        "Tokyo Standard Time"
    };

    static void Main()
    {
        Console.WriteLine("=== Bem-vindo ao Conversor de Fuso Horário ===");

        while (true)
        {
            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("1 - Cadastrar compromisso");
            Console.WriteLine("2 - Converter compromisso");
            Console.WriteLine("3 - Sair");
            Console.Write("Opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarCompromisso();
                    break;
                case "2":
                    ConverterCompromisso();
                    break;
                case "3":
                    Console.WriteLine("Encerrando o programa...");
                    return;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void CadastrarCompromisso()
    {
        Console.Write("Título do compromisso: ");
        string titulo = Console.ReadLine();

        Console.WriteLine("Escolha o fuso de origem:");
        MostrarFusos();
        string fusoOrigem = EscolherFuso();

        Console.Write("Dia (dd/MM/yyyy): ");
        DateTime data = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

        Console.Write("Hora (HH:mm): ");
        TimeSpan hora = TimeSpan.Parse(Console.ReadLine());

        DateTime dataHora = data.Date + hora;

        agenda.Add(new AgendaEntrada(dataHora, titulo, fusoOrigem, conversor));
        Console.WriteLine("Compromisso cadastrado com sucesso!");
    }

    static void ConverterCompromisso()
    {
        if (agenda.Count == 0)
        {
            Console.WriteLine("Nenhum compromisso cadastrado.");
            return;
        }

        Console.WriteLine("Escolha um compromisso:");
        for (int i = 0; i < agenda.Count; i++)
        {
            Console.Write($"{i + 1} - ");
            agenda[i].Imprimir();
        }

        int escolha;
        while (!int.TryParse(Console.ReadLine(), out escolha) || escolha < 1 || escolha > agenda.Count)
        {
            Console.WriteLine("Escolha inválida, tente novamente.");
        }

        var compromisso = agenda[escolha - 1];

        Console.WriteLine("Escolha o fuso de destino:");
        MostrarFusos();
        string fusoDestino = EscolherFuso();

        var convertido = conversor.ConverterParaFusoHorario(compromisso.DataHora, fusoDestino);

        Console.WriteLine("\n=== Resultado da Conversão ===");
        Console.WriteLine($"Título: {compromisso.Titulo}");
        Console.WriteLine($"Novo horário ({fusoDestino}): {convertido:dd/MM/yyyy HH:mm}");
    }

    static void MostrarFusos()
    {
        for (int i = 0; i < fusosSugeridos.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {fusosSugeridos[i]}");
        }
    }

    static string EscolherFuso()
    {
        int escolha;
        while (!int.TryParse(Console.ReadLine(), out escolha) || escolha < 1 || escolha > fusosSugeridos.Count)
        {
            Console.WriteLine("Escolha inválida, tente novamente.");
        }
        return fusosSugeridos[escolha - 1];
    }
}
