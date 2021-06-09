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
    public class IzvestajUpravnikaServis : IzvestajUtility
    {
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
            tabela.Columns.Add("Id prostorije");
            tabela.Columns.Add("Pocetak zauzetosti");
            tabela.Columns.Add("Kraj zauzetosti");
            foreach (Prostorija prostorija in ProstorijaRepo.Instance.Prostorije)
            {
                foreach (Termin termin in prostorija.TerminiProstorije)
                {
                    tabela.Rows.Add(new string[] { prostorija.Id, termin.Vreme.ToString("g"),
                        termin.Vreme.AddMinutes(termin.Trajanje).ToString("g") });
                }
            }
            return tabela;
        }
    }
}
