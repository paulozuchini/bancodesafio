using BancoDesafio.ConsoleApp.Models;

namespace BancoDesafio.ConsoleApp.Services
{
    public class LiberacaoDeCreditoService
    {
        private BANCODESAFIOContext _context;

        public LiberacaoDeCreditoService(BANCODESAFIOContext context)
        {
            _context = context;
        }


        public void MenuOpcaoUm()
        {
            Console.WriteLine("Carregando resultados...");

            using (BANCODESAFIOContext con = _context)
            {
                var getclientes = con.Clientes.Where(p => p.Uf == "SP");
                List<OpcaoUm> result = new List<OpcaoUm>();

                foreach (var cliente in getclientes)
                {
                    var financiamentos = con.Financiamentos.Where(p => p.IdCliente == cliente.Id);

                    foreach (var financiamento in financiamentos)
                    {
                        var parcelasPagas = con.Parcelas.Where(
                            p => p.IdFinanciamento == financiamento.Id
                            && p.DataPagamento != null
                            );


                        var valorParcelasPagas = parcelasPagas.Sum(p => p.ValorParcela);

                        var porcentagemPago = valorParcelasPagas / financiamento.ValorTotal * 100;

                        if (porcentagemPago > 60)
                        {
                            result.Add(new OpcaoUm
                            {
                                IdCliente = cliente.Id,
                                Nome = cliente.Nome,
                                Uf = cliente.Uf,
                                IdFinanciamento = financiamento.Id,
                                PorcentagemPago = porcentagemPago,
                                ValorPago = valorParcelasPagas,
                                ValorFinanciamento = financiamento.ValorTotal
                            });
                        }

                    }
                }

                if(result.Count > 0)
                {
                    Console.WriteLine("Resultado:");
                    Console.WriteLine("");

                    foreach (var item in result)
                    {
                        Console.WriteLine("IdCliente: {0}", item.IdCliente);
                        Console.WriteLine("Nome: {0}", item.Nome);
                        Console.WriteLine("Uf: {0}", item.Uf);
                        Console.WriteLine("IdFinanciamento: {0}", item.IdFinanciamento);
                        Console.WriteLine("PorcentagemPago: {0}", item.PorcentagemPago);
                        Console.WriteLine("---");
                        Console.WriteLine("");
                    }
                }
                else
                {
                    Console.WriteLine("Nao foram encontrados resultados para a opcao desejada:");
                    Console.WriteLine("");
                }
            }
        }

        public void MenuOpcaoDois()
        {
            Console.WriteLine("Carregando resultados...");

            using (BANCODESAFIOContext con = _context)
            {
                var getclientes = con.Clientes;
                List<OpcaoDois> result = new List<OpcaoDois>();

                foreach (var cliente in getclientes)
                {
                    var financiamentos = con.Financiamentos.Where(p => p.IdCliente == cliente.Id);

                    foreach (var financiamento in financiamentos)
                    {
                        var parcelasAtrasadas = con.Parcelas.Where(
                            p => p.IdFinanciamento == financiamento.Id
                            && p.DataPagamento == null
                            && DateTime.Today > p.DataVencimento.Date);

                        foreach(var parcela in parcelasAtrasadas)
                        {
                            var vencimento = parcela.DataVencimento.Date;
                            var today = DateTime.Today;

                            if (today > vencimento)
                            {
                                TimeSpan atrasoSpan = today - vencimento;
                                
                                result.Add(new OpcaoDois
                                {
                                    IdCliente = cliente.Id,
                                    Nome = cliente.Nome,
                                    IdFinanciamento = financiamento.Id,
                                    ValorFinanciamento = financiamento.ValorTotal,
                                    IdParcela = parcela.Id,
                                    NumeroParcela = parcela.NumeroParcela,
                                    ValorParcela = parcela.ValorParcela,
                                    DataVencimento = vencimento,
                                    DiasEmAtraso = atrasoSpan.Days
                                });
                            }
                        }
                    }
                }

                if(result.Count > 0)
                {
                    Console.WriteLine("Resultado:");
                    Console.WriteLine("");

                    foreach (var item in result)
                    {
                        Console.WriteLine("IdCliente: {0}", item.IdCliente);
                        Console.WriteLine("Nome: {0}", item.Nome);
                        Console.WriteLine("IdFinanciamento: {0}", item.IdFinanciamento);
                        Console.WriteLine("ValorFinanciamento: {0}", item.ValorFinanciamento);
                        Console.WriteLine("IdParcela: {0}", item.IdParcela);
                        Console.WriteLine("NumeroParcela: {0}", item.NumeroParcela);
                        Console.WriteLine("ValorParcela: {0}", item.ValorParcela);
                        Console.WriteLine("DataVencimento: {0}", item.DataVencimento.ToString("dd/MM/yyyy"));
                        Console.WriteLine("DiasEmAtraso: {0}", item.DiasEmAtraso);
                        Console.WriteLine("---");
                        Console.WriteLine("");
                    }
                }
                else
                {
                    Console.WriteLine("Nao foram encontrados resultados para a opcao desejada:");
                    Console.WriteLine("");
                }
            }
        }

