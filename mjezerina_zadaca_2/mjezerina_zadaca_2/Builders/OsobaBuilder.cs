using mjezerina_zadaca_2;
using mjezerina_zadaca_2.FileReaderSingleton;
using mjezerina_zadaca_2.OutputSingleton;
using System;
using System.Collections.Generic;

namespace Zadaca_2.Builders
{
    class OsobaBuilder : IOsobaBuilder
    {
        private List<Osoba> listaOsoba = new List<Osoba>();
        private static OutputWriterSingleton outputWriterSingleton;

        public OsobaBuilder()
        {
            outputWriterSingleton = OutputWriterSingleton.Instance();
        }
        public void BuildOsoba(string putanja, CitacDatotekaSingleton citacDatoteka)
        {
            citacDatoteka.CitajDatoteku(putanja);
            List<String[]> listaRedaka = citacDatoteka.DohvatiListuRedaka();
            if(listaRedaka.Count == 0)
            {
                outputWriterSingleton.ispisGreske("Datoteka koja sadrži osobe je prazna!");
            }
            for (int i = 0; i < listaRedaka.Count; i++)
            {
                int idOsoba = int.Parse(listaRedaka[i][0]);
                String ime_i_prezime = listaRedaka[i][1];
                
                Osoba osoba = new Osoba(idOsoba,ime_i_prezime);
                listaOsoba.Add(osoba);
            }
        }

        public List<Osoba> dohvatiOsobe()
        {
            return listaOsoba;
        }

        public String dohvatiNazivOsobe(int idOsobe)
        {
            string naziv = "";
            if (listaOsoba.Count != 0)
            {
                foreach (var item in listaOsoba)
                {
                    if (item.id == idOsobe)
                    {
                        naziv = item.ime_i_prezime;
                    }
                }
            }
            return naziv;

        }


    }
}
