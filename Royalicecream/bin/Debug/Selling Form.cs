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
    public partial class Form1 : Form
    {
        DataTable BillInformation=new DataTable();
        DataTable InvoiceProducts = new DataTable();
        DataTable CustomerDetails = new DataTable();

        String C_Product;
        int C_QTY;

        public Form1()
        {
            InitializeComponent();
          

        }

        public class ProductDetails
        {
         public   bool status = false;
          public  int rownumber;
        }

        public string Calculate_Totalprice()
        {
            int totalamount=0;

            for(int i = 0; i < DataCart.Rows.Count; i++)
            {
                totalamount = totalamount + Convert.ToInt32(DataCart.Rows[i].Cells[5].Value);

            }

            return totalamount.ToString();
        }


        public ProductDetails product_available_on_cart(string ProductName)
        {
            ProductDetails status=new ProductDetails();

            for(int i = 0; i < DataCart.Rows.Count; i++)
            {
                string datacartproduct = DataCart.Rows[i].Cells[2].Value.ToString();

                if (datacartproduct == ProductName)
                {
                  status.status= true;
                  status.rownumber = i;

                }

            }

            return status;
        }


        public static void fill_autocomplete(TextBox txtbox, DataSet dataset,int index)
        {

            AutoCompleteStringCollection allowedStatorTypes = new AutoCompleteStringCollection();

            for (int i = 0; i < dataset.Tables[index].Rows.Count; i++)
            {
                allowedStatorTypes.Add(dataset.Tables[index].Rows[i][0].ToString());
            }


            txtbox.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtbox.AutoCompleteCustomSource = allowedStatorTypes;




        }


        private void Form1_Load(object sender, EventArgs e)
        {

            BillInformation.Columns.Add("TOTAL_ITEMS", typeof(int));
            BillInformation.Columns.Add("PRICE", typeof(decimal));
            BillInformation.Columns.Add("TAX", typeof(decimal));
            BillInformation.Columns.Add("VAT", typeof(decimal));
            BillInformation.Columns.Add("DISCOUNT", typeof(decimal));
            BillInformation.Columns.Add("TOTAL_PRICE", typeof(decimal));
            BillInformation.Columns.Add("PAY_BY", typeof(string));
            BillInformation.Columns.Add("CHEQUE_NUMBER", typeof(string));
            BillInformation.Columns.Add("NOTES", typeof(string));
            BillInformation.Columns.Add("DATE", typeof(DateTime));
            BillInformation.Columns.Add("OrderType", typeof(string));




            InvoiceProducts.Columns.Add("PRODUCT_NAME", typeof(string));
            InvoiceProducts.Columns.Add("PRICE", typeof(long));
            InvoiceProducts.Columns.Add("QTY", typeof(int));
            InvoiceProducts.Columns.Add("TOTAL_PRICE", typeof(long));
            InvoiceProducts.Columns.Add("CATEGORY", typeof(string));
            


            CustomerDetails.Columns.Add("CUSTOMER_NAME", typeof(string));
            CustomerDetails.Columns.Add("SHOP_ADDRESS", typeof(string));
            CustomerDetails.Columns.Add("CITY", typeof(string));
            CustomerDetails.Columns.Add("AREA", typeof(string));
            CustomerDetails.Columns.Add("CONTACT", typeof(string));
            CustomerDetails.Columns.Add("EMAIL", typeof(string));
            CustomerDetails.Columns.Add("NOTES", typeof(string));









            SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_PRODUCT_DETAILS]", Royalicecream.Program.con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 9;

            DataSet ds = new DataSet();

            da.Fill(ds);

            String status = ds.Tables[0].Rows[0]["STATUS"].ToString();

            if (status == "SUCCESS")
            {

                txtCategory.DataSource = ds.Tables[1];
                txtCategory.DisplayMember = "CATEGORY";
                
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
                //fill_autocomplete(txtCustomerName, ds, 1);
                txtCustomerName.DataSource = ds.Tables[1];
                txtCustomerName.DisplayMember = "CUSTOMER";
                txtCustomerName.ValueMember = "CONTACT";
               
            }
            else
            {
                MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }






        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void txtmobilenumber_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtemail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cbcust_name_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtSelectProduct_Enter(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_product_details2]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@Product_Name", SqlDbType.VarChar).Value = txtCategory.Text;
                da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 7;
                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    //fill_autocomplete(txtSelectProduct, ds,1);
                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch
            {
                MessageBox.Show("Error Occured","Application Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void txtSelectProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSelectProduct_Leave(sender, e);
                txtQty.Text = "1";
                ProductDetails obj = product_available_on_cart(txtCategory.Text);
                int qtyc;
         
                if (obj.status == true)
                {
                    qtyc = Convert.ToInt32(txtQty.Text) + Convert.ToInt32(DataCart.Rows[obj.rownumber].Cells[2].Value);
                }
                else
                {
                    qtyc = Convert.ToInt32(txtQty.Text);

                }


                if (C_QTY >= qtyc)
                {
                    txtQty.Focus();
                    
                }
                else
                {
                    MessageBox.Show("QTY Not Available", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQty.Text = "";
                }

                  
            }
        }

        public string CalculateBillAmount()
        {
                double BillAmount = 0;
                double VAT=0;
                double TAX=0;
                double Discount;

           
           
            if (txtDiscount.Text != "")
            {
               Discount = Convert.ToDouble(txtDiscount.Text);
            }
            else
            {
                Discount = 0;
            }
            
                double TotalPercentege = VAT + TAX;
                double TotalPrice = Convert.ToDouble(Calculate_Totalprice());
                BillAmount = TotalPrice + ((TotalPrice * TotalPercentege) / 100) - Discount ;
            
          

            return BillAmount.ToString();
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtCategory.Text!="" && txtQty.Text != "" && txtPrice.Text!="" && txt_product.Text!="")
                {
                    ProductDetails obj = product_available_on_cart(txt_product.Text);
                    int qtyc;
                    if (obj.status == true)
                    {
                        qtyc = Convert.ToInt32(txtQty.Text) + Convert.ToInt32(DataCart.Rows[obj.rownumber].Cells[3].Value);
                    }
                    else
                    {
                        qtyc = Convert.ToInt32(txtQty.Text);

                    }

                    if (txtCategory.Text != "")
                    {
                        if (txtQty.Text != "")
                        {

                            if (C_QTY >= qtyc)
                            {
                                if (txtPrice.Text != "")
                                {

                                    ProductDetails status = product_available_on_cart(txt_product.Text);

                                    string Index = (DataCart.Rows.Count + 1).ToString();
                                    string Category = txtCategory.Text;
                                    string Product = txt_product.Text;
                                    string QTY = txtQty.Text;
                                    string UnitPrice = txtPrice.Text;
                                    double TotalProductPrice = (Convert.ToInt32(QTY) * Convert.ToDouble(UnitPrice));

                                    if (status.status == true)
                                    {
                                        double currentPrice = Convert.ToDouble(DataCart.Rows[status.rownumber].Cells[5].Value);
                                        int currentQty = Convert.ToInt32(DataCart.Rows[status.rownumber].Cells[3].Value);
                                        int TotalQty = currentQty + Convert.ToInt32(QTY);
                                        double total = currentPrice + TotalProductPrice;
                                        DataCart.Rows[status.rownumber].Cells[5].Value = total.ToString();
                                        DataCart.Rows[status.rownumber].Cells[3].Value = TotalQty;

                                    }
                                    else
                                    {
                                        DataCart.Rows.Add(Index, Category,Product, QTY, UnitPrice, TotalProductPrice);
                                    }
                                    
                                    lbltotalamount.Text = Calculate_Totalprice();

                                    lbl_bill_amount.Text = CalculateBillAmount();                 
                                }
                                else
                                {
                                    MessageBox.Show("Price is not available", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("QTY Not Available, " + txt_product.Text + " has only "+ C_QTY + " qty left.", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Must enter QTY", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Choose product", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Operation");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured" + ex.Message+ex.StackTrace, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnGenrateBill_Click(object sender, EventArgs e)
        {
            
            int TotalItems = 0;
            for (int i = 0; i < DataCart.Rows.Count; i++)
            {
                TotalItems = TotalItems + Convert.ToInt32(DataCart.Rows[i].Cells[3].Value);
            }

           
            decimal Price = Convert.ToDecimal(lbltotalamount.Text);
            decimal TAX = Convert.ToDecimal(0);
            decimal VAT = Convert.ToDecimal(0);
            decimal DISCOUNT = Convert.ToDecimal(txtDiscount.Text);
            decimal TotalPrice = Convert.ToDecimal(lbl_bill_amount.Text);
            string Payby=null;
            string ChequeNumber = txt_cheque_no.Text;
            string Billnotes = txtNotes.Text;
            string OrderType;
            if (rb_cash.Checked == true)
            {
                Payby = "Cash";
            }
            else if (rb_cheque.Checked == true)
            {
                Payby = "Cheque";
            }

            if (rb_Retail.Checked == true)
            {
                OrderType = "Retail";
            }
            else
            {
                OrderType = "Wholesale";
                CustomerDetails.Rows.Add(txtCustomerName.Text, txtAddress.Text, txtCity.Text, txtArea.Text, txtContace.Text, null, txtNotes.Text);

            }


            BillInformation.Rows.Add(TotalItems, TotalPrice, TAX, VAT, DISCOUNT, TotalPrice, Payby,ChequeNumber, Billnotes, Convert.ToDateTime(DateTime.Today),OrderType);


            for(int i = 0; i < DataCart.Rows.Count; i++)
            {
                string ProductName=DataCart.Rows[i].Cells[2].Value.ToString();
                double ProductPrice=Convert.ToDouble(DataCart.Rows[i].Cells[4].Value.ToString());
                int QTY=Convert.ToInt32(DataCart.Rows[i].Cells[3].Value.ToString());
                double totalPrice=Convert.ToDouble(DataCart.Rows[i].Cells[5].Value.ToString());
                string Category = DataCart.Rows[i].Cells[1].Value.ToString();

                InvoiceProducts.Rows.Add(ProductName,ProductPrice,QTY,totalPrice,Category);
                
            }


            //try
            //{
            SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_INVOICE_DETAILS]", Royalicecream.Program.con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 1;

            if (rb_Retail.Checked == false)
            {
                da.SelectCommand.Parameters.Add("@CUSTOMER_DETAILS", SqlDbType.Structured).Value = CustomerDetails;
            }
            
            da.SelectCommand.Parameters.Add("@BILL_INFORMATION", SqlDbType.Structured).Value = BillInformation;

            da.SelectCommand.Parameters.Add("@INVOICE_PRODUCTS_DETAILS", SqlDbType.Structured).Value = InvoiceProducts;



            DataSet ds = new DataSet();

            da.Fill(ds);

            string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

            if (status == "SUCCESS")
            {
                MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Printing.Print frm = new Printing.Print();
                //frm.MdiParent = this.ParentForm;
                //frm.ds = ds;
                //frm.Show();

                if (rb_DirectPrint.Checked == true)
                {
                    Printing.Form1 frm = new Printing.Form1("Direct Print");
                    frm.MdiParent = this.ParentForm;
                    frm.ds = ds;
                    frm.Show();
                    frm.Visible = false;

                }
                else if(rb_print_preview.Checked==true)
                {
                    
                Printing.Form1 frm = new Printing.Form1();
                frm.MdiParent = this.ParentForm;
                frm.ds = ds;
                frm.Show();
              
                }
                else if(rb_noprint.Checked==true)
                {
                    Form1 frm = new Form1();
                    frm.Show();
                    frm.MdiParent = this.ParentForm;
                    this.Close();
                    this.Dispose();
                }

                // CLEAN ALL TEXTBOXES

                txtCustomerName.Text = "";
                txtContace.Text = "";

                txtAddress.Text = "";

                txtCity.Text = "";
                txtArea.Text = "";


                txtDiscount.Text = "";
                txtNotes.Text = "";
                txt_cheque_no.Text = "";


            }
            else
            {
                MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //}
            //catch
            //{
            //    MessageBox.Show("Error Occured", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}



        }

        private void txtTax_TextChanged(object sender, EventArgs e)
        {
            lbl_bill_amount.Text = CalculateBillAmount();
            
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            lbl_bill_amount.Text = CalculateBillAmount();
            
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            lbl_bill_amount.Text = CalculateBillAmount();
            
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtContace_Leave(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_CUSTOMER_DETAILS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CONTACT", SqlDbType.VarChar).Value = txtContace.Text;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 2;
                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    txtCustomerName.Text= ds.Tables[1].Rows[0]["CUSTOMER"].ToString();

                    //txtEmail.Text= ds.Tables[1].Rows[0]["EMAIL"].ToString();
                    txtAddress.Text= ds.Tables[1].Rows[0]["SHOP"].ToString();
                    txtCity.Text= ds.Tables[1].Rows[0]["CITY"].ToString();
                    txtArea.Text= ds.Tables[1].Rows[0]["AREA"].ToString();


                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch
            {
                MessageBox.Show("Error Occured", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSelectProduct_Leave(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_product_details2]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 8;
                da.SelectCommand.Parameters.Add("@Product_Name", SqlDbType.VarChar).Value = txtCategory.Text;
                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    txtPrice.Text = ds.Tables[1].Rows[0]["Price"].ToString();
                    C_QTY = Convert.ToInt32(ds.Tables[1].Rows[0]["QTY"]);
                    C_Product = ds.Tables[1].Rows[0]["Price"].ToString();
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

        private void DataCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {

                DialogResult dr = MessageBox.Show("Are you sure you delete " + DataCart.Rows[e.RowIndex].Cells[2].Value + " from the list ?", "Delete Item", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    DataCart.Rows.RemoveAt(e.RowIndex);
                    lbltotalamount.Text = Calculate_Totalprice();

                    lbl_bill_amount.Text = CalculateBillAmount();

                }
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtQty.Text != "")
                {
                    btnAdd_Click(sender, e);
                    txtCategory.Focus();
                }
                else
                {
                    MessageBox.Show("Must Enter QTY", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtContace_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar == '!' || e.KeyChar == '~' || e.KeyChar == '@' || e.KeyChar == '#' || e.KeyChar == '$' || e.KeyChar == '%' || e.KeyChar == '^' || e.KeyChar == '&' || e.KeyChar == '*' || e.KeyChar == '(' || e.KeyChar == ')' || e.KeyChar == '_' || e.KeyChar == '-' || e.KeyChar == '+' || e.KeyChar == '=' || e.KeyChar == '/' || e.KeyChar == '*' || e.KeyChar == '{' || e.KeyChar == '[' || e.KeyChar == ']' || e.KeyChar == '}' || e.KeyChar == ';' || e.KeyChar == ':' || e.KeyChar == ',' || e.KeyChar == '"' || e.KeyChar == '.' || e.KeyChar == '/' || e.KeyChar == '?' || e.KeyChar == '`' || e.KeyChar == '!' || e.KeyChar == '~' || e.KeyChar == '@' || e.KeyChar == '#' || e.KeyChar == '$' || e.KeyChar == '%' || e.KeyChar == '^' || e.KeyChar == '&' || e.KeyChar == '*' || e.KeyChar == '(' || e.KeyChar == ')' || e.KeyChar == '_' || e.KeyChar == '-' || e.KeyChar == '+' || e.KeyChar == '=' || e.KeyChar == '/' || e.KeyChar == '*' || e.KeyChar == '{' || e.KeyChar == '[' || e.KeyChar == ']' || e.KeyChar == '}' || e.KeyChar == ';' || e.KeyChar == ':' || e.KeyChar == ',' || e.KeyChar == '"' || e.KeyChar == '.' || e.KeyChar == '/' || e.KeyChar == '?' || e.KeyChar == '`' || e.KeyChar == 39 || e.KeyChar == 32)
            {

                e.Handled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                btnAdd_Click(sender, e);
            }
        }

        private void rbInvoiceBill_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void rbSimpleBill_CheckedChanged(object sender, EventArgs e)
        {
        
        }

        private void rbQuatation_CheckedChanged(object sender, EventArgs e)
        {
        
        }

        private void rbEstimate_CheckedChanged(object sender, EventArgs e)
        {
        
        }

        private void rbInvoiceBill_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void rb_cash_CheckedChanged(object sender, EventArgs e)
        {
            txt_cheque_no.Enabled = false;
        }

        private void rb_cheque_CheckedChanged(object sender, EventArgs e)
        {
            txt_cheque_no.Enabled = true;
        }

        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCustomerName.Text == "Type Customer Name")
            {
                txtCustomerName.Text = "";
            }

            if (e.KeyCode == Keys.Enter)
            {
                txtContace.Focus();
            }
        }

        private void txtCustomerName_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text == "Type Customer Name")
            {
                txtCustomerName.Text = "";
            }
        }

        private void txtCustomerName_Enter(object sender, EventArgs e)
        {
            try
            {
                //SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_product_details2]", Royalicecream.Program.con);
                //da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 8;
                //da.SelectCommand.Parameters.Add("@Product_Name", SqlDbType.VarChar).Value = txtCategory.Text;
                //DataSet ds = new DataSet();

                //da.Fill(ds);

                //string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                //if (status == "SUCCESS")
                //{
                    
                //}
                //else
                //{
                //    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured" + ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtContace_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtContace.Text == "Type Customer Number")
            {
                txtContace.Text = "";
            }

            if (e.KeyCode == Keys.Enter)
            {
                txtAddress.Focus();
            }
        }

        private void txtContace_Click(object sender, EventArgs e)
        {
            if (txtContace.Text == "Type Customer Number")
            {
                txtContace.Text = "";
            }
        }

        private void txtEmail_Click(object sender, EventArgs e)
        {
          
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
           

            if (e.KeyCode == Keys.Enter)
            {
                txtAddress.Focus();
            }
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
           
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtAddress.Text == "Type Address")
            {
                txtAddress.Text = "";
            }

            if (e.KeyCode == Keys.Enter)
            {
                txtCity.Focus();
            }
        }

        private void txtAddress_Click(object sender, EventArgs e)
        {
            if (txtAddress.Text == "Type Address")
            {
                txtAddress.Text = "";
            }
        }

        private void txtAddress_Enter(object sender, EventArgs e)
        {
            if (txtAddress.Text == "Type Address")
            {
                txtAddress.Text = "";
            }
        }

        private void txtCity_Enter(object sender, EventArgs e)
        {
            if (txtCity.Text == "Type City")
            {
                txtCity.Text = "";
            }
        }

        private void txtCity_Click(object sender, EventArgs e)
        {
            if (txtCity.Text == "Type City")
            {
                txtCity.Text = "";
            }
        }

        private void txtCity_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtCity.Text == "Type City")
            {
                txtCity.Text = "";
            }
        }

        private void txtPincode_Enter(object sender, EventArgs e)
        {
            if (txtArea.Text == "Type Pincode")
            {
                txtArea.Text = "";
            }
        }

        private void txtPincode_Click(object sender, EventArgs e)
        {
            if (txtArea.Text == "Type Pincode")
            {
                txtArea.Text = "";
            }
        }

        private void txtPincode_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtArea.Text == "Type Pincode")
            {
                txtArea.Text = "";
            }

            if (e.KeyCode == Keys.Enter)
            {
                txtCategory.Focus();
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

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
                        //txt_product.Text = ds.Tables[1].Rows[0]["PRODUCT NAME"].ToString(); 
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

        private void txt_product_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (txt_product.Text != "System.Data.DataRowView")
            {   
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_PRODUCT_DETAILS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 8;
                da.SelectCommand.Parameters.Add("@PRODUCT_CATEGORY", SqlDbType.VarChar).Value = txtCategory.Text;
                da.SelectCommand.Parameters.Add("@PRODUCT_NAME", SqlDbType.VarChar).Value = txt_product.Text;

                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    txtPrice.Text = ds.Tables[1].Rows[0]["SELLING PRICE"].ToString();
                    C_QTY = Convert.ToInt32(ds.Tables[1].Rows[0]["AVAILABLE STOCK"].ToString());

                }
                else
                {

                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                }
            }
        }

        private void txtQty_Enter(object sender, EventArgs e)
        {
            if (txtQty.Text == "")
            {
                txtQty.Text = "1";
            }
        }

        private void txt_notes_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtArea.Focus();
            }
        }

        private void txtCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_product.Focus();
            }
        }

        private void txt_product_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtQty.Focus();
            }
        }

        private void txtCustomerName_Leave(object sender, EventArgs e)
        {
            if (txtCustomerName.Text != "")
            {
                try
                {
                    txtContace.Text = txtCustomerName.SelectedValue.ToString();
                    txtContace_Leave(sender, e);

                    txtCategory.Focus();
                }
                catch
                {

                }
                  
                
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rb_Retail.Checked == true)
            {
                txtCustomerName.Enabled = false;
                txtContace.Enabled = false;
                txtAddress.Enabled = false;
                txtCity.Enabled = false;
                txtArea.Enabled = false;

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                txtCustomerName.Enabled = true;
                txtContace.Enabled = true;
                txtAddress.Enabled = true;
                txtCity.Enabled = true;
                txtArea.Enabled = true;

            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void rb_print_preview_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtCustomerName_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCustomerName.Text != "")
                {

                    txtContace.Text = txtCustomerName.SelectedValue.ToString();
                    txtContace_Leave(sender, e);

                    txtCategory.Focus();

                }
            }
        }
    }
}
