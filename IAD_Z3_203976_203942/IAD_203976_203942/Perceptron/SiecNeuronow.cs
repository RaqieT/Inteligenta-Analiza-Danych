using System;
using System.Collections.Generic;
using System.Linq;
using Perceptron.Neurony;

namespace Perceptron
{
    public class SiecNeuronow
    {
        private readonly List<NeuronKopiujacy> _warstwaWejsciowa = new List<NeuronKopiujacy>();

        private readonly List<NeuronUkryty> _warstwaUkryta = new List<NeuronUkryty>();

        private readonly List<NeuronWyjsciowy> _warstwaWyjsciowa = new List<NeuronWyjsciowy>();

        public SiecNeuronow(double krok, bool withBias)
        {
            Random rand = new Random();

            for (int i = 0; i < 4; i++)
            {
                _warstwaWejsciowa.Add(new NeuronKopiujacy());
            }

            if (withBias)
            {
                _warstwaWejsciowa.Add(new NeuronKopiujacy {Wyjscie = 1});
            }

            for (int i = 0; i < 2; i++)
            {
                _warstwaUkryta.Add(new NeuronUkryty(rand, krok, _warstwaWejsciowa.Count));
            }

            for (int i = 0; i < 4; i++)
            {
                _warstwaWyjsciowa.Add(new NeuronWyjsciowy(rand, krok, withBias ? _warstwaUkryta.Count+1 : _warstwaUkryta.Count));
            }
        }

        public List<double> GetUkrytaWarstwaWyniki(WzorTreningowy wzor)
        {
            for (var i = 0; i < wzor.Dane.Count; i++)
            {
                _warstwaWejsciowa[i].Oblicz(wzor.Dane[i]);
            }

            foreach (var neuronUkryty in _warstwaUkryta)
            {
                neuronUkryty.Oblicz(_warstwaWejsciowa.Select(neuron => neuron.Wyjscie));
            }

            return _warstwaUkryta.Select(neuron => neuron.Wyjscie).ToList();
        }

        public List<double> Oblicz(WzorTreningowy wzor)
        {
            for (var i = 0; i < wzor.Dane.Count; i++)
            {
                _warstwaWejsciowa[i].Oblicz(wzor.Dane[i]);
            }

            foreach (var neuronUkryty in _warstwaUkryta)
            {
                neuronUkryty.Oblicz(_warstwaWejsciowa.Select(neuron => neuron.Wyjscie));
            }

            foreach (var outputNeuron in _warstwaWyjsciowa)
            {
                outputNeuron.Oblicz(_warstwaUkryta.Select(neuron => neuron.Wyjscie).Concat(new List<double> {1}));
            }

            return _warstwaWyjsciowa.Select(neuron => neuron.Wyjscie).ToList();
        }

        private void ObliczBlad(WzorTreningowy wzor)
        {
            for (var i = 0; i < _warstwaWyjsciowa.Count; i++)
            {
                _warstwaWyjsciowa[i].ObliczSygnalBledu(wzor.OczekiwanaWartosc[i]);
            }

            for (int i = 0; i < _warstwaUkryta.Count; i++)
            {
                _warstwaUkryta[i].ObliczSygnalBledu(_warstwaWyjsciowa.Sum(neuron => neuron.Wagi[i] * neuron.SygnalBledu));
            }
        }

        private void ObliczPonownieWagi()
        {
            foreach (var outputNeuron in _warstwaWyjsciowa)
            {
                var list = _warstwaUkryta.Select(neuron => neuron.Wyjscie).ToList();
                list.Add(1);
                outputNeuron.ObliczPonownieWagi(list);
            }
            
            foreach (var neuronUkryty in _warstwaUkryta)
            {
                    neuronUkryty.ObliczPonownieWagi(_warstwaWejsciowa.Select(neuron => neuron.Wyjscie).ToList());
            }
        }

        public void Trening(int numberOfEpochs, List<WzorTreningowy> WzorceTreningowe, bool randomWzor)
        {
            for (int z = 0; z < numberOfEpochs; z++)
            {
                List<WzorTreningowy> wzorce = WzorceTreningowe;
                if (randomWzor)
                    wzorce = WzorceTreningowe.OrderBy(a => Guid.NewGuid()).ToList();
                foreach (var wzorceTreningowe in wzorce)
                {
                    Oblicz(wzorceTreningowe);

                    ObliczBlad(wzorceTreningowe);

                    ObliczPonownieWagi();
                }
            }
        }
    }
}