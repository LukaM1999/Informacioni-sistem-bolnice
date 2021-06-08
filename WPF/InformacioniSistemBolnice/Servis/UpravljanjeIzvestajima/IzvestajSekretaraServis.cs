﻿using InformacioniSistemBolnice.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InformacioniSistemBolnice.Utilities;
using Model;
using Repozitorijum;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Tables;

namespace InformacioniSistemBolnice.Servis.UpravljanjeIzvestajima
{
    public class IzvestajSekretaraServis : IzvestajUtility
    {
        public override void KreirajDokument()
        {
            using (PdfDocument doc = new PdfDocument())
            {
                PdfPage page = doc.Pages.Add();

                PdfGrid pdfTable = new PdfGrid();

                DataTable tabela = new DataTable();
                tabela.Columns.Add("Lekar");
                tabela.Columns.Add("Pocetak zauzetosti");
                tabela.Columns.Add("Kraj zauzetosti");
                tabela.Rows.Add(new string[] { "Lekar", "Pocetak zauzetosti", "Kraj zauzetosti" });
                foreach (Lekar lekar in LekarRepo.Instance.Lekari)
                {
                    foreach (Termin termin in lekar.ZakazaniTermini)
                    {
                        tabela.Rows.Add(new string[] { lekar.Ime + " " + lekar.Prezime, termin.Vreme.ToString("g"),
                        termin.Vreme.AddMinutes(termin.Trajanje).ToString("g") });
                    }
                }

                pdfTable.DataSource = tabela;

                pdfTable.Draw(page, new PointF(0, 0));

                doc.Save("D:\\izvestaji\\zauzetostLekara.pdf");

                doc.Close(true);
            }
        }

        public override void PrikaziObavestenje()
        {
            MessageBox.Show("Uspesno kreiran izvestaj! Izvestaj se nalazi u D:\\izvestaji");
        }
    }
}