using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGVPrinterHelper;

namespace Royalicecream
{
    public partial class SalesReport : Form
    {
        public SalesReport()
        {
            InitializeComponent();
        }

        private void PrintDoc()
        {
            PrintDialog printDialog = new PrintDialog(); //make a printDialog object

            PrintDocument printDocument = new PrintDocument(); // make a print doc object

            printDialog.Document = printDocument; //document for printing is printDocument

            printDocument.PrintPage += printDocument_PrintPage; //event handler fire



            DialogResult result = printDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument.Print();
            }

        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs ev)
        {
            
            Graphics g = ev.Graphics;
            foreach (DataRow row in DataView.Rows)
            {
              
                string text = row.ToString(); //or whatever you want from the current row
                g.DrawString(text, new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, 20, 225);
            }


        }




        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataView.Rows.Count > 0)
                {
                    DataView.DataSource=null;
                    DataView.Columns.Clear();
                }
              

                SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_INVOICE_DETAILS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 5;
                da.SelectCommand.Parameters.Add("@DATE_FROM", SqlDbType.Date).Value = Convert.ToDateTime(txtStartDate.Text).ToShortDateString();
                da.SelectCommand.Parameters.Add("@DATE_To", SqlDbType.Date).Value = Convert.ToDateTime(txtEndDate.Text).ToShortDateString();
                
                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    DataView.DataSource = ds.Tables[1];
                    DataView.Columns[0].HeaderText = "Customer";
                    DataView.Columns[0].Width = 110;
                    DataView.Columns[1].Width = 27;
                    DataView.Columns[0].Frozen = true;
                    DataView.Columns[1].Frozen = true;
                    DataView.Columns.Add("Total","T");
                    DataView.Columns[DataView.Columns.Count - 1].Width = 27;
                    for (int i = 0; i < DataView.Rows.Count; i++)
                    {
                        int Total = 0;
                        


                        for (int j=0;j< DataView.Columns.Count; j++)
                        {

                            if(j!=0 && j != 1 && j!= DataView.ColumnCount-1)
                            {
                                string Header = DataView.Columns[j].HeaderText;
                                DataView.Columns[j].HeaderText = DataView.Columns[j].HeaderText.Substring(Header.Length - 2, 2);

                                if (i!= DataView.Rows.Count -1)
                                {
                                    DataView.Columns[j].Width = 26;

                                }
                                else{
                                    DataView.Columns[j].Width = 30;

                                }
                            }

                            int Current = 0;
                            if (j > 1)
                            {
                                Current = Convert.ToInt32(DataView.Rows[i].Cells[j].Value);
                            }
                            
                            if (j > 1 && Current!=0)
                            {
                                Total = Current + Total;
                            }
                            else if (j > 1 && Current == 0)
                            {
                                DataView.Rows[i].Cells[j].Value = DBNull.Value;
                            }
                        }

                        DataView.Rows[i].Cells[(DataView.Columns.Count - 1)].Value = Total;


                        

                    }


                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Error Occured", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SalesReport_Load(object sender, EventArgs e)
        {

        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            int Columncount = DataView.ColumnCount;


            if (!(Columncount > 33))
            {
                DGVPrinter printer = new DGVPrinter();
                printer.Title = "Sales Report Of ROYAL SALES AGENCY GANDEVI";
                printer.SubTitle = "From " + txtStartDate.Text + " To " + txtEndDate.Text;


                printer.SubTitleFormatFlags = StringFormatFlags.LineLimit |

                                              StringFormatFlags.NoClip;

                printer.PageNumbers = true;
                printer.PageNumberInHeader = false;
                printer.PorportionalColumns = true;
                printer.HeaderCellAlignment = StringAlignment.Near;
                printer.Footer = "CONTACT NO : 9879174559 / 9033337877";
                printer.FooterSpacing = 25;
                printer.printDocument.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                printer.printDocument.DefaultPageSettings.Landscape = true;
                printer.PrintDataGridView(DataView);


            }
            else
            {
                MessageBox.Show("Sorry, you need to set only 31 days records for printing","Warning!!",MessageBoxButtons.OK,MessageBoxIcon.Warning); 
            }


        }

    }
}
