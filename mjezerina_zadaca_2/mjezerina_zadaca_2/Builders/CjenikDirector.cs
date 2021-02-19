using mjezerina_zadaca_2.FileReaderSingleton;

namespace Zadaca_2.Builders
{
    class CjenikDirector
    {
        private ICjenikBuilder builder;

        public ICjenikBuilder Builder
        {
            set { builder = value; }
        }

        public void buildCjenik(string putanja, CitacDatotekaSingleton citacDatoteka)
        {
            this.builder.BuildCjenik(putanja, citacDatoteka);

        }
    }
}
