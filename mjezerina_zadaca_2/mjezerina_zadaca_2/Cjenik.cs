using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mjezerina_zadaca_2
{
    class Cjenik
    {
        public int id_vrste_vozila;
        public decimal najam;
        public decimal posatu;
        public decimal pokm;
        public List<Cjenik> listaCjenik;

        public Cjenik(int idvrste, decimal najam, decimal posatu, decimal pokm)
        {
            this.id_vrste_vozila = idvrste;
            this.najam = najam;
            this.posatu = posatu;
            this.pokm = pokm;
        }
    }
}
