using mjezerina_zadaca_2.FileReaderSingleton;

namespace Zadaca_2.Builders
{
    interface ILokacijaKapacitetBuilder
    {
        void BuildLokacijaKapacitet(string putanja, CitacDatotekaSingleton citacDatoteka);
    }
}
