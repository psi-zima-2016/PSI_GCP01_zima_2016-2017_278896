using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Siec
    {
        public Warstwa wejscia_sieci;
        public ArrayList warstwy;
        public Siec(int liczba_warstw)
        {
            warstwy = new ArrayList(liczba_warstw);
            wejscia_sieci = null;
        }
        public void NormalizujWagi()
        {
            foreach (Neuron n in ((Warstwa)warstwy[0]).Neurony)
            {
                n.NormalizujWagi();
            }
        }
        public void DodajWarstwe(Warstwa w)
        {
            warstwy.Add(w);
        }
        public void PolaczWarstwy()
        {
            for (int i = 1; i < warstwy.Count; i++)
            {
                ((Warstwa)warstwy[i]).PolaczWarstwy((Warstwa)warstwy[i - 1]);
            }
            ((Warstwa)warstwy[0]).PolaczWarstwy(wejscia_sieci);
        }
        public void LosujWagi(double min, double max, Random r)
        {
            foreach (Warstwa w in warstwy)
            {
                w.LosujWagi(min, max, r);
            }
        }
        public void ObliczWyjscia()
        {
            for (int i=0;i<warstwy.Count;i++)
            {
                ((Warstwa)warstwy[i]).ObliczWyjscia();
            }
        }
        public double ZwrocWyjscie()
        {
            double sumaWyjsc = 0;
            for (int i = 0; i<((Warstwa)warstwy[warstwy.Count-1]).Neurony.Count; i++)
            {
                sumaWyjsc += ((Neuron)((Warstwa)warstwy[warstwy.Count - 1]).Neurony[i]).wyjscie;
            }
            return sumaWyjsc;
        }
        public double ObliczBlad(double[] poprWyjscie)
        {
            double suma_bledow = 0;
            foreach(Neuron n in ((Warstwa)warstwy[warstwy.Count-1]).Neurony)
            {
                suma_bledow += n.blad;
            }
            return suma_bledow;
        }
        public void poprawWagi(double wspUczenia)
        {
            ((Warstwa)warstwy[warstwy.Count - 1]).PoprawWagi(wspUczenia);
        }
        public void PoprawWagiWTA(double wspUczenia)
        {
            foreach (Warstwa w in warstwy)
            {
                w.PoprawWagiWTA(wspUczenia);
            }
        }
        public void zerujBias()
        {
            foreach (Warstwa w in warstwy)
            {
                w.ZerujBias();
            }
        }
    }
}
