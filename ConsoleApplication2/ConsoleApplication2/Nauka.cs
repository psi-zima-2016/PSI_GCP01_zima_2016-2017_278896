using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Nauka
    {
        public double wspUczenia;
        public double wspZapominania;
        public int liczbaEpok;
        //public double[] listaWag;
        public double[] bledyUczenia;
        public double[] bledyWalidacji;
        ArrayList listaUczaca;
        ArrayList listaWalidujaca;
        public Siec siec;
        public Nauka(int liczbaEpok, double wspUczenia, Siec siec, DaneUczace dane)
        {
            this.liczbaEpok = liczbaEpok;
            this.wspUczenia = wspUczenia;
            wspZapominania = 0;
            bledyUczenia = new double[liczbaEpok];
            bledyWalidacji = new double[liczbaEpok];
            this.siec = siec;
            //listaWag = new double[neuron.liczba_wejsc+1];
            listaUczaca = dane.zbior_uczacy;
            listaWalidujaca = dane.zbior_walidujacy;
        }
        /*public void ZapiszWagi()
        {
            for (int i =0; i<neuron.liczba_wejsc; i++)
            {
                listaWag[i] = neuron.wagi[i];
            }
            listaWag[neuron.liczba_wejsc] = neuron.wagaBiasu;
        }*/
        public double Waliduj()
        {
            double[] bledy = new double[75];
            for (int i = 0; i < 75; i++)
            {
                ((Warstwa)siec.wejscia_sieci).UstawWyjscia((double[])listaWalidujaca[i]);
                siec.ObliczWyjscia();
                double[] temp = new double[1];
                temp[0] = ((double[])listaWalidujaca[i])[9];
                double tempBlad;
                tempBlad = siec.ObliczBlad(temp);
                bledy[i] = tempBlad * tempBlad;
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
            Random r = new Random();
            //tablica błędów cząstkowych epoki
            double[] bledy = new double[177];
            for (int i = 0; i<177; i++)
            {
                int los = r.Next(0, kopiaUczaca.Count);
                ((Warstwa)siec.wejscia_sieci).UstawWyjscia((double[])kopiaUczaca[los]);
                foreach (Warstwa w in siec.warstwy)
                {
                    w.ZerujBledy();
                }
                siec.ObliczWyjscia();
                double[] temp = new double[1];
                temp[0] = ((double[])kopiaUczaca[los])[9];
                double tempBlad;
                ((Warstwa)siec.warstwy[siec.warstwy.Count - 1]).ObliczBledy(temp);
                tempBlad = siec.ObliczBlad(temp);
                bledy[i] = tempBlad*tempBlad;
                //rzutowanie błędów wstecz
                for (int j = siec.warstwy.Count - 1; j >= 0; j--)
                {
                    ((Warstwa)siec.warstwy[j]).RzutujBledy();
                }
                //poprawienie wag
                for (int j = 0; j < siec.warstwy.Count; j++)
                {
                    ((Warstwa)siec.warstwy[j]).PoprawWagi(wspUczenia);
                }
                kopiaUczaca.RemoveAt(los); //usuwamy wykorzystane wektor z listy
            }
            double bladUczenia, suma = 0;
            for (int i = 0; i<177; i++)
            {
                suma += bledy[i];
            }
            bladUczenia = suma / 177;
            return bladUczenia;
        }
        public double UczRegulaOji()
        {
            //kopiowanie listy uczącej
            ArrayList kopiaUczaca = new ArrayList();
            for (int i = 0; i < 177; i++)
            {
                kopiaUczaca.Add(new double[12]);
                for (int j = 0; j < 12; j++)
                {
                    ((double[])kopiaUczaca[i])[j] = ((double[])listaUczaca[i])[j];
                }
            }
            Random r = new Random();
            //tablica błędów cząstkowych epoki
            double[] bledy = new double[177];
            for (int i = 0; i < 177; i++)
            {
                int los = r.Next(0, kopiaUczaca.Count);
                ((Warstwa)siec.wejscia_sieci).UstawWyjscia((double[])kopiaUczaca[los]);
                foreach (Warstwa w in siec.warstwy)
                {
                    w.ZerujBledy();
                }
                siec.ObliczWyjscia();
                double[] temp = new double[3];
                temp[0] = ((double[])kopiaUczaca[los])[9];
                temp[1] = ((double[])kopiaUczaca[los])[10];
                temp[2] = ((double[])kopiaUczaca[los])[11];
                double tempBlad;
                ((Warstwa)siec.warstwy[siec.warstwy.Count - 1]).ObliczBledy(temp);
                tempBlad = siec.ObliczBlad(temp);
                bledy[i] = tempBlad * tempBlad;
                //poprawienie wag
                foreach (Warstwa w in siec.warstwy)
                {
                    w.PoprawWagiOji(wspUczenia, temp);
                }
                kopiaUczaca.RemoveAt(los); //usuwamy wykorzystany wektor z listy
            }
            double bladUczenia, suma = 0;
            for (int i = 0; i < 177; i++)
            {
                suma += bledy[i];
            }
            bladUczenia = suma / 177;
            return bladUczenia;
        }
        public double UczRegulaHebba(double wsp_zapominania)
        {
            //kopiowanie listy uczącej
            ArrayList kopiaUczaca = new ArrayList();
            for (int i = 0; i < 177; i++)
            {
                kopiaUczaca.Add(new double[12]);
                for (int j = 0; j < 12; j++)
                {
                    ((double[])kopiaUczaca[i])[j] = ((double[])listaUczaca[i])[j];
                }
            }
            Random r = new Random();
            //tablica błędów cząstkowych epoki
            double[] bledy = new double[177];
            for (int i = 0; i < 177; i++)
            {
                int los = r.Next(0, kopiaUczaca.Count);
                ((Warstwa)siec.wejscia_sieci).UstawWyjscia((double[])kopiaUczaca[los]);
                foreach (Warstwa w in siec.warstwy)
                {
                    w.ZerujBledy();
                }
                siec.ObliczWyjscia();
                double[] temp = new double[3];
                temp[0] = ((double[])kopiaUczaca[los])[9];
                temp[1] = ((double[])kopiaUczaca[los])[10];
                temp[2] = ((double[])kopiaUczaca[los])[11];
                double tempBlad;
                ((Warstwa)siec.warstwy[siec.warstwy.Count - 1]).ObliczBledy(temp);
                tempBlad = siec.ObliczBlad(temp);
                bledy[i] = tempBlad * tempBlad;
                //poprawienie wag
                foreach (Warstwa w in siec.warstwy)
                {
                    w.PoprawWagiHebb(wspUczenia, wspZapominania, temp);
                }
                kopiaUczaca.RemoveAt(los); //usuwamy wykorzystany wektor z listy
            }
            double bladUczenia, suma = 0;
            for (int i = 0; i < 177; i++)
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
                w = Waliduj();
                bledyWalidacji[i] = w;
            }
        }
    }
}
