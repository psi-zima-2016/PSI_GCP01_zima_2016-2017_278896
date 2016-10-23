using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Polaczenie
    {
        public Neuron n;
        public double waga;
        public Polaczenie(Neuron n, double w)
        {
            this.n = n;
            this.waga = w;
        }
    }
}
