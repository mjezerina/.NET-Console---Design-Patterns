using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mjezerina_zadaca_2
{
    class Najam
    {
        public int idKorisnika;
        public int vrstaVozila;
        public int sifraVozila;
        public DateTime vrijemeVracanja;

        public Najam(int idKorisnika, int idVozila, int sifraVozila, DateTime vrijemeVracanja)
        {
            this.idKorisnika = idKorisnika;
            this.vrstaVozila = idVozila;
            this.sifraVozila = sifraVozila;
            this.vrijemeVracanja = vrijemeVracanja;
        }
    }
}
