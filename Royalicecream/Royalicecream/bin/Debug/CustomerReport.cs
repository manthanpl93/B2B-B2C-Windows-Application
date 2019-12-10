using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DGVPrinterHelper;
using System.Drawing.Printing;

namespace Royalicecream
{
    public partial class CustomerReport : Form
    {
        public CustomerReport()
        {
            InitializeComponent();
        }

        private void CustomerReport_Load(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_CUSTOMER_DETAILS]",Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 3;
                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    DataView.DataSource = ds.Tables[1];
                    DataView.Columns[1].Width = 250;
                    //DataView.Columns[1].Width = 200;
                    //DataView.Columns[2].Width = 150;
                    //DataView.Columns[3].Width = 200;
                    //DataView.Columns[4].Width = 100;
                    //DataView.Columns[5].Width = 100;
                    //DataView.Columns[6].Width = 300;

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_customers_details]","");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 5;
                da.SelectCommand.Parameters.Add("@Search", SqlDbType.VarChar).Value = txtSearch.Text;
                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    DataView.DataSource = ds.Tables[1];
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

        private void DataView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_CUSTOMER_DETAILS]", Royalicecream.Program.con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 4;
                    da.SelectCommand.Parameters.Add("@SEARCH", SqlDbType.VarChar).Value = txtSearch.Text;


                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                    if (status == "SUCCESS")
                    {
                        DataView.DataSource = ds.Tables[1];
                 //       DataView.Columns[0].Width = 250;
                        //DataView.Columns[1].Width = 200;
                        //DataView.Columns[2].Width = 150;
                        //DataView.Columns[3].Width = 200;
                        //DataView.Columns[4].Width = 100;
                        //DataView.Columns[5].Width = 100;
                        //DataView.Columns[6].Width = 300;

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
        }

        private void DataView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                string CustomerName = DataView.Rows[e.RowIndex].Cells[1].Value.ToString();

                DialogResult dr = MessageBox.Show("Do you want to delete "+ CustomerName + " ?","Warning",MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    
                    string Contact = DataView.Rows[e.RowIndex].Cells[5].Value.ToString();


                    SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_CUSTOMER_DETAILS]", Royalicecream.Program.con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 6;
                    da.SelectCommand.Parameters.Add("@CONTACT", SqlDbType.VarChar).Value = Contact;

                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                    if (status == "SUCCESS")
                    {
                        MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                }

                }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            int Columncount = DataView.ColumnCount;


            if (!(Columncount > 33))
            {
                DGVPrinter printer = new DGVPrinter();
                printer.Title = "Customer Report Of ROYAL SALES AGENCY GANDEVI";
              

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
                MessageBox.Show("Sorry, you need to set only 31 days records for printing", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

    }
}
