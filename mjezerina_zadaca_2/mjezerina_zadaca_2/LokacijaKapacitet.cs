using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mjezerina_zadaca_2
{
    class LokacijaKapacitet
    {
        public int id_lokacija;
        public int id_vrste_vozila;
        public int broj_mjesta_za_vrstu;
        public int raspolozivi_broj_vrsta;
        public List<LokacijaKapacitet> lokacijeKapaciteti;

        public LokacijaKapacitet(int id, int id_vrste, int brojMjestaZaVrstu, int raspBrojVrsta)
        {
            this.id_lokacija = id;
            this.id_vrste_vozila = id_vrste;
            this.broj_mjesta_za_vrstu = brojMjestaZaVrstu;
            this.raspolozivi_broj_vrsta = raspBrojVrsta;
        }
    }
}
