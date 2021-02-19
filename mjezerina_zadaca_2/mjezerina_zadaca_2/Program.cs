using mjezerina_zadaca_2.Composite;
using mjezerina_zadaca_2.FileReaderSingleton;
using mjezerina_zadaca_2.OutputSingleton;
using mjezerina_zadaca_2.Prototype;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Zadaca_2.Builders;

namespace mjezerina_zadaca_2
{
    class Program
    {
        private static String vozila = "";
        private static String lokacije = "";
        private static String cjenik = "";
        private static String kapaciteti = "";
        private static String osobe = "";
        private static String vrijeme = "";
        private static String skupni = "";
        private static String konfiguracija = "";
        private static String tvrtka = "";
        private static String formatiranoVrijeme = "";
        private static int tekst;
        private static int cijeli;
        private static int decimala;
        private static List<String> skupneNaredbe = new List<String>();
        private static List<Osoba> listaOsoba = new List<Osoba>();
        private static DateTime pocetnoVrijeme;
        private static List<LokacijaKapacitet> listaKapaciteta = new List<LokacijaKapacitet>();
        private static List<Vozilo> listaVozila = new List<Vozilo>();
        private static List<Vozilo> listaRaspolozivihVozila = new List<Vozilo>();
        private static List<Cjenik> listaCijena = new List<Cjenik>();
        private static List<Tvrtka> listaTvrtki = new List<Tvrtka>();
        private static List<Lokacija> listaLokacija = new List<Lokacija>();
        private static FunkcijeAplikacije funkcijaAplikacije = new FunkcijeAplikacije();
        private static bool gotovoCitanje = false;



        //Instatacija Prototype
        private static PuniVozila puniVozila = new PuniVozila();

        //Instantacija Builder-a
        private static LokacijeDirector lokacijeDirector = new LokacijeDirector();
        private static LokacijaBuilder lokacijaBuilder = new LokacijaBuilder();
        private static CjenikDirector cjenikDirector = new CjenikDirector();
        private static CjenikBuilder cjenikBuilder = new CjenikBuilder();
        private static LokacijaKapacitetDirector lokacijaKapacitetDirector = new LokacijaKapacitetDirector();
        private static LokacijaKapacitetBuilder lokacijaKapacitetBuilder = new LokacijaKapacitetBuilder();
        private static OsobaDirector osobaDirector = new OsobaDirector();
        private static OsobaBuilder osobaBuilder = new OsobaBuilder();


