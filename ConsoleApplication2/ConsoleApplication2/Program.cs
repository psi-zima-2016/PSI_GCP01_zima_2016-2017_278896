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
            FunkcjaLiniowa f = new FunkcjaLiniowa();
            int liczbaEpok = 1;
            //int liczbaSieci = 100;

            DaneUczace dane = new DaneUczace();
            dane.GenerujDane();
            dane.GenerujZbiorUczacy();

            ArrayList sieci = new ArrayList();
            Siec siecWTA = new Siec(1);
            siecWTA.wejscia_sieci = new Warstwa(9, f);
            siecWTA.DodajWarstwe(new Warstwa(1, f));
            siecWTA.PolaczWarstwy();
            siecWTA.LosujWagi(-1, 1, r);
            sieci.Add(siecWTA);

            WTA wta = new WTA(liczbaEpok, 0.1, sieci, dane);
            wta.Ucz();

            Console.WriteLine("Zakonczono uczenie!");
    
                //zapis wyników do plików
                /*FileStream fileStream = new FileStream("C:\\Users\\Dell Latitude 3330\\wszystkie_sieci.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
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
                streamWriter.Close();*/

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
