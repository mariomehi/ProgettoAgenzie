using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoAgenzie
{
    public abstract class Agenzia
    {
        public string _nomeAg;

        public Viaggio[] _aviaggio;
        public Mezzo[] _amezzo;
        public Controllore[] _acontrollore;
        public abstract void CreaControllore(string nome, string cognome, Agenzia age);
        public abstract void CreaMezzo(int id, int nposti, Fermate[] fermate);
        public abstract void Controllo();
        public abstract void aggPosto(Biglietto bi, int mezzo);
    }
}
