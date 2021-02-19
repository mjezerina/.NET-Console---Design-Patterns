using mjezerina_zadaca_2.Composite;
using mjezerina_zadaca_2.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mjezerina_zadaca_2
{
    class FunkcijeAplikacije
    {
        public List<LokacijaKapacitet> lokKapacitet;
        public List<Vozilo> listaVozila;
        public List<Cjenik> listaCijena;
        public List<Tvrtka> listaTvrtka;
        public List<Tvrtka> listaTvrtkaHijerarhija = new List<Tvrtka>();
        public List<Lokacija> listaLokacija;
        public List<Najam> listaNajmova1 = new List<Najam>();
        int brojRaspolozivihVozilaVrste;
        int brojRaspolozivihMjestaVozila;
        UcitajOrganizacijskeJedinice organizacijskeJedinice = new UcitajOrganizacijskeJedinice();
        List<KeyValuePair<int,DateTime>> listaNajmova = new List<KeyValuePair<int, DateTime>>();

        public void ucitajNaredbePrva(List<LokacijaKapacitet> lokKap, List<Vozilo> listVoz, List<Cjenik> listCijena, List<Tvrtka> listaTvrtka, List<Lokacija> listaLokacija)
        {
            
            this.lokKapacitet = lokKap;
            this.listaVozila = listVoz;
            this.listaCijena = listCijena;
            this.listaTvrtka = listaTvrtka;
            this.listaLokacija = listaLokacija;
        }
        public int pregledRaspolozivihVozila(int idLokacije, int idVrstaVozila)
        {
            foreach (var item in lokKapacitet)
            {
                if (item.id_lokacija == idLokacije)
                {
                    if (idVrstaVozila == item.id_vrste_vozila)
                    {
                        brojRaspolozivihVozilaVrste = item.raspolozivi_broj_vrsta;
                    }

                }
            }
            return brojRaspolozivihVozilaVrste;
        }



        public bool najamOdabranogVozila(int idVrstaVozila, int idLokacije,DateTime vrijemeNajma, int idKorisnik){
            Boolean uspjeh = false;
            foreach (var item in lokKapacitet)
            {
                if (item.id_lokacija == idLokacije)
                {
                    foreach (var item2 in listaVozila)
                    {
                        if (item2.lokacijaId == idLokacije)
                        {
                            if (item2.id == idVrstaVozila)
                            {
                                brojRaspolozivihVozilaVrste = item.raspolozivi_broj_vrsta;
                                if (brojRaspolozivihVozilaVrste > 0)
                                {
                                    item2.iznajmljeno = true;
                                    item.raspolozivi_broj_vrsta = item.raspolozivi_broj_vrsta - 1;
                                    uspjeh = true;
                                    Najam najam = new Najam(idKorisnik, idVrstaVozila, item2.jedinstveniId, vrijemeNajma);
                                    listaNajmova1.Add(najam);
                                    var list = new List<KeyValuePair<int, DateTime>>();
                                    listaNajmova.Add(new KeyValuePair<int, DateTime>(idKorisnik, vrijemeNajma));
                                }
                            }


                        }
                    }
                }
            }
            
            return uspjeh;
        }

        public int pregledRaspolozivihMjesta(int idLokacije, int idVrstaVozila)
        {

            foreach (var item in lokKapacitet)
            {
                if (item.id_lokacija == idLokacije)
                {
                    if (item.id_vrste_vozila == idVrstaVozila)
                    {
                        brojRaspolozivihMjestaVozila = item.broj_mjesta_za_vrstu;
                    }
                }
            }
            return brojRaspolozivihMjestaVozila;
        }

        public bool vracanjeVozila(int idVrstaVozila, int idLokacije, int idKorisnik,int brojkm)
        {
            bool uspjeh = false;
            bool nijeMoguce = false;

            if (listaNajmova1.Count != 0)
            {
                foreach (var item in listaNajmova1)
                {
                    if (item.idKorisnika == idKorisnik)
                    {
                        if (item.vrstaVozila == idVrstaVozila)
                        {
                            foreach (var item2 in lokKapacitet)
                            {
                                if (item2.id_lokacija == idLokacije)
                                {
                                    if (item2.id_vrste_vozila == idVrstaVozila)
                                    {
                                        if (item2.broj_mjesta_za_vrstu > item2.raspolozivi_broj_vrsta)
                                        {
                                            item2.raspolozivi_broj_vrsta = item2.raspolozivi_broj_vrsta + 1;
                                            uspjeh = true;
                                            vratiVozilo(item.sifraVozila, brojkm);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Nema raspoloživih mjesta na odabranoj lokaciji za odabranu vrstu vozila!");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        nijeMoguce = true;
                    }
                } 
                

            }
            else
            {
                Console.WriteLine("Nije moguće vratiti vozilo jer trenutno nema iznajmljenih vozila!");
            }
            if (nijeMoguce)
            {
                Console.WriteLine("Korisnik nije iznajmio vozilo stoga ga ne može vratiti");
            }
            return uspjeh;
        }

        public decimal izracunCijeneNajma(int idVrstaVozila)
        {
            decimal cijena = 0;
            foreach (var item in listaCijena)
            {
                if(item.id_vrste_vozila == idVrstaVozila)
                {
                    cijena = item.najam;
                }
            }
            return cijena;
        }

        public decimal izracunCijenePoSatu(int idVrstaVozila, DateTime vrijemeVracanja, int idKorisnik)
        {
            decimal cijenaPoSatu = 0;
            decimal ukupnoCijena = 0;
            DateTime vrijemePosudbe;
            int brojSati;
            foreach (var item in listaCijena)
            {
                if (item.id_vrste_vozila == idVrstaVozila)
                {
                    cijenaPoSatu = item.posatu;

                    if(listaNajmova.Count != 0) {
                        vrijemePosudbe = listaNajmova.Find(pair => pair.Key == idKorisnik).Value;
                        brojSati = vrijemeVracanja.Hour - vrijemePosudbe.Hour;
                        brojSati = brojSati + 1;
                        ukupnoCijena = brojSati * cijenaPoSatu;
                        foreach (var item2 in listaNajmova.ToList())
                        {
                            if(item2.Key == idKorisnik &&item2.Value == vrijemePosudbe)
                            {
                                listaNajmova.Remove(item2);
                                if(listaNajmova.Count == 0)
                                {
                                    break;
                                }
                            }

                        }
                    }       
                    
                }
            }
            return ukupnoCijena;
        }

        public decimal izracunCijenePoKm(int idVrstaVozila, int brojKm)
        {
            decimal cijenaKm = 0;
            decimal cijenaUkupno = 0;
            foreach (var item in listaCijena)
            {
                if (item.id_vrste_vozila == idVrstaVozila)
                {
                    cijenaKm = item.pokm;
                }
            }
            cijenaUkupno = cijenaKm * brojKm;
            return cijenaUkupno;
        }

        public void prikaziStrukturu() {
            string nazivJedinice = "";
            string nazivLokacije = "";

            foreach (var item in listaTvrtka)
            {
                nazivJedinice = item.naziv_jedinice;
                Console.WriteLine("----- STRUKTURNA JEDINICA:");
                Console.WriteLine("-------- " + nazivJedinice);
                int[] lokacije = item.listaLokacija;
                Console.WriteLine("--------- " + " NAZIV LOKACIJA:");
                foreach (var item2 in lokacije)
                {
                    foreach(var item3 in listaLokacija)
                    {
                        if (item2 == item3.id)
                        {
                            nazivLokacije = item3.naziv;
                            Console.WriteLine("----------- " + item3.naziv);
                        } 
                    }
                }
            }
        }

        public void prikaziStrukturuJedinice(int id) {
            string nazivJedinice = "";
            string nazivLokacije = "";
            organizacijskeJedinice.listaTvrtki = listaTvrtka;
            listaTvrtkaHijerarhija = organizacijskeJedinice.vratiPodredene(id);
            foreach (var item in listaTvrtkaHijerarhija)
            {
                nazivJedinice = item.naziv_jedinice;
                Console.WriteLine("----- STRUKTURNA JEDINICA:");
                Console.WriteLine("-------- " + nazivJedinice);
                int[] lokacije = item.listaLokacija;
                Console.WriteLine("--------- " + " NAZIV LOKACIJA:");
                foreach (var item2 in lokacije)
                {
                    foreach (var item3 in listaLokacija)
                    {
                        if (item2 == item3.id)
                        {
                            nazivLokacije = item3.naziv;
                            Console.WriteLine("----------- " + item3.naziv);
                        }
                    }
                }
            }
        }

        public void prikaziStanjeJedinice(int id) {
        
            
        }

        public void prikaziStrukturuStanje()
        {

        }

        public void prikaziStanjeStruktura() { }

        public void prikaziStrukturuStanjeVrste(int n)
        {

        }

        public void prikaziStanjeStrukturaVrste(int n)
        {

        }

        public void vratiVozilo(int sifra, int km)
        {
            foreach (var item in listaVozila)
            {
                if(item.jedinstveniId == sifra)
                {
                    item.iznajmljeno = false;
                    item.brojNajma = item.brojNajma + 1;
                    item.brojKm = km;
                }
            }
        }
    }
}

