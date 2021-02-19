using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mjezerina_zadaca_2.Composite
{
    public class CompositeTvrtka : Tvrtka, ITvrtka
    {
        List<Tvrtka> listaTvrtka = new List<Tvrtka>();
        List<Tvrtka> listaPodredenih = new List<Tvrtka>();

        public CompositeTvrtka(int id, string nazivJed, int nadredena, int[] listaLokacija) : base(id, nazivJed, nadredena, listaLokacija)
        {

        }
        public void Add(Tvrtka tvrtka)
        {
            listaPodredenih.Add(tvrtka);
        }

        public override List<Tvrtka> dajPodredene()
        {
            return listaPodredenih;
        }

        public override List<Tvrtka> dohvatiTvrtke()
        {
            return listaTvrtka;
        }

        public void Remove(Tvrtka tvrtka)
        {
            listaTvrtka.Remove(tvrtka);
        }

        
    }
}
