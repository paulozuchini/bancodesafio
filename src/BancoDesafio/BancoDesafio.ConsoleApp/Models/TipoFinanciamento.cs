namespace BancoDesafio.ConsoleApp.Models
{
    public partial class TipoFinanciamento
    {
        public TipoFinanciamento()
        {
            Financiamentos = new HashSet<Financiamento>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public decimal Taxa { get; set; }

        public virtual ICollection<Financiamento> Financiamentos { get; set; }
    }
}