        private static OutputWriterSingleton outputWriter = OutputWriterSingleton.Instance();
        private static CitacDatotekaSingleton citacDatoteka = CitacDatotekaSingleton.Instance();
        static void Main(string[] args)
        {

            if (args.Length == 1)
            {
                ucitavanjeKonfiguracije(args);
                provjeriPostojanjeDatoteke(konfiguracija);
                citacDatoteka.CitajDatotekuKonfiguracije(konfiguracija);
                List<String[]> skupneNaredbe = citacDatoteka.DohvatiListuRedaka();
                ucitavanjePutanjaKonf(skupneNaredbe);
                provjeriPostojanjeDatoteke(vozila);
                provjeriPostojanjeDatoteke(lokacije);
                provjeriPostojanjeDatoteke(cjenik);
                provjeriPostojanjeDatoteke(kapaciteti);
                provjeriPostojanjeDatoteke(osobe);
                if (skupni != "")
                {
                    provjeriPostojanjeDatoteke(skupni);
                    citajNaredbeSkupni(skupni);
                }
                if (tvrtka != "") {
                    provjeriPostojanjeDatoteke(tvrtka);
                }
                
                formatiranoVrijeme = vrijeme;



            }
            if (args.Length > 1)
            {
                provjeraPostojanjaArgumenata(args);
                //Postoje svi argumenti idi na učitavanje putanja
                ucitavanjePutanja(args);
                //Putanje su učitane u varijable, slijedi provjera postojanja datoteka.
                provjeriPostojanjeDatoteke(vozila);
                provjeriPostojanjeDatoteke(lokacije);
                provjeriPostojanjeDatoteke(cjenik);
                provjeriPostojanjeDatoteke(kapaciteti);
                provjeriPostojanjeDatoteke(osobe);
                if (skupni != "")
                {
                    provjeriPostojanjeDatoteke(skupni);
                    citajNaredbeSkupni(skupni);
                }
                if (tvrtka != "") {
                    provjeriPostojanjeDatoteke(tvrtka);
                }
                
                formatirajVrijemePrikaz(vrijeme);

            }

            //Datoteke postoje,slijedi čitanje sadržaja datoteka

 
            Console.WriteLine("Vrijeme sustava: " + formatiranoVrijeme);

            ////ucitaj composite
            if(tvrtka != "")
            {
                UcitajOrganizacijskeJedinice citajOrgJed = new UcitajOrganizacijskeJedinice();
                citajOrgJed.BuildComposite(tvrtka, citacDatoteka);
                listaTvrtki = citajOrgJed.vratiListuTvrtka();
            }





            //Učitavanje podatka za vozilo
            puniVozila.BuildVozilo(vozila, citacDatoteka);
            listaVozila = puniVozila.dajListuVozila();
            if (listaVozila.Count == 0)
            {
                outputWriter.ispisGreske("Nisu ispravno učitani podaci o vozilima");
            }

            //Učitavanje podatka za lokacije
            lokacijeDirector.Builder = lokacijaBuilder;
            lokacijaBuilder.BuildLokacija(lokacije, citacDatoteka);
            listaLokacija = lokacijaBuilder.dohvatiLokacije();
            if (listaLokacija.Count == 0)
            {
                outputWriter.ispisGreske("Nisu ispravno učitani podaci o lokacijama");
            }

           

            //Učitavanje podataka za cijene
            cjenikDirector.Builder = cjenikBuilder;
            cjenikBuilder.BuildCjenik(cjenik, citacDatoteka);
            listaCijena = cjenikBuilder.dohvatiCjenik();
            if (listaCijena.Count == 0)
            {
                outputWriter.ispisGreske("Nisu ispravno učitani podaci o cijenama.");
            }

            //Učitavanje podataka  za kapacitete lokacija
            lokacijaKapacitetDirector.Builder = lokacijaKapacitetBuilder;
            lokacijaKapacitetBuilder.BuildLokacijaKapacitet(kapaciteti, citacDatoteka);

            listaKapaciteta = lokacijaKapacitetBuilder.dohvatiListuKapacitetaLokacija();
            if (listaKapaciteta.Count == 0)
            {
                outputWriter.ispisGreske("Nisu ispravno učitani podaci o kapacitetima.");
            }

            

            //Učitavanje podataka svih osoba
            osobaDirector.Builder = osobaBuilder;
            osobaBuilder.BuildOsoba(osobe, citacDatoteka);
            listaOsoba = osobaBuilder.dohvatiOsobe();
            if (listaOsoba.Count == 0)
            {
                outputWriter.ispisGreske("Nisu ispravno učitani podaci o osobama.");
            }
            Console.WriteLine("Datoteke uspješno učitane, unesite naredbu:");
            pocetnoVrijeme = formatirajVrijeme(formatiranoVrijeme);
            napuniListuJedinstvenihVozila();
            bool pocetak = true;
            while (pocetak)
            {
                FunkcijeAplikacije funkcijaAplikacije = new FunkcijeAplikacije();
                String naredba;
                if (skupneNaredbe.Count != 0 && !gotovoCitanje)
                {
                    for (int i = 0; i < skupneNaredbe.Count; i++)
                    {
                        naredba = skupneNaredbe[i];
                        upravljanjeNaredbom(naredba);
                    }
                    gotovoCitanje = true;
                }
                Console.InputEncoding = Encoding.Unicode;
                naredba = Console.ReadLine();
                upravljanjeNaredbom(naredba);
            }
        }

        private static void napuniListuJedinstvenihVozila()
        {
            foreach (var item in listaKapaciteta)
            {
                foreach (var item2 in listaVozila)
                {
                    if (item.id_vrste_vozila == item2.id)
                    {
                        Vozilo novoVozilo = item2.Clone() as Vozilo;
                        novoVozilo.lokacijaId = item.id_lokacija;
                        listaRaspolozivihVozila.Add(novoVozilo);
                    }
                }

            }
        }
        

