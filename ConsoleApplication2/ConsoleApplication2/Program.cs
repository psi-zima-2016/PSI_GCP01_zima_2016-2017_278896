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
        static void Main(string[] args)
        {
            //Random r = new Random();
            
            //FileStream fileStream = new FileStream("C:\\Users\\Dell Latitude 3330\\bledyneuronu.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //StreamWriter streamWriter = new StreamWriter(fileStream);

            DaneUczace daneUczace = new DaneUczace();
            daneUczace.LosujIndeksy();
            daneUczace.GenerujDane();
            daneUczace.ObliczWyniki();
            daneUczace.GenerujZbiorUczacy();

            int liczbaEpok = 10;
            double wspUczenia = 1;
            Uczenie uczenie = new Uczenie(liczbaEpok,wspUczenia);
            uczenie.Nauczanie(daneUczace);
            Console.WriteLine(uczenie.skutecznoscUczenia);


                //streamWriter.WriteLine();
                //Console.WriteLine();
                //streamWriter.Close();
                Console.Read();
        }
    }
}
