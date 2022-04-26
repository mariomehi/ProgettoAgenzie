using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoAgenzie
{
    public class Mezzo
    {
        public int _mezzoID;
        public int _numPosti;
        public Fermate[] _fermate = new Fermate[3];
        //Agenzia ag;
        public Controllore _controllore;
        public Biglietto[] Posti = new Biglietto[5];

        public Mezzo(int id, int nposti, Fermate[] fermate)
        {
            _mezzoID = id;
            _numPosti = nposti;
            for (int i = 0; i < fermate.Length; i++)
            {
                _fermate[i] = fermate[i];
            }
        }
    }
}
