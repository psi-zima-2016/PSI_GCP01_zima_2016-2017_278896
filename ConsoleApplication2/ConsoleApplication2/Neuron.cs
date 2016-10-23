using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Neuron
    {
        public IFunkcjaAktywacji funkcja;
        public double wyjscie;
        public ArrayList wejscia;
        public double wagaBiasu;
        public double blad;
        public Neuron(IFunkcjaAktywacji f)
        {
            blad = 0.0;
            wyjscie = 0.0;
            wejscia = new ArrayList();
            funkcja = f;
            wagaBiasu = 0.0;
        }
        public void DodajWejscie(Neuron n)
        {
            wejscia.Add(new Polaczenie(n, 1.0));
        }
        public void DodajWejscia(Neuron n, double w)
        {
            wejscia.Add(new Polaczenie(n, w));
        }
        public void DodajWejscia(Warstwa w)
        {
            foreach(Neuron n in w.Neurony)
            {
                DodajWejscie(n);
            }
        }
        public void LosujWagi(double min, double max, Random r)
        {
            foreach (Polaczenie p in wejscia)
            {
                p.waga = (r.NextDouble() * (max - min)) + min;
            }
            wagaBiasu = (r.NextDouble() * (max - min)) + min;
        }
        public void ObliczWyjscie()
        {
            wyjscie = 0.0;
            foreach (Polaczenie p in wejscia)
            {
                wyjscie += p.waga * p.n.wyjscie;
            }
            wyjscie += wagaBiasu * 1.0;
            wyjscie = funkcja.ObliczWartosc(wyjscie);
        }
        public void ObliczBlad(double poprWyjscie)
        {
            blad = poprWyjscie - wyjscie;
        }
        public void AktualizujWagi(double wspUczenia)
        { 
            foreach (Polaczenie p in wejscia)
            {
                p.waga += wspUczenia * blad * p.n.wyjscie;
            }
            wagaBiasu += wspUczenia * blad;
        }
    }
}
