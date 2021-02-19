using mjezerina_zadaca_2.FileReaderSingleton;

namespace Zadaca_2.Builders
{
    class LokacijeDirector
    {
        private ILokacijaBuilder builder;

        public ILokacijaBuilder Builder
        {
            set { builder = value; }
        }

        public void buildLokacije(string putanja, CitacDatotekaSingleton citacDatoteka)
        {
            this.builder.BuildLokacija(putanja, citacDatoteka);

        }
    }
}
