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
    public partial class Show_Invoices : Form
    {
        public Show_Invoices()
        {
            InitializeComponent();
        }
        //search buttion
        private void btnSve_Click(object sender, EventArgs e)
        {
            try
            {

                SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_INVOICE_DETAILS]", Royalicecream.Program.con);
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 3;
                da.SelectCommand.Parameters.Add("@SEARCH", SqlDbType.VarChar).Value = txtCustomerName.Text;
                da.SelectCommand.Parameters.Add("@DATE_FROM", SqlDbType.Date).Value = Convert.ToDateTime(txtStartDate.Text).ToShortDateString();
                da.SelectCommand.Parameters.Add("@DATE_TO", SqlDbType.Date).Value = Convert.ToDateTime(txtEndDate.Text).ToShortDateString();
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    DataView.DataSource = ds.Tables[1];
                    lbltotal.Text = ds.Tables[2].Rows[0][0].ToString();
                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured" + ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DataView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DialogResult dr = MessageBox.Show("Do you want to print again ?","Royal Icecream", MessageBoxButtons.YesNo);
                int INVOICENUMBER = Convert.ToInt32(DataView.Rows[e.RowIndex].Cells[2].Value);

                if (dr == DialogResult.Yes)
                {
                    SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_INVOICE_DETAILS]", Royalicecream.Program.con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 4;
                    da.SelectCommand.Parameters.Add("@BILL_NUMBER", SqlDbType.Int).Value = INVOICENUMBER;
                    


                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                    if (status == "SUCCESS")
                    {
                        //MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Printing.Print frm = new Printing.Print();
                        //frm.MdiParent = this.ParentForm;
                        //frm.ds = ds;
                        //frm.Show();

                        Printing.Form1 frm = new Printing.Form1();
                        frm.MdiParent = this.ParentForm;
                        frm.ds = ds;
                        frm.Show();
                        

                    }
                    else
                    {
                        MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
        }

       

        private void Show_Invoices_Load(object sender, EventArgs e)
        {
            try
            {

                SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_INVOICE_DETAILS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 2;

                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    DataView.DataSource = ds.Tables[1];
                    DataView.Columns[3].Width = 200;
                    DataView.Columns[1].Width = 50;
                    lbltotal.Text = ds.Tables[2].Rows[0]["TOTAL"].ToString();
                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


                da = new SqlDataAdapter("[dbo].[SP_CUSTOMER_DETAILS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 3;

                ds = new DataSet();

                da.Fill(ds);

                status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {

                    ds.Tables[1].Rows.Add("ALL");

                    txtCustomerName.DataSource = ds.Tables[1];
                    txtCustomerName.DisplayMember = "CUSTOMER";
                    txtCustomerName.Text = "ALL";
                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured" + ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BTN_DELETE_Click(object sender, EventArgs e)
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("Invoice_Id", typeof(int));

            for(int i=0;i< DataView.Rows.Count;i++)
            {
                if (Convert.ToBoolean(DataView.Rows[i].Cells[0].Value) == true)
                {
                    DT.Rows.Add(Convert.ToInt32(DataView.Rows[i].Cells[2].Value));
                }

            }


            SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_INVOICE_DETAILS]", Royalicecream.Program.con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 6;
            da.SelectCommand.Parameters.Add("@INVOICE_NUMBERS", SqlDbType.Structured).Value = DT;

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                for(int i=0;i< DataView.Rows.Count; i++)
                {
                    DataView.Rows[i].Cells[0].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < DataView.Rows.Count; i++)
                {
                    DataView.Rows[i].Cells[0].Value = false;
                }
            }
        }
    }
}
