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
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
        }

        
        private void Search_Load(object sender, EventArgs e)
        {
            try
            {

                SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_PRODUCT_DETAILS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 4;

                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    dataviewproducts.DataSource = ds.Tables[1];
                    dataviewproducts.Columns[3].Width = 200;
                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                da = new SqlDataAdapter("[dbo].[PRODUCTS_STOCKS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 4;

                ds = new DataSet();

                da.Fill(ds);

                status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    DataviewStockIn.DataSource = ds.Tables[1];

                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


                da = new SqlDataAdapter("[dbo].[PRODUCTS_STOCKS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 5;

                ds = new DataSet();

                da.Fill(ds);

                status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    Dataviewstockout.DataSource = ds.Tables[1];
                    Dataviewstockout.Columns[3].Width = 200;
                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


                da = new SqlDataAdapter("[dbo].[SP_PRODUCT_DETAILS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 4;

                ds = new DataSet();

                da.Fill(ds);

                status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    txtsearchStockin.DataSource = ds.Tables[1];
                    txtsearchStockin.DisplayMember = "PRODUCT NAME";

                    txtStockOut.DataSource = ds.Tables[1];
                    txtStockOut.DisplayMember = "PRODUCT NAME";

                    txtSearch.DataSource = ds.Tables[1];
                    txtSearch.DisplayMember = "PRODUCT NAME";


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

        private void dataviewproducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DialogResult rs = MessageBox.Show("Do you want to delete this product ?","Warning", MessageBoxButtons.YesNo);

                if (rs == DialogResult.Yes)
                {
            
                }
                else
                {
                    MessageBox.Show("Operation cancelled", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }
            else if (e.ColumnIndex == 1)
            {

                string productname = dataviewproducts.Rows[e.RowIndex].Cells[3].Value.ToString();
                string Category = dataviewproducts.Rows[e.RowIndex].Cells[2].Value.ToString();
                int ProductId;
                string Unit=dataviewproducts.Rows[e.RowIndex].Cells[4].Value.ToString();
                string Size=dataviewproducts.Rows[e.RowIndex].Cells[5].Value.ToString();
                string Price=dataviewproducts.Rows[e.RowIndex].Cells[8].Value.ToString();
                string QTY= dataviewproducts.Rows[e.RowIndex].Cells[7].Value.ToString();
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_PRODUCT_DETAILS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 10;
                da.SelectCommand.Parameters.Add("@PRODUCT_NAME", SqlDbType.VarChar).Value = productname;
                da.SelectCommand.Parameters.Add("@PRODUCT_CATEGORY", SqlDbType.VarChar).Value = Category;


                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    ProductId = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductId"].ToString());
                    EDIT_PRODUCT frm = new EDIT_PRODUCT(ProductId, productname, Category,Unit,Size,Price,QTY);
                    frm.MdiParent = this.ParentForm;
                    frm.Show();
                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                //SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_kit_details]", "");
                //da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 3;
                //da.SelectCommand.Parameters.Add("@Name", SqlDbType.VarChar).Value = txt_search_kit.Text;
                //DataSet ds = new DataSet();

                //da.Fill(ds);

                //string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                //if (status == "SUCCESS")
                //{
                //    kitview.DataSource = ds.Tables[1];
                //}
                //else
                //{
                //    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Error Occured", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_product_details2]", "");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 15;
                da.SelectCommand.Parameters.Add("@Search", SqlDbType.VarChar).Value = txtSearch.Text;
                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    dataviewproducts.DataSource = ds.Tables[1];
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

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender,e);
            }
        }

        private void btnSearchStockin_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[PRODUCTS_STOCKS]",Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 6;
                da.SelectCommand.Parameters.Add("@Search", SqlDbType.VarChar).Value = txtsearchStockin.Text;
                da.SelectCommand.Parameters.Add("@DateFrom", SqlDbType.Date).Value = Convert.ToDateTime(txtStartDate.Text).ToShortDateString();
                da.SelectCommand.Parameters.Add("@DateTo", SqlDbType.Date).Value = Convert.ToDateTime(txtEndDate.Text).ToShortDateString();

                DataSet ds = new DataSet();
                string status;
                ds = new DataSet();

                da.Fill(ds);

                status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    DataviewStockIn.DataSource = ds.Tables[1];


                    lbl_Stocksin_Product_name.Text = ds.Tables[2].Rows[0]["PRODUCT"].ToString();
                    lbl_StocksIn_Category.Text = ds.Tables[2].Rows[0]["CATEGORY"].ToString();
                    lbl_StocksIn_Date_From.Text = Convert.ToDateTime(ds.Tables[2].Rows[0]["DATE FROM"]).ToShortDateString().ToString();
                    lbl_StocksIn_Date_To.Text =Convert.ToDateTime(ds.Tables[2].Rows[0]["DATE TO"]).ToShortDateString().ToString();
                    lbl_StocksIn_Total_Stocks_Come.Text = ds.Tables[2].Rows[0]["STOCK COME"].ToString();
                    

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

        private void BtnSearchStockout_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[PRODUCTS_STOCKS]", Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 7;
                da.SelectCommand.Parameters.Add("@Search", SqlDbType.VarChar).Value = txtStockOut.Text;
                da.SelectCommand.Parameters.Add("@DateFrom", SqlDbType.Date).Value = Convert.ToDateTime(txtdatestockoutfrom.Text).ToShortDateString();
                da.SelectCommand.Parameters.Add("@DateTo", SqlDbType.Date).Value = Convert.ToDateTime(txtdatestockoutTo.Text).ToShortDateString();

                DataSet ds = new DataSet();
                string status;
                ds = new DataSet();

                da.Fill(ds);

                status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    Dataviewstockout.DataSource = ds.Tables[1];


                    lbl_Stockout_Product_Name.Text = ds.Tables[2].Rows[0]["PRODUCT"].ToString();
                    lbl_stock_out_catgeory.Text = ds.Tables[2].Rows[0]["CATEGORY"].ToString();
                    lbl_Stockout_Datefrom.Text =Convert.ToDateTime(ds.Tables[2].Rows[0]["DATE FROM"]).ToShortDateString ().ToString();
                    lbl_Stocksout_Date_To.Text =Convert.ToDateTime(ds.Tables[2].Rows[0]["DATE TO"]).ToShortDateString().ToString();
                    lbl_Stocks_Out_Stocks_Sells.Text = ds.Tables[2].Rows[0]["STOCK OUT"].ToString();
                    lbl_Stocksout_Selling_price.Text = ds.Tables[2].Rows[0]["TOTAL SELLS"].ToString();


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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (txtSearch.Text != "")
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_PRODUCT_DETAILS]", Program.con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 5;
                    da.SelectCommand.Parameters.Add("@PRODUCT_NAME", SqlDbType.VarChar).Value = txtSearch.Text;

                    DataSet ds = new DataSet();
                    string status;
                    ds = new DataSet();

                    da.Fill(ds);

                    status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                    if (status == "SUCCESS")
                    {
                        dataviewproducts.DataSource = ds.Tables[1];
                    }
                    else
                    {
                        //MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " Error Occured", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
    }
}
