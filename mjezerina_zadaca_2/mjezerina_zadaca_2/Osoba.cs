using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mjezerina_zadaca_2
{
    class Osoba
    {
        public int id;
        public string ime_i_prezime;
        public List<Osoba> listaOsoba;

        public Osoba(int id, string imeiprezime)
        {
            this.id = id;
            this.ime_i_prezime = imeiprezime;
        }
    }
}
