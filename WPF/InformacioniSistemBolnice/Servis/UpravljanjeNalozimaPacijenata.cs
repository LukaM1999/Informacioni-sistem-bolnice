using System;
using Model;
using Repozitorijum;
using System.Windows.Controls;
using InformacioniSistemBolnice;


namespace Servis
{
   public class UpravljanjeNalozimaPacijenata
   {
        private static readonly Lazy<UpravljanjeNalozimaPacijenata>
          lazy =
          new Lazy<UpravljanjeNalozimaPacijenata>
              (() => new UpravljanjeNalozimaPacijenata());

        public static UpravljanjeNalozimaPacijenata Instance { get { return lazy.Value; } }

        public void KreiranjeNaloga(RegistracijaPacijentaForma registracija)
      {
            DateTime datum = DateTime.Parse(registracija.datumUnos.Text);
            Pacijent p = new Pacijent(new Osoba(registracija.imeUnos.Text, registracija.prezimeUnos.Text, registracija.JMBGUnos.Text, datum, registracija.telUnos.Text, registracija.mailUnos.Text,
                new Korisnik(registracija.korisnikUnos.Text, registracija.lozinkaUnos.Password, (Model.UlogaKorisnika)Enum.Parse(typeof(Model.UlogaKorisnika), "pacijent"))));
            Pacijenti.Instance.listaPacijenata.Add(p);
            Pacijenti.Instance.Serijalizacija("../../../json/pacijenti.json");

        }
      
      public void UklanjanjeNaloga(ListView ListaPacijenata)
      {
            if (ListaPacijenata.SelectedValue != null)
            {
                Pacijent pacijent = (Pacijent)ListaPacijenata.SelectedValue;
                Pacijenti pacijenti = Pacijenti.Instance;
                foreach (Pacijent p in pacijenti.listaPacijenata)
                {
                    if (p.jmbg.Equals(pacijent.jmbg))
                    {
                        pacijenti.listaPacijenata.Remove(p);
                        Pacijenti.Instance.Serijalizacija("../../../json/pacijenti.json");
                        break;
                    }
                }
            }
        }
      
      public void IzmenaNaloga(IzmenaNalogaPacijentaForma izmena, ListView ListaPacijenata)
      {
            if (ListaPacijenata.SelectedValue != null)
            {
                Pacijent p = (Pacijent)ListaPacijenata.SelectedItem;
                p.ime = izmena.imeUnos.Text;
                p.prezime = izmena.prezimeUnos.Text;
                p.jmbg = izmena.JMBGUnos.Text;
                p.datumRodjenja = DateTime.Parse(izmena.datumUnos.Text);
                p. telefon = izmena.telUnos.Text;
                p.email = izmena.mailUnos.Text;
                p.korisnik.korisnickoIme = izmena.korisnikUnos.Text;
                p.korisnik.lozinka = izmena.lozinkaUnos.Password;
                Pacijenti.Instance.Serijalizacija("../../../json/pacijenti.json");
                Pacijenti.Instance.Deserijalizacija("../../../json/pacijenti.json");
                ListaPacijenata.ItemsSource = Pacijenti.Instance.listaPacijenata;
            }
        }

        public void PregledNaloga(ListView pacijenti)
        {
            if (pacijenti.SelectedIndex >= 0)
            {
                PregledNalogaPacijenta pregled = new PregledNalogaPacijenta(pacijenti);
                Pacijent p = (Pacijent)pacijenti.SelectedItem;
                pregled.imeLabela.Content = p.ime;
                pregled.prezimeLabela.Content = p.prezime;
                pregled.JMBGLabela.Content = p.jmbg;
                pregled.datumLabela.Content = p.datumRodjenja.ToString("MM/dd/yyyy");
                pregled.mailLabela.Content = p.email;
                pregled.telLabela.Content = p.telefon;
                pregled.korisnikLabela.Content = p.korisnik.korisnickoIme;
                pregled.lozinkaLabela.Content = p.korisnik.lozinka.ToString();
                pregled.Show();
            }
        }
   }
}