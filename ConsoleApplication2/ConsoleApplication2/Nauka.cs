using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Nauka
    {
        /*public double wspUczenia;
        public int liczbaEpok;
        public double[] listaWag;
        public double[] bledyUczenia;
        public double[] bledyWalidacji;
        ArrayList listaUczaca;
        ArrayList listaWalidujaca;
        public Neuron neuron;
        public Nauka(int liczbaEpok, double wspUczenia, Neuron neuron, DaneUczace dane)
        {
            this.liczbaEpok = liczbaEpok;
            this.wspUczenia = wspUczenia;
            bledyUczenia = new double[liczbaEpok];
            bledyWalidacji = new double[liczbaEpok];
            this.neuron = neuron;
            listaWag = new double[neuron.liczba_wejsc+1];
            listaUczaca = dane.zbior_uczacy;
            listaWalidujaca = dane.zbior_walidujacy;
        }
        public void ZapiszWagi()
        {
            for (int i =0; i<neuron.liczba_wejsc; i++)
            {
                listaWag[i] = neuron.wagi[i];
            }
            listaWag[neuron.liczba_wejsc] = neuron.wagaBiasu;
        }
        public double Waliduj()
        {
            double[] bledy = new double[75];
            for (int i = 0; i < 75; i++)
            {
                neuron.UstawWejscia((double[])listaWalidujaca[i]);
                neuron.ObliczWyjscie();
                neuron.ObliczBlad(((double[])listaWalidujaca[i])[9]);
                bledy[i] = neuron.blad*neuron.blad;
            }
            double bladWalidacji, suma = 0;
            for (int i=0; i < 75; i++)
            {
                suma += bledy[i];
            }
            bladWalidacji = suma / 75;
            return bladWalidacji;
        }
        public double Ucz()
        {
            ArrayList kopiaUczaca = new ArrayList();
            for (int i = 0; i < 177; i++)
            {
                kopiaUczaca.Add(new double[10]);
                for (int j = 0; j < 10; j++)
                {
                    ((double[])kopiaUczaca[i])[j] = ((double[])listaUczaca[i])[j];
                }
            }
            Random r = new Random();
            double[] bledy = new double[177];
            for (int i = 0; i<177; i++)
            {
                int los = r.Next(0, kopiaUczaca.Count);
                neuron.UstawWejscia((double[])kopiaUczaca[los]);
                neuron.ObliczWyjscie();
                neuron.ObliczBlad(((double[])kopiaUczaca[los])[9]);
                bledy[i] = neuron.blad*neuron.blad;
                neuron.PoprawWagi(wspUczenia);
                kopiaUczaca.RemoveAt(los);
            }
            double bladUczenia, suma = 0;
            for (int i = 0; i<177; i++)
            {
                suma += bledy[i];
            }
            bladUczenia = suma / 177;
            return bladUczenia;
        }
        public void Uczenie()
        {
            double u, w;
            for (int i = 0; i < liczbaEpok; i++)
            {
                u = Ucz();
                bledyUczenia[i] = u;
                Console.WriteLine(u);
                w = Waliduj();
                bledyWalidacji[i] = w;
            }
        }*/
    }
}
