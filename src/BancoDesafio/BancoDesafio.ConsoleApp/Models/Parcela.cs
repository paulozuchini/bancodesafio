namespace BancoDesafio.ConsoleApp.Models
{
    public partial class Parcela
    {
        public int Id { get; set; }
        public int IdFinanciamento { get; set; }
        public int NumeroParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }

        public virtual Financiamento IdFinanciamentoNavigation { get; set; } = null!;
    }
}
