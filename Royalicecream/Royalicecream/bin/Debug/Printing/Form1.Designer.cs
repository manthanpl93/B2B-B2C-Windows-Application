namespace Royalicecream.Printing
{
    partial class Form1
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
            this.Product_InfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Invoice = new Royalicecream.Printing.Invoice();
            this.Bill_InformationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Customer_DetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.Product_InfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Invoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bill_InformationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer_DetailsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Product_InfoBindingSource
            // 
            this.Product_InfoBindingSource.DataMember = "Product_Info";
            this.Product_InfoBindingSource.DataSource = this.Invoice;
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
            // Customer_DetailsBindingSource
            // 
            this.Customer_DetailsBindingSource.DataMember = "Customer_Details";
            this.Customer_DetailsBindingSource.DataSource = this.Invoice;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "PRODUCTS";
            reportDataSource1.Value = this.Product_InfoBindingSource;
            reportDataSource2.Name = "Bill_Information";
            reportDataSource2.Value = this.Bill_InformationBindingSource;
            reportDataSource3.Name = "Customer_Details";
            reportDataSource3.Value = this.Customer_DetailsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Royalicecream.Printing.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1254, 586);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 586);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Product_InfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Invoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bill_InformationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer_DetailsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource Product_InfoBindingSource;
        private Invoice Invoice;
        private System.Windows.Forms.BindingSource Bill_InformationBindingSource;
        private System.Windows.Forms.BindingSource Customer_DetailsBindingSource;
    }
}