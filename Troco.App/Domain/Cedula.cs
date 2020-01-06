using System;

namespace Troco.App.Domain
{
    public class Cedula
    {
        public decimal ValorTroco { get; set; }
        public decimal ValorPago { get; set; }

        public decimal Valor
        {
            get
            {
                var nomeClasse = this.GetType().Name;
                var nomePuro = nomeClasse.LastIndexOf("Rea");

                return (nomeClasse.Substring(0, nomePuro < 0 ? 0 : nomePuro)) switch
                {
                    "Um" => 1,
                    "Cinco" => 5,
                    "Dez" => 10,
                    "Vinte" => 20,
                    "Cinquenta" => 50,
                    "Cem" => 100,
                    _ => 0,
                };
            }
            set {
            }
        }

        public override string ToString()
        {
            var nomeClasse = this.GetType().Name;
            var plural = string.Compare(nomeClasse.Substring(nomeClasse.Length - 1), "s", System.StringComparison.CurrentCultureIgnoreCase) == 0;
            return string.Concat(nomeClasse.Remove(plural ? nomeClasse.Length - 5 : nomeClasse.Length - 4), plural ? " reais" : " real");
        }

        public static Cedula operator +(Cedula cedula1, Cedula cedula2)
        {
            var cedula = new Cedula
            {
                ValorPago = ((cedula1.ValorPago > 0 ? cedula1.ValorPago : cedula1.Valor) + cedula2.Valor)
            };

            return cedula;
        }

        public static Cedula operator *(Cedula cedula1, Cedula cedula2)
        {
            var cedula = new Cedula
            {
                ValorPago = ((cedula1.ValorPago > 0 ? cedula1.ValorPago : cedula1.Valor) * cedula2.Valor)
            };

            return cedula;
        }

        public static Cedula operator *(Cedula cedula1, decimal valor)
        {
            var cedula = new Cedula
            {
                ValorPago = ((cedula1.ValorPago > 0 ? cedula1.ValorPago : cedula1.Valor) * valor)
            };

            return cedula;
        }
    }
}