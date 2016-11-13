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
        public ArrayList wejscia;
        public double wyjscie;
        public double wagaBiasu;
        public double blad;
        //public int liczba_wejsc;
        //public double[] wejscia;
        //public double[] wagi;
        public Neuron(IFunkcjaAktywacji f)
        {
            funkcja = f;
            wyjscie = 0.0;
            wejscia = new ArrayList();
            //this.liczba_wejsc = liczba_wejsc;
            //wejscia = new double[liczba_wejsc];
            //wagi = new double[liczba_wejsc];
            wagaBiasu = 0.0;
            blad = 0.0;
        }
        public void DodajWejscie(Neuron n)
        {
            wejscia.Add(new Polaczenie(n, 1.0));
        }
        public void DodajWejscia(Warstwa w)
        {
            foreach (Neuron n in w.Neurony)
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
            blad = (poprWyjscie - wyjscie) * funkcja.Pochodna(wyjscie);
        }
        public void PoprawWagi(double wspUczenia)
        {
            foreach (Polaczenie p in wejscia)
            {
                p.waga += wspUczenia * blad * p.n.wyjscie;
            }
            wagaBiasu += wspUczenia * blad;
        }
        /*public void ObliczWyjscie()
        {
            wyjscie = 0;
            for (int i = 0; i < liczba_wejsc; i++)
            {
                wyjscie += wejscia[i] * wagi[i];
            }
            wyjscie += wagaBiasu * 1.0;
            wyjscie = funkcja.ObliczWartosc(wyjscie);
        }*/
        /*public void LosujWagi(double min, double max, Random r)
        {
            for (int i = 0; i<liczba_wejsc; i++)
            {
                wagi[i] = (r.NextDouble() * (max - min)) + min;
            }
            wagaBiasu = (r.NextDouble() * (max - min)) + min;
        }*/
        /*public void ObliczBlad(double poprWyjscie)
        {
            blad = poprWyjscie - wyjscie;
        }*/
        /*public void UstawWejscia(double[] wektor)
        {
            for (int i=0; i < liczba_wejsc; i++)
            {
                wejscia[i] = wektor[i];
            }
        }*/
        /*public void PoprawWagi(double wspUczenia)
        {
            for (int i=0;i<liczba_wejsc;i++)
            {
                wagi[i] += wspUczenia * blad * wejscia[i];
            }
            wagaBiasu += wspUczenia * blad;
        }*/
        /*public void DodajWejscia(Neuron n, double w)
        {
            wejscia.Add(new Polaczenie(n, w));
        }*/
        /**/
    }
}
