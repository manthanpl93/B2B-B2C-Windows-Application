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
    public partial class EDIT_PRODUCT : Form
    {
        public int Product_Id;

        public EDIT_PRODUCT(int Product_ID,string Product=null,string category=null,string Unit=null,string Size=null,string Price=null,string QTY="0")
        {
            Product_Id = Product_ID;
          
            InitializeComponent();
            txtProductName.Text = Product;
            txtCategoryName.Text = category;
            TXT_UNIT.Text = Unit;
            TXT_SIZE.Text = Size;
            TXT_SELLING_PRICE.Text = Price;
            TXT_QTY.Text = QTY;
        }

        private void EDIT_PRODUCT_Load(object sender, EventArgs e)
        {

        }

        private void btn_Edit_Product_Click(object sender, EventArgs e)
        {

            SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_PRODUCT_DETAILS]", Royalicecream.Program.con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 3;
            da.SelectCommand.Parameters.Add("@PRODUCT_NAME", SqlDbType.VarChar).Value = txtProductName.Text;
            da.SelectCommand.Parameters.Add("@QTY", SqlDbType.Int).Value = Convert.ToInt32(TXT_QTY.Text);
            da.SelectCommand.Parameters.Add("@SELLING_PRICE", SqlDbType.Money).Value = Convert.ToDouble(TXT_SELLING_PRICE.Text);
            da.SelectCommand.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = TXT_UNIT.Text;
            da.SelectCommand.Parameters.Add("@SIZE", SqlDbType.VarChar).Value = TXT_SIZE.Text;
            da.SelectCommand.Parameters.Add("@NOTES", SqlDbType.VarChar).Value = TXT_NOTES.Text;
            da.SelectCommand.Parameters.Add("@PRODUCT_ID", SqlDbType.Int).Value = Product_Id;
            da.SelectCommand.Parameters.Add("@CATEGORY_NAME", SqlDbType.VarChar).Value = txtCategoryName.Text;



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
