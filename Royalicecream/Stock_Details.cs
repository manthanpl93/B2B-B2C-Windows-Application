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
    public partial class Stock_Details : Form
    {
        public Stock_Details()
        {
            InitializeComponent();
        }

        private void Stock_Details_Load(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[PRODUCTS_STOCKS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 2;
                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    DataView.DataSource = ds.Tables[1];
            //        DataView.Columns[0].Width = 250;
                    //DataView.Columns[1].Width = 200;
                    //DataView.Columns[2].Width = 150;
                    //DataView.Columns[3].Width = 200;
                    DataView.Columns[4].Width = 150;
                    //DataView.Columns[5].Width = 100;
                    //DataView.Columns[6].Width = 300;

                    lbltotalsellings.Text = ds.Tables[2].Rows[0]["TOTAL AMOUNT"].ToString();
                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


                da = new SqlDataAdapter("[dbo].[SP_PRODUCT_DETAILS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 9;

                ds = new DataSet();

                da.Fill(ds);

                status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {

                    txtCategory.DataSource = ds.Tables[1];
                    txtCategory.DisplayMember = "CATEGORY";

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[PRODUCTS_STOCKS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 8;
                da.SelectCommand.Parameters.Add("@DateFrom", SqlDbType.Date).Value = Convert.ToDateTime(TXTSTARTDATE.Text).ToShortDateString();
                da.SelectCommand.Parameters.Add("@DateTo", SqlDbType.Date).Value = Convert.ToDateTime(TXTENDDATE.Text).ToShortDateString();
                da.SelectCommand.Parameters.Add("@Search", SqlDbType.VarChar).Value = txt_product.Text;
                da.SelectCommand.Parameters.Add("@CATEGOEY_SEARCH", SqlDbType.VarChar).Value = txtCategory.Text;

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

        private void txt_product_Enter(object sender, EventArgs e)
        {
            try
            {

                SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_PRODUCT_DETAILS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 7;
                da.SelectCommand.Parameters.Add("@PRODUCT_CATEGORY", SqlDbType.VarChar).Value = txtCategory.Text;

                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    txt_product.DataSource = ds.Tables[1];
                    txt_product.DisplayMember = "PRODUCT NAME";

                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {

            }
        }

        private void DataView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string Product = DataView.Rows[e.RowIndex].Cells[5].Value.ToString();
            string Category = DataView.Rows[e.RowIndex].Cells[4].Value.ToString();
            int Id = Convert.ToInt32(DataView.Rows[e.RowIndex].Cells[3].Value);
            int stocks = Convert.ToInt32(DataView.Rows[e.RowIndex].Cells[6].Value);
            if (e.ColumnIndex == 2)
            {
                Edit_Stocks FRM = new Edit_Stocks(Product, Category, stocks.ToString(), Id.ToString());
                
               
                FRM.Show();
            }


            if (e.ColumnIndex == 1)
            {
                
             DialogResult dr;
                dr = MessageBox.Show("Are you sure, you delete the stock of " + Product + "?", "Delete Warning", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    SqlDataAdapter da = new SqlDataAdapter("[dbo].[PRODUCTS_STOCKS]", Royalicecream.Program.con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 9;
                    da.SelectCommand.Parameters.Add("@CATEGOEY_SEARCH", SqlDbType.VarChar).Value = Category;
                    da.SelectCommand.Parameters.Add("@Search", SqlDbType.VarChar).Value = Product;
                    da.SelectCommand.Parameters.Add("@SUPPLY_ID", SqlDbType.Int).Value = Id;

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
                else
                {
                    MessageBox.Show("Operation cancelled");
                }
            }
        }

        private void BTN_DELETE_Click(object sender, EventArgs e)
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("Invoice_Id", typeof(int));

            for (int i = 0; i < DataView.Rows.Count; i++)
            {
                if (Convert.ToBoolean(DataView.Rows[i].Cells[0].Value) == true)
                {
                    DT.Rows.Add(Convert.ToInt32(DataView.Rows[i].Cells[3].Value));
                }

            }

            SqlDataAdapter da = new SqlDataAdapter("[dbo].[PRODUCTS_STOCKS]", Royalicecream.Program.con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 11;
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
                for (int i = 0; i < DataView.Rows.Count; i++)
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
