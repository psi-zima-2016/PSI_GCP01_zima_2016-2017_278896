using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class DaneUczace
    {
        public double[,] zbior_danych;
        public double[,] zbior_uczacy;
        public double[,] zbior_walidujacy;
        public double[,] zbior_indeksow;
        public DaneUczace()
        {
            zbior_indeksow = new double[126, 4];
            zbior_danych = new double[252, 10];
            zbior_uczacy = new double[178, 10];
            zbior_walidujacy = new double[75, 10];
        }
        public void LosujIndeksy()
        {
            int iterator = 0;
            int i, j, k, l;
            for (i = 0; i < 6; i++)
            {
                for (j = i + 1; j < 7; j++)
                {
                    for (k = j + 1; k < 8; k++)
                    {
                        for (l = k + 1; l < 9; l++)
                        {
                            zbior_indeksow[iterator, 0] = i;
                            zbior_indeksow[iterator, 1] = j;
                            zbior_indeksow[iterator, 2] = k;
                            zbior_indeksow[iterator, 3] = l;
                            iterator++;
                        }
                    }
                }
            }
            Console.WriteLine(iterator);
        }
        public void GenerujDane()
        {
            for (int i =0; i < 126; i++)
            {
                int j = 0;
                while(j<zbior_indeksow[i,0])
                {
                    zbior_danych[i, j] = 0;
                    zbior_danych[i + 126, j] = 1;
                    j++;
                }
                zbior_danych[i, j] = 1;
                zbior_danych[i + 126, j] = 0;
                j++;
                while (j < zbior_indeksow[i, 1])
                {
                    zbior_danych[i, j] = 0;
                    zbior_danych[i + 126, j] = 1;
                    j++;
                }
                zbior_danych[i, j] = 1;
                zbior_danych[i + 126, j] = 0;
                j++;
                while (j < zbior_indeksow[i, 2])
                {
                    zbior_danych[i, j] = 0;
                    zbior_danych[i + 126, j] = 1;
                    j++;
                }
                zbior_danych[i, j] = 1;
                zbior_danych[i + 126, j] = 0;
                j++;
                while (j < zbior_indeksow[i, 3])
                {
                    zbior_danych[i, j] = 0;
                    zbior_danych[i + 126, j] = 1;
                    j++;
                }
                zbior_danych[i, j] = 1;
                zbior_danych[i + 126, j] = 0;
                j++;
                while (j<9)
                {
                    zbior_danych[i, j] = 0;
                    zbior_danych[i + 126, j] = 1;
                    j++;
                }
            }
        }
        public int SprawdzCzyWygralKrzyzyk(double[] plansza)
        {
            int wynik = 0;
            if (plansza[0] == 1 && plansza[3] == 1 && plansza[6] == 1)
                wynik = 1;
            if (plansza[1] == 1 && plansza[4] == 1 && plansza[7] == 1)
                wynik = 1;
            if (plansza[2] == 1 && plansza[5] == 1 && plansza[8] == 1)
                wynik = 1;
            if (plansza[0] == 1 && plansza[1] == 1 && plansza[2] == 1)
                wynik = 1;
            if (plansza[3] == 1 && plansza[4] == 1 && plansza[5] == 1)
                wynik = 1;
            if (plansza[6] == 1 && plansza[7] == 1 && plansza[8] == 1)
                wynik = 1;
            if (plansza[0] == 1 && plansza[4] == 1 && plansza[8] == 1)
                wynik = 1;
            if (plansza[2] == 1 && plansza[4] == 1 && plansza[6] == 1)
                wynik = 1;
            return wynik;
        }
        public void ObliczWyniki()
        {
            double[] wiersz = new double[9];
            for (int i = 0; i<252;i++)
            {
                for (int j=0; j< 9; j++)
                {
                    wiersz[j] = zbior_danych[i, j];
                }
                zbior_danych[i,9] = SprawdzCzyWygralKrzyzyk(wiersz);
            }
        }
        public void GenerujZbiorUczacy()
        {
            double[] tab = new double[252];
            int licznik = 0;
            for (int i = 0; i < 252; i++)
            {
                tab[i] = 0;
            }
            Random rand = new Random();
            int los = rand.Next(0, 252);
            tab[los] = 1;
            licznik++;
            while (licznik < 75)
            {
                while (tab[los] != 0)
                {
                    los = rand.Next(0, 252);
                }
                tab[los] = 1;
                licznik++;
            }
            int licznik_u=0, licznik_w=0;
            for (int i = 0; i < 252; i++)
            {
                if (tab[i] == 0)
                {
                    for (int j=0; j<10; j++)
                    {
                        zbior_uczacy[licznik_u, j] = zbior_danych[i, j];
                    }
                    licznik_u++;
                }
                else
                {
                    for (int j = 0; j < 10; j++)
                    {
                        zbior_walidujacy[licznik_w, j] = zbior_danych[i, j];
                    }
                    licznik_w++;
                }
            }
        }
    }
}
