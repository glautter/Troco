using System;
using Troco.App.Domain;
using Xunit;

namespace Troco.Test
{
    public class DomainMoedaTest
    {
        #region Retornar por escrito "valor reais"
        [Fact]
        public void DeveRetornarPorExtensoOValorCinquentaReais()
        {
            var cinquenta = new CinquentaCentavos();

            Assert.Equal("Cinquenta centavos", cinquenta.ToString());
        }

        [Fact]
        public void DeveRetornarPorExtensoOValorVinteReais()
        {
            var vinteCinco = new VinteCincoCentavos();

            Assert.Equal("VinteCinco centavos", vinteCinco.ToString());
        }

        [Fact]
        public void DeveRetornarPorExtensoOValorDezReais()
        {
            var dez = new DezCentavos();

            Assert.Equal("Dez centavos", dez.ToString());
        }

        [Fact]
        public void DeveRetornarPorExtensoOValorCincoReais()
        {
            var cinco = new CincoCentavos();

            Assert.Equal("Cinco centavos", cinco.ToString());
        }

        [Fact]
        public void DeveRetornarPorExtensoOValorUmReal()
        {
            var um = new UmCentavo();

            Assert.Equal("Um centavo", um.ToString());
        }
        #endregion

        #region Retornar valor numérico
        [Fact]
        public void DeveRetornarPorValor_0_50()
        {
            var cinquenta = new CinquentaCentavos();
            Assert.Equal(0.50M, cinquenta.Valor);
        }

        [Fact]
        public void DeveRetornarPorValor_0_25()
        {
            var vinteCinco = new VinteCincoCentavos();
            Assert.Equal(0.25M, vinteCinco.Valor);
        }

        [Fact]
        public void DeveRetornarPorValor_0_10()
        {
            var dez = new DezCentavos();
            Assert.Equal(0.10M, dez.Valor);
        }

        [Fact]
        public void DeveRetornarPorValor_0_05()
        {
            var cinco = new CincoCentavos();
            Assert.Equal(0.05M, cinco.Valor);
        }

        [Fact]
        public void DeveRetornarPorValor_0_01()
        {
            var um = new UmCentavo();
            Assert.Equal(0.01M, um.Valor);
        }
        #endregion
    }
}
