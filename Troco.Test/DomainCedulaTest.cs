using System;
using Troco.App.Domain;
using Xunit;

namespace Troco.Test
{
    public class DomainCedulaTest
    {
        #region Retornar por escrito "valor reais"
        [Fact]
        public void DeveRetornarPorExtensoOValorCemReais()
        {
            var cem = new CemReais();

            Assert.Equal("Cem reais", cem.ToString());
        }

        [Fact]
        public void DeveRetornarPorExtensoOValorCinquentaReais()
        {
            var cinquenta = new CinquentaReais();

            Assert.Equal("Cinquenta reais", cinquenta.ToString());
        }

        [Fact]
        public void DeveRetornarPorExtensoOValorVinteReais()
        {
            var vinte = new VinteReais();

            Assert.Equal("Vinte reais", vinte.ToString());
        }

        [Fact]
        public void DeveRetornarPorExtensoOValorDezReais()
        {
            var dez = new DezReais();

            Assert.Equal("Dez reais", dez.ToString());
        }

        [Fact]
        public void DeveRetornarPorExtensoOValorCincoReais()
        {
            var cinco = new CincoReais();

            Assert.Equal("Cinco reais", cinco.ToString());
        }

        [Fact]
        public void DeveRetornarPorExtensoOValorUmReal()
        {
            var um = new UmReal();

            Assert.Equal("Um real", um.ToString());
        }
        #endregion

        #region Retornar valor numérico
        [Fact]
        public void DeveRetornarValor_1()
        {
            Cedula um = new UmReal();

            Assert.Equal(1, um.Valor);
        }

        [Fact]
        public void DeveRetornarValor_5()
        {
            var cinco = new CincoReais();

            Assert.Equal(5, cinco.Valor);
        }

        [Fact]
        public void DeveRetornarValor_10()
        {
            var dez = new DezReais();

            Assert.Equal(10, dez.Valor);
        }

        [Fact]
        public void DeveRetornarValor_20()
        {
            var vinte = new VinteReais();

            Assert.Equal(20, vinte.Valor);
        }

        [Fact]
        public void DeveRetornarValor_50()
        {
            var cinquenta = new CinquentaReais();

            Assert.Equal(50, cinquenta.Valor);
        }

        [Fact]
        public void DeveRetornarValor_100()
        {
            var cem = new CemReais();

            Assert.Equal(100, cem.Valor);
        }
        #endregion
    }
}
