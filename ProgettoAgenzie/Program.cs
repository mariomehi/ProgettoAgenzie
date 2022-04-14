using System;

namespace ProgettoAgenzie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Inizializzo!
            Agenzia ag1 = new AgenziaAer("Alitalia");
            Agenzia ag2 = new AgenziaAer("ITA");
            Agenzia ag3 = new AgenziaAer("Rayanair");

            Console.WriteLine("*** Benvenuto! Seleziona un agenzia: ***");
            Console.WriteLine("--- 1. Alitalia ---");
            Console.WriteLine("--- 2. ITA ---");
            Console.WriteLine("--- 3. Rayanair ---");
            string ags=Console.ReadLine();
            Console.WriteLine($"Hai Scelto: Alitalia");
            Console.WriteLine("Inserisci nome e cognome: ");
            string nome = Console.ReadLine();
            string cognome = Console.ReadLine();
            Passeggero pass1 = new Passeggero(nome, cognome);
            Console.WriteLine("Benvenuto: "+ nome + " " + cognome);
            Console.WriteLine("*** 1. Acquista Biglietto ***");
            Console.WriteLine("*** 2. Modifica Biglietto ***");
            Console.WriteLine("*** 0. Esci ***");
            int scelta = 0;
            //scelta = Convert.ToInt32(Console.ReadLine());

            do
            {
                scelta = Convert.ToInt32(Console.ReadLine());
                if (scelta == 1) {
                    Console.WriteLine("Acquista Biglietto:");
                    ag1.AcquistaB(pass1, ag1);
                } else if (scelta == 2) {
                    Console.WriteLine("Modifica Biglietto:");
                } else Console.WriteLine("Scelta non valida!");

            } while (scelta != 0);  


        }

        public void init()
        {
            Agenzia ag1 = new AgenziaAer("Alitalia");
            Agenzia ag2 = new AgenziaAer("ITA");

            Controllore con1 = new Controllore("C02", "Mario", ag1);
            Controllore con2 = new Controllore("C05", "Eli", ag1);

            ag1.AggMezz(5, con1, 600);
            ag1.AggMezz(6, con2, 600);
        }
    }
    public abstract class Agenzia {
        public string _nomeAg;
        public string _tipoAgenzia;
        public Biglietto[] biglietto;
        public MezzoTrasporto[] mezzoTrasporto;
        public Controllore[] controllore;
        public abstract Biglietto AcquistaB(Passeggero pass, Agenzia age);
        public abstract void ModificaB(Biglietto nb);
        public abstract MezzoTrasporto AggMezz(int num, Controllore c, int pass);
    }
    public class AgenziaAer : Agenzia {
        public AgenziaAer(string ag) {
            biglietto = new Biglietto[100];
            mezzoTrasporto = new MezzoTrasporto[100];
            controllore = new Controllore[100];
            _nomeAg = ag; }

        public int _numb = 0;
        public override Biglietto AcquistaB(Passeggero pass, Agenzia age) {
            Passeggero pers1 = new Passeggero("Mario", "Mehi");
            Biglietto bi = new Biglietto(pers1);
            biglietto[_numb] = bi;
            _numb++;
            return bi;
        }
        public override void ModificaB(Biglietto nb) {
            if (biglietto[_numb] == nb)
                Console.WriteLine("puoi modificare");
            else
                Console.WriteLine("biglietto non trovato");
        }
        public override MezzoTrasporto AggMezz(int num, Controllore c, int pass) {
            MezzoTrasporto mezz = new MezzoTrasporto(num, c, pass);
            if (mezzoTrasporto[num]==null) {
                mezzoTrasporto[0] = mezz;
            }
            return mezz;
        }
        public void assContr(MezzoTrasporto m, Controllore c){
            //_mezzoTrasporto.
        }
    }
    public class MezzoTrasporto {
        public int _numID;
        public int _numpassT;
        public Controllore _controllore;
        public Passeggero[] _passeggeri;

        public MezzoTrasporto(int id, Controllore con, int numpass) {
            _numID = id;
            _controllore = con;
            _numpassT = numpass;
        }
        public void CheckIn() {
        }
    }
    public class Biglietto{
        public DateTime _data;
        public int _mezzoT;
        public string _agenzia;
        public string _persona;
        public string pagamento;
        private int numID;

        public Biglietto(Persona pers){
            _data = DateTime.Now;
            _persona = pers._cognome;
            //_agenzia = age._nomeAg;
            //_mezzoT = mezzt._numID; //mezzo assegnato
        }
    }
    public class Persona{
        public string _nome;
        public string _cognome;
    }
    public class Controllore : Persona
    {
        public string _numID;
        public Controllore(string num, string nome, Agenzia ag){
            this._nome = nome;
            _numID=num;
        }
        public void ControllaB() { }
    }
    public class Passeggero : Persona {
        //compra biglietto
        //mezzo di trasporto assegnato
        public Passeggero (String nome, string cognome) {
            _nome=nome;
            _cognome = cognome;
        }

                /*
        foreach (Biglietto bi in ag1.biglietto){
            Console.WriteLine(bi);
        }

        // Loop di mezzi di trasporto
        foreach (MezzoTrasporto me in ag1.mezzoTrasporto)
        {
            Console.WriteLine(me);
        }

        // Loop di controllori
        foreach (Controllore co in ag1.controllore)
        {
            Console.WriteLine(co);
        }*/
    }
}