        private static void upravljanjeNaredbom(string naredba)
        {
            int idKorisnik;
            int idLokacije;
            int idVrstaVozila;
            String nazivVozila;
            String nazivLokacije;
            String nazivKorisnika;
            Regex regex = new Regex(@"^([0-9]+)(;) ((„|,)[0-9]{4})([-]{0,1})([0-9]{2})([-]{0,1})([0-9]{2})([ ]{0,1})([0-9]{2})([:]{0,1})([0-9]{2})([:]{0,1})([0-9]{2}(“|""))((;) ([0-9]+))?((;) ([0-9]+))?((;) ([0-9]+))?((;) ([0-9]+))?((;) ([a-zA-Z0-9_ ]+))?$");
            Regex regex2 = new Regex(@"^([5-8]+)(;) (\w)+(([.]{0,1})(\w)){3}( (\w)+)?( [0-9])?$");
            var rezultat = regex.Match(naredba);
            var rezultat2 = regex2.Match(naredba);
            if (rezultat.Success || rezultat2.Success)
            {
                funkcijaAplikacije.ucitajNaredbePrva(listaKapaciteta, listaRaspolozivihVozila, listaCijena,listaTvrtki,listaLokacija);
                if (naredba.StartsWith("1"))
                {
                    string[] linije = parsirajNaredbu(naredba);
                    string vrijeme = linije[1].Substring(2, linije[1].Length - 3);
                    idKorisnik = int.Parse(linije[2]);
                    idLokacije = int.Parse(linije[3]);
                    idVrstaVozila = int.Parse(linije[4]);
                    nazivVozila = puniVozila.dohvatiNazivVozila(idVrstaVozila);
                    nazivLokacije = lokacijaBuilder.dohvatiNazivLokacije(idLokacije);
                    int brojRaspolozivihVozila = funkcijaAplikacije.pregledRaspolozivihVozila(idLokacije, idVrstaVozila);
                    DateTime novoVrijeme = formatirajVrijeme(vrijeme);
                    if (novoVrijeme <= pocetnoVrijeme)
                    {
                        Console.WriteLine("Nije uneseno ispravno vrijeme! Vrijeme mora biti veće od trenutnog vremena sustava");

                    }
                    else
                    {
                        pocetnoVrijeme = novoVrijeme;
                        if (brojRaspolozivihVozila == 0)
                        {
                            Console.WriteLine("nema raspolozivih vozila vrste:" + nazivVozila + " na lokaciji:" + nazivLokacije);
                        }
                        else
                        {
                            Console.WriteLine("Broj raspolozivih vozila vrste:" + nazivVozila + " na lokaciji:" + nazivLokacije + " iznosi: " + brojRaspolozivihVozila);
                        }
                        Console.WriteLine("Trenutno vrijeme je: " + pocetnoVrijeme);
                    }



                }
                if (naredba.StartsWith("2"))
                {
                    string[] linije = parsirajNaredbu(naredba);
                    string vrijeme = linije[1].Substring(2, linije[1].Length - 3);
                    idKorisnik = int.Parse(linije[2]);
                    idLokacije = int.Parse(linije[3]);
                    idVrstaVozila = int.Parse(linije[4]);
                    nazivVozila = puniVozila.dohvatiNazivVozila(idVrstaVozila);
                    nazivLokacije = lokacijaBuilder.dohvatiNazivLokacije(idLokacije);
                    nazivKorisnika = osobaBuilder.dohvatiNazivOsobe(idKorisnik);
                    DateTime novoVrijeme = formatirajVrijeme(vrijeme);
                    if (novoVrijeme <= pocetnoVrijeme)
                    {
                        Console.WriteLine("Nije uneseno ispravno vrijeme! Vrijeme mora biti veće od trenutnog vremena sustava");

                    }
                    else
                    {
                        pocetnoVrijeme = novoVrijeme;
                        if (funkcijaAplikacije.najamOdabranogVozila(idVrstaVozila, idLokacije, novoVrijeme, idKorisnik))
                        {
                            Console.WriteLine("U " + pocetnoVrijeme + " korisnik " + nazivKorisnika + " traži na lokaciji" + nazivLokacije + " najam " + nazivVozila);
                        }
                        else
                        {
                            Console.WriteLine("U " + pocetnoVrijeme + "korisnik " + nazivKorisnika + " traži na lokaciji" + nazivLokacije + " najam " + nazivVozila + " no nema dostupnih vozila");
                        }
                        Console.WriteLine("Trenutno vrijeme je: " + pocetnoVrijeme);
                    }
                }
                if (naredba.StartsWith("3"))
                {
                    string[] linije = parsirajNaredbu(naredba);
                    string vrijeme = linije[1].Substring(2, linije[1].Length - 3);
                    idKorisnik = int.Parse(linije[2]);
                    idLokacije = int.Parse(linije[3]);
                    idVrstaVozila = int.Parse(linije[4]);
                    nazivVozila = puniVozila.dohvatiNazivVozila(idVrstaVozila);
                    nazivLokacije = lokacijaBuilder.dohvatiNazivLokacije(idLokacije);
                    int brojRaspolozivihMjesta = funkcijaAplikacije.pregledRaspolozivihMjesta(idLokacije, idVrstaVozila);
                    DateTime novoVrijeme = formatirajVrijeme(vrijeme);
                    if (novoVrijeme <= pocetnoVrijeme)
                    {
                        Console.WriteLine("Nije uneseno ispravno vrijeme! Vrijeme mora biti veće od trenutnog vremena sustava");

                    }
                    else
                    {
                        pocetnoVrijeme = novoVrijeme;
                        if (brojRaspolozivihMjesta == 0)
                        {
                            Console.WriteLine("nema raspolozivih mjesta vozila vrste:" + nazivVozila + " na lokaciji:" + nazivLokacije);
                        }
                        else
                        {
                            Console.WriteLine("Broj raspolozivih mjesta za vozilo vrste:" + nazivVozila + " na lokaciji:" + nazivLokacije + " iznosi: " + brojRaspolozivihMjesta);
                        }
                        Console.WriteLine("Trenutno vrijeme je: " + pocetnoVrijeme);
                    }
                }
                if (naredba.StartsWith("4") && naredba.Length < 38)
                {
                    string[] linije = parsirajNaredbu(naredba);
                    string vrijeme = linije[1].Substring(2, linije[1].Length - 3);
                    idKorisnik = int.Parse(linije[2]);
                    idLokacije = int.Parse(linije[3]);
                    idVrstaVozila = int.Parse(linije[4]);
                    int brojKm = int.Parse(linije[5]);
                    decimal cijenaNajmaVozila;
                    decimal cijenaPoSatu;
                    decimal cijenaNajmaKm;
                    decimal cijenaUkupno;
                    nazivKorisnika = osobaBuilder.dohvatiNazivOsobe(idKorisnik);
                    nazivVozila = puniVozila.dohvatiNazivVozila(idVrstaVozila);
                    nazivLokacije = lokacijaBuilder.dohvatiNazivLokacije(idLokacije);
                    DateTime novoVrijeme = formatirajVrijeme(vrijeme);
                    if (novoVrijeme <= pocetnoVrijeme)
                    {
                        Console.WriteLine("Nije uneseno ispravno vrijeme! Vrijeme mora biti veće od trenutnog vremena sustava");

                    }
                    else
                    {
                        pocetnoVrijeme = novoVrijeme;
                        if (funkcijaAplikacije.vracanjeVozila(idVrstaVozila, idLokacije, idKorisnik,brojKm))
                        {
                            cijenaNajmaVozila = funkcijaAplikacije.izracunCijeneNajma(idVrstaVozila);
                            cijenaNajmaKm = funkcijaAplikacije.izracunCijenePoKm(idVrstaVozila, brojKm);
                            cijenaPoSatu = funkcijaAplikacije.izracunCijenePoSatu(idVrstaVozila, novoVrijeme, idKorisnik);
                            cijenaUkupno = cijenaNajmaVozila + cijenaNajmaKm + cijenaPoSatu;
                            Console.WriteLine("U " + pocetnoVrijeme + " korisnik " + nazivKorisnika + " vraća na lokaciji" + nazivLokacije + " vozilo: " + nazivVozila);
                            Console.WriteLine();
                            Console.WriteLine("------Stavke računa:");
                            Console.WriteLine("------------" + " Najam vozila: " + cijenaNajmaVozila);
                            Console.WriteLine("------------" + " Cijena po kilometru: " + cijenaNajmaKm);
                            Console.WriteLine("------------" + " Cijena po satu: " + cijenaPoSatu);
                            Console.WriteLine("------------" + " Ukupno: " + cijenaUkupno);
                            Console.WriteLine();

                        }
                        
                        Console.WriteLine("Trenutno vrijeme je: " + pocetnoVrijeme);
                    }
                }

                if (naredba.StartsWith("4") && naredba.Length > 38)
                {
                    string[] linije = parsirajNaredbu(naredba);
                    string vrijeme = linije[1].Substring(2, linije[1].Length - 3);
                    idKorisnik = int.Parse(linije[2]);
                    idLokacije = int.Parse(linije[3]);
                    idVrstaVozila = int.Parse(linije[4]);
                    int brojKm = int.Parse(linije[5]);
                    string pomoc;
                    string razlog = "";
                    for (int i = 6; i < linije.Length; i++)
                    {
                        pomoc = linije[i];
                        razlog = razlog + pomoc;
                    }
                    decimal cijenaNajmaVozila;
                    decimal cijenaPoSatu;
                    decimal cijenaNajmaKm;
                    decimal cijenaUkupno;
                    nazivKorisnika = osobaBuilder.dohvatiNazivOsobe(idKorisnik);
                    nazivVozila = puniVozila.dohvatiNazivVozila(idVrstaVozila);
                    nazivLokacije = lokacijaBuilder.dohvatiNazivLokacije(idLokacije);
                    DateTime novoVrijeme = formatirajVrijeme(vrijeme);
                    if (novoVrijeme <= pocetnoVrijeme)
                    {
                        Console.WriteLine("Nije uneseno ispravno vrijeme! Vrijeme mora biti veće od trenutnog vremena sustava");

                    }
                    else
                    {
                        pocetnoVrijeme = novoVrijeme;
                        if (funkcijaAplikacije.vracanjeVozila(idVrstaVozila, idLokacije, idKorisnik,brojKm))
                        {
                            cijenaNajmaVozila = funkcijaAplikacije.izracunCijeneNajma(idVrstaVozila);
                            cijenaNajmaKm = funkcijaAplikacije.izracunCijenePoKm(idVrstaVozila, brojKm);
                            cijenaPoSatu = funkcijaAplikacije.izracunCijenePoSatu(idVrstaVozila, novoVrijeme, idKorisnik);
                            cijenaUkupno = cijenaNajmaVozila + cijenaNajmaKm + cijenaPoSatu;
                            Console.WriteLine("U " + pocetnoVrijeme + " korisnik " + nazivKorisnika + " vraća na lokaciji" + nazivLokacije + " vozilo: " + nazivVozila);
                            Console.WriteLine("Korisnik:" + nazivKorisnika + " pri vracanju vozila prijavljuje da je vozilo " + nazivVozila + " neispravno uz poruku: " + razlog);
                            Console.WriteLine();
                            Console.WriteLine("------Stavke računa:");
                            Console.WriteLine("------------" + " Najam vozila: " + cijenaNajmaVozila);
                            Console.WriteLine("------------" + " Cijena po kilometru: " + cijenaNajmaKm);
                            Console.WriteLine("------------" + " Cijena po satu: " + cijenaPoSatu);
                            Console.WriteLine("------------" + " Ukupno: " + cijenaUkupno);
                            Console.WriteLine();

                        }

                        Console.WriteLine("Trenutno vrijeme je: " + pocetnoVrijeme);
                    }
                }

                if (naredba.StartsWith("5"))
                {
                    string[] linije = parsirajNaredbu(naredba);
                    string putanjaAktivnosti = linije[1].Trim();
                    provjeriPostojanjeDatoteke(putanjaAktivnosti);
                    citajNaredbeSkupni(putanjaAktivnosti);
                    gotovoCitanje = false;                   
                }

                if (naredba.StartsWith("6"))
                {
                    int orgJed = 0;
                    string vrsta = "";
                    string druga = "";
                    string[] linije = parsirajNaredbuNovi(naredba);
                    if (linije.Length == 2)
                    {
                        funkcijaAplikacije.prikaziStrukturu();
                    }
                    if(linije.Length == 3) {
                        var isNumeric = int.TryParse(linije[2], out int n);
                        if (isNumeric.Equals(false))
                        {
                            vrsta = linije[2].Trim();
                            if (vrsta.Contains("stanje"))
                            {
                                funkcijaAplikacije.prikaziStrukturuStanje();
                            }
                            if (vrsta.Contains("struktura"))
                            {
                                funkcijaAplikacije.prikaziStanjeStruktura();
                            }
                        }
                        else
                        {
                            druga = linije[1].Trim();
                            if (druga.Contains("stanje"))
                            {
                                funkcijaAplikacije.prikaziStanjeJedinice(n);
                            }
                            if (druga.Contains("struktura"))
                            {
                                funkcijaAplikacije.prikaziStrukturuJedinice(n);
                            }
                        }
                    }
                    if(linije.Length == 4)
                    {
                        vrsta = linije[2].Trim();
                        orgJed = int.Parse(linije[3].Trim());
                    }
                    if (vrsta.Contains("stanje"))
                    {
                        funkcijaAplikacije.prikaziStrukturuStanjeVrste(orgJed);
                    }
                    if (vrsta.Contains("struktura"))
                    {
                        funkcijaAplikacije.prikaziStanjeStrukturaVrste(orgJed);
                    }

                    
                }

                if (naredba.StartsWith("0"))
                {
                    string[] linije = parsirajNaredbu(naredba);
                    string vrijeme = linije[1].Substring(2, linije[1].Length - 3);
                    DateTime novoVrijeme = formatirajVrijeme(vrijeme);
                    if (novoVrijeme <= pocetnoVrijeme)
                    {
                        Console.WriteLine("Nije uneseno ispravno vrijeme! Vrijeme mora biti veće od trenutnog vremena sustava");

                    }
                    else
                    {
                        pocetnoVrijeme = novoVrijeme;
                        outputWriter.IspisGreskeGasenjePrograma("U:" + pocetnoVrijeme + " program zavrsava s radom.");

                    }
                }
            }
            else
            {
                Console.WriteLine("Unesena neispravna naredba.");
            }

        }

