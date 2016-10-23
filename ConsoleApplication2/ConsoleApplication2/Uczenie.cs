using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Uczenie
    {
        double wspUczenia;
        ArrayList listaWag;
        public double[] bledyNeuronu;
        public double skutecznoscUczenia;
        public int liczbaEpok;
        ArrayList listaUczaca;
        public Uczenie(int liczbaEpok, double wspUczenia)
        {
            this.wspUczenia = wspUczenia;
            bledyNeuronu = new double[liczbaEpok];
            this.liczbaEpok = liczbaEpok;
            listaWag = new ArrayList();
            skutecznoscUczenia = 0.0;
        }
        public double Epoka(Warstwa neuron, Warstwa wejscia, double[,] tab)
        {
            listaUczaca = new ArrayList(PrzepiszDane(tab));
            Random random = new Random();
            double sumaBledow = 0;
            int liczebnoscListy = listaUczaca.Count;
            for (int i=0;i<liczebnoscListy;i++)
            {
                int indeks = random.Next(0, listaUczaca.Count);
                double[] pomoc = new double[((double[])listaUczaca[indeks]).Length];
                for (int j = 0; j < pomoc.Length; j++)
                {
                    pomoc[j] = ((double[])listaUczaca[indeks])[j];
                }
                wejscia.UstawWyjscia(pomoc);
                neuron.ObliczWyjscia();
                neuron.ZerujBledy();
                double[] pomoc2 = new double[1];
                pomoc2[0] = ((double[])listaUczaca[indeks])[9];
                neuron.ObliczBledy(pomoc2);
                neuron.PoprawWagi(wspUczenia);
                sumaBledow += ((Neuron)(neuron.Neurony[0])).blad;
                listaUczaca.RemoveAt(indeks);
            }
            return (sumaBledow / listaUczaca.Count);
        }
        public ArrayList PrzepiszDane(double[,] tab)
        {
            int wymiar1 = tab.GetUpperBound(0)+1;
            int wymiar2 = tab.GetUpperBound(1)+1;
            ArrayList lista = new ArrayList();
            double[] pomocnicza = new double[wymiar2];
            for(int i=0;i<wymiar1; i++)
            {
                for (int j=0;j<wymiar2;j++)
                {
                    pomocnicza[j] = tab[i, j];
                }
                lista.Add(pomocnicza);
            }
            return lista;
        }
        public void Nauczanie(DaneUczace dane)
        {
            Random random = new Random();
            IFunkcjaAktywacji funkcja = new FunkcjaProgowa();
            Warstwa neuron = new Warstwa(1,funkcja);
            Warstwa wejscia = new Warstwa(9,funkcja);
            neuron.PolaczWarstwy(wejscia);
            neuron.LosujWagi(-3,3,random);
            foreach (Neuron n in neuron.Neurony)
            {
                foreach (Polaczenie pol in n.wejscia)
                {
                    listaWag.Add(pol.waga);
                }
            }
            for (int i=0; i < liczbaEpok; i++)
            {
                bledyNeuronu[i] = Epoka(neuron, wejscia, dane.zbior_uczacy);
            }

            //walidacja
            int pom = 0;
            int rozmiarListy = dane.zbior_walidujacy.GetUpperBound(0)+1;
            for (int i=0; i < rozmiarListy; i++)
            {
                double[] pomoc = new double[dane.zbior_walidujacy.GetUpperBound(1) + 1];
                for (int j = 0; j <= dane.zbior_walidujacy.GetUpperBound(1); j++)
                {
                    pomoc[j] = dane.zbior_walidujacy[i, j];
                }
                wejscia.UstawWyjscia(pomoc);
                neuron.ObliczWyjscia();
                double[] wyniki = neuron.ZwrocWyjscia();
                double[] pomoc2 = new double[1];
                pomoc2[0] = dane.zbior_walidujacy[i, 9];
                int flaga=0;
                for (int j=0; j <= pomoc2.GetUpperBound(0); j++)
                {
                    if (wyniki[j] != pomoc2[j])
                        flaga = 1;
                }
                if (flaga == 0)
                    pom++;
            }
            skutecznoscUczenia = pom / (dane.zbior_walidujacy.GetUpperBound(0) + 1);
        }
    }
}
