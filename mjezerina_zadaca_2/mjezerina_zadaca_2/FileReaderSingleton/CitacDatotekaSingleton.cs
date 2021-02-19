using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mjezerina_zadaca_2.FileReaderSingleton
{
    class CitacDatotekaSingleton
    {
        private String[] sadrzaj;
        private List<String[]> linijeSadrzaja;
        private List<String> listaLinija;
        private static CitacDatotekaSingleton instance;
        private static object syncLock = new object();

        private CitacDatotekaSingleton() { }

        public static CitacDatotekaSingleton Instance()
        {
            if (instance == null)
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        instance = new CitacDatotekaSingleton();
                    }
                }
            }
            return instance;
        }

        public void CitajDatoteku(string putanja)
        {

            if (File.Exists(putanja))
            {
                linijeSadrzaja = new List<String[]>();
                sadrzaj = File.ReadAllLines(putanja);
                for (int i = 1; i < sadrzaj.Length; i++)
                {
                    if(sadrzaj[i].Length < 2)
                    {
                        Console.WriteLine("Preskocena neispravna linija!");
                        continue;
                    }
                    linijeSadrzaja.Add(sadrzaj[i].Split(';'));
                }
            }
            else
            {
                Console.WriteLine("Datoteka nije na navedenoj lokaciji " + putanja);
                throw new System.Exception();
            }
        }

        public void CitajDatotekuLista(string putanja)
        {

            if (File.Exists(putanja))
            {
                listaLinija = new List<String>();
                sadrzaj = File.ReadAllLines(putanja);
                for (int i = 1; i < sadrzaj.Length; i++)
                {
                    if (sadrzaj[i].Length < 2)
                    {
                        Console.WriteLine("Preskocena neispravna linija!");
                        continue;
                    }
                    listaLinija.Add(sadrzaj[i]);
                }
            }
            else
            {
                Console.WriteLine("Datoteka nije na navedenoj lokaciji " + putanja);
                throw new System.Exception();
            }
        }

        public void CitajDatotekuKonfiguracije(string putanja)
        {

            if (File.Exists(putanja))
            {
                linijeSadrzaja = new List<String[]>();
                sadrzaj = File.ReadAllLines(putanja);
                for (int i = 0; i < sadrzaj.Length; i++)
                {
                    if (sadrzaj[i].Length < 2)
                    {
                        Console.WriteLine("Preskocena neispravna linija!");
                        continue;
                    }
                    linijeSadrzaja.Add(sadrzaj[i].Split(';'));
                }
            }
            else
            {
                Console.WriteLine("Datoteka nije na navedenoj lokaciji " + putanja);
                throw new System.Exception();
            }
        }



        public List<String[]> DohvatiListuRedaka()
        {
            return linijeSadrzaja;
        }

        public List<String> DohvatiListLinija()
        {
            return listaLinija;
        }
    }
}
