using System;
using System.Collections;
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
            FunkcjaSigmoidalna f = new FunkcjaSigmoidalna();
            int liczbaEpok = 100;
            int liczbaSieci = 100;

            DaneUczace dane = new DaneUczace();
            dane.GenerujDane();
            dane.GenerujZbiorUczacy();

            Nauka[] uczoneSieci = new Nauka[liczbaSieci];
            for (int i = 0; i < liczbaSieci; i++)
            {
                uczoneSieci[i] = new Nauka(liczbaEpok, 0.6, new Siec(1), dane);
                uczoneSieci[i].siec.wejscia_sieci = new Warstwa(9, f);
                uczoneSieci[i].siec.DodajWarstwe(new Warstwa(1, f));
                uczoneSieci[i].siec.PolaczWarstwy();
                uczoneSieci[i].siec.LosujWagi(-5,5,r);
                //uczoneSieci[i].ZapiszWagi();
                uczoneSieci[i].Uczenie();
            }

            Console.WriteLine("Zakonczono uczenie!");
    
                //zapis wyników do plików
                FileStream fileStream = new FileStream("C:\\Users\\Dell Latitude 3330\\wszystkie_sieci.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter streamWriter = new StreamWriter(fileStream);

                double[] bledySieci = new double[liczbaSieci];
                for (int i = 0; i < liczbaSieci;i++)
                {
                    bledySieci[i] = znajdzMinimum(uczoneSieci[i].bledyUczenia, liczbaEpok);
                    streamWriter.WriteLine(bledySieci[i]);
                }
                streamWriter.WriteLine();
                streamWriter.Close();

                fileStream = new FileStream("C:\\Users\\Dell Latitude 3330\\naj_siec_uczenie.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
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
                    streamWriter.WriteLine(uczoneSieci[naj_siec].bledyUczenia[i]);
                }
                streamWriter.Close();

                fileStream = new FileStream("C:\\Users\\Dell Latitude 3330\\naj_siec_walidacja.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                streamWriter = new StreamWriter(fileStream);

                for (int i = 0; i < liczbaEpok; i++)
                {
                    streamWriter.WriteLine(uczoneSieci[naj_siec].bledyWalidacji[i]);
                }
                streamWriter.Close();

                /*fileStream = new FileStream("C:\\Users\\Dell Latitude 3330\\naj_neuron_wagi.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                streamWriter = new StreamWriter(fileStream);

                for (int i = 0; i < liczba_wejsc; i++)
                {
                    streamWriter.WriteLine(uczoneNeurony[naj_neuron].listaWag[i]);
                }
                streamWriter.Close();*/
                Console.WriteLine("Zapisano do pliku!");
                Console.Read();
        }
    }
}
