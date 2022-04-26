using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoAgenzie
{
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
}
