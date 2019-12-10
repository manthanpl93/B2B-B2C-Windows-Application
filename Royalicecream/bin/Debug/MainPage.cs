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
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void show_remeberme()
        {
            SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_remeber_me]", Royalicecream.Program.con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value =2;
            DataSet ds = new DataSet();

            da.Fill(ds);

            string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

            if (status == "SUCCESS")
            {
                RememberDataview.DataSource = ds.Tables[1];
            }
            else
            {
                MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           

        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            show_remeberme();
            txtbackupname.Text = Application.ProductName.ToString()+ DateTime.Today.ToShortDateString();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_PRODUCT_DETAILS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 11;
                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    lblTotalCustomer.Text= ds.Tables[1].Rows[0]["CUSTOMERS"].ToString();
                    lblTotalProduct.Text= ds.Tables[1].Rows[0]["PRODUCTS"].ToString();
                    lblStockIn.Text= ds.Tables[1].Rows[0]["STOCK_IN"].ToString();
                    lblStockOut.Text= ds.Tables[1].Rows[0]["STOCK_OUT"].ToString();
                    lblTotalProvider.Text= ds.Tables[1].Rows[0]["SUPPLIERS"].ToString();
                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }



                da = new SqlDataAdapter("[dbo].[SP_PRODUCT_DETAILS]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 12;
                ds = new DataSet();

                da.Fill(ds);

            
                    DataView.DataSource = ds.Tables[0];
                DataView.Columns[1].Width = 170;        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Error Occured", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_remeber_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_remeber_me]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 1;
                da.SelectCommand.Parameters.Add("@Notes", SqlDbType.VarChar).Value = txtnotes.Text;
                da.SelectCommand.Parameters.Add("@Date", SqlDbType.Date).Value = Convert.ToDateTime(remebermedate.Text);
        

                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    show_remeberme();
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString() + "-" + ds.Tables[0].Rows[0]["Number"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured" + ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RememberDataview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_add_product_Click(object sender, EventArgs e)
        {
            AddNewProduct frm = new AddNewProduct();
            frm.MdiParent = this.ParentForm;
            frm.Show();
        }

        private void btn_customers_Click(object sender, EventArgs e)
        {
            CustomerReport frm = new CustomerReport();
            frm.MdiParent = this.ParentForm;
            frm.Show();
        }

        private void MainPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyData == Keys.O)

            {
                Form1 frm = new Form1();
                frm.Show();
                frm.MdiParent = this;
               
            }


            if (e.Control && e.KeyData == Keys.I)

            {
                Stock_In_Product frm = new Stock_In_Product();
                frm.Show();
                frm.MdiParent = this;
             
            }

            if (e.Control && e.KeyData == Keys.S)

            {
                Stock_In_Product frm = new Stock_In_Product();
                frm.Show();
                frm.MdiParent = this;
             
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Printing.Form1 FRM = new Printing.Form1();
            FRM.MdiParent = this.ParentForm;
            FRM.Show();
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select a folder";
            fbd.SelectedPath = Application.StartupPath;

            if (DialogResult.OK == fbd.ShowDialog())
            {
                txtdestination.Text = fbd.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_BACKUP_AND_RESTORE]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@OPTION", SqlDbType.SmallInt).Value = 1;
                da.SelectCommand.Parameters.Add("@FILENAME", SqlDbType.VarChar).Value = txtbackupname.Text;
                da.SelectCommand.Parameters.Add("@LOCATION", SqlDbType.VarChar).Value = txtdestination.Text + @"\";
               

                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    show_remeberme();
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString() + "-" + ds.Tables[0].Rows[0]["Number"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured" + ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.DefaultExt = "BAK";
            openFileDialog1.Title = "Browse BAK Files";
            openFileDialog1.ShowDialog();
            textBox2.Text = openFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_BACKUP_AND_RESTORE]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@OPTION", SqlDbType.SmallInt).Value = 2;
                
                da.SelectCommand.Parameters.Add("@FINAL_LOCATION", SqlDbType.VarChar).Value = textBox2.Text;


                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    show_remeberme();
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString() + "-" + ds.Tables[0].Rows[0]["Number"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured" + ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
