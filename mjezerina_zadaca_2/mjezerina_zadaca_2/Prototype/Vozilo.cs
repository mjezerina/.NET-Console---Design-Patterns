using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace mjezerina_zadaca_2.Prototype
{
    class Vozilo : VoziloPrototype
    {
        public int id;
        public String nazivVozila;
        public int vrijemePunjenjaBaterije;
        public int domet;
        public bool iznajmljeno;
        public bool kvar;
        public int jedinstveniId { get; set; }
        public List<Vozilo> listaVozila;
        public int brojNajma;
        public int brojKm;
        public int lokacijaId;

        public Vozilo(int id, string naziv, int vrijemePunjenja, int domet)
        {
            this.id = id;
            this.nazivVozila = naziv;
            this.vrijemePunjenjaBaterije = vrijemePunjenja;
            this.domet = domet;
            iznajmljeno = false;
            brojNajma = 0;
            brojKm = 0;
            kvar = false;
            jedinstveniId = dodjeliJedinstveniId();
            
        }

        public override VoziloPrototype Clone()
        {
            return this.MemberwiseClone() as Vozilo;
        }

        public int dodjeliJedinstveniId()
        {
            Random random = new Random();
            int broj = random.Next(1, 1000);
            int serial = broj * domet * vrijemePunjenjaBaterije + 10000;
            return serial;
        }
    }
}
