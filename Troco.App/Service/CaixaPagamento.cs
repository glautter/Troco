using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Troco.App.Domain;

namespace Troco.App.Service
{
    public class CaixaPagamento
    {
        private readonly decimal ValorCompra;
        public IList<object> NotasTroco = new List<object>();

        public CaixaPagamento(decimal valorCompra)
        {
            ValorCompra = valorCompra;
        }

        public decimal ValorPago { get; set; }

        public decimal Troco
        {
            get { return ValorPago - ValorCompra; }
        }

        public bool TemCentavos(decimal valor)
        {
            var casasDecimais = valor.ToString().Substring(valor.ToString().LastIndexOf(',') + 1, 2);
            return int.Parse(casasDecimais) > 0;
        }

        public IList<object> SelecionarCedulasDoTroco 
        { 
            get
            {
                var instanciasDeCedulas = GetInstanciasDasCedulas();
                var instanciasDeMoedas = GetInstanciasDasMoedas();
                var troco = Troco;

                while (true)
                {
                    if (troco >= 1)
                    {
                        var cedulaEncontrada = SepararMaiorCedulaDoTroco(troco, instanciasDeCedulas);
                        NotasTroco.Add(cedulaEncontrada);
                        troco -= cedulaEncontrada.Valor;
                    }
                    else if (troco > 0)
                    {
                        var moedaEncontrada = SepararMaiorMoedaDoTroco(troco, instanciasDeMoedas);
                        NotasTroco.Add(moedaEncontrada);
                        troco -= moedaEncontrada.Valor;
                    }

                    if (troco <= 0.00M)
                        break;
                }
                
                return NotasTroco; 
            }
        }

        private Moeda SepararMaiorMoedaDoTroco(decimal troco, Moeda[] instanciasDeMoedas)
        {
            foreach (var moeda in instanciasDeMoedas.OrderByDescending(x => x.Valor))
            {
                if (moeda.Valor <= troco)
                    return moeda;
            }

            return null;
        }

        private Cedula SepararMaiorCedulaDoTroco(decimal troco, Cedula[] instanciasDeCedulas)
        {
            foreach (var cedula in instanciasDeCedulas.OrderByDescending(x => x.Valor))
            {
                if (cedula.Valor <= troco)
                    return cedula;
            }

            return null;
        }

        public CaixaPagamento Pagar(decimal valorPago)
        {
            ValorPago = valorPago;

            return this;
        }

        public Cedula[] GetInstanciasDasCedulas()
        {
            Type parentType = typeof(Cedula);
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            List<Cedula> tipos = new List<Cedula>();

            IEnumerable<Type> subclasses = types.Where(t => t.IsSubclassOf(parentType));

            foreach (var type in subclasses)
                tipos.Add((Cedula)Activator.CreateInstance(type));

            return tipos.OrderBy(x => x.Valor).ToArray();
        }

        public Moeda[] GetInstanciasDasMoedas()
        {
            Type parentType = typeof(Moeda);
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            List<Moeda> tipos = new List<Moeda>();

            IEnumerable<Type> subclasses = types.Where(t => t.IsSubclassOf(parentType));

            foreach (var type in subclasses)
                tipos.Add((Moeda)Activator.CreateInstance(type));

            return tipos.OrderBy(x => x.Valor).ToArray();
        }
    }
}
