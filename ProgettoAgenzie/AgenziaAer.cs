using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace ProgettoAgenzie
{
    public class AgenziaAer : Agenzia, IAgenzia
    {

        private readonly ILogger<AgenziaAer> logger;
        private readonly IConfiguration config;

        public AgenziaAer(ILogger<AgenziaAer> logger, IConfiguration config) // DI
        {
            this.logger = logger;
            this.config = config;
        }
        public AgenziaAer(string ag)
        {
            _nomeAg = ag;
            _aviaggio = new Viaggio[5];
            _amezzo = new Mezzo[5];
            _acontrollore = new Controllore[5];
        }
        int countc = 0;
        public override void CreaControllore(string nome, string cognome, Agenzia age)
        {
            Controllore contr = new Controllore(nome, cognome, age);
            _acontrollore[countc] = contr;
            countc++;
        }
        int countm = 0;
        public override void CreaMezzo(int id, int nposti, Fermate[] fermate)
        {
            Mezzo mezz = new Mezzo(id, nposti, fermate);
            _amezzo[countm] = mezz;
            countm++;
        }
        int numbigli = 0;
        int nposto = 0;
        public override void aggPosto(Biglietto bi, int mezzo)
        {
            for (int i = 0; i < _amezzo.Length; i++)
            {
                if (_amezzo[i]._mezzoID == mezzo)
                    _amezzo[i].Posti[nposto] = bi;
            }
            //Posti[numbigli] = bi;
            numbigli++;
            nposto++;
        }

        public void Init()
        {
            CreaControllore("AA", "AAA", this);
            CreaControllore("BB", "BBB", this);
            CreaControllore("CC", "CCC", this);
            CreaControllore("DD", "DDD", this);
            CreaControllore("EE", "EEE", this);
            Fermate[] ferm1 = { Fermate.GE, Fermate.TO, Fermate.RO };
            Fermate[] ferm2 = { Fermate.MI, Fermate.TO, Fermate.NA };
            Fermate[] ferm3 = { Fermate.GE, Fermate.FI, Fermate.NA };
            Fermate[] ferm4 = { Fermate.VE, Fermate.FI, Fermate.RO };
            Fermate[] ferm5 = { Fermate.MI, Fermate.FI, Fermate.VE };
            CreaMezzo(11, 110, ferm1);
            CreaMezzo(12, 120, ferm2);
            CreaMezzo(13, 130, ferm3);
            CreaMezzo(14, 140, ferm4);
            CreaMezzo(15, 150, ferm5);

            for (int i = 0; i < _amezzo.Length; i++)
             {
                _amezzo[i]._controllore = _acontrollore[i];
             }

            //stampa Mezzi e Controllore
            foreach (Mezzo me in _amezzo){
                Console.Write("ID: "+me._mezzoID+" N°Posti: "+me._numPosti+" Controllore: "+me._controllore._nome+"\n");
                Console.Write("Fermate del mezzo: ");
                for (int i = 0; i < me._fermate.Length; i++)
                    Console.Write("" + me._fermate[i] + " ");
                Console.WriteLine("");
            }
        }
        public override void Controllo()
        {
            Console.WriteLine("\nNumero mezzo: " + _amezzo[1]._mezzoID);
            Console.WriteLine("Fermate: "+_amezzo[1]._fermate[0].ToString()+" "+ _amezzo[1]._fermate[1].ToString()+" "+_amezzo[1]._fermate[2].ToString());
            Console.WriteLine("Nome controllore: " + _amezzo[1]._controllore._nome);
            Console.WriteLine("Numero Posti Tot: " + _amezzo[1]._numPosti);

            Console.WriteLine("Numero viaggio: " + _aviaggio[0].numViaggio);
            Console.WriteLine("Numero biglietto: " + _aviaggio[0].bi.numID);
            Console.WriteLine("Nome viaggiatore: " + _amezzo[1].Posti[0].viagg._nome);
        }
    }
}
