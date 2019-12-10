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
    public partial class Account_Management : Form
    {
        public Account_Management()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_account_management]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 8;
                da.SelectCommand.Parameters.Add("@Search", SqlDbType.VarChar).Value = TXTSEARCH.Text;
                da.SelectCommand.Parameters.Add("@DateFrom", SqlDbType.Date).Value = Convert.ToDateTime(txtStartDate.Text);
                da.SelectCommand.Parameters.Add("@DateTo", SqlDbType.Date).Value = Convert.ToDateTime(txtEndDate.Text);
                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    DataView.DataSource = ds.Tables[1];
                    DataView.Columns[0].Width = 300;
                    DataView.Columns[1].Width = 100;
                    DataView.Columns[2].Width = 100;
                    DataView.Columns[3].Width = 100;
                    DataView.Columns[4].Width = 250;
                    DataView.Columns[5].Width = 100;
                    DataView.Columns[6].Width = 150;
                    DataView.Columns[7].Width = 100;
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

        private void txtAcName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtAcName.Text != "")
                {
                    if (rbProvider.Checked == true)
                    {
                        SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_providers_details]", Royalicecream.Program.con);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 5;
                        da.SelectCommand.Parameters.Add("@AC_Number", SqlDbType.VarChar).Value = txtAcName.Text;
                        DataSet ds = new DataSet();

                        da.Fill(ds);

                        string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                        if (status == "SUCCESS")
                        {
                            lblname.Text = ds.Tables[1].Rows[0]["Provider Name"].ToString();
                            lblcontact.Text = ds.Tables[1].Rows[0]["Contact Number"].ToString();
                            lblcity.Text = ds.Tables[1].Rows[0]["Bank City"].ToString();
                        }
                        else
                        {
                            MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else if (rbCustomer.Checked == true)
                    {
                        MessageBox.Show("under maintanance", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                  
                }
                else
                {
                    MessageBox.Show("Must enter account number", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSve_Click(object sender, EventArgs e)
        {

            //bool C = true;
            //bool C2 = true;
            //if (rb_cheque.Checked == true && txt_cheque_no.Text!="")
            //{
            //    C = true;
            //}
            //else
            //{
            //    C = false;
            //}

            //if(rb_other.Checked==true && txt_contact.Text != "")
            //{
            //    C2 = true;
            //}
            //else
            //{
            //    C2 = false;
            //}

            if (txtAcName.Text!="" )
            {

                string PaymentBy = null;

                if (rb_cash.Checked == true)
                {
                    PaymentBy = "Cash";
                }
                else
                {
                    PaymentBy = "Cheque";
                }


                string accountType = null;
                string Customer_type = null;

                if (rbCredit.Checked == true)
                {
                    accountType = rbCredit.Text;
                }
                else if (rbDebit.Checked == true)
                {
                    accountType = rbDebit.Text;
                }

                if (rbCustomer.Checked == true)
                {
                    Customer_type = rbCustomer.Text;
                }
                else if (rbProvider.Checked == true)
                {
                    Customer_type = rbProvider.Text;
                }
                else if (rb_other.Checked == true)
                {
                    Customer_type = "Other";
                }

                SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_account_management]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@userType", SqlDbType.VarChar).Value = Customer_type;
                da.SelectCommand.Parameters.Add("@AccountantName", SqlDbType.VarChar).Value = txtAcName.Text;
                da.SelectCommand.Parameters.Add("@TransactionType", SqlDbType.VarChar).Value = accountType;
                da.SelectCommand.Parameters.Add("@Amount", SqlDbType.Float).Value = Convert.ToDouble(txtAmount.Text);
                da.SelectCommand.Parameters.Add("@Notes", SqlDbType.VarChar).Value = txtNote.Text;
                da.SelectCommand.Parameters.Add("@Category", SqlDbType.SmallInt).Value = 1;

                da.SelectCommand.Parameters.Add("@PayemntBy", SqlDbType.VarChar).Value = PaymentBy;

                if (rb_other.Checked == true)
                {
                    da.SelectCommand.Parameters.Add("@Contact", SqlDbType.VarChar).Value = txt_contact.Text;
                }
                else
                {
                    da.SelectCommand.Parameters.Add("@Number", SqlDbType.VarChar).Value = txtAcName.SelectedValue.ToString();
                }

                if (rb_cheque.Checked == true)
                {
                    da.SelectCommand.Parameters.Add("@ChequeNumber", SqlDbType.VarChar).Value = txt_cheque_no.Text;
                }


                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();
                string message = ds.Tables[0].Rows[0]["MESSAGE"].ToString();
                if (status == "SUCCESS")
                {
                    if (accountType == rbCredit.Text)
                    {
                       
                    }
                    else
                    {
                        
                    }

                    MessageBox.Show(message, "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Account_Management_Load(sender, e);
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

        }

        private void btnReset_Click(object sender, EventArgs e)
        {

        }

        private void Account_Management_Load(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_account_management]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 4;
                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    DataView.DataSource = ds.Tables[1];
                    DataView.Columns[0].Width = 300;
                    DataView.Columns[1].Width = 100;
                    DataView.Columns[2].Width = 100;
                    DataView.Columns[3].Width = 100;
                    DataView.Columns[4].Width = 250;
                    DataView.Columns[5].Width = 100;
                    DataView.Columns[6].Width = 150;
                    DataView.Columns[7].Width = 100;
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

        private void txtAcName_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtAcName_Enter_1(object sender, EventArgs e)
        {
            if (rbProvider.Checked == true)
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_providers_details]", Royalicecream.Program.con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 6;
                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                    if (status == "SUCCESS")
                    {
                        txtAcName.DataSource = ds.Tables[1];
                        txtAcName.ValueMember = "Contact Number";
                        txtAcName.DisplayMember = "Provider Total";
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
            else
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_customers_details]", Royalicecream.Program.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 4;
                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    txtAcName.DataSource = ds.Tables[1];
                    txtAcName.ValueMember = "Contact";
                    txtAcName.DisplayMember = "Customer Total";
                }
                else
                {
                    MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void txtdatefrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtAcName_KeyDown_1(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (txtAcName.Text != "")
                {
                    if (rbProvider.Checked == true)
                    {
                        SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_providers_details]", Royalicecream.Program.con);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 7;
                        da.SelectCommand.Parameters.Add("@Contact", SqlDbType.VarChar).Value = txtAcName.SelectedValue;
                        DataSet ds = new DataSet();

                        da.Fill(ds);

                        string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                        if (status == "SUCCESS")
                        {
                            lblname.Text = ds.Tables[1].Rows[0]["Provider Name"].ToString();
                            lblcontact.Text = ds.Tables[1].Rows[0]["Contact Number"].ToString();
                            lblcity.Text = ds.Tables[1].Rows[0]["Bank City"].ToString();
                        }
                        else
                        {
                            MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else if (rbCustomer.Checked == true)
                    {
                        SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_customers_details]", Royalicecream.Program.con);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 6;
                        da.SelectCommand.Parameters.Add("@ContactNumber", SqlDbType.VarChar).Value = txtAcName.SelectedValue;
                        DataSet ds = new DataSet();

                        da.Fill(ds);

                        string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                        if (status == "SUCCESS")
                        {
                            lblname.Text = ds.Tables[1].Rows[0]["Customer Name"].ToString();
                            lblcontact.Text = ds.Tables[1].Rows[0]["Contact"].ToString();
                            lblcity.Text = ds.Tables[1].Rows[0]["City"].ToString();
                        }
                        else
                        {
                            MessageBox.Show(ds.Tables[0].Rows[0]["MESSAGE"].ToString(), "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }


                        
                    }

                }
                else
                {
                    MessageBox.Show("Must enter account number", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void rbCustomer_CheckedChanged(object sender, EventArgs e)
        {
            txt_contact.Enabled = false;
        }

        private void rbProvider_CheckedChanged(object sender, EventArgs e)
        {
            txt_contact.Enabled = false;
        }

        private void rb_other_CheckedChanged(object sender, EventArgs e)
        {
            txt_contact.Enabled = true;
        }

        private void rb_cash_CheckedChanged(object sender, EventArgs e)
        {
            txt_cheque_no.Enabled = false;
        }

        private void rb_cheque_CheckedChanged(object sender, EventArgs e)
        {
            txt_cheque_no.Enabled = true;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
                    }
    }
}