        //Metoda koja se poziva ukoliko je broj argumenata 1
        private static void ucitavanjeKonfiguracije(string[] args)
        {
            konfiguracija = args[0];
        }

        private static void ucitavanjePutanjaKonf(List<string[]> listaNaredbi)
        {
            String[] privremena;
            for (int i = 0; i < listaNaredbi.Count; i++)
            {
                if ((listaNaredbi[i][0]).StartsWith("lokacije"))
                {
                    privremena = (listaNaredbi[i][0]).Split('=');
                    lokacije = privremena[1].Trim();
                }
                if ((listaNaredbi[i][0]).StartsWith("vozila"))
                {
                    privremena = (listaNaredbi[i][0]).Split('=');
                    vozila = privremena[1].Trim();
                }
                if ((listaNaredbi[i][0]).StartsWith("cjenik"))
                {
                    privremena = (listaNaredbi[i][0]).Split('=');
                    cjenik = privremena[1].Trim();
                }
                if ((listaNaredbi[i][0]).StartsWith("kapaciteti"))
                {
                    privremena = (listaNaredbi[i][0]).Split('=');
                    kapaciteti = privremena[1].Trim();
                }
                if ((listaNaredbi[i][0]).StartsWith("osobe"))
                {
                    privremena = (listaNaredbi[i][0]).Split('=');
                    osobe = privremena[1].Trim();
                }
                if ((listaNaredbi[i][0]).StartsWith("vrijeme"))
                {
                    privremena = (listaNaredbi[i][0]).Split('=');
                    vrijeme = privremena[1].Trim();
                }
                if ((listaNaredbi[i][0]).StartsWith("tekst"))
                {
                    privremena = (listaNaredbi[i][0]).Split('=');
                    tekst = int.Parse(privremena[1]);
                }
                if ((listaNaredbi[i][0]).StartsWith("cijeli"))
                {
                    privremena = (listaNaredbi[i][0]).Split('=');
                    cijeli = int.Parse(privremena[1]);
                }
                if ((listaNaredbi[i][0]).StartsWith("decimala"))
                {
                    privremena = (listaNaredbi[i][0]).Split('=');
                    decimala = int.Parse(privremena[1]);
                }
                if ((listaNaredbi[i][0]).StartsWith("struktura"))
                {
                    privremena = (listaNaredbi[i][0]).Split('=');
                    tvrtka = privremena[1].Trim();
                }
                if ((listaNaredbi[i][0]).StartsWith("aktivnosti"))
                {
                    privremena = (listaNaredbi[i][0]).Split('=');
                    skupni = privremena[1].Trim();
                }

            }
        }

