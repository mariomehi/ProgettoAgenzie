using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoAgenzie
{
    public class AgenziaMar : Agenzia
    {
        public override void aggPosto(Biglietto bi, int mezzo)
        {
            throw new NotImplementedException();
        }

        public override void Controllo()
        {
            throw new NotImplementedException();
        }

        public override void CreaControllore(string nome, string cognome, Agenzia age)
        {
            throw new NotImplementedException();
        }
        public override void CreaMezzo(int id, int nposti, Fermate[] fermate)
        {
            throw new NotImplementedException();
        }
    }
}
