using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoAgenzie
{
    public class Biglietteria
    {
        const int N = 5;
        public Viaggio[] _vi = new Viaggio[N];
        public Agenzia[] _ag = new Agenzia[N];
        int _count = 0;
        public void creaViaggio(Agenzia ag, int mezzo, Biglietto bi)
        {
            Viaggio viaggio = new Viaggio(ag, mezzo, bi);
            _vi[_count] = viaggio;
            _ag[_count] = ag;
            ag._aviaggio[_count] = viaggio;
            _count++;
        }
        public void GetViaggio(Biglietto bi)
        {
            foreach (Viaggio vi in _vi)
            {
                if (bi.numID == vi.bi.numID)
                {
                    Console.WriteLine("*** Riepilogo del tuo viaggio: ***");
                    Console.WriteLine("Numero viaggio: " + vi.numViaggio);
                    Console.WriteLine("Numero biglietto: " + vi.bi.numID);
                    Console.WriteLine("Data acquisto:" + vi.date);
                    Console.WriteLine("Scadenza biglietto: " + bi._data2);
                    Console.WriteLine("Numero del mezzo: " + vi.mez._mezzoID);
                    Console.WriteLine($"Fermate del mezzo: {vi.mez._fermate[0]} {vi.mez._fermate[1]} {vi.mez._fermate[2]}");
                }
                break;
            }
        }
    }
}