        public void MenuOpcaoTres()
        {
            Console.WriteLine("Carregando resultados...");

            using (BANCODESAFIOContext con = _context)
            {
                var getclientes = con.Clientes;
                List<OpcaoTres> resultParcial = new List<OpcaoTres>();

                foreach (var cliente in getclientes)
                {
                    var financiamentos = con.Financiamentos.Where(p => p.IdCliente == cliente.Id);

                    foreach (var financiamento in financiamentos)
                    {
                        var parcelasAtrasadas = con.Parcelas.Where(
                            p => p.IdFinanciamento == financiamento.Id
                            && DateTime.Today > p.DataVencimento.Date
                            && (p.DataPagamento > p.DataVencimento || p.DataPagamento == null));

                        foreach (var parcela in parcelasAtrasadas)
                        {
                            var vencimento = parcela.DataVencimento.Date;
                            var today = DateTime.Today;
                            TimeSpan atrasoSpan = new TimeSpan();

                            if (today > vencimento && parcela.DataPagamento == null)
                                atrasoSpan = today - vencimento;
                            else
                                if (parcela.DataPagamento.Value.Date > vencimento)
                                    atrasoSpan = parcela.DataPagamento.Value.Date - vencimento;

                            var stringDataPagamento = parcela.DataPagamento.HasValue ? parcela.DataPagamento.Value.ToString("dd/MM/yyyy") : "NAO PAGO";

                            if (atrasoSpan.Days > 10)
                            {
                                resultParcial.Add(new OpcaoTres
                                {
                                    IdCliente = cliente.Id,
                                    Nome = cliente.Nome,
                                    IdFinanciamento = financiamento.Id,
                                    ValorFinanciamento = financiamento.ValorTotal,
                                    IdParcela = parcela.Id,
                                    NumeroParcela = parcela.NumeroParcela,
                                    ValorParcela = parcela.ValorParcela,
                                    DataVencimento = vencimento,
                                    DataPagamento = stringDataPagamento,
                                    DiasEmAtraso = atrasoSpan.Days
                                });
                            }
                        }
                    }
                }

                List<OpcaoTres> individualClientList = new List<OpcaoTres>();
                List<OpcaoTres> resultFinal = new List<OpcaoTres>();

                var _auxId = 0;
                var _lastId = 0;

                foreach(var item in resultParcial)
                {
                    if(item.IdCliente == _auxId && item.DiasEmAtraso > 10)
                    {
                        individualClientList.Add(item);
                    }
                    else
                    {
                        if (_auxId == 0 || individualClientList.Count == 0)
                        {
                            individualClientList.Add(item);
                            _auxId = item.IdCliente;
                        }
                        else
                        {
                            if (individualClientList.Count > 2)
                            {
                                foreach (var indv in individualClientList)
                                {
                                    resultFinal.Add(indv);
                                }
                            }
                            _auxId = item.IdCliente;
                            individualClientList = new List<OpcaoTres>();
                            individualClientList.Add(item);
                        }
                    }
                }

                if(resultFinal.Count > 0)
                {
                    Console.WriteLine("Resultado:");
                    Console.WriteLine("");

                    foreach (var item in resultFinal)
                    {
                        Console.WriteLine("IdCliente: {0}", item.IdCliente);
                        Console.WriteLine("Nome: {0}", item.Nome);
                        Console.WriteLine("IdFinanciamento: {0}", item.IdFinanciamento);
                        Console.WriteLine("ValorFinanciamento: {0}", item.ValorFinanciamento);
                        Console.WriteLine("IdParcela: {0}", item.IdParcela);
                        Console.WriteLine("NumeroParcela: {0}", item.NumeroParcela);
                        Console.WriteLine("ValorParcela: {0}", item.ValorParcela);
                        Console.WriteLine("DataVencimento: {0}", item.DataVencimento.ToString("dd/MM/yyyy"));
                        Console.WriteLine("DataPagamento: {0}", item.DataPagamento);
                        Console.WriteLine("DiasEmAtraso: {0}", item.DiasEmAtraso);
                        Console.WriteLine("---");
                        Console.WriteLine("");
                    }
                }
                else
                {
                    Console.WriteLine("Nao foram encontrados resultados para a opcao desejada:");
                    Console.WriteLine("");
                }
            }
        }
    }

    public class OpcaoUm
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }
        public int IdFinanciamento { get; set; }
        public decimal PorcentagemPago { get; set; }
        public decimal ValorPago { get; set;}
        public decimal ValorFinanciamento { get;set;}
    }

    public class OpcaoDois
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public int IdFinanciamento { get; set; }
        public decimal ValorFinanciamento { get; set; }
        public int IdParcela { get; set; }
        public int NumeroParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public int DiasEmAtraso { get; set; }
    }

    public class OpcaoTres
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public int IdFinanciamento { get; set; }
        public decimal ValorFinanciamento { get; set; }
        public int IdParcela { get; set; }
        public int NumeroParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public string DataPagamento { get; set; }
        public int DiasEmAtraso { get; set; }
    }
}
