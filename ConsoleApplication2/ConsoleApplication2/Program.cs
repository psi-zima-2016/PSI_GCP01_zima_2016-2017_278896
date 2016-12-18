using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication2
{
    class Program
    {
        public static double znajdzMinimum(double[] tab,int rozmiar)
        {
            int minimum=0;
            for (int i=1; i < rozmiar; i++)
            {
                if (tab[i] < tab[minimum])
                {
                    minimum = i;
                }
            }
            return tab[minimum];
        }

        static void Main(string[] args)
        {
            Random r = new Random();
            FunkcjaLiniowa f = new FunkcjaLiniowa();
            FunkcjaSigmoidalna f1 = new FunkcjaSigmoidalna();
            int liczbaEpok = 100;
            int liczbaSieci = 100;

            DaneUczace dane = new DaneUczace();
            dane.GenerujDane();
            dane.GenerujZbiorUczacy();

            ArrayList sieci = new ArrayList();
            Siec siecWTA = new Siec(1);
            siecWTA.wejscia_sieci = new Warstwa(9, f);
            siecWTA.DodajWarstwe(new Warstwa(1, f));
            siecWTA.PolaczWarstwy();
            Siec siecWTA1 = new Siec(1);
            siecWTA1.wejscia_sieci = new Warstwa(9, f);
            siecWTA1.DodajWarstwe(new Warstwa(1, f));
            siecWTA1.PolaczWarstwy();
            Siec siecWTA2 = new Siec(1);
            siecWTA2.wejscia_sieci = new Warstwa(9, f);
            siecWTA2.DodajWarstwe(new Warstwa(1, f));
            siecWTA2.PolaczWarstwy();
            sieci.Add(siecWTA);
            sieci.Add(siecWTA1);
            sieci.Add(siecWTA2);

            FileStream fileStream = new FileStream("C:\\Users\\Dell Latitude 3330\\Klasyfikacja.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            FileStream fileStream1 = new FileStream("C:\\Users\\Dell Latitude 3330\\CzasKlasyfikacji.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter streamWriter1 = new StreamWriter(fileStream1);

            Stopwatch stopwatch = new Stopwatch();
            ArrayList[] tab = new ArrayList[3];

            for (int i = 0; i < liczbaSieci; i++)
            {
                for (int j = 0; j < sieci.Count; j++)
                    ((Siec)sieci[j]).LosujWagi(-1, 1, r);
                WTA wta = new WTA(liczbaEpok, 0.1, sieci, dane);
                stopwatch.Restart();
                for (int j = 0; j < liczbaEpok; j++)
                    wta.Ucz();

                tab = wta.DzielNaKlasy();
                streamWriter.WriteLine(tab[0].Count);
                streamWriter.WriteLine(tab[1].Count);
                streamWriter.WriteLine(tab[2].Count);
                stopwatch.Stop();
                long time = stopwatch.ElapsedMilliseconds;
                streamWriter1.WriteLine(time);
            }
            streamWriter.Close();
            streamWriter1.Close();
            Console.WriteLine("Zakonczono klasyfikacje!");

            fileStream = new FileStream("C:\\Users\\Dell Latitude 3330\\CzasUczenia.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            streamWriter = new StreamWriter(fileStream);
            ArrayList sieci1 = new ArrayList(150);
            for (int i = 0; i < 150; i++)
            {
                sieci1.Add(new Siec(3));
                ((Siec)sieci1[i]).wejscia_sieci = new Warstwa(9, f1);
                ((Siec)sieci1[i]).DodajWarstwe(new Warstwa(9, f1));
                ((Siec)sieci1[i]).DodajWarstwe(new Warstwa(5, f1));
                ((Siec)sieci1[i]).DodajWarstwe(new Warstwa(1, f1));
                ((Siec)sieci1[i]).PolaczWarstwy();
                ((Siec)sieci1[i]).LosujWagi(-1,1,r);
            }
            ArrayList nauka = new ArrayList(150);
            dane.zbior_uczacy = tab[0];
            for (int i = 0; i < 50; i++)
            {
                nauka.Add(new Nauka(100, 0.2, (Siec)sieci1[i], dane));
            }
            dane.zbior_uczacy = tab[1];
            for (int i = 50; i < 100; i++)
            {
                nauka.Add(new Nauka(100, 0.2, (Siec)sieci1[i], dane));
            }
            dane.zbior_uczacy = tab[2];
            for (int i = 100; i < 150;i++)
            {
                nauka.Add(new Nauka(100, 0.2, (Siec)sieci1[i], dane));
            }

            for (int i = 0; i < 150; i++)
            {
                stopwatch.Restart();
                ((Nauka)nauka[i]).Uczenie();
                stopwatch.Stop();
                long time = stopwatch.ElapsedMilliseconds;
                streamWriter.WriteLine(time);
            }
            streamWriter.Close();

            Console.WriteLine("Zakonczono uczenie!");

            
    
                //zapis wyników do plików
                fileStream = new FileStream("C:\\Users\\Dell Latitude 3330\\BledyUczeniaSieci.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                streamWriter = new StreamWriter(fileStream);

                double[] bledySieci = new double[150];
                for (int i = 0; i < liczbaSieci;i++)
                {
                    bledySieci[i] = znajdzMinimum(((Nauka)nauka[i]).bledyUczenia, liczbaEpok);
                    streamWriter.WriteLine(bledySieci[i]);
                }
                streamWriter.WriteLine();
                streamWriter.Close();

                fileStream = new FileStream("C:\\Users\\Dell Latitude 3330\\BledyUczeniaNajSieci.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                streamWriter = new StreamWriter(fileStream);

                int naj_siec = 0;
                double najm_blad = znajdzMinimum(bledySieci, liczbaSieci);
                for (int i = 0; i < liczbaSieci; i++)
                {
                    if (bledySieci[i] == najm_blad)
                        naj_siec = i;
                }
                for (int i = 0; i < liczbaEpok; i++)
                {
                    streamWriter.WriteLine(((Nauka)nauka[naj_siec]).bledyUczenia[i]);
                }
                streamWriter.Close();

                fileStream = new FileStream("C:\\Users\\Dell Latitude 3330\\BledyWalidacjiNajSieci.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                streamWriter = new StreamWriter(fileStream);

                for (int i = 0; i < liczbaEpok; i++)
                {
                    streamWriter.WriteLine(((Nauka)nauka[naj_siec]).bledyWalidacji[i]);
                }
                streamWriter.Close();

                Console.WriteLine("Zakonczono zapis do pliku!");

                /*fileStream = new FileStream("C:\\Users\\Dell Latitude 3330\\naj_neuron_wagi.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                streamWriter = new StreamWriter(fileStream);

                for (int i = 0; i < liczba_wejsc; i++)
                {
                    streamWriter.WriteLine(uczoneNeurony[naj_neuron].listaWag[i]);
                }
                streamWriter.Close();*/
                //Console.WriteLine("Zapisano do pliku!");
                Console.Read();
        }
    }
}
