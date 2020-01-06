using System;
using System.Collections.Generic;
using System.Text;

namespace Troco.App.Domain
{
    public abstract class Moeda
    {
        public decimal Valor
        {
            get
            {
                var nomeClasse = this.GetType().Name;

                return (nomeClasse.Substring(0, nomeClasse.LastIndexOf("Centavo"))) switch
                {
                    "Um" => 0.01M,
                    "Cinco" => 0.05M,
                    "Dez" => 0.10M,
                    "VinteCinco" => 0.25M,
                    "Cinquenta" => 0.50M,
                    _ => 0,
                };
            }
        }

        public override string ToString()
        {
            var nomeClasse = this.GetType().Name;
            var plural = string.Compare(nomeClasse.Substring(nomeClasse.Length - 1), "s", System.StringComparison.CurrentCultureIgnoreCase) == 0;
            return string.Concat(nomeClasse.Remove(plural ? nomeClasse.Length - 8 : nomeClasse.Length - 7), plural ? " centavos" : " centavo");
        }
    }
}
