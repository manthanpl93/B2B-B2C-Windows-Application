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
    public partial class Providers_Details : Form
    {
        public Providers_Details()
        {
            InitializeComponent();
        }

        private void Providers_Details_Load(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_providers_details]", "");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 6;
                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    DataView.DataSource = ds.Tables[1];
                    //DataView.Columns[0].Width = 200;
                    //DataView.Columns[1].Width = 150;
                    //DataView.Columns[2].Width = 150;
                    //DataView.Columns[3].Width = 200;
                    //DataView.Columns[4].Width = 100;
                    //DataView.Columns[5].Width = 100;
                    //DataView.Columns[6].Width = 300;

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_providers_details]", "");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@category", SqlDbType.SmallInt).Value = 8;
                da.SelectCommand.Parameters.Add("@SEARCH", SqlDbType.VarChar).Value = txtSearch.Text;
                DataSet ds = new DataSet();

                da.Fill(ds);

                string status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "SUCCESS")
                {
                    DataView.DataSource = ds.Tables[1];
                    //DataView.Columns[0].Width = 200;
                    //DataView.Columns[1].Width = 150;
                    //DataView.Columns[2].Width = 150;
                    //DataView.Columns[3].Width = 200;
                    //DataView.Columns[4].Width = 100;
                    //DataView.Columns[5].Width = 100;
                    //DataView.Columns[6].Width = 300;

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
