using BancoDesafio.ConsoleApp.Models;
using System.Globalization;

namespace BancoDesafio.ConsoleApp.Services
{
    public class LiberacaoDeCreditoService
    {
        private readonly BANCODESAFIOContext _context;
        private readonly ClienteService _clienteService;
        private readonly FinanciamentoService _financiamentoService;
        private readonly ParcelaService _parcelaService;

        public LiberacaoDeCreditoService(BANCODESAFIOContext context)
        {
            _context = context;
            _clienteService = new ClienteService(context);
            _financiamentoService = new FinanciamentoService(context);
            _parcelaService = new ParcelaService(context);
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

                _context.Dispose();
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

            _context.Dispose();
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

            _context.Dispose();
        }

        public void MenuOpcaoQuatro()
        {
            MenuTipoCredito();
            
            var inputTipo = Console.ReadLine();
            
            if (SanitaInt(inputTipo) <0 || SanitaInt(inputTipo) > 5)
            {
                Console.WriteLine("Valor digitado invalido");
                return;
            }

            Console.WriteLine("Favor digite o valor do emprestimo:");
            string inputValorEmprestimo = Console.ReadLine();

            if (SanitaDecimal(inputValorEmprestimo) < 0 || SanitaDecimal(inputValorEmprestimo) > 1000000)
            {
                Console.WriteLine("Valor digitado invalido");
                return;
            }

            if(SanitaInt(inputTipo) == 3 && SanitaDecimal(inputValorEmprestimo) < 15000)
            {
                Console.WriteLine("Credito para pessoas juridicas liberado valor somente a partir de 15000");
                return;
            }

            Console.WriteLine("Favor digite a quantidade de parcelas desejada");
            string inputParcelas = Console.ReadLine();

            if(SanitaInt(inputParcelas) < 0 || SanitaInt(inputParcelas) < 5 || SanitaInt(inputParcelas) > 72)
            {
                Console.WriteLine("Quantidade de parcelas deve ser maior que 5 e menor que 72");
                return;
            }

            Console.WriteLine("Digite a data do vencimento desejada:");
            Console.WriteLine("Ano");
            string inputAno = Console.ReadLine();
            Console.WriteLine("Mes");
            string inputMes = Console.ReadLine();
            Console.WriteLine("Dia");
            string inputDia = Console.ReadLine();

            if (SanitaInt(inputAno) < 0 || SanitaInt(inputAno) < DateTime.Today.Year)
            {
                Console.WriteLine("Ano invalido");
                return;
            }

            if (SanitaInt(inputMes) < 0 || SanitaInt(inputMes) > 12)
            {
                Console.WriteLine("Mes invalido");
                return;
            }

            if (SanitaInt(inputDia) < 0 || SanitaInt(inputDia) > 31)
            {
                Console.WriteLine("Dia invalido");
                return;
            }

            DateTime inputData = ConvertToDateTime(inputAno, inputMes, inputDia);
            DateTime dMais15 = DateTime.Today.AddDays(15);
            DateTime dMais40 = DateTime.Today.AddDays(40);

            if (DateTime.Today > inputData.Date
                || dMais15.Date < inputData.Date
                || dMais40.Date < inputData.Date)
            {
                Console.WriteLine("Data de vencimento invalida. Deve ser no formato (yyyy-MM-dd) e entre D+15 e D+40");
                return;
            }

            MenuCliente();
            Console.WriteLine("Selecione um cliente pelo campo ID");
            var inputIdCliente = Console.ReadLine();
            
            if(inputIdCliente != null && SanitaInt(inputIdCliente)< 0)
            {
                Console.WriteLine("ID invalido");
                return;
            }

            Cliente clienteEscolhido = _clienteService.GetById(SanitaInt(inputIdCliente));

            var valorComJurosCompostos = CalculaJurosCompostos(
                    SanitaDecimal(inputValorEmprestimo),
                    SanitaInt(inputTipo),
                    SanitaInt(inputParcelas));

            var idFinanciamentoNew = _financiamentoService.Create(new Financiamento
            {
                Id = 0, //AutoIncrement
                TipoFinanciamento = SanitaInt(inputTipo),
                IdCliente = clienteEscolhido.Id,
                ValorTotal = valorComJurosCompostos,
                DataVencimento = inputData
            });



            for(int i = 1; i <= SanitaInt(inputParcelas); i++)
            {
                _parcelaService.Create(new Parcela
                {
                    Id = 0, //AutoIncrement
                    IdFinanciamento = idFinanciamentoNew,
                    NumeroParcela = i,
                    ValorParcela = CalculaValorParcela(valorComJurosCompostos, SanitaInt(inputParcelas)),
                    DataVencimento = CalculaDataVencimentoPorParcela(
                        dataVencimento: inputData, 
                        numeroParcela: i),
                    DataPagamento = null
                });
            }

            Console.WriteLine("");
            Console.WriteLine("Resultado da Liberacao de Credito");
            Console.WriteLine("Status do credito: APROVADO");
            Console.WriteLine("ID do financiamento criado: {0}", idFinanciamentoNew);
            Console.WriteLine("Cliente ID: {0} ", clienteEscolhido.Id);
            Console.WriteLine("Cliente Nome: {0} ", clienteEscolhido.Nome);
            Console.WriteLine("Tipo do Financiamento: {0} ", SanitaInt(inputTipo));
            Console.WriteLine("Valor solicitado: {0}", SanitaInt(inputValorEmprestimo));
            Console.WriteLine(@"Valor total com juros: {0}", valorComJurosCompostos);
            Console.WriteLine(@"Valor dos juros: {0}", CalculaValorJuros(SanitaDecimal(inputValorEmprestimo), valorComJurosCompostos));
            Console.WriteLine(@"Vencimento da primeira parcela: {0}", inputData.ToString("dd/MM/yyyy"));
            Console.WriteLine("");
            Console.WriteLine("Pressione qualquer tecla para retornar ao Menu");
            Console.ReadKey();
            return;
        }

