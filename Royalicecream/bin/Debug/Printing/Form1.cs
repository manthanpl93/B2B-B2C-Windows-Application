using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace Royalicecream.Printing
{
    public partial class Form1 : Form
    {
        public DataSet ds;
        private IList<Stream> m_streams;
        public static int invoiceid;
        private int m_currentPageIndex;
        string Print_Type;

        public Form1(string PrintingType= "Preview")
        {
            InitializeComponent();
            Print_Type = PrintingType;
        }

        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name,
          string fileNameExtension, Encoding encoding,
          string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }


        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }


        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report)
        {
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                
                <PageWidth>29.7cm</PageWidth>
                <PageHeight>21cm</PageHeight>
                <MarginTop>0in</MarginTop>
                <MarginLeft>0in</MarginLeft>
                <MarginRight>0in</MarginRight>
                <MarginBottom>0in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();

            report.Render("Image", deviceInfo, CreateStream,
               out warnings);

            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private void Print_Report()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            printDoc.DefaultPageSettings.Landscape = true;
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintController = new StandardPrintController();
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();

            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {


            Customer_DetailsBindingSource.DataSource = ds.Tables[1];
            Bill_InformationBindingSource.DataSource = ds.Tables[2];

            ds.Tables[3].Columns.Add("Sr_No", typeof(string));

            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
            {
                ds.Tables[3].Rows[i]["Sr_No"] = i + 1;
            }


            Product_InfoBindingSource.DataSource = ds.Tables[3];


            if(Print_Type!="Direct Print")
            {
                var setup = reportViewer1.GetPageSettings();
                setup.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                reportViewer1.SetPageSettings(setup);
                this.reportViewer1.RefreshReport();

            }
            else
            {
                Product_InfoBindingSource.DataSource = ds.Tables[3];
                var setup = reportViewer1.GetPageSettings();
                setup.Margins = new System.Drawing.Printing.Margins(1, 1, 1, 1);
                reportViewer1.SetPageSettings(setup);
                this.reportViewer1.RefreshReport();


                LocalReport report = new LocalReport();
                report = reportViewer1.LocalReport;

                Export(report);



                Thread thread = new Thread(() => Print_Report());
                thread.Start();

                Customer_DetailsBindingSource.DataSource = null;
                Bill_InformationBindingSource.DataSource = null;
                Product_InfoBindingSource.DataSource = null;

            }




        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
