using mjezerina_zadaca_2.Composite;
using mjezerina_zadaca_2.FileReaderSingleton;
using mjezerina_zadaca_2.OutputSingleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mjezerina_zadaca_2
{
    class UcitajOrganizacijskeJedinice
    {
        public List<Tvrtka> listaTvrtki = new List<Tvrtka>();
        public List<Tvrtka> listaPodredenihTvrtki = new List<Tvrtka>();
        public List<Tvrtka> privremenaLista = new List<Tvrtka>();
        public List<Tvrtka> privremenaLista2 = new List<Tvrtka>();
        public List<Tvrtka> konacnaLista = new List<Tvrtka>();
        private static OutputWriterSingleton outputWriterSingleton;
        
        public void BuildComposite(string putanja, CitacDatotekaSingleton citacDatoteka)
        {
            citacDatoteka.CitajDatoteku(putanja);
            List<String[]> listaRedaka = citacDatoteka.DohvatiListuRedaka();
            if (listaRedaka.Count == 0)
            {
                outputWriterSingleton.ispisGreske("Datoteka koja sadrži cijene je prazna!");
            }
            for (int i = 0; i < listaRedaka.Count; i++)
            {
                int id = int.Parse(listaRedaka[i][0]);
                string naziv_jedinice = listaRedaka[i][1];
                int nadredena;
                if (listaRedaka[i][2] != " ")
                {
                    nadredena = int.Parse(listaRedaka[i][2]);
                }
                else
                {
                    nadredena = 0;
                }
                string[] lokacije = listaRedaka[i][3].Trim().Replace(" ","").Split(',');
                int[] idlokacije = new int[lokacije.Length];

                for (int b = 0; b < (lokacije.Length-1); b++)
                {
                    idlokacije[b] = int.Parse(lokacije[b]);
                }

                CompositeTvrtka tvrtka = new CompositeTvrtka(id, naziv_jedinice, nadredena, idlokacije);

                if(listaTvrtki.Count != 0)
                {
                    foreach (var item in listaTvrtki)
                    {
                        if (item.id == tvrtka.nadredena)
                        {
                            CompositeTvrtka nesto = dodajNadredenu(tvrtka);
                            
                        }
                    }
                }
                
                listaTvrtki.Add(tvrtka);

            }
        }

        public CompositeTvrtka dodajNadredenu(CompositeTvrtka tvrtka)
        {
            CompositeTvrtka tvrtka1 = null;
            foreach (var item in listaTvrtki)
            {
                if(tvrtka.nadredena == item.id)
                {
                    tvrtka1 = (CompositeTvrtka)item;
                    tvrtka1.Add(tvrtka);
                }
            }
            return tvrtka1;
        }

        public List<Tvrtka> vratiListuTvrtka()
        {
            return listaTvrtki;
        }

        public List<Tvrtka> vratiPodredene(int id)
        {
            foreach (var item in listaTvrtki)
            {
                if(item.id == id)
                {
                    listaPodredenihTvrtki.Add(item);
                    privremenaLista2 = item.dajPodredene();                   

                }
            }
            if (listaPodredenihTvrtki.Count != 0)
            {
                dodajPodredene(privremenaLista2);
            }

            listaPodredenihTvrtki.AddRange(privremenaLista2);
            return listaPodredenihTvrtki;
        }

        public void dodajPodredene(List<Tvrtka> listaPodredenih)
        {   
            if (listaPodredenih.Count != 0)
            {
                foreach (var item in listaPodredenih)
                {
                    privremenaLista.AddRange(item.dajPodredene());
                    
                }
            }
            privremenaLista2.AddRange(privremenaLista);
            //if (privremenaLista.Count != 0)
            //{
            //    listaPodredenihTvrtki.Union(privremenaLista);
            //}

        }

       

        


    }
}
