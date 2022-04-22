using System;

namespace ProgettoAgenzie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AgenziaAer ag1 = new AgenziaAer("AgenziaPinco");
            ag1.Init();

            Biglietteria bi = new Biglietteria();
            static void menu() {
                Console.WriteLine("\n*** 1. Acquista Biglietto ***");
                Console.WriteLine("*** 2. Fai il Check In ***");
                Console.WriteLine("*** 3. Vai sul mezzo. Controllo biglietto ***");
                Console.WriteLine("*** 0. Esci ***");
            }
            menu();

            int scelta = 0;
            do
            {
                try {
                    scelta = Convert.ToInt32(Console.ReadLine());
                } catch(Exception ex){
                    Console.WriteLine(ex);
                }

                if (scelta == 1)
                {
                    Console.WriteLine("*** Benvenuto nella Biglietteria PincoPallo ***");
                    Console.WriteLine("++ Fermate principali: ++");
                    foreach (Fermate ferm in Enum.GetValues(typeof(Fermate))) {
                        Console.Write($" {ferm} ");
                    }
                    Console.WriteLine("\n++ Scrivi la città di partenza: ++");
                    string vpartenza = "";
                    try
                    {
                        vpartenza = Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    Console.WriteLine("++ Scrivi la città di destinazione: ++");
                    string vdest = "";
                    try
                    {
                        vdest = Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine($"Hai scelto da " + vpartenza + " a " + vdest);

                    Console.WriteLine("Inserisci il tuo nome e cognome: ");
                    string nome = Console.ReadLine();
                    string cognome = Console.ReadLine();
                    Console.WriteLine("Benvenuto: " + nome + " " + cognome);
                    Viaggiatore viagg = new Viaggiatore(nome, cognome);

                    Console.WriteLine("** Viaggi Disponibili: **");
                    
                    int tratta = 0;
                    Fermate[] fer = new Fermate[2];
                    for(int i=0; i<ag1._amezzo.Length;i++)
                    {
                        Console.Write(i+" ");
                        for(int j=0; j< ag1._amezzo[i]._fermate.Length;j++)
                        {
                            Console.Write(ag1._amezzo[i]._fermate[j].ToString() + " ");
                            if (ag1._amezzo[i]._fermate[0].ToString() == vpartenza && ag1._amezzo[i]._fermate[2].ToString() == vdest)
                                tratta=ag1._amezzo[i]._mezzoID;
                        }
                        Console.Write("\n");
                    }
                    Console.WriteLine($"Abbiamo trovato 1 viaggio con il mezzo n° {tratta}");
                    Console.WriteLine($"Confermi acquisto di questo viaggio?");
                    string confermav = Console.ReadLine();
                    DateTime today = DateTime.Now;
                    if (confermav == "si")
                    {
                        Biglietto bigl = new Biglietto(viagg, ag1, tratta, today.AddDays(30));
                        Console.WriteLine($"Treno n°: {bigl.me._mezzoID}");
                        Console.WriteLine($"Treno tratte: {bigl.me._fermate[0]} {bigl.me._fermate[1]} {bigl.me._fermate[2]}");
                        bi.creaViaggio(ag1,tratta, bigl);

                        bi.GetViaggio(bigl);
                    }
                    ag1.Controllo();
                    menu();

                }
                else if (scelta == 2)
                {
                    Console.WriteLine("** Benvenuto in Agenzia! Inserisci il numero del biglietto: **");
                    var numbigl = Convert.ToInt32(Console.ReadLine());

                    foreach (Viaggio vi in ag1._aviaggio) {
                        if (vi.numB == numbigl) {
                            Console.WriteLine("Biglietto trovato. Check In Completato!");
                            Console.WriteLine($"Viaggio: {vi.date} Mezzo: {vi.mez._mezzoID} viaggio n°: {vi.numV}");
                            Console.WriteLine($"Biglietto: {vi.bi.numID} Nome: {vi.bi.viagg._nome} Cognome: {vi.bi.viagg._cognome}");
                            break;
                        } else {
                            Console.WriteLine("Biglietto non trovato!");
                            break;
                        }
                    }
                    menu();
                }
                else if (scelta == 3)
                {
                    Console.WriteLine($"** In quale mezzo vuoi salire? inserisci il n° mezzo: **");
                    int nummezz = Convert.ToInt32(Console.ReadLine());
                    Controllore controllore=null;
                    Biglietto bigli = null;
                    foreach (Mezzo i in ag1._amezzo) {
                        if(i._mezzoID == nummezz) {
                            controllore = i._controllore;
                            bigli = i.Posti[0];
                        }
                    }
                    Console.WriteLine($"** Sono {controllore._nome}, mostrami il tuo biglietto: **");
                    int numbigl = Convert.ToInt32(Console.ReadLine());
                    controllore.CheckTicket(nummezz, bigli);

                    menu();
                }
            } while (scelta != 0);
        }

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
        public interface IAgenzia
        {
            public void CheckIn();
        }
        public class AgenziaAer : Agenzia
        {
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

                for (int i=0;i<_amezzo.Length;i++){
                    _amezzo[i]._controllore = _acontrollore[i];
                }
                /*  stampa Fermate
                foreach (Mezzo me in _amezzo){
                    Console.WriteLine(me._mezzoID + " "+ me._numPosti + " " + me._controllore._nome);
                    for (int i = 0; i < me._fermate.Length; i++)
                        Console.Write("" + me._fermate[i] + " ");
                    Console.WriteLine("\n");
                }*/
            }
            public override void Controllo()
            {
                Console.WriteLine("\nNumero treno: "+_amezzo[1]._mezzoID);
                Console.WriteLine("Fermate: "+_amezzo[1]._fermate[0].ToString()+_amezzo[1]._fermate[1].ToString()+_amezzo[1]._fermate[2].ToString());
                Console.WriteLine("Nome controllore: " + _amezzo[1]._controllore._nome);
                Console.WriteLine("Numero Posti Tot: "+_amezzo[1]._numPosti);

                Console.WriteLine("Numero viaggio: " + _aviaggio[0].numV);
                Console.WriteLine("Numero biglietto: " + _aviaggio[0].bi.numID);
                Console.WriteLine("Numero treno: " + _aviaggio[0].mez._mezzoID);
                Console.WriteLine("Nome viaggiatore: " + _amezzo[1].Posti[0].viagg._nome);
            }
        }
        public class AgenziaTerr : Agenzia
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
        public enum Fermate { MI, NA, TO, RO, FI, VE, GE }
        public class Mezzo
        {
            public int _mezzoID;
            public int _numPosti;
            public Fermate[] _fermate = new Fermate[3];
            Agenzia ag;
            public Controllore _controllore;
            public Biglietto[] Posti = new Biglietto[5];

            public Mezzo(int id, int nposti, Fermate[] fermate)
            {
                _mezzoID = id;
                _numPosti = nposti;
                for(int i = 0; i < fermate.Length; i++)
                {
                    _fermate[i] = fermate[i];
                }
            }
        }
        public class Viaggio
        {
            public DateTime date;
            public int numV = 0;
            public int numB = 0;
            public Biglietto bi;
            public Mezzo mez;

            public Viaggio(Agenzia ag, int mezzo, Biglietto big)
            {
                date = DateTime.Now;
                numB = big.numID;
                bi = big;
                for(int i = 0; i < ag._amezzo.Length; i++)
                {
                    if (ag._amezzo[i]._mezzoID == mezzo)
                    {
                        mez = ag._amezzo[i];
                        bi = big;
                    }
                }
                ag.aggPosto(big, mezzo);
                ag._amezzo[numV] = mez;
                numV++;
            }
            public void getTratta(Mezzo[] me) {

            }
        }
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
                    if (bi.numID == vi.numB)
                    {
                        Console.WriteLine("*** Riepilogo del tuo viaggio: ***");
                        Console.WriteLine("Numero viaggio: " + vi.numV);
                        Console.WriteLine("Numero biglietto: " + vi.numB);
                        Console.WriteLine("Acquistato il:"+ vi.date);
                        Console.WriteLine("Scadenza biglietto: "+ bi._data2);
                        Console.WriteLine("Numero del mezzo: " + vi.mez._mezzoID);
                        Console.WriteLine($"Fermate della tratta: {vi.mez._fermate[0]} {vi.mez._fermate[1]} {vi.mez._fermate[2]}");
                    }
                    break;
                }
            }
        }

        public class Biglietto
        {
            public DateTime _data;
            public string _agenzia;
            public int numID = 0;
            public DateTime _data2;
            public Viaggiatore viagg;
            public Mezzo me;

            public Biglietto(Viaggiatore viag, Agenzia age, int mezzoid, DateTime date)
            {
                _data = DateTime.Now;
                _data2 = date;
                viagg = viag;
                _agenzia = age._nomeAg;
                for(int i = 0; i < age._amezzo.Length; i++) {
                    if (age._amezzo[i]._mezzoID == mezzoid)
                        me=age._amezzo[i];
                }
                numID = Utility.RandomN();
            }
            public Biglietto getBiglietto(Biglietto bi)
            {
                return bi;
            }
        }
        public class Persona
        {
            public string _nome;
            public string _cognome;
        }
        public class Controllore : Persona
        {
            public int _contrID = 0;
            public Mezzo _me;
            public Agenzia _ag;
            public Controllore(string nome, string cognome, Agenzia ag)
            {
                _nome = nome;
                _cognome = cognome;
                _ag = ag;
                _contrID = Utility.RandomN();
            }
            public void CheckTicket(int mezzo, Biglietto bi)
            {
                // controlla treno e biglietto valido
                if (_ag._amezzo[1]._mezzoID == mezzo && _ag._amezzo[1].Posti[0].numID==bi.numID)
                {
                    if(bi._data2>DateTime.Now && bi._data<bi._data2)
                        Console.WriteLine("Controllo biglietto eseguito con successo.");
                }
            }
        }
        public class Viaggiatore : Persona
        {
            public int id;
            public Biglietto bi;
            public Viaggiatore(string nome, string cognome)
            {
                _nome = nome;
                _cognome = cognome;
            }
        }

        public static class Utility
        {
            public static int RandomN()
            {
                Random rnd = new Random();
                int nrand = rnd.Next(10, 99);
                return nrand;
            }
        }
    }
}
