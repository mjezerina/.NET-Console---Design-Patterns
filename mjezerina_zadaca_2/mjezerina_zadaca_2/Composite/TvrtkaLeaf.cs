using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mjezerina_zadaca_2.Composite
{
    public class TvrtkaLeaf : Tvrtka
    {
        

        public TvrtkaLeaf(int id, string nazivJed, int nadredena, int[] listaLokacija):base(id,nazivJed,nadredena,listaLokacija)
        {
            

        }

        public void Add(CompositeTvrtka tvrtka)
        {
            throw new NotImplementedException();
        }

        public override List<Tvrtka> dajPodredene()
        {
            throw new NotImplementedException();
        }

        public override List<Tvrtka> dohvatiTvrtke()
        {
            throw new NotImplementedException();
        }

        public void Remove(CompositeTvrtka tvrtka)
        {
            throw new NotImplementedException();
        }

        
    }
}
