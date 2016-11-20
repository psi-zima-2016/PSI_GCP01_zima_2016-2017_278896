using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Warstwa
    {
        public ArrayList Neurony;
        public void DodajNeuron(Neuron n)
        {
            Neurony.Add(n);
        }
        public Warstwa(int iloscNeuronow, IFunkcjaAktywacji f)
        {
            Neurony = new ArrayList();
            for (int i=0; i<iloscNeuronow; i++)
            {
                DodajNeuron(new Neuron(f));
            }
        }
        public void PolaczWarstwy(Warstwa w)
        {
            foreach(Neuron n in Neurony)
            {
                n.DodajWejscia(w);
            }
        }
        public void LosujWagi(double min, double max, Random r)
        {
            foreach (Neuron n in Neurony)
            {
                n.LosujWagi(min, max, r);
            }
        }
        public void UstawWyjscia(double[] wektorWejsciowy)
        {
            for (int i = 0; i < Neurony.Count; i++)
            {
                ((Neuron)Neurony[i]).wyjscie = wektorWejsciowy[i];
            }
        }
        /*public double[] ZwrocWyjscia()
        {
            double[] wektorWyjsciowy = new double[Neurony.Count];
            for (int i =0; i < Neurony.Count; i++)
            {
                wektorWyjsciowy[i] = ((Neuron)Neurony[i]).wyjscie;
            }
            return wektorWyjsciowy;
        }*/
        public void ObliczWyjscia()
        {
            foreach (Neuron n in Neurony)
            {
                n.ObliczWyjscie();
            }
        }
        public void ZerujBledy()
        {
            foreach (Neuron n in Neurony)
            {
                n.blad = 0.0;
            }
        }
        public void ObliczBledy(double[] poprWyjscia)
        {
            for (int i = 0; i < Neurony.Count; i++)
            {
                ((Neuron)Neurony[i]).ObliczBlad(poprWyjscia[i]);
            }
        }
        public void RzutujBledy()
        {
            foreach (Neuron n in Neurony)
            {
                n.RzutujBledy();
            }
        }
        public void PoprawWagi(double wspUczenia)
        {
            foreach(Neuron n in Neurony)
            {
                n.PoprawWagi(wspUczenia);
            }
        }
    }
}
