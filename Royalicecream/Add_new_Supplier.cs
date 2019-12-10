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
    public partial class Add_new_Supplier : Form
    {
        public Add_new_Supplier()
        {
            InitializeComponent();
        }

        private void txtpincode_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //try
            //{


                if(txtName.Text!="" && txtContact.Text!="" 
                   && txtCity.Text!=""
                   && txtAcName.Text!="" && txtAcNumber.Text!="" &&
                    txtBankName.Text!="" && txtBankBranch.Text!=""  &&
                    txtBankIsfc.Text!="")
                {

                    string SUPPLIER_NAME = txtName.Text;
                    string CONTACT= txtContact.Text;
                    string EMAIL=txtEmail.Text;
                    string CITY=txtCity.Text;
                    string ACC_NUMBER= txtAcNumber.Text;
                    string ACC_NAME= txtAcName.Text;
                    string BANK_NAME= txtBankName.Text;
                    string BANK_BRANCH= txtBankBranch.Text;
                    string IFSC_CODE= txtBankIsfc.Text;
                    string NOTES=txtnotes.Text;


                    SqlDataAdapter da = new SqlDataAdapter("[dbo].[SP_SUPPLIER_DETAILS]", Royalicecream.Program.con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@PROVIDER_NAME", SqlDbType.VarChar).Value =SUPPLIER_NAME;
                    da.SelectCommand.Parameters.Add("@PROVIDER_NUMBER", SqlDbType.VarChar).Value = CONTACT;
                    da.SelectCommand.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtCity.Text;
                    da.SelectCommand.Parameters.Add("@AC_NAME", SqlDbType.VarChar).Value = ACC_NAME;
                    da.SelectCommand.Parameters.Add("@ACC_NUMBER", SqlDbType.VarChar).Value = ACC_NUMBER;
                    da.SelectCommand.Parameters.Add("@BANK", SqlDbType.VarChar).Value = BANK_NAME;
                    da.SelectCommand.Parameters.Add("@BRANCH", SqlDbType.VarChar).Value = BANK_BRANCH;
                    da.SelectCommand.Parameters.Add("@IFSC_CODE", SqlDbType.VarChar).Value = IFSC_CODE;
                    da.SelectCommand.Parameters.Add("@NOTES", SqlDbType.VarChar).Value = NOTES;
                    da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.SmallInt).Value = 1;
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
                else
                {
                    MessageBox.Show("Invalid Operation", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
               
            //}
            //catch
            //{
            //    MessageBox.Show("Error Occured", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void lbl_heading_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtName.Text == "Type Name")
            {
                txtName.Text = "";
            }
           
        }

        private void txtContact_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtContact.Text == "Type Number")
            {
                txtContact.Text = "";
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtEmail.Text == "Type Email")
            {
                txtEmail.Text = "";
            }
        }

        private void txtCategory_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void txtCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCity.Text == "Type city")
            {
                txtCity.Text = "";
            }
        }

        private void Add_Material_Provider_Load(object sender, EventArgs e)
        {

        }
    }
}
