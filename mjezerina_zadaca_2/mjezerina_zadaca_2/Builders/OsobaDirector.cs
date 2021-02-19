using mjezerina_zadaca_2.FileReaderSingleton;

namespace Zadaca_2.Builders
{
    class OsobaDirector
    {
        private IOsobaBuilder builder;

        public IOsobaBuilder Builder
        {
            set { builder = value; }
        }

        public void buildOsoba(string putanja, CitacDatotekaSingleton citacDatoteka)
        {
            this.builder.BuildOsoba(putanja, citacDatoteka);

        }
    }
}
