using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class FunkcjaLiniowa : IFunkcjaAktywacji
    {
        public double ObliczWartosc(double wejscie)
        {
            return wejscie;
        }
        public double Pochodna(double wejscie)
        {
            return 1.0;
        }
    }
}
