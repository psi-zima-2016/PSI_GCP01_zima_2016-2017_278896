﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class FunkcjaProgowa : IFunkcjaAktywacji
    {
        public double ObliczWartosc(double wejscie)
        {
            double temp = -1;
            if (wejscie >= 0)
                temp = 1;
            return temp;
        }
        public double Pochodna(double wejscie)
        {
            return 1;
        }
    }
}