        private static void ucitavanjePutanja(string[] args)
        {
            string[] pomoc = new string[20];
            string nesto;
            for (var i = 0; i < args.Length; i++)
            {

                if (args[i] == "-v")
                {
                    vozila = args[i + 1];
                }
                if (args[i] == "-l")
                {
                    lokacije = args[i + 1];
                }
                if (args[i] == "-c")
                {
                    cjenik = args[i + 1];
                }
                if (args[i] == "-k")
                {
                    kapaciteti = args[i + 1];
                }
                if (args[i] == "-o")
                {
                    osobe = args[i + 1];
                }
                if (args[i] == "-t")
                {
                    vrijeme = args[i + 1] + " " + args[i + 2].Substring(0,8);
                    nesto = args[i + 2].Substring(9,18);
                    pomoc[0] = nesto;
                }
                if (args[i] == "-s")
                {
                    skupni = args[i + 1];
                }
                if (args[i] == "-os" || pomoc.Length > 0)
                {
                    tvrtka = pomoc[0];
                    tvrtka = args[i + 1];
                }

            }
        }

        private static void provjeraPostojanjaArgumenata(string[] args)
        {
            if (!args.Any(x => x == "-v"))
            {
                outputWriter.IspisGreskeGasenjePrograma("Nedostaje argument '-v' , Nije moguće početi sa radom.");
            }
            else if (!args.Any(x => x == "-l"))
            {
                outputWriter.IspisGreskeGasenjePrograma("Nedostaje argument '-l' , Nije moguće početi sa radom.");
            }
            else if (!args.Any(x => x == "-c"))
            {
                outputWriter.IspisGreskeGasenjePrograma("Nedostaje argument '-c' , Nije moguće početi sa radom.");
            }
            else if (!args.Any(x => x == "-k"))
            {
                outputWriter.IspisGreskeGasenjePrograma("Nedostaje argument '-k' , Nije moguće početi sa radom.");
            }
            else if (!args.Any(x => x == "-o"))
            {
                outputWriter.IspisGreskeGasenjePrograma("Nedostaje argument '-o' , Nije moguće početi sa radom.");
            }
            else if (!args.Any(x => x == "-t"))
            {
                outputWriter.IspisGreskeGasenjePrograma("Nedostaje argument '-t' , Nije moguće početi sa radom.");
            }
            else if (!args.Any(x => x == "-l"))
            {
                outputWriter.IspisGreskeGasenjePrograma("Nedostaje argument '-l' , Nije moguće početi sa radom.");
            }
            

        }

