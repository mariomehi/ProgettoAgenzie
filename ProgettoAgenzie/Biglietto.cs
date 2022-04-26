using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoAgenzie
{
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
            for (int i = 0; i < age._amezzo.Length; i++)
            {
                if (age._amezzo[i]._mezzoID == mezzoid)
                    me = age._amezzo[i];
            }
            numID = Utility.RandomN();
        }
        public Biglietto getBiglietto(Biglietto bi)
        {
            return bi;
        }
    }
}
