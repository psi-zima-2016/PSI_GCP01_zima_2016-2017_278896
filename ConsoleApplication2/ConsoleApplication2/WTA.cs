using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class WTA
    {
        public double wspUczenia;
        public int liczbaEpok;
        ArrayList listaUczaca;
        public ArrayList Neurony;
        public WTA(int liczbaEpok, double wspUczenia, ArrayList sieci, DaneUczace dane)
        {
            this.liczbaEpok = liczbaEpok;
            this.wspUczenia = wspUczenia;
            listaUczaca = dane.zbior_uczacy;
            Neurony = sieci;
        }
        public double ObliczDlugoscWektora(double[] wektor)
        {
            double sumaWsp = 0;
            for (int i = 0; i < wektor.Length; i++)
            {
                sumaWsp += Math.Pow(wektor[i], 2);
            }
            return Math.Sqrt(sumaWsp);
        }
        public double[] NormalizujWektor(double[] wektor)
        {
            double normaWektora;
            normaWektora = ObliczDlugoscWektora(wektor);
            if (normaWektora != 0)
            {
                for (int i = 0; i < wektor.Length; i++)
                {
                    wektor[i] /= normaWektora;
                }
            }
            return wektor;
        }
        public int ZnajdzMinimum(double[] tab)
        {
            int minimum = 0;
            for (int i = 1; i < tab.Length; i++)
            {
                if (tab[i] < tab[minimum])
                {
                    minimum = i;
                }
            }
            return minimum;
        }
        public void Ucz()
        {
            //normalizacja wag każdego z neuronów
            foreach (Siec s in Neurony)
            {
                s.zerujBias();
                s.NormalizujWagi();
            }
            //kopiowanie listy uczącej
            ArrayList kopiaUczaca = new ArrayList();
            for (int i = 0; i < 177; i++)
            {
                kopiaUczaca.Add(new double[9]);
                for (int j = 0; j < 9; j++)
                {
                    ((double[])kopiaUczaca[i])[j] = ((double[])listaUczaca[i])[j];
                }
            }
            //przepuszczenie wszystkich wektorów uczących przez sieć
            Random r = new Random();
            for (int i = 0; i < listaUczaca.Count; i++)
            {
                //losowanie wektora uczącego
                int los = r.Next(0, kopiaUczaca.Count);
                //ustawienie wejść neuronów i obliczenie wyjść
                foreach (Siec s in Neurony)
                {
                    ((Warstwa)s.wejscia_sieci).UstawWyjscia((double[])kopiaUczaca[los]);
                    s.ObliczWyjscia();
                }
                //znalezienie zwycięzcy
                double[] tabWyjsc = new double[Neurony.Count];
                for (int j = 0; j < Neurony.Count; j++)
                {
                    tabWyjsc[j] = ((Siec)Neurony[j]).ZwrocWyjscie();
                }
                int zwyciezca;
                zwyciezca = ZnajdzMinimum(tabWyjsc);
                //poprawienie wag zwycięzcy
                ((Siec)Neurony[zwyciezca]).PoprawWagiWTA(wspUczenia);
                kopiaUczaca.RemoveAt(los); //usuwamy wykorzystany wektor z listy
            }
        }
        public ArrayList[] DzielNaKlasy()
        {
            //utworzenie list dla klas wektorów
            ArrayList[] klasy = new ArrayList[Neurony.Count];
            for (int i = 0; i < Neurony.Count; i++)
            {
                klasy[i] = new ArrayList();
            }
            //kopiowanie listy uczącej
            ArrayList kopiaUczaca = new ArrayList();
            for (int i = 0; i < 177; i++)
            {
                kopiaUczaca.Add(new double[10]);
                for (int j = 0; j < 10; j++)
                {
                    ((double[])kopiaUczaca[i])[j] = ((double[])listaUczaca[i])[j];
                }
            }
            //przepuszczenie wszystkich wektorów uczących przez sieć
            Random r = new Random();
            for (int i = 0; i < listaUczaca.Count; i++)
            {
                //losowanie wektora uczącego
                int los = r.Next(0, kopiaUczaca.Count);
                //ustawienie wejść neuronów i obliczenie wyjść
                foreach (Siec s in Neurony)
                {
                    ((Warstwa)s.wejscia_sieci).UstawWyjscia((double[])kopiaUczaca[los]);
                    s.ObliczWyjscia();
                }
                //znalezienie zwycięzcy
                double[] tabWyjsc = new double[Neurony.Count];
                for (int j = 0; j < Neurony.Count; j++)
                {
                    tabWyjsc[j] = ((Siec)Neurony[j]).ZwrocWyjscie();
                }
                int zwyciezca;
                zwyciezca = ZnajdzMinimum(tabWyjsc);
                klasy[zwyciezca].Add(kopiaUczaca[los]);
                kopiaUczaca.RemoveAt(los); //usuwamy wykorzystany wektor z listy
            }
            return klasy;
        }
    }
}
