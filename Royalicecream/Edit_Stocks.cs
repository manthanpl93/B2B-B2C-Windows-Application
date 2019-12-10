using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Royalicecream
{
    public partial class Edit_Stocks : Form
    {
        public Edit_Stocks(string Product,string Category,string Stocks,string Id)
        {

            InitializeComponent();

            lbl_Product_Name.Text = Product;
            lbl_Category.Text = Category;
            lbl_Id.Text = Id;
            lbl_Stocks.Text = Stocks;
            lbl_Stocks_After_Update.Text = Stocks;


        }

        private void Edit_Stocks_Load(object sender, EventArgs e)
        {

        }

        private void txtAcName_TextChanged(object sender, EventArgs e)
        {
            if (txtAcName.Text != "")
            {
                int stocks = Convert.ToInt32(lbl_Stocks.Text);
                int qty = Convert.ToInt32(txtAcName.Text);
                int StocksAfterUpdate=0;

                if (rbDecreased.Checked == true)
                {
                    StocksAfterUpdate = stocks - qty;
                }
                else
                {
                    StocksAfterUpdate = stocks + qty;
                }

                lbl_Stocks_After_Update.Text = StocksAfterUpdate.ToString();
            }
            else
            {
                lbl_Stocks_After_Update.Text = lbl_Stocks.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                bool incress = false;
                if (rbIncreased.Enabled == true)
                {
                    incress = true;
                }


                SqlDataAdapter da = new SqlDataAdapter("[dbo].[PRODUCTS_STOCKS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 10;
                da.SelectCommand.Parameters.Add("@Search", SqlDbType.VarChar).Value = lbl_Product_Name.Text;
                da.SelectCommand.Parameters.Add("@SUPPLIER_ID", SqlDbType.Int).Value = Convert.ToInt32(lbl_Id.Text);
                da.SelectCommand.Parameters.Add("@CATEGOEY_SEARCH", SqlDbType.VarChar).Value = lbl_Category.Text;
                da.SelectCommand.Parameters.Add("@Incress", SqlDbType.Bit).Value = incress;
                da.SelectCommand.Parameters.Add("@EDIT_STOCKS", SqlDbType.Int).Value = Convert.ToInt32(txtAcName.Text);


                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Operation Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
}
