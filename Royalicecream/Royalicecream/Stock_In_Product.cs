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
    public partial class Stock_In_Product : Form
    {
        DataTable Stocks_Products = new DataTable();

        public Stock_In_Product()
        {
            InitializeComponent();
        }

        public string Calculate_TotalPrice()
        {
            double total=0;
            for(int i=0;i<Dataview.Rows.Count;i++)
            {
                total = total + (Convert.ToDouble(Dataview.Rows[i].Cells[3].Value.ToString())* Convert.ToDouble(Dataview.Rows[i].Cells[4].Value.ToString()));
            }

            return total.ToString();
        }


        void MAIN()
        {
            

        SqlDataAdapter           da = new SqlDataAdapter("[dbo].[SP_PRODUCT_DETAILS]", Royalicecream.Program.con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 4;

       DataSet ds = new DataSet();

            da.Fill(ds);

     String     status = ds.Tables[0].Rows[0]["STATUS"].ToString();

            if (status == "SUCCESS")
            {
                fill_autocomplete(txt_category_name, ds, 1, "CATEGORY");
            }
            else
            {
                MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }




        }


        public static void fill_autocomplete(TextBox txtbox, DataSet dataset, int index,string column)
        {
            try
            {
                AutoCompleteStringCollection allowedStatorTypes = new AutoCompleteStringCollection();

                for (int i = 0; i < dataset.Tables[index].Rows.Count; i++)
                {
                    allowedStatorTypes.Add(dataset.Tables[index].Rows[i][column].ToString());
                }


                txtbox.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtbox.AutoCompleteCustomSource = allowedStatorTypes;

            }
            catch
            {

            }



        }



        private void add_new_product_Load(object sender, EventArgs e)
        {
            //ADD COLUMNS IN THE STOCK_PRODUCTS FOR STORE PROCEDURE
            Stocks_Products.Columns.Add("PRODUCT_ID", typeof(int));
            Stocks_Products.Columns.Add("CATEGORY", typeof(string));
            Stocks_Products.Columns.Add("PRODUCT_NAME",typeof(string));
            Stocks_Products.Columns.Add("STOCKS", typeof(int));
            Stocks_Products.Columns.Add("PURCHASE_PRICE", typeof(double));
            Stocks_Products.Columns.Add("SELLING_PRICE", typeof(double));
            Stocks_Products.Columns.Add("NOTES", typeof(string));


            MAIN();            



        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_provider_Enter(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_SUPPLIER_DETAILS]", Royalicecream.Program.con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 3;
            DataSet ds = new DataSet();

            da.Fill(ds);

            string status = ds.Tables[0].Rows[0]["STATUS"].ToString();
         
            if (status == "SUCCESS")
            {
                txt_provider.DataSource = ds.Tables[1];
                txt_provider.DisplayMember = "SUPPLIER NAME";
                txt_provider.ValueMember = "Id";
            }
            else
            {
                MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txt_provider_Leave(object sender, EventArgs e)
        {
            if (txt_provider.Text != "")
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_SUPPLIER_DETAILS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 4;
                da.SelectCommand.Parameters.Add("@PROVIDER_ID", SqlDbType.Int).Value = Convert.ToInt32(txt_provider.SelectedValue);
                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    txtcontact.Text= ds.Tables[1].Rows[0]["CONTACT NUMBER"].ToString();
                    txtcity.Text= ds.Tables[1].Rows[0]["CITY"].ToString();

                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
           
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if(txt_provider.Text!="" || TXT_PRODUCT_NAME.Text != ""  || TXT_QTY.Text != "" ||
                TXT_PURCHASE.Text != "" || TXT_SELLING_PRICE.Text != "" || TXT_NOTES.Text != "" ) 
            {
               
                MAIN();

                string PRODUCT_NAME;
                string CATEGORY;
                int STOCKS;
                double PURCHASE_PRICE;
                double SELLING_PRICE;
            
                string NOTES;

                string PayBy;
                string ChequeNo = txt_cheque_no.Text;
                string TotalAmount = txtTotalAmount.Text;
                string BillNotes = txtNotes.Text;


                if (rb_cash.Checked == true)
                {
                    PayBy = "Cash";
                }
                else
                {
                    PayBy = "Cheque";
                }

                for (int i = 0; i < Dataview.Rows.Count; i++)
                {
                    int iProductId = 0;
                    CATEGORY = Dataview.Rows[i].Cells[1].Value.ToString();
                    PRODUCT_NAME = Dataview.Rows[i].Cells[2].Value.ToString();
                    STOCKS = Convert.ToInt32(Dataview.Rows[i].Cells[3].Value.ToString());
                    PURCHASE_PRICE = Convert.ToDouble(Dataview.Rows[i].Cells[4].Value.ToString());
                    SELLING_PRICE = Convert.ToDouble(Dataview.Rows[i].Cells[5].Value.ToString());
                    NOTES = Dataview.Rows[i].Cells[6].Value.ToString();

                    Stocks_Products.Rows.Add(iProductId,CATEGORY, PRODUCT_NAME, STOCKS, PURCHASE_PRICE, SELLING_PRICE,NOTES);

                }

                SqlDataAdapter da = new SqlDataAdapter("[dbo].[PRODUCTS_STOCKS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 1;
                da.SelectCommand.Parameters.Add("@PRODUCTS_STOCKS", SqlDbType.Structured).Value = Stocks_Products;
                da.SelectCommand.Parameters.Add("@PAY_BY", SqlDbType.VarChar).Value = PayBy;
                da.SelectCommand.Parameters.Add("@CHEQUE_NUMBER", SqlDbType.VarChar).Value = ChequeNo;
                da.SelectCommand.Parameters.Add("@TOTAL_AMOUNT", SqlDbType.Money).Value = TotalAmount;
         
                da.SelectCommand.Parameters.Add("@NOTES", SqlDbType.VarChar).Value = BillNotes;
                da.SelectCommand.Parameters.Add("@SUPPLY_ID", SqlDbType.Int).Value = Convert.ToInt32(txt_provider.SelectedValue.ToString());
                da.SelectCommand.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(TXTSTARTDATE.Text);

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
                MessageBox.Show("Must enter all values", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

  

        private void TXT_PRODUCT_NAME_Enter(object sender, EventArgs e)
        {
            
        }

        private void txt_category_name_Enter(object sender, EventArgs e)
        {
           
        }

        private void TXT_PRODUCT_NAME_Leave(object sender, EventArgs e)
        {
            if (TXT_PRODUCT_NAME.Text != "")
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_PRODUCT_DETAILS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 5;
                da.SelectCommand.Parameters.Add("@PRODUCT_NAME", SqlDbType.VarChar).Value = TXT_PRODUCT_NAME.Text;

                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {

                    txt_category_name.Text = ds.Tables[1].Rows[0]["CATEGORY"].ToString();
                    
                  
                    TXT_QTY.Text= ds.Tables[1].Rows[0]["AVAILABLE STOCK"].ToString();
               
                  
                    TXT_SELLING_PRICE.Text= ds.Tables[1].Rows[0]["SELLING PRICE"].ToString();
                  
                    TXT_NOTES.Text= ds.Tables[1].Rows[0]["NOTES"].ToString();


                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txt_category_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void Mainpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Dataview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                DialogResult dr = MessageBox.Show("Are you sure you delete " + Dataview.Rows[e.RowIndex].Cells[2].Value + " from the list ?", "Delete Item", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    Dataview.Rows.RemoveAt(e.RowIndex);

                    txtTotalAmount.Text = Calculate_TotalPrice();

                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (txt_provider.Text != "" || TXT_PRODUCT_NAME.Text != "" || TXT_QTY.Text != ""
                || TXT_PURCHASE.Text != "" || TXT_SELLING_PRICE.Text != "" || TXT_NOTES.Text != "")
            {

                string PRODUCT_NAME = TXT_PRODUCT_NAME.Text;
                string CATEGORY = txt_category_name.Text;
                string QTY = TXT_QTY.Text;
                string PURCHASE_PRICE = TXT_PURCHASE.Text;
                string SELLING_PRICE = TXT_SELLING_PRICE.Text;
                string importantNotes = txtNotes.Text;


                Dataview.Rows.Add((Dataview.Rows.Count + 1), CATEGORY, PRODUCT_NAME, QTY, PURCHASE_PRICE, SELLING_PRICE, importantNotes);

                txtTotalAmount.Text = Calculate_TotalPrice();

                TXT_QTY.Text = "";
                TXT_PURCHASE.Text = "";
                TXT_SELLING_PRICE.Text = "";
            }
            else
            {
                MessageBox.Show("Must enter all values", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void txt_category_name_Enter_1(object sender, EventArgs e)
        {

        }

        private void TXT_PRODUCT_NAME_Enter_1(object sender, EventArgs e)
        {

            try
            {

                SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_PRODUCT_DETAILS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 7;
                da.SelectCommand.Parameters.Add("@PRODUCT_CATEGORY", SqlDbType.VarChar).Value = txt_category_name.Text;

                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                  
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

        private void TXT_PRODUCT_NAME_Enter_2(object sender, EventArgs e)
        {
            try
            {

                SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_PRODUCT_DETAILS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 7;
                da.SelectCommand.Parameters.Add("@PRODUCT_CATEGORY", SqlDbType.VarChar).Value = txt_category_name.Text;

                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    TXT_PRODUCT_NAME.DataSource = ds.Tables[1];
                    TXT_PRODUCT_NAME.DisplayMember = "PRODUCT NAME";
                    TXT_PRODUCT_NAME.ValueMember = "Id";
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

        private void txt_category_name_Leave(object sender, EventArgs e)
        {

        }

        private void txt_provider_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcontact.Focus();
            }
        }

        private void txtcontact_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcity.Focus();
            }
        }

        private void txtcity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_category_name.Focus();
            }
        }

        private void txt_category_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TXT_PRODUCT_NAME.Focus();
            }
        }

        private void TXT_PRODUCT_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               
                if (TXT_PRODUCT_NAME.Text != "")
                {
                    int iProductId = Convert.ToInt32(TXT_PRODUCT_NAME.SelectedValue);
                    SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 1 SELLING_PRICE FROM TBL_PRODUCTS_DETAILS WHERE Id=" + iProductId, Royalicecream.Program.con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    bool dataAvailale = ds.Tables[0].Rows.Count > 0;
                    if (dataAvailale)
                    {
                        string sSellingPrice = ds.Tables[0].Rows[0]["SELLING_PRICE"].ToString();
                        TXT_PURCHASE.Text = sSellingPrice;
                        TXT_SELLING_PRICE.Text = sSellingPrice;
                        TXT_QTY.Focus();
                    }
                }

            }
        }

        private void TXT_QTY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TXT_PURCHASE.Focus();
            }
        }

        private void TXT_PURCHASE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TXT_SELLING_PRICE.Focus();
            }
        }

        private void TXT_NOTES_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click_1(sender,e);
                txt_category_name.Focus();
            }
        }

        private void TXT_SELLING_PRICE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TXT_NOTES.Focus();
            }
        }

        private void TXT_PRODUCT_NAME_SelectedIndexChanged(object sender, EventArgs e)
        {

           
            
        }

        private void TXT_PRODUCT_NAME_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
