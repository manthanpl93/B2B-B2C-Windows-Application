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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {

            if(txtusername.Text!="" && txtpassword.Text != "")
            {

                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_login_process]", Royalicecream.Program.con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 1;
                    da.SelectCommand.Parameters.Add("@Username", SqlDbType.VarChar).Value = txtusername.Text;
                    da.SelectCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = txtpassword.Text;

                    DataSet ds = new DataSet();

                    da.Fill(ds);


                    string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                    if (status == "SUCCESS")
                    {
                        Home frm = new Home();
                        frm.Show();
                        this.Hide();
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
            else
            {
                MessageBox.Show("Please enter username & password", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnlogin_Click(sender,e);
            }
        }
    }
}
