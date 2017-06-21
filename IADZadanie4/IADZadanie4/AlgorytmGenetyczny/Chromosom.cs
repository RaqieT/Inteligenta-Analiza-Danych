using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorytmGenetyczny
{
    public class Chromosom
    {
        public delegate double DoubleReturningDoubleParameterDelegate(double x);

        private DoubleReturningDoubleParameterDelegate evalFunc;
        public Chromosom(int OczekiwanyRozmiar, int value, DoubleReturningDoubleParameterDelegate del)
        {
            IntValue = value;
            List<int> tmp = new List<int>();
            for (int i = 0; i < OczekiwanyRozmiar; i++)
            {
                tmp.Add(value % 2);
                value /= 2;
            }
            tmp.Reverse();
            BinarnaWartosc = tmp.ToArray();
            evalFunc = del;
        }

        public double IntValue { get; set; }

        public int[] BinarnaWartosc { get; set; }

        public double Ocena
        {
            get => evalFunc(IntValue);
        }

        public double Prawdopodobienstwo { get; set; }

        public double Rozklad { get; set; }

        private void PrzeliczanieIntVal()
        {
            IntValue = 0;
            for (int i = 0; i < BinarnaWartosc.Length; i++)
            {
                IntValue += BinarnaWartosc[i] * Math.Pow(2, i);
            }
        }

        public void Krzyzowanie(int index, Chromosom other)
        {
            var chrom1 = BinarnaWartosc.Take(index + 1).ToArray();
            var chrom2 = other.BinarnaWartosc.Take(index + 1).ToArray();
            for (int i = 0; i < index; i++)
            {
                BinarnaWartosc[i] = chrom2[i];
                other.BinarnaWartosc[i] = chrom1[i];
            }
            PrzeliczanieIntVal();
            other.PrzeliczanieIntVal();
        }

        public void Mutowanie(int index)
        {
            BinarnaWartosc[index] += 1;
            BinarnaWartosc[index] %= 2;

            PrzeliczanieIntVal();
        }

        public override string ToString()
        {
            return string.Join("", BinarnaWartosc);
        }
    }
}
