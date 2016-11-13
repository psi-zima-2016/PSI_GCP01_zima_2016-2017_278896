using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class FunkcjaSigmoidalna : IFunkcjaAktywacji
    {
        public double ObliczWartosc(double wejscie)
        {
            double temp;
            temp = 1.0 / (1.0+Math.Exp((-1)*wejscie));
            return temp;
        }
        public double Pochodna(double wejscie)
        {
            double temp;
            temp = ObliczWartosc(wejscie) * (1.0 - ObliczWartosc(wejscie));
            return temp;
        }
    }
}
