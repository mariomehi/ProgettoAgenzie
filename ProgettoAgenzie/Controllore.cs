using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoAgenzie
{
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
            if (_ag._amezzo[1]._mezzoID == mezzo && _ag._amezzo[1].Posti[0].numID == bi.numID)
            {
                if (bi._data2 > DateTime.Now && bi._data < bi._data2)
                    Console.WriteLine("Controllo biglietto eseguito con successo.");
            }
        }
    }
}
