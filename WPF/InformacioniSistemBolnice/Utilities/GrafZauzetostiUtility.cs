using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repozitorijum;
using Model;
using InformacioniSistemBolnice.DTO;
using System.Collections.ObjectModel;

namespace InformacioniSistemBolnice.Utilities
{
    public class GrafZauzetostiUtility
    {
        public static ObservableCollection<GrafZauzetostiDto> PrebrojTermine(DateTime pocetakIntervala, DateTime krajIntervala)
        {
            int intervalZauzetosti = (krajIntervala - pocetakIntervala).Days;
            ObservableCollection<GrafZauzetostiDto> paroviVremeBrojTermina = new();
            for (int i = 0; i < intervalZauzetosti; i++)
            {
                int termina = 0;
                foreach (Lekar lekar in LekarRepo.Instance.Lekari)
                    foreach (Termin termin in lekar.ZakazaniTermini)
                    {
                        string d = termin.Vreme.ToShortDateString();
                        string p = pocetakIntervala.ToShortDateString();
                        if (termin.Vreme.ToShortDateString()==(pocetakIntervala.ToShortDateString())) termina++;
                    }
                paroviVremeBrojTermina.Add(new GrafZauzetostiDto(pocetakIntervala.ToString(DatumUtility.FormatDatumaKratak), termina));
                pocetakIntervala = pocetakIntervala.AddDays(1);
            }
            return paroviVremeBrojTermina;
        }
    }
}