        public decimal CalculaValorParcela(decimal valorComJurosCompostos, int numeroTotalParcelas)
        {
            var auxValor = valorComJurosCompostos / numeroTotalParcelas;
            return Math.Round(auxValor, 2, MidpointRounding.AwayFromZero);
        }

        public DateTime CalculaDataVencimentoPorParcela(DateTime dataVencimento, int numeroParcela)
        {
            int AddMonth = numeroParcela;
            return dataVencimento.AddMonths(AddMonth);
        }

        public decimal CalculaValorJuros(decimal valorEmprestimo, decimal valorComJurosCompostos)
        {
            return valorComJurosCompostos - valorEmprestimo;
        }

        public decimal CalculaJurosCompostos(decimal valorEmprestimo, int tipoFinanciamento, int parcelas)
        {
            double taxaJuros;

            if (tipoFinanciamento == 1)
                taxaJuros = 2d / 100d;
            else
            if (tipoFinanciamento == 2)
                taxaJuros = 1d / 100d;
            else
            if (tipoFinanciamento == 3)
                taxaJuros = 5d / 100d;
            else
            if (tipoFinanciamento == 4)
                taxaJuros = 3d / 100d;
            else
            if (tipoFinanciamento == 5)
                taxaJuros = (9d / 100);
            else
                throw new ArgumentOutOfRangeException(nameof(tipoFinanciamento), tipoFinanciamento, "Valor invalido");


            double doubleResultValor;
            double doubleValorEmprestimo = decimal.ToDouble(valorEmprestimo);
            double doubleParcelas = decimal.ToDouble(parcelas);

            double divideBy;
            if (tipoFinanciamento == 5)
                divideBy = 12;
            else
                divideBy = 1;

            doubleResultValor = doubleValorEmprestimo * Math.Pow((1 + taxaJuros), doubleParcelas/divideBy);
            
            return Convert.ToDecimal(doubleResultValor);
        }

        public DateTime ConvertToDateTime(string inputAno, string inputMes, string inputDia)
        {
            DateTime result;

            if(inputDia.Length < 2)
            {
                inputDia = 0 + inputDia;
            }

            if(inputMes.Length < 2)
            {
                inputMes = 0 + inputMes;
            }
            
            string dateString = string.Format(@"{0}-{1}-{2}", inputAno, inputMes, inputDia);
            Console.WriteLine(@"Input data: {0}", dateString);

            if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                Console.WriteLine(@"VALIDO: Input data: {0}", dateString);
                return result;
            }
            else
            {
                Console.WriteLine(@"INVALIDO: Input data: {0}", dateString);
                return DateTime.Today;
            }
        }


        public decimal SanitaDecimal(string valor)
        {
            decimal valorDecimal;
            if (decimal.TryParse(valor, out valorDecimal))
            {
                return (decimal)valorDecimal;
            }
            else
            {
                return -1;
            }
        }

        public int SanitaInt(string valor)
        {
            int valorInt;
            if (int.TryParse(valor,out valorInt))
            {
                return (int)valorInt;
            }
            else
            {
                return -1;
            }
        }

        public void MenuTipoCredito()
        {
            Console.WriteLine("Favor digite o tipo de crédito que deseja:");
            Console.WriteLine("1- Credito Direto - Taxa de 2% ao mes");
            Console.WriteLine("2- Credito Consignado - Taxa de 1% ao mes");
            Console.WriteLine("3- Credito Pessoa Juridica - Taxa de 5% ao mes");
            Console.WriteLine("4- Credito Pessoa Fisica - Taxa de 3% ao mes");
            Console.WriteLine("5- Credito Imobiliario - Taxa de 9% ao ano");
        }

        public void MenuCliente()
        {
            IEnumerable<Cliente> Clientes = _clienteService.GetAll();
            foreach(var cliente in Clientes)
            {
                Console.WriteLine(@"Cliente ID: {0}", cliente.Id);
                Console.WriteLine(@"Cliente Nome: {0}", cliente.Nome);
                Console.WriteLine(@"Cliente UF: {0}", cliente.Uf);
                Console.WriteLine(@"Cliente Celular: {0}", cliente.Celular);
                Console.WriteLine("---");
                Console.WriteLine("");
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
