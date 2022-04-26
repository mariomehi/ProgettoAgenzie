using System;
using System.Collections;
using System.Collections.Generic;

namespace ProgettoAgenzie
{
    public class Viaggio
    {
        public DateTime date;
        public int numViaggio = 1;
        public Biglietto bi;
        public Mezzo mez;
        Dictionary<DateTime, Biglietto> dict=new Dictionary<DateTime, Biglietto>();

        public Viaggio(Agenzia ag, int mezzo, Biglietto bigli)
        {
            date = DateTime.Now;
            bi = bigli;
            dict.Add(new DateTime(),bigli);

            for (int i = 0; i < ag._amezzo.Length; i++)
            {
                if (ag._amezzo[i]._mezzoID == mezzo){
                    mez = ag._amezzo[i];
                }
            }
            ag.aggPosto(bigli, mezzo);
            ag._amezzo[numViaggio] = mez;
            numViaggio++;
        }
        //public void getTratta(Mezzo[] me){
        //}
        public void aggViaggio()
        {
            if (!dict.ContainsKey(date))
            {
                dict.Add(date, bi);
            }
        }
    }
}
