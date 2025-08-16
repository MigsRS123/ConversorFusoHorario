using System;

namespace ConversorFusoHorario
{
    public class AgendaEntrada : IAgendaEntrada
    {
        public DateTime DataHora { get; set; }
        public string Titulo { get; set; }
        public string FusoOrigem { get; set; }

        private readonly IConversorHora _conversor;

        public AgendaEntrada(DateTime dataHora, string titulo, string fusoOrigem, IConversorHora conversor)
        {
            DataHora = dataHora;
            Titulo = titulo;
            FusoOrigem = fusoOrigem;
            _conversor = conversor;
        }

        public void Imprimir(string? idFusoDestino = null)
        {
            var dt = idFusoDestino != null
                ? _conversor.ConverterParaFusoHorario(DataHora, idFusoDestino)
                : DataHora;
            Console.WriteLine($"{dt:dd/MM/yyyy HH:mm} - {Titulo}");
        }

        public void ImprimirHora(string? idFusoDestino = null)
        {
            var dt = idFusoDestino != null
                ? _conversor.ConverterParaFusoHorario(DataHora, idFusoDestino)
                : DataHora;
            Console.WriteLine($"{dt:HH:mm}");
        }

        public void ImprimirDia(string? idFusoDestino = null)
        {
            var dt = idFusoDestino != null
                ? _conversor.ConverterParaFusoHorario(DataHora, idFusoDestino)
                : DataHora;
            Console.WriteLine($"{dt:dd/MM/yyyy}");
        }

        public void ImprimirDiaSemana(string? idFusoDestino = null)
        {
            var dt = idFusoDestino != null
                ? _conversor.ConverterParaFusoHorario(DataHora, idFusoDestino)
                : DataHora;
            Console.WriteLine(dt.ToString("dddd"));
        }
    }
}
