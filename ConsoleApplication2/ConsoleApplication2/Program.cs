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
            FunkcjaProgowa f = new FunkcjaProgowa();
            int liczba_wejsc = 9;
            int liczbaNeuronow = 100;
            int liczbaEpok = 100;
            double min = -5, max = 5;

            DaneUczace dane = new DaneUczace();
            dane.GenerujDane();
            dane.GenerujZbiorUczacy();

            Nauka[] uczoneNeurony = new Nauka[liczbaNeuronow];
            for (int i = 0; i < liczbaNeuronow; i++)
            {
                uczoneNeurony[i] = new Nauka(liczbaEpok, 1, new Neuron(f, liczba_wejsc), dane);
                uczoneNeurony[i].neuron.LosujWagi(min,max,r);
                uczoneNeurony[i].ZapiszWagi();
                uczoneNeurony[i].Uczenie();
            }

            FileStream fileStream = new FileStream("C:\\Users\\Dell Latitude 3330\\wszystkie_neurony.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            double[] bledyNeuronow = new double[liczbaNeuronow];
            for (int i = 0; i < liczbaNeuronow;i++)
            {
                bledyNeuronow[i] = znajdzMinimum(uczoneNeurony[i].bledyUczenia, liczbaEpok);
                streamWriter.WriteLine(bledyNeuronow[i]);
            }
            streamWriter.WriteLine();
            streamWriter.Close();

            fileStream = new FileStream("C:\\Users\\Dell Latitude 3330\\naj_neuron_uczenie.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            streamWriter = new StreamWriter(fileStream);

            int naj_neuron = 0;
            double najm_blad = znajdzMinimum(bledyNeuronow, liczbaNeuronow);
            for (int i = 0; i < liczbaNeuronow; i++)
            {
                if (bledyNeuronow[i] == najm_blad)
                    naj_neuron = i;
            }
            for (int i = 0; i < liczbaEpok; i++)
            {
                streamWriter.WriteLine(uczoneNeurony[naj_neuron].bledyUczenia[i]);
            }
            streamWriter.Close();

            fileStream = new FileStream("C:\\Users\\Dell Latitude 3330\\naj_neuron_walidacja.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            streamWriter = new StreamWriter(fileStream);

            for (int i = 0; i < liczbaEpok; i++)
            {
                streamWriter.WriteLine(uczoneNeurony[naj_neuron].bledyWalidacji[i]);
            }
            streamWriter.Close();

            fileStream = new FileStream("C:\\Users\\Dell Latitude 3330\\naj_neuron_wagi.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            streamWriter = new StreamWriter(fileStream);

            for (int i = 0; i < liczba_wejsc; i++)
            {
                streamWriter.WriteLine(uczoneNeurony[naj_neuron].listaWag[i]);
            }
            streamWriter.Close();
            Console.Read();
        }
    }
}
