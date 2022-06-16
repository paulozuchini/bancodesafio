namespace BancoDesafio.ConsoleApp.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Financiamentos = new HashSet<Financiamento>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Uf { get; set; } = null!;
        public string Celular { get; set; } = null!;

        public virtual ICollection<Financiamento> Financiamentos { get; set; }
    }
}
