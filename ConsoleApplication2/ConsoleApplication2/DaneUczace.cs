using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class DaneUczace
    {
        public ArrayList zbior_danych;
        public ArrayList zbior_uczacy;
        public ArrayList zbior_walidujacy;
        public int[,] zbior_indeksow;
        public DaneUczace()
        {
            zbior_indeksow = new int[126, 4];
            zbior_danych = new ArrayList(252);
            zbior_uczacy = new ArrayList(177);
            zbior_walidujacy = new ArrayList(75);
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
        }
        public void GenerujDane()
        {
            LosujIndeksy();
            double[] temp1 = new double[12];
            double[] temp2 = new double[12];
            for (int i = 0; i < 252; i++)
            {
                zbior_danych.Add(new double[12]);
            }
            for (int i = 0; i < 126; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    temp1[j] = 0;
                    temp2[j] = 1;
                }
                for (int j = 0; j < 4; j++)
                {
                    temp1[zbior_indeksow[i, j]] = 1;
                    temp2[zbior_indeksow[i, j]] = 0;
                }
                if (SprawdzCzyWygralKrzyzyk(temp1) == 1)
                {
                    temp1[9] = 1;
                    /*temp1[9] = 1;
                    temp1[10] = -1;
                    temp1[11] = -1;*/
                }
                if (SprawdzCzyWygralKrzyzyk(temp1) == 0)
                {
                    temp1[9] = 0;
                    /*temp1[9] = -1;
                    temp1[10] = 1;
                    temp1[11] = -1;*/
                }
                if (SprawdzCzyWygralKrzyzyk(temp1) == -1)
                {
                    temp1[9] = 0;
                    /*temp1[9] = -1;
                    temp1[10] = -1;
                    temp1[11] = 1;*/
                }
                if (SprawdzCzyWygralKrzyzyk(temp2) == 1)
                {
                    temp2[9] = 1;
                    /*temp2[9] = 1;
                    temp2[10] = -1;
                    temp2[11] = -1;*/
                }
                if (SprawdzCzyWygralKrzyzyk(temp2) == 0)
                {
                    temp2[9] = 0;
                    /*temp2[9] = -1;
                    temp2[10] = 1;
                    temp2[11] = -1;*/
                }
                if (SprawdzCzyWygralKrzyzyk(temp2) == -1)
                {
                    temp2[9] = 0;
                    /*temp2[9] = -1;
                    temp2[10] = -1;
                    temp2[11] = 1;*/
                }
                for (int j = 0; j < 12; j++)
                {
                    ((double[])zbior_danych[i])[j] = temp1[j];
                    ((double[])zbior_danych[i + 126])[j] = temp2[j];
                }
            }
        }
        public int SprawdzCzyWygralKrzyzyk(double[] plansza)
        {
            int temp = -1;
            for (int i = 0; i < 3; i++)
            {
                if (plansza[i * 3 + 0] != -1 && plansza[i * 3 + 1] == plansza[i * 3 + 0] && plansza[i * 3 + 2] == plansza[i * 3 + 0])
                    temp = (int)plansza[i * 3 + 0];
                if (plansza[i + 0] != -1 && plansza[i + 3] == plansza[i + 0] && plansza[i + 6] == plansza[i + 0])
                    temp = (int)plansza[i + 0];
            }
            if (plansza[0] != -1 && plansza[4] == plansza[0] && plansza[8] == plansza[0])
                temp = (int)plansza[0];
            if (plansza[2] != -1 && plansza[4] == plansza[2] && plansza[6] == plansza[2])
                temp = (int)plansza[2];
            return temp;
        }
        public void GenerujZbiorUczacy()
        {
            ArrayList kopiaDanych = new ArrayList(252);
            for (int i = 0; i<252;i++)
            {
                kopiaDanych.Add(new double[12]);
                for(int j =0; j < 12; j++)
                {
                    ((double[])kopiaDanych[i])[j] = ((double[])zbior_danych[i])[j];
                }
            }
            Random r = new Random();
            int licznik = 0;
            //generacja zbioru uczącego
            for (int i = 0; i<177; i++)
            {
                int los = r.Next(0, kopiaDanych.Count);
                zbior_uczacy.Add(new double[12]);
                for (int j = 0; j < 12; j++)
                {
                    ((double[])zbior_uczacy[licznik])[j] = ((double[])kopiaDanych[los])[j];
                }
                kopiaDanych.RemoveAt(los);
                licznik++;
            }
            //generacja zbioru walidującego
            for (int i=0; i<75; i++)
            {
                zbior_walidujacy.Add(new double[12]);
                for (int j =0; j < 12; j++)
                {
                    ((double[])zbior_walidujacy[i])[j] = ((double[])kopiaDanych[i])[j];
                }
            }
        }
    }
}
