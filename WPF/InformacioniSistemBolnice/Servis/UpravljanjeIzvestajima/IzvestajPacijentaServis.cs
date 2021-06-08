using System;
using System.Data;
using System.Drawing;
using System.Windows;
using InformacioniSistemBolnice.Utilities;
using Model;
using Repozitorijum;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Tables;

namespace InformacioniSistemBolnice.Servis.UpravljanjeIzvestajima
{
    public class IzvestajPacijentaServis : IzvestajUtility
    {
        private static readonly Lazy<IzvestajPacijentaServis> Lazy = new(() => new IzvestajPacijentaServis());
        public static IzvestajPacijentaServis Instance => Lazy.Value;

        private PdfLightTable _pdfTable;
        private DataTable _tabela;
        private PdfDocument _doc;
        private PdfPage _page;

        public override void KreirajDokument()
        {
            InicijalizujDokument();
            DodajKolone();
            PostaviStilZaglavlja();
            for (int i = 0; i < 100; i++) PopuniTabelu();
            PopuniTabelu();
            _pdfTable.DataSource = _tabela;
            SacuvajDokument();
        }

        private void InicijalizujDokument()
        {
            _pdfTable = new();
            _tabela = new();
            _doc = new();
            _page = new();
            _page = _doc.Pages.Add();
        }

        public override void PrikaziObavestenje()
        {
            MessageBox.Show("Uspesno kreiran izvestaj o uzimanju lekova!" +
                            "\nIzvestaj se nalazi u D:\\izvestaji");
        }

        private void SacuvajDokument()
        {
            _pdfTable.Draw(_page, new PointF(0, 0), new PdfLayoutFormat
            { Layout = PdfLayoutType.Paginate, Break = PdfLayoutBreakType.FitPage });
            _doc.Save("D:\\izvestaji\\izvestajTerapija.pdf");
            _doc.Close(true);
        }

        private void PopuniTabelu()
        {
            foreach (Recept recept in GlavniProzorPacijentaView.ulogovanPacijent.zdravstveniKarton.recepti)
                DodajTerapijeUTabelu(recept);
        }

        private void DodajTerapijeUTabelu(Recept recept)
        {
            foreach (Terapija terapija in recept.terapije)
            {
                _tabela.Rows.Add(terapija.Lek, terapija.MeraLeka,
                    terapija.PocetakTerapije.ToString(DatumUtility.FormatDatumaKratak),
                    terapija.KrajTerapije.ToString(DatumUtility.FormatDatumaKratak), terapija.RedovnostTerapije);
            }
        }

        private void PostaviStilZaglavlja()
        {
            PdfCellStyle headerStyle = new PdfCellStyle(new PdfStandardFont(PdfFontFamily.TimesRoman, 13, PdfFontStyle.Bold),
                new PdfSolidBrush(new PdfColor(Color.Black)), new PdfPen(Color.Black));
            headerStyle.StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            _pdfTable.Style.HeaderStyle = headerStyle;
            _pdfTable.Style.HeaderStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(Color.LightGray));
            _pdfTable.Style.ShowHeader = true;
            _pdfTable.Style.RepeatHeader = true;
        }

        private void DodajKolone()
        {
            _tabela.Columns.Add("Naziv leka");
            _tabela.Columns.Add("Mera [mg]");
            _tabela.Columns.Add("Pocetak terapije");
            _tabela.Columns.Add("Kraj terapije");
            _tabela.Columns.Add("Redovnost [sati]");
        }
    }
}
