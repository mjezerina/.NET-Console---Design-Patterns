using mjezerina_zadaca_2.FileReaderSingleton;

namespace Zadaca_2.Builders
{
    class LokacijaKapacitetDirector
    {
        private ILokacijaKapacitetBuilder builder;

        public ILokacijaKapacitetBuilder Builder
        {
            set { builder = value; }
        }

        public void buildLokacijeKapacitet(string putanja, CitacDatotekaSingleton citacDatoteka)
        {
            this.builder.BuildLokacijaKapacitet(putanja, citacDatoteka);

        }
    }
}
