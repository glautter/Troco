using System;
using System.Linq;
using Troco.App.Domain;
using Troco.App.Service;
using Xunit;

namespace Troco.Test
{
    public class CaixaPagamentoTest
    {
        [Fact]
        public void DeveFazerPagamentoNoValorDe_200()
        {
            var valorProduto = 180M;
            var cedulas = new CemReais() * 2;
            var caixa = new CaixaPagamento(valorProduto);
            var caixaPagto = caixa.Pagar(cedulas.ValorPago);

            Assert.Equal(caixaPagto.ValorPago, cedulas.ValorPago);
        }

        [Fact]
        public void DeveFazerPagamentoNoValorDe_500()
        {
            var valorProduto = 250M;
            var cedulas = new CemReais() * 5;
            var caixa = new CaixaPagamento(valorProduto);
            var caixaPagto = caixa.Pagar(cedulas.ValorPago);

            Assert.Equal(caixaPagto.ValorPago, cedulas.ValorPago);
        }

        [Fact]
        public void DeveFazerPagamentoNoValorDe_500_ReceberTrocoTotal50Reais()
        {
            var valorProduto = 450M;
            var cedulas = new CemReais() * 5;
            var caixa = new CaixaPagamento(valorProduto);
            var caixaPagto = caixa.Pagar(cedulas.ValorPago);

            Assert.Equal(50, caixaPagto.Troco);
        }

        [Fact]
        public void DeveFazerPagamentoNoValorDe_400_ReceberTrocoTotal10Reais()
        {
            var valorProduto = 390M;
            var cedulas = new CemReais() * 4;
            var caixa = new CaixaPagamento(valorProduto);
            var caixaPagto = caixa.Pagar(cedulas.ValorPago);

            Assert.Equal(10, caixaPagto.Troco);
        }

        [Fact]
        public void DeveFazerPagamentoNoValorDe_25_ReceberTrocoTotal25Centavos()
        {
            var valorProduto = 24.75M;
            var cedulas = new VinteReais() + new CincoReais();
            var caixa = new CaixaPagamento(valorProduto);
            var caixaPagto = caixa.Pagar(cedulas.ValorPago);

            Assert.Equal(0.25M, caixaPagto.Troco);
        }

        [Fact]
        public void DeveFazerPagamentoNoValorDe_205_ReceberTrocoTotal50Centavos()
        {
            var valorProduto = 204M;
            var cedulas = new CemReais() * 2 + new CincoReais();
            var caixa = new CaixaPagamento(valorProduto);
            var caixaPagto = caixa.Pagar(cedulas.ValorPago);

            Assert.NotEqual(0.50M, caixaPagto.Troco);
        }

        [Fact]
        public void DeveReceberTrocoDe50ReaisQuandoValorProduto350ReaisMasPagouCom400Reais()
        {
            var valorProduto = 350M;
            var cedulas = new CemReais() * 4;
            var caixa = new CaixaPagamento(valorProduto);
            var caixaPagto = caixa.Pagar(cedulas.ValorPago);

            Assert.Equal(50, caixaPagto.Troco);
        }

        [Fact]
        public void DeveReceberTrocoDe90ReaisQuandoValorProduto910ReaisMasPagouCom1000Reais()
        {
            var valorProduto = 910M;
            var cedulas = new CemReais() * new DezReais();
            var caixa = new CaixaPagamento(valorProduto);
            var caixaPagto = caixa.Pagar(cedulas.ValorPago);

            Assert.Equal(90, caixaPagto.Troco);
        }

        [Fact]
        public void DeveInstanciarTodasAsSubClassesDaCedula()
        {
            var valorProduto = 910M;
            var caixa = new CaixaPagamento(valorProduto);
            var cedulasTroco = caixa.GetInstanciasDasCedulas();

            var umReal = new UmReal();
            var cincoReais = new CincoReais();
            var dezReais = new DezReais();
            var vinteReais = new VinteReais();
            var cinquentaReais = new CinquentaReais();
            var cemReais = new CemReais();

            var valoresCedulas = cedulasTroco.Select(x => x.Valor);

            Assert.Contains(umReal.Valor, valoresCedulas);
            Assert.Contains(cincoReais.Valor, valoresCedulas);
            Assert.Contains(dezReais.Valor, valoresCedulas);
            Assert.Contains(vinteReais.Valor, valoresCedulas);
            Assert.Contains(cinquentaReais.Valor, valoresCedulas);
            Assert.Contains(cemReais.Valor, valoresCedulas);

            Assert.Equal(6, valoresCedulas.Count());
        }

