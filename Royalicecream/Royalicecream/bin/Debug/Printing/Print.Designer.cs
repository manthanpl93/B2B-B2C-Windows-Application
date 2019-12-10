namespace Royalicecream.Printing
{
    partial class Print
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Customer_DetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Invoice = new Royalicecream.Printing.Invoice();
            this.Bill_InformationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Product_InfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Customer_DetailsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Invoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bill_InformationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Product_InfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.Customer_DetailsBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.Bill_InformationBindingSource;
            reportDataSource3.Name = "DataSet3";
            reportDataSource3.Value = this.Product_InfoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Royalicecream.Printing.INVOICE.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1254, 586);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // Customer_DetailsBindingSource
            // 
            this.Customer_DetailsBindingSource.DataMember = "Customer_Details";
            this.Customer_DetailsBindingSource.DataSource = this.Invoice;
            // 
            // Invoice
            // 
            this.Invoice.DataSetName = "Invoice";
            this.Invoice.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Bill_InformationBindingSource
            // 
            this.Bill_InformationBindingSource.DataMember = "Bill_Information";
            this.Bill_InformationBindingSource.DataSource = this.Invoice;
            // 
            // Product_InfoBindingSource
            // 
            this.Product_InfoBindingSource.DataMember = "Product_Info";
            this.Product_InfoBindingSource.DataSource = this.Invoice;
            // 
            // Print
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 586);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Print";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print";
            this.Load += new System.EventHandler(this.Print_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Customer_DetailsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Invoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bill_InformationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Product_InfoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource Customer_DetailsBindingSource;
        private Invoice Invoice;
        private System.Windows.Forms.BindingSource Bill_InformationBindingSource;
        private System.Windows.Forms.BindingSource Product_InfoBindingSource;
    }
}