        private static void provjeriPostojanjeDatoteke(string putanja)
        {
            if (string.IsNullOrEmpty(putanja) || !File.Exists(putanja))
            {
                outputWriter.IspisGreskeGasenjePrograma("Datoteka na putanji:" + Path.GetFullPath(putanja) + " ne postoji.");
            }
        }

    

        public static DateTime formatirajVrijeme(String vrijeme)
        {
            DateTime virtualnoVrijeme = DateTime.ParseExact(vrijeme, "yyyy-MM-dd HH:mm:ss", null);
            return virtualnoVrijeme;
        }

        public static string[] parsirajNaredbu(String naredba)
        {
            string[] redovi = naredba.Split(';');
            return redovi;
        }

        public static string[] parsirajNaredbuNovi(String naredba)
        {
            string[] redovi = naredba.Split(' ');
            return redovi;
        }

        //Metoda koja učitava naredbe za skupni način rada u listu
        public static void citajNaredbeSkupni(string putanjaSkupni)
        {

            citacDatoteka.CitajDatotekuLista(putanjaSkupni);
            skupneNaredbe = citacDatoteka.DohvatiListLinija();

        }

        public static string formatirajVrijemePrikaz(string vrijeme)
        {
            if (vrijeme != "")
            {
                if (vrijeme.Length == 20)
                {
                    formatiranoVrijeme = vrijeme.Substring(1, vrijeme.Length - 1);
                }
                else
                {
                    formatiranoVrijeme = vrijeme.Substring(1, vrijeme.Length - 2);
                }
            }
            return formatiranoVrijeme;
        }

       
    }
}


