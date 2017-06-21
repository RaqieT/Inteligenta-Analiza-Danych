using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AlgorytmGenetyczny
{
    class Program
    {
        static void Main(string[] args)
        {
            double krzyzowaniePrawd = 0.5;
            double mutacjaPrawd = 0.1;
            double precyzja = 0.001;
            int maxGen = 10;
            Tuple<double, double> interval = new Tuple<double, double>(.5, 2.5);
            int count = (int)((interval.Item2 - interval.Item1) / precyzja);
            int rozmiarBinarny = Convert.ToString(count, 2).Length;
            //int count = (int) Math.Pow(2, rozmiarBinarny);
            //double step = (interval.Item2 - interval.Item1) / count;

            double Oblicz(double x)
            {
                x = interval.Item1 + x * (interval.Item2 - interval.Item1) / (Math.Pow(2, rozmiarBinarny) - 1);          
                return (Math.Exp(x) * Math.Sin(10*Math.PI*x)+1)/x;
            }

            double EvaluationFunction(double x)
            {
                return Oblicz(x) + 10;
            }

            List<Chromosom> populacja = new List<Chromosom>();
            List<Chromosom> nowaPopulacja = new List<Chromosom>();

            Random rand = new Random();

            while (populacja.Count < count)
            {
                populacja.Add(new Chromosom(rozmiarBinarny, rand.Next(0, count), EvaluationFunction));
            }

            for (int gen = 0; gen < maxGen; gen++)
            {
                if (gen > 0)
                {
                    populacja = nowaPopulacja;
                    nowaPopulacja = new List<Chromosom>();
                }
                double evalSum = 0;
                foreach (var chrom in populacja)
                    evalSum += chrom.Ocena;

                double sum = 0.0;

                foreach (var chromosom in populacja)
                {
                    chromosom.Prawdopodobienstwo = chromosom.Ocena / evalSum;
                    sum += chromosom.Prawdopodobienstwo;
                    chromosom.Rozklad = sum;
                    //                    chromosom.Rozklad = sum;
                    //                    sum += chromosom.Ocena / evalSum;
                }
                //              populacja.Last().Rozklad = 1.0;

                while (nowaPopulacja.Count < count)
                {
                    double randomNum = rand.NextDouble();
                    double caseNumber = rand.NextDouble();
                    if (caseNumber < krzyzowaniePrawd)
                    {
                        var theChosenMate = populacja.Last();
                        var theOtherChosenMate = populacja.Last();
                        var randomNum2 = rand.NextDouble();
                        for (int i = populacja.Count - 2; i >= 0; i--)
                        {
                            var inner = populacja[i];
                            if (inner.Rozklad > randomNum && inner.Rozklad < theChosenMate.Rozklad)
                                theChosenMate = inner;
                            if (inner.Rozklad > randomNum2 && inner.Rozklad < theOtherChosenMate.Rozklad)
                                theOtherChosenMate = inner;
                        }
                        //                            var theList1 = populacja
                        //                                .OrderBy(chromosom => Math.Abs(chromosom.Rozklad - randomNum)).ToList();
                        //                            var theChosenMate = theList1.First();
                        //                            randomNum = rand.NextDouble();
                        //                            theList1 = populacja
                        //                                .OrderBy(chromosom => Math.Abs(chromosom.Rozklad - randomNum)).ToList();
                        //                            var theOtherChosenMate = theList1.First();
                        theChosenMate.Krzyzowanie(rand.Next(0, rozmiarBinarny), theOtherChosenMate);
                        nowaPopulacja.Add(theOtherChosenMate);
                        nowaPopulacja.Add(theOtherChosenMate);

                    }
                    else if (caseNumber < krzyzowaniePrawd + mutacjaPrawd)
                    {
                        var theChosenMutant = populacja.Last();
                        for (int i = populacja.Count - 2; i >= 0; i--)
                        {
                            var inner = populacja[i];
                            if (inner.Rozklad > randomNum && inner.Rozklad < theChosenMutant.Rozklad)
                                theChosenMutant = inner;
                        }
                        //                            var theList0 = populacja
                        //                                .OrderBy(chromosom => Math.Abs(chromosom.Rozklad - randomNum)).ToList();
                        //                            var theChosenMutant = theList0.First();
                        theChosenMutant.Mutowanie(rand.Next(0, rozmiarBinarny));
                        nowaPopulacja.Add(theChosenMutant);
                    }
                    else
                    {
                        var theChosenOne = populacja.Last();
                        for (int i = populacja.Count - 2; i >= 0; i--)
                        {
                            var inner = populacja[i];
                            if (inner.Rozklad > randomNum && inner.Rozklad < theChosenOne.Rozklad)
                                theChosenOne = inner;
                        }
                        //                            var theList2 = populacja
                        //                                .OrderBy(chromosom => Math.Abs(chromosom.Rozklad - randomNum)).ToList();
                        //                            var theChosenOne = theList2.First();
                        nowaPopulacja.Add(theChosenOne);

                    }
                }

                
                Console.WriteLine($"Generacja #{gen+1}: Srednia: {Math.Round(nowaPopulacja.Sum(chromosom => Oblicz(chromosom.IntValue)) / nowaPopulacja.Count, precyzja.ToString(CultureInfo.InvariantCulture).Length - 2)}   " +
                                  $"\tNajlepsza: {Math.Round(Oblicz(nowaPopulacja.OrderBy(chromosom => chromosom.Ocena).Last().IntValue),precyzja.ToString(CultureInfo.InvariantCulture).Length - 2)}" +
                                  $"\tWartosc X: {interval.Item1 + nowaPopulacja.OrderBy(chromosom => chromosom.Ocena).Last().IntValue * (interval.Item2 - interval.Item1) / (Math.Pow(2, rozmiarBinarny) - 1)}");

            }
            Console.ReadKey();
        }
    }
}
