using mjezerina_zadaca_2;
using mjezerina_zadaca_2.FileReaderSingleton;
using mjezerina_zadaca_2.OutputSingleton;
using System;
using System.Collections.Generic;

namespace Zadaca_2.Builders
{
    class LokacijaKapacitetBuilder : ILokacijaKapacitetBuilder
    {
        private List<LokacijaKapacitet> listaKapacitetaLokacija = new List<LokacijaKapacitet>();
        private static OutputWriterSingleton outputWriterSingleton;

        public LokacijaKapacitetBuilder()
        {
            outputWriterSingleton = OutputWriterSingleton.Instance();
        }
        public void BuildLokacijaKapacitet(string putanja, CitacDatotekaSingleton citacDatoteka)
        {
            citacDatoteka.CitajDatoteku(putanja);
            List<String[]> listaRedaka = citacDatoteka.DohvatiListuRedaka();
            if (listaRedaka.Count == 0)
            {
                outputWriterSingleton.ispisGreske("Datoteka koja sadrži kapacitete lokacija je prazna!");
            }
            for (int i = 0; i < listaRedaka.Count; i++)
            {
                int idLokacije = int.Parse(listaRedaka[i][0]);
                int idVrsteVozila = int.Parse(listaRedaka[i][1]);
                int brojMjesta = int.Parse(listaRedaka[i][2]);
                int raspoloziviBrojVrsta = int.Parse(listaRedaka[i][3]);

                LokacijaKapacitet lokacijaKapacitet = new LokacijaKapacitet(idLokacije, idVrsteVozila, brojMjesta, raspoloziviBrojVrsta);
                listaKapacitetaLokacija.Add(lokacijaKapacitet);
            }
        }

        public List<LokacijaKapacitet> dohvatiListuKapacitetaLokacija()
        {
            return listaKapacitetaLokacija;
        }

       
    }
}
