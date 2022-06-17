namespace BancoDesafio.ConsoleApp.Models
{
    public partial class Financiamento
    {
        public Financiamento()
        {
            Parcelas = new HashSet<Parcela>();
        }

        public int Id { get; set; }
        public int TipoFinanciamento { get; set; }
        public int IdCliente { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataVencimento { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual TipoFinanciamento TipoFinanciamentoNavigation { get; set; } = null!;
        public virtual ICollection<Parcela> Parcelas { get; set; }
    }
}
