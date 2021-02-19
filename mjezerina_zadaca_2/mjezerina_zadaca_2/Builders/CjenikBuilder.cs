using mjezerina_zadaca_2;
using mjezerina_zadaca_2.FileReaderSingleton;
using mjezerina_zadaca_2.OutputSingleton;
using System;
using System.Collections.Generic;
using System.Globalization;
using Zadaca_2.Builders;

namespace Zadaca_2.Builders
{
    class CjenikBuilder : ICjenikBuilder
    {
        public List<Cjenik> listaCijena = new List<Cjenik>();
        private static OutputWriterSingleton outputWriterSingleton;
        public CjenikBuilder()
        {
            outputWriterSingleton = OutputWriterSingleton.Instance();
        }
        public void BuildCjenik(string putanja, CitacDatotekaSingleton citacDatoteka)
        {
            citacDatoteka.CitajDatoteku(putanja);
            List<String[]> listaRedaka = citacDatoteka.DohvatiListuRedaka();
            if (listaRedaka.Count == 0)
            {
                outputWriterSingleton.ispisGreske("Datoteka koja sadrži cijene je prazna!");
            }
            for (int i = 0; i < listaRedaka.Count; i++)
            {
                int idvrste = int.Parse(listaRedaka[i][0]);
                decimal najam = decimal.Parse(listaRedaka[i][1].Replace(',', '.'));
                decimal posatu = decimal.Parse(listaRedaka[i][2].Replace(',', '.'));
                decimal pokm = decimal.Parse(listaRedaka[i][3].Replace(',', '.'));

                Cjenik cjenik = new Cjenik(idvrste, najam, posatu, pokm);
                listaCijena.Add(cjenik);
            }
        }

        public List<Cjenik> dohvatiCjenik()
        {
            return listaCijena;
        }

        
    }
}
