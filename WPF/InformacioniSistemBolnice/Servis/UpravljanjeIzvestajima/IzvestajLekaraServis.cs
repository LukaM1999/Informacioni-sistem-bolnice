using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InformacioniSistemBolnice.Utilities;
using InformacioniSistemBolnice.ViewModels.LekarViewModel;
using InformacioniSistemBolnice.ViewModels.PacijentViewModel;
using InformacioniSistemBolnice.Views.LekarView;
using Model;
using Repozitorijum;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;

namespace InformacioniSistemBolnice.Servis.UpravljanjeIzvestajima
{
    class IzvestajLekaraServis : IzvestajUtility
    {
        private static readonly Lazy<IzvestajLekaraServis> Lazy = new(() => new IzvestajLekaraServis());
        public static IzvestajLekaraServis Instance => Lazy.Value;
        public override void KreirajDokument()
        {
            using (PdfDocument doc = new PdfDocument())
            {
                PdfPage page = doc.Pages.Add();
                PdfGrid pdfTabela = new PdfGrid();
                pdfTabela.DataSource = NapuniTabelu(new DataTable()); ;
                pdfTabela.Draw(page, new PointF(0, 0));
                doc.Save("D:\\izvestaji\\zauzetostProstorije.pdf");
                doc.Close(true);
            }
        }

        public override void PrikaziObavestenje()
        {
            MessageBox.Show("Uspesno kreiran izvestaj! Izvestaj se nalazi u D:\\izvestaji");
        }

        private DataTable NapuniTabelu(DataTable tabela)
        {
            tabela.Columns.Add("Naziv");
            tabela.Columns.Add("Sadrzaj");
            tabela.Rows.Add(new string[] { "Sadasnja bolest", LekarAnamnezaViewModel.Anamneza.SadasnjaBolest});
            tabela.Rows.Add(new string[] { "Prethodne bolesti", LekarAnamnezaViewModel.Anamneza.RanijeBolesti});
            tabela.Rows.Add(new string[] { "Porodicne bolesti", LekarAnamnezaViewModel.Anamneza.PorodicneAnamneze});
            tabela.Rows.Add(new string[] { "Zakljucak", LekarAnamnezaViewModel.Anamneza.Zakljucak});

            return tabela;
        }
    }
}
