using System;
using System.Collections.Generic;
using System.Linq;

namespace Perceptron.Neurony
{
    public class NeuronWyjsciowy
    {
        private readonly double _krok;
        public double Wyjscie { get; private set; }
        public double SygnalBledu { get; private set; }
        public List<double> Wagi { get; }

        public NeuronWyjsciowy(Random rand, double krok, int iloscWag = 2)
        {
            _krok = krok;
            Wagi = new List<double>();
            while (iloscWag-- > 0)
            {
                Wagi.Add(rand.NextDouble() - .5);
            }
        }

        public void Oblicz(IEnumerable<double> inputs)
        {
            var weightedSum = inputs.Zip(Wagi, Tuple.Create).Sum(tuple => tuple.Item1 * tuple.Item2);
            Wyjscie = 1 / (1 + Math.Exp(-weightedSum));
        }

        public void ObliczSygnalBledu(int oczekiwanaWartosc)
        {
            SygnalBledu = Wyjscie * (1 - Wyjscie) * (oczekiwanaWartosc - Wyjscie);
        }

        public void ObliczPonownieWagi(List<double> wartosc)
        {
            for (var i = 0; i < Wagi.Count; i++)
            {
                Wagi[i] = Wagi[i] + SygnalBledu * _krok * wartosc[i];
            }
        }
    }
}
