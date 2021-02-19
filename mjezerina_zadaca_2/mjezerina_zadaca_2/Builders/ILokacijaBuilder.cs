using mjezerina_zadaca_2.FileReaderSingleton;

namespace Zadaca_2.Builders
{
    interface ILokacijaBuilder
    {
        void BuildLokacija(string putanja, CitacDatotekaSingleton citacDatoteka);
    }
}