        [Fact]
        public void DeveInstanciarTodasAsSubClassesDaMoeda()
        {
            var valorProduto = 910M;
            var caixa = new CaixaPagamento(valorProduto);
            var cedulasTroco = caixa.GetInstanciasDasMoedas();

            var umCentavo = new UmCentavo();
            var cincoCentavos = new CincoCentavos();
            var dezCentavos = new DezCentavos();
            var vinteCincoCentavos = new VinteCincoCentavos();
            var cinquentaCentavos = new CinquentaCentavos();

            var valoresMoedas = cedulasTroco.Select(x => x.Valor);

            Assert.Contains(umCentavo.Valor, valoresMoedas);
            Assert.Contains(cincoCentavos.Valor, valoresMoedas);
            Assert.Contains(dezCentavos.Valor, valoresMoedas);
            Assert.Contains(vinteCincoCentavos.Valor, valoresMoedas);
            Assert.Contains(cinquentaCentavos.Valor, valoresMoedas);

            Assert.Equal(5, valoresMoedas.Count());
        }

        [Fact]
        public void DeveReceberTrocoDe90ReaisComUmaNotaDeCinquentaReaisEDuasNotasDeVinteReais()
        {
            var valorProduto = 910M;
            var cedulas = new CemReais() * new DezReais();
            var caixa = new CaixaPagamento(valorProduto);
            var caixaPagto = caixa.Pagar(cedulas.ValorPago);

            var notasTroco = caixaPagto.SelecionarCedulasDoTroco;
            var valoresCedulas = notasTroco.Select(x => x.GetType() == typeof(Cedula) ? ((Cedula)x).Valor : ((Moeda)x).Valor);

            var vinteReais = new VinteReais();
            var cinquentaReais = new CinquentaReais();

            Assert.Equal(3, valoresCedulas.Count());

            foreach (var valor in valoresCedulas)
            {
                switch (valor)
                {
                    case 50:
                        {
                            Assert.Equal(cinquentaReais.Valor, valor);
                            Assert.Equal("Cinquenta reais", cinquentaReais.ToString());
                            break;
                        }
                    case 20:
                        {
                            Assert.Equal(vinteReais.Valor, valor);
                            Assert.Equal("Vinte reais", vinteReais.ToString());
                            break;
                        }
                }
            }
        }

        [Fact]
        public void DeveReceberTrocoDe11ReaisE55CentavosComUmaNotaDeDezReaisEUmaNotaDeUmRealEUmaMoedaDeVinteCincoCentavosEDuasMoedasDeDezCentavos()
        {
            var valorProduto = 538.45M;
            var cedulas = new CinquentaReais() * 11;
            var caixa = new CaixaPagamento(valorProduto);
            var caixaPagto = caixa.Pagar(cedulas.ValorPago);

            var notasTroco = caixaPagto.SelecionarCedulasDoTroco;
            var valoresCedulas = notasTroco.Select(x => x.GetType() == typeof(Cedula) ? ((Cedula)x).Valor : ((Moeda)x).Valor);

            var vinteReais = new VinteReais();
            var cinquentaReais = new CinquentaReais();

            Assert.Equal(3, valoresCedulas.Count());

            foreach (var valor in valoresCedulas)
            {
                switch (valor)
                {
                    case 50:
                        {
                            Assert.Equal(cinquentaReais.Valor, valor);
                            Assert.Equal("Cinquenta reais", cinquentaReais.ToString());
                            break;
                        }
                    case 20:
                        {
                            Assert.Equal(vinteReais.Valor, valor);
                            Assert.Equal("Vinte reais", vinteReais.ToString());
                            break;
                        }
                }
            }
        }
    }
}
