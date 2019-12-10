using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Royalicecream
{
    public partial class Home : Form
    {
        static MainPage obj = new MainPage();
       

        public Home()
        {
            InitializeComponent();
            try
            {

                obj.MdiParent = this;
     
               
                if (obj.Visible == false)
                {
                    obj.Visible = true;

                }
                else
                {
                    obj.Visible = true;

                }

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Warning");
                obj = new MainPage();
                obj.MdiParent = this;
                obj.Show();
            }
        }

        private void customerOrderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    obj.MdiParent = this;


            //    //frm.MdiParent = this;
            //    if (obj.Visible == false)
            //    {
            //        obj.Visible = true;

            //    }
            //    else
            //    {
            //        obj.Visible = true;

            //    }

            //}
            //catch (Exception E)
            //{
            //    MessageBox.Show(E.Message, "Warning");
            //    obj = new MainPage();
            //    obj.MdiParent = this;
            //    obj.Show();
            //}


           


        }

        private void addNewProductToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }

        private void accountManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void addMaterialProviderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            


        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void invoiceDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
          

           

        }

        private void expenceDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void customerDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void bankInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
           
        }

        private void addKitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void repairToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void attandanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void salerySlipToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            frm.MdiParent = this;
            obj.Visible = false;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
           
        }

        private void productDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Search frm = new Search();
            frm.Show();
            frm.MdiParent = this;
            obj.Visible = false;
        }

        private void addNewProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void addMaterialProviderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
          
        }
            //Show Invoices
        private void salarySlipToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Show_Invoices frm = new Show_Invoices();
            frm.Show();
            frm.MdiParent = this;
            obj.Visible = false;
        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Customer_Invoice_Report frm = new Customer_Invoice_Report();
            //frm.Show();
            //frm.MdiParent = this;
            //obj.Visible = false;
        }

        private void attendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void addNewKitToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void customerDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerReport frm = new CustomerReport();
            frm.Show();
            frm.MdiParent = this;
            obj.Visible = false;
        }

        private void bankDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void creditDebitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Account_Management frm = new Account_Management();
            frm.Show();
            frm.MdiParent = this;
            obj.Visible = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainPage frm = new MainPage();
            frm.Show();
            frm.MdiParent = this;
            obj.Visible = false;
        }

        private void expenceDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            frm.MdiParent = this;
            obj.Visible = false;
        }

        private void rojMedToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        //Add New Product
        private void rentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           Stock_In_Product frm = new Stock_In_Product();
            frm.Show();
            frm.MdiParent = this;
            obj.Visible = false;
        }
        //Stock Details
        private void searchRentDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stock_Details frm = new Stock_Details();
            frm.Show();
            frm.MdiParent = this;
            obj.Visible = false;
        }

        private void logoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           

        }

        private void passwordToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void providerDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Providers_Details frm = new Providers_Details();
            frm.Show();
            frm.MdiParent = this;

            obj.Visible = false;
        }

        private void accountToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

            obj.Visible = false;
        }

        private void salarySlipToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //Search Product
        private void searchProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Search frm = new Search();
            frm.Show();
            frm.MdiParent = this;
            obj.Visible = false;
        }

        private void sellsInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show_Invoices frm = new Show_Invoices();
            frm.Show();
            frm.MdiParent = this;
            obj.Visible = false;
        }

        private void addNewSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_new_Supplier frm = new Add_new_Supplier();
            frm.Show();
            frm.MdiParent = this;
            obj.Visible = false;
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            obj.Visible = true;
           

        }

        private void Home_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.KeyData ==( Keys.Control  | Keys.O))

            {
                Form1 frm = new Form1();
                frm.Show();
                frm.MdiParent = this;
                obj.Visible = false;
            }


            if (e.KeyData == (Keys.Control | Keys.I))

            {
               Stock_In_Product frm = new Stock_In_Product();
            frm.Show();
            frm.MdiParent = this;
            obj.Visible = false;
            }

            if (e.KeyData == (Keys.Control | Keys.S))

            {
                Stock_In_Product frm = new Stock_In_Product();
                frm.Show();
                frm.MdiParent = this;
                obj.Visible = false;
            }

            if (e.KeyData == (Keys.Control | Keys.P))

            {
                AddNewProduct frm = new AddNewProduct();
                frm.MdiParent = this.ParentForm;
                frm.Show();

            }

            if (e.KeyData == (Keys.Control | Keys.D))

            {
                Stock_Details frm = new Stock_Details();
                frm.Show();
                frm.MdiParent = this;
                obj.Visible = false;

            }

            if (e.KeyData == (Keys.Control | Keys.S))

            {

                Add_new_Supplier frm = new Add_new_Supplier();
                frm.Show();
                frm.MdiParent = this;
                obj.Visible = false;
            }


            if (e.KeyData == (Keys.Control | Keys.C))

            {
                CustomerReport frm = new CustomerReport();
                frm.MdiParent = this.ParentForm;
                frm.Show();
            }


            if (e.KeyData == (Keys.Control | Keys.A))

            {
              
            }

            if (e.KeyData == (Keys.Control | Keys.Q))

            {
                Search frm = new Search();
                frm.Show();
                frm.MdiParent = this;
                obj.Visible = false;
            }




        }

        private void customerDetailsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CustomerReport frm = new CustomerReport();
            frm.MdiParent = this;
            frm.Show();
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewProduct frm = new AddNewProduct();
            frm.MdiParent = this;
            frm.Show();
        }

        private void logoutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void salesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalesReport frm = new SalesReport();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
