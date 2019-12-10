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
    public partial class AddNewProduct : Form
    {
        public AddNewProduct()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string Product = txtProductName.Text;
            string Category = txtCategoryName.Text;
            string Code = TXT_CODE.Text;
            string Size = TXT_SIZE.Text;
            string QTY = TXT_QTY.Text;
            string unit = TXT_UNIT.Text;

            double SellingPrice = Convert.ToDouble(TXT_SELLING_PRICE.Text);
            string Barcode = txt_barcode.Text;


            SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_PRODUCT_DETAILS]", Royalicecream.Program.con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@PRODUCT_NAME", SqlDbType.VarChar).Value = Product;
            da.SelectCommand.Parameters.Add("@QTY", SqlDbType.VarChar).Value = QTY;
            da.SelectCommand.Parameters.Add("@PRODUCT_CATEGORY", SqlDbType.VarChar).Value = Category;
            da.SelectCommand.Parameters.Add("@SELLING_PRICE", SqlDbType.VarChar).Value = SellingPrice;
            da.SelectCommand.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unit;
            da.SelectCommand.Parameters.Add("@SIZE", SqlDbType.VarChar).Value = Size;
            da.SelectCommand.Parameters.Add("@PRODUCT_CODE", SqlDbType.VarChar).Value = Code;
            da.SelectCommand.Parameters.Add("@BARCODE", SqlDbType.VarChar).Value = Barcode;
            da.SelectCommand.Parameters.Add("@NOTES", SqlDbType.VarChar).Value = TXT_NOTES.Text;
            da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 1;
            da.SelectCommand.Parameters.Add("@RequestResult", SqlDbType.SmallInt).Value = 1;
            DataSet ds = new DataSet();
           
            da.Fill(ds);

            string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

            if (status == "SUCCESS")
            {
                MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void txtCategoryName_Enter(object sender, EventArgs e)
        {
            SqlDataAdapter  da = new SqlDataAdapter("[dbo].[SP_PRODUCT_DETAILS]", Royalicecream.Program.con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 4;
            DataSet ds = new DataSet();
            da.Fill(ds);

           string status = ds.Tables[0].Rows[0]["STATUS"].ToString();



            if (status == "SUCCESS")
            {
               Stock_In_Product.fill_autocomplete(txtCategoryName, ds, 1, "CATEGORY");
            }
            else
            {
                MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
    }
}
