using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Royalicecream
{
    public partial class Customer_Invoice_Report : Form
    {
        public Customer_Invoice_Report()
        {
            InitializeComponent();
        }

        void showallsimpleinvoices()
        {
            SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_sells_product]", "");
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 8;
            DataSet ds = new DataSet();

            da.Fill(ds);

            string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

            if (status == "SUCCESS")
            {
                DATASIMPLEBILLVIEW.DataSource = ds.Tables[1];
                DATASIMPLEBILLVIEW.Columns[0].Width = 50;
                DATASIMPLEBILLVIEW.Columns[1].Width = 50;
                DATASIMPLEBILLVIEW.Columns[2].Width = 50;
                DATASIMPLEBILLVIEW.Columns[3].Width = 200;
                DATASIMPLEBILLVIEW.Columns[4].Width = 200;
                DATASIMPLEBILLVIEW.Columns[5].Width = 100;
                DATASIMPLEBILLVIEW.Columns[6].Width = 100;
                DATASIMPLEBILLVIEW.Columns[7].Width = 100;
                DATASIMPLEBILLVIEW.Columns[8].Width = 100;
                DATASIMPLEBILLVIEW.Columns[9].Width = 100;
                DATASIMPLEBILLVIEW.Columns[10].Width = 100;
            }
            else
            {
                MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        void showallestimation()
        {
            SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_sells_product]","");
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 9;
            DataSet ds = new DataSet();

            da.Fill(ds);

            string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

            if (status == "SUCCESS")
            {
                dataquatationeview.DataSource = ds.Tables[1];
                dataquatationeview.Columns[0].Width = 50;
                dataquatationeview.Columns[1].Width = 50;
                dataquatationeview.Columns[2].Width = 150;
                dataquatationeview.Columns[3].Width = 100;
                dataquatationeview.Columns[4].Width = 250;
                dataquatationeview.Columns[5].Width = 250;
                dataquatationeview.Columns[6].Width = 150;
                dataquatationeview.Columns[0].Width = 100;
            }
            else
            {
                MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void Customer_Invoice_Report_Load(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_sells_product]","");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 4;
                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    Dataview.DataSource = ds.Tables[1];
                    Dataview.Columns[0].Width = 60;
                    Dataview.Columns[1].Width = 50;
                    Dataview.Columns[2].Width = 50;
                    Dataview.Columns[3].Width = 200;
                    Dataview.Columns[4].Width = 200;
                    Dataview.Columns[5].Width = 100;
                    Dataview.Columns[6].Width = 50;
                    Dataview.Columns[7].Width = 50;
                    Dataview.Columns[8].Width = 100;
                    Dataview.Columns[9].Width = 50;
                    Dataview.Columns[10].Width = 150;
                    Dataview.Columns[11].Width = 100;
                    Dataview.Columns[12].Width = 100;
                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


                showallsimpleinvoices();
                showallestimation();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message +" Error Occured", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                string tab = tabControl1.SelectedIndex.ToString();


                //TAX INVOICE
                if (tab == "0")
                {
                    SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_sells_product]","");
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 5;
                    da.SelectCommand.Parameters.Add("@Search", SqlDbType.VarChar).Value = txtsearch.Text;
                    da.SelectCommand.Parameters.Add("@DateFrom", SqlDbType.Date).Value = Convert.ToDateTime(txtStartDate.Text);
                    da.SelectCommand.Parameters.Add("@DateTo", SqlDbType.Date).Value = Convert.ToDateTime(txtEndDate.Text);
                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                    if (status == "SUCCESS")
                    {
                        Dataview.DataSource = ds.Tables[1];
                    }
                    else
                    {
                        MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                //simple invoice
                else if (tab == "1")
                {
                    SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_sells_product]","");
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 10;
                    da.SelectCommand.Parameters.Add("@Search", SqlDbType.VarChar).Value = txtsearch.Text;
                    da.SelectCommand.Parameters.Add("@DateFrom", SqlDbType.Date).Value = Convert.ToDateTime(txtStartDate.Text);
                    da.SelectCommand.Parameters.Add("@DateTo", SqlDbType.Date).Value = Convert.ToDateTime(txtEndDate.Text);
                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                    if (status == "SUCCESS")
                    {
                        DATASIMPLEBILLVIEW.DataSource = ds.Tables[1];
                    }
                    else
                    {
                        MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                //quatation
                else if (tab == "2")
                {
                    SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_sells_product]","");
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 11;
                    da.SelectCommand.Parameters.Add("@Search", SqlDbType.VarChar).Value = txtsearch.Text;
                    da.SelectCommand.Parameters.Add("@DateFrom", SqlDbType.Date).Value = Convert.ToDateTime(txtStartDate.Text);
                    da.SelectCommand.Parameters.Add("@DateTo", SqlDbType.Date).Value = Convert.ToDateTime(txtEndDate.Text);
                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                    if (status == "SUCCESS")
                    {
                        dataquatationeview.DataSource = ds.Tables[1];
                    }
                    else
                    {
                        MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Error Occured", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Dataview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int id = Convert.ToInt32(Dataview.Rows[e.RowIndex].Cells[1].Value);

                

            }
        }

        private void DATASIMPLEBILLVIEW_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int id = Convert.ToInt32(DATASIMPLEBILLVIEW.Rows[e.RowIndex].Cells[1].Value);


            }
        }

        private void dataquatationeview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int id = Convert.ToInt32(DATASIMPLEBILLVIEW.Rows[e.RowIndex].Cells[1].Value);

               

            }
        }
    }
}
