using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComponentCounter
{
    public partial class MainForm : Form
    {
        ViewController vc;
        public MainForm()
        {
            InitializeComponent();
            vc = new ViewController(this);

            dtpDateFrom.Value = DateTime.Today.AddDays(-1);
            dtpDateTo.Value = DateTime.Today;
            dtpTimeFrom.Value = DateTime.Parse("06:00");
            dtpTimeTo.Value = DateTime.Parse("06:00");

            cbxLine.Items.AddRange(vc.GetLineNames());
            cbxLine.SelectedIndex = 1;
        }

        private void BtnGetData_Click(object sender, EventArgs e)
        {
            btnGetData.Text = "Pobieram";
            btnGetData.Enabled = false;

            dgvDataFromDB.DataSource = null;
            dgvDataFromDB.Refresh();
            dgvDataFromDB.DataSource = vc.FillDataGridView(dtpDateFrom.Value, dtpTimeFrom.Value, dtpDateTo.Value, dtpTimeTo.Value, cbxLine.SelectedIndex);

            btnGetData.Text = "Pobierz dane";
            btnGetData.Enabled = true;
        }
    }
}
