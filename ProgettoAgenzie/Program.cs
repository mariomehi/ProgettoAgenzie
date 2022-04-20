using System;

namespace ProgettoAgenzie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Inizializzo
            Agenzia ag1 = new AgenziaAer("AgenziaPinco");
            Controllore p1 = new Controllore("Mario", "Mehi");
            Controllore p2 = new Controllore("Mari", "Meh");
            Controllore p3 = new Controllore("Mar", "Me");

            ag1.AggiungiC(p1, null, ag1);
            ag1.AggiungiC(p1, null, ag1);
            ag1.AggiungiC(p1, null, ag1);

            ag1.AggMezz(0, "Genova", "Firenze", p1, 150);
            ag1.AggMezz(1, "Roma", "Torino", p2, 175);
            ag1.AggMezz(2, "Torino", "Milano", p2, 175);
            ag1.AggMezz(3, "Milano", "Venezia", p3, 200);

            Biglietteria bi = new Biglietteria();
            Console.WriteLine("*** 1. Acquista Biglietto ***");
            Console.WriteLine("*** 2. Fai il Check In ***");
            Console.WriteLine("*** 3. Vai sul mezzo. Controllo biglietto ***");
            Console.WriteLine("*** 0. Esci ***");

            int scelta = 0;
            do {
                scelta = Convert.ToInt32(Console.ReadLine());
                if (scelta == 1) {
                    Console.WriteLine("*** Benvenuto nella Biglietteria PincoPallo ***");
                    Console.WriteLine("++ Roma, Milano, Torino, Genova, Firenze, Venezia ++");
                    Console.WriteLine("++ Scrivi la città di partenza: ++");
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
                    Viaggiatore pass1 = new Viaggiatore(nome, cognome);

                    Console.WriteLine("** Ecco le tue tratte durante il vaggio: **");
                    string tratta1="";
                    //int[] mezzi = new int[2]; //array mezzi per il viaggio
                    int[] mezzi = new int[5];
                    foreach (MezzoTrasporto mez in ag1.mezzoTrasporto) {
                        if (vpartenza == mez._trattap) {
                            Console.WriteLine(" >> " + mez._numID + " " + mez._trattap + " - " + mez._trattaa);
                            tratta1 = mez._trattaa;
                            mezzi[0] = mez._numID;
                        }
                    }
                    foreach (MezzoTrasporto mez in ag1.mezzoTrasporto)
                    {
                        if ((vdest == mez._trattaa) && (mez._trattap == tratta1)) {
                            Console.WriteLine(" >> " + mez._numID + " " + mez._trattap + " - " + mez._trattaa);
                            mezzi[1] = mez._numID;
                        }
                    }
                    Console.WriteLine("Confermi il viaggio?");
                    string conferma = Console.ReadLine();
                    Biglietto bigliacq;
                    if (conferma == "si") {
                        bigliacq = new Biglietto(pass1, ag1);
                        bi.creaViaggio(ag1, mezzi, bigliacq);
                    }
                    else{
                        break;
                    }
                    Console.WriteLine("*** Grazie. Il tuo viaggio è stato confermato");
                    bi.getViaggio(bigliacq);
Console.WriteLine($"Il tuo biglietto (id: {bigliacq.numID}) con {bigliacq._agenzia} è stato acquistato in data {bigliacq._data}");
                    //Console.WriteLine(bi._vi[0].numV);

                }
                else if (scelta == 2) {
                    Console.WriteLine("** Inserisci il numero del biglietto: **");
                    int numbigl = Convert.ToInt32(Console.ReadLine());
                    foreach (Biglietto b in ag1.biglietto) {
                        if (b.numID == numbigl) {
                            Console.WriteLine("Biglietto trovato. Check In Completato!");
                            break;
                        } else {
                            Console.WriteLine("Biglietto non trovato!");
                            break;
                        }
                    }
                    Console.WriteLine("\n*** 0 per uscire ***");
                } else if (scelta == 3) {
                    Console.WriteLine(" ++ Mosta biglietto al controllore: ");
                }
            } while (scelta != 0);
        }
    }
    public abstract class Agenzia {
        public string _nomeAg;
        public Biglietto[] biglietto;
        public Viaggio[] viaggio;
        public MezzoTrasporto[] mezzoTrasporto;
        public Controllore[] controllore;
        //public abstract Biglietto AcquistaB(Viaggiatore pass, Agenzia age, int mezzo);
        public abstract void Checkin(Biglietto nb);
        public abstract Controllore AggiungiC(Controllore con, MezzoTrasporto me, Agenzia ag);
        public abstract MezzoTrasporto AggMezz(int num, string trp, string tra, Controllore c, int pass);
        public abstract void ControlloBiglietto(Biglietto bi, Viaggiatore vi);
    }
    public class AgenziaAer : Agenzia {
        public AgenziaAer(string ag) {
            biglietto = new Biglietto[3];
            viaggio = new Viaggio[3];
            mezzoTrasporto = new MezzoTrasporto[4];
            controllore = new Controllore[3];
            _nomeAg = ag; }

        /*public int _numb = 4;
        public override Biglietto AcquistaB(Viaggiatore pass, Agenzia age, int mezzo) {
            Biglietto bi = new Biglietto(pass, age, mezzoTrasporto[mezzo]);
            try{
                biglietto[_numb] = bi;
            }
            catch(IndexOutOfRangeException ex) {
                Console.WriteLine(ex.Message);
            }
            _numb++;
            return bi;
        }*/
        public override void ControlloBiglietto(Biglietto bi, Viaggiatore vi) {
            throw new NotImplementedException();
        }
        public int _count = 0;
        public override Controllore AggiungiC(Controllore con, MezzoTrasporto me, Agenzia ag)
        {
            Controllore contr = new Controllore(con._nome, con._cognome, me, ag);
            try{
                controllore[_count] = contr;
            }
            catch (IndexOutOfRangeException ex) {
                Console.WriteLine(ex.Message);
            }
            _count++;
            return contr;
        }
        int _nmezz = 0;
        public override MezzoTrasporto AggMezz(int num, string tra, string trp, Controllore con, int pass) {
            MezzoTrasporto mezz = new MezzoTrasporto(num, tra, trp, con, pass);
            try {
            mezzoTrasporto[_nmezz] = mezz;
            }
            catch (IndexOutOfRangeException ex) {
                Console.WriteLine(ex.Message);
            }
            _nmezz++;
            return mezz;
        }
        public override void Checkin(Biglietto nb)
        {
            if (biglietto[0] == nb)
                Console.WriteLine("puoi modificare");
            else
                Console.WriteLine("biglietto non trovato");
        }
    }
    public class Viaggio {
        //public DateTime date;
        public int numV = 0;
        public int numB = 0;
        //public Biglietto bi;
        MezzoTrasporto mez;

        public Viaggio(Agenzia ag, int[] mezzi, Biglietto big) {
            numB = big.numID;
            mez.aggPosto(big, mezzi);
            numV++;
        }

        public void getTratta(MezzoTrasporto[] me) {
            for (int i = 0; i < me.Length; i++)
            {
                for(int j=0; j<me[i].Posti.Length; j++)
                {
                    if(me[i].Posti[j].numID==numB)
                    {
                        Console.WriteLine("biglietto esiste nel mezzo");
                        Console.WriteLine(me[i]._numID);
                        Console.WriteLine(me[i]._trattaa);
                        Console.WriteLine(me[i]._trattap);
                    }

                }

            }
        }
    }
    public class Biglietteria {
        const int N = 5;
        public Viaggio[] _vi = new Viaggio[N];
        public Agenzia[] _ag = new Agenzia[N];
        int _count = 0;
        public void creaViaggio(Agenzia ag, int[] mez, Biglietto bi) {
            Viaggio viaggio = new Viaggio(ag, mez, bi);
            _vi[_count] = viaggio;
            _ag[_count] = ag;
            ag.viaggio[_count] = viaggio;
            _count++;
        }
        public void getViaggio(Biglietto bi) {
            foreach (Viaggio vi in _vi) {
                if (bi.numID == vi.numB) {
                    Console.WriteLine("Riepilogo viaggio:");
                    Console.WriteLine("Numero viaggio: " + vi.numV);
                    Console.WriteLine("Numero biglietto: " + vi.numB);
                    //vi.getTratta();
                }
                break;
            }
        }
    }
    //public enum Fermate { MI, NA, TO, RO, FI, VE }
    public class MezzoTrasporto {
        public int _numID;
        public static int _numpassT;
        public string _trattap;
        public string _trattaa;
        //Fermate[] _fermate = new Fermate[10];

        public Controllore _controllore;
        public Biglietto[] Posti=new Biglietto[_numpassT];

        public MezzoTrasporto(int id, string trattap, string trattaa, Controllore con, int numpass) {
            _numID = id;
            _trattap = trattap;
            _trattaa = trattaa;
            _controllore = con;
            _numpassT = numpass;
        }
        int count=0;
        public void aggPosto(Biglietto bi, int[] mezzi)
        {
            for (int i = 0; i < mezzi.Length; i++)
            {
               if(_numID==mezzi[i])
                {
                    Posti[i] = bi;
                }
            }
        }
    } 
    public class Biglietto{
        public DateTime _data;
        public int _mezzoT;
        public string _agenzia;
        public string _persona;
        public string pagamento;
        public int numID=0;

        public Biglietto(Persona pers, Agenzia age){
            _data = DateTime.Now;
            _persona = pers._cognome;
            _agenzia = age._nomeAg;
            numID = RandomN();
            //age.mezzoTrasporto[1] = 
        }
        public int RandomN()
        {
            Random rnd = new Random();
            int nrand = rnd.Next(10, 99);
            return nrand;
        }
    }
    public class Persona{
        public string _nome;
        public string _cognome;
    }
    public class Controllore : Persona
    {
        public int _numID = 0;
        public MezzoTrasporto _me;
        public Agenzia _ag;
        public Controllore(string nome, string cognome){
            _nome = nome;
            _cognome = cognome;
        }
        public Controllore(string nome, string cognome, MezzoTrasporto me, Agenzia ag){
            _nome = nome;
            _cognome = cognome;
            _ag = ag;
            _me = me;
            _numID++;
        }
        public void ControllaB() { }
    }
    public class Viaggiatore : Persona {
        public int id;
        public Biglietto bi;
        public Viaggiatore(String nome, string cognome) {
            _nome=nome;
            _cognome = cognome;
        }
    }
}