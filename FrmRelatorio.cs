using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trabalho_DAD_livro
{
    public partial class FrmRelatorio : Form
    {
        DataTable dt = new DataTable();
        public FrmRelatorio(DataTable dt)
        {
            InitializeComponent();
            this.dt = dt;
        }

        private void FrmRelatorio_Load(object sender, EventArgs e)
        {
            
            //this.livroTableAdapter.Fill(this.trabalhoDadDataSet.livro);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt));
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
