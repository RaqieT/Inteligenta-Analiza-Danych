using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Perceptron
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class Program
    {

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        static void Main(string[] args)
        {
            Console.Title = "Perceptron Created by Robert Radczyc & Dawid Michałowski";
            var krok = 0.1;
            SiecNeuronow network = new SiecNeuronow(krok, true);
            var WzorceTreningowe = new List<WzorTreningowy>
            {
                new WzorTreningowy {Dane = new List<int> {1, 0, 0, 0}, OczekiwanaWartosc = new List<int> {1, 0, 0, 0}},
                new WzorTreningowy {Dane = new List<int> {0, 1, 0, 0}, OczekiwanaWartosc = new List<int> {0, 1, 0, 0}},
                new WzorTreningowy {Dane = new List<int> {0, 0, 1, 0}, OczekiwanaWartosc = new List<int> {0, 0, 1, 0}},
                new WzorTreningowy {Dane = new List<int> {0, 0, 0, 1}, OczekiwanaWartosc = new List<int> {0, 0, 0, 1}}
            };

            network.Trening(1000, WzorceTreningowe, false);
            
            Console.WriteLine("\t\tWyjscie z warstwy Ukrytej:");
            Console.WriteLine("\t\t1 Neuron\t2 Neuron");

            Console.WriteLine("\t\t" + string.Join("\t\t", network.GetUkrytaWarstwaWyniki(WzorceTreningowe[0]).Select(d => Math.Round(d, 4))));
            Console.WriteLine("\t\t" + string.Join("\t\t", network.GetUkrytaWarstwaWyniki(WzorceTreningowe[1]).Select(d => Math.Round(d, 4))));
            Console.WriteLine("\t\t" + string.Join("\t\t", network.GetUkrytaWarstwaWyniki(WzorceTreningowe[2]).Select(d => Math.Round(d, 4))));
            Console.WriteLine("\t\t" + string.Join("\t\t", network.GetUkrytaWarstwaWyniki(WzorceTreningowe[3]).Select(d => Math.Round(d, 4))));

            Console.WriteLine("\t       Wyjscie z warstwy Wynikowej:");
            Console.WriteLine("1 Neuron\t2 Neuron\t3 Neuron\t4 Neuron");
            Console.WriteLine(string.Join("\t\t", network.Oblicz(WzorceTreningowe[0]).Select(d => Math.Round(d, 4))));
            Console.WriteLine(string.Join("\t\t", network.Oblicz(WzorceTreningowe[1]).Select(d => Math.Round(d, 4))));
            Console.WriteLine(string.Join("\t\t", network.Oblicz(WzorceTreningowe[2]).Select(d => Math.Round(d, 4))));
            Console.WriteLine(string.Join("\t\t", network.Oblicz(WzorceTreningowe[3]).Select(d => Math.Round(d, 4))));
            

            Console.ReadKey();

            Console.ReadKey();
        }
    }
}