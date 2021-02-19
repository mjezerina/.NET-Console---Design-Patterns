using mjezerina_zadaca_2.FileReaderSingleton;
using mjezerina_zadaca_2.OutputSingleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mjezerina_zadaca_2.Prototype;

namespace mjezerina_zadaca_2
{
    class PuniVozila
    {
        private static OutputWriterSingleton outputWriterSingleton;
        List<Vozilo> listaVozila = new List<Vozilo>();


        public void BuildVozilo(string putanja, CitacDatotekaSingleton citacDatoteka)
        {
            citacDatoteka.CitajDatoteku(putanja);
            List<String[]> listaRedaka = citacDatoteka.DohvatiListuRedaka();
            if (listaRedaka.Count == 0)
            {
                outputWriterSingleton.ispisGreske("Datoteka koja sadrži vozila je prazna!");
            }
            for (int i = 0; i < listaRedaka.Count; i++)
            {
                int id = int.Parse(listaRedaka[i][0]);
                string naziv = listaRedaka[i][1];
                int vrijemePunjenja = int.Parse(listaRedaka[i][2]);
                int domet = int.Parse(listaRedaka[i][3]);



                Vozilo vozilo = new Vozilo(id, naziv, vrijemePunjenja, domet);
                listaVozila.Add(vozilo);

            }

        }

        public List<Vozilo> dajListuVozila()
        {
            return listaVozila;
        }

        public string dohvatiNazivVozila(int id)
        {
            string naziv = "";
            foreach (var item in listaVozila)
            {
                if(item.id == id)
                {
                    naziv = item.nazivVozila;
                }
            }
            return naziv;
        }
    }
}
