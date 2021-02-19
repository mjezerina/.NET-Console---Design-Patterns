using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mjezerina_zadaca_2.Composite
{
    public abstract class Tvrtka
    {
        public int id; 
        public string naziv_jedinice; 
        public int nadredena;
        public int[] listaLokacija;
        public List<Tvrtka> listaTvrtki = new List<Tvrtka>();


        public Tvrtka(int id, string nazivJed, int nadredena, int[] listaLokacija)
        {
            this.id = id;
            this.naziv_jedinice = nazivJed;
            this.nadredena = nadredena;
            this.listaLokacija = listaLokacija;

        }

        public abstract List<Tvrtka> dohvatiTvrtke();

        public abstract List<Tvrtka> dajPodredene();
    }

    
}
