using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mjezerina_zadaca_2
{
    class Lokacija
    {
        public int id;
        public string naziv;
        public string adresa_lokacije;
        public string gpsKoordinate;
        public List<Lokacija> listaLokacija;

        public Lokacija(int id, string naziv, string adresa, string koordinate)
        {
            this.id = id;
            this.naziv = naziv;
            this.adresa_lokacije = adresa;
            this.gpsKoordinate = koordinate;
        }
    }
}
