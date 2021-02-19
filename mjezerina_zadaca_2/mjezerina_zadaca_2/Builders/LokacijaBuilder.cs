using mjezerina_zadaca_2;
using mjezerina_zadaca_2.FileReaderSingleton;
using mjezerina_zadaca_2.OutputSingleton;
using System;
using System.Collections.Generic;

namespace Zadaca_2.Builders
{
    class LokacijaBuilder : ILokacijaBuilder
    {
        private List<Lokacija> listaLokacija = new List<Lokacija>();
        private static OutputWriterSingleton outputWriterSingleton;

        public LokacijaBuilder()
        {
            outputWriterSingleton = OutputWriterSingleton.Instance();
        }
        public void BuildLokacija(string putanja, CitacDatotekaSingleton citacDatoteka)
        {
            citacDatoteka.CitajDatoteku(putanja);
            List<String[]> listaRedaka = citacDatoteka.DohvatiListuRedaka();
            if (listaRedaka.Count == 0)
            {
                outputWriterSingleton.ispisGreske("Datoteka koja sadrži lokacije je prazna!");
            }
            for (int i = 0; i < listaRedaka.Count; i++)
            {
                int id = int.Parse(listaRedaka[i][0]);
                string naziv = listaRedaka[i][1];
                string adresaLokacije = listaRedaka[i][2];
                string gpsKoordinate = listaRedaka[i][3];

                Lokacija lokacije = new Lokacija(id,naziv,adresaLokacije,gpsKoordinate);
                listaLokacija.Add(lokacije);
            }

        }

        public List<Lokacija> dohvatiLokacije()
        {
            return listaLokacija;
        }

        public String dohvatiNazivLokacije(int idLokacije)
        {
            string naziv = "";
            if (listaLokacija.Count != 0)
            {
                foreach (var item in listaLokacija)
                {
                    if (item.id == idLokacije)
                    {
                        naziv = item.naziv;
                    }
                }
            }
            return naziv;
        }
    }
}
