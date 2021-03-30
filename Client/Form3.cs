using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form3 : Form
    {
        private DataTable dt;

        public Form3()
        {
            InitializeComponent();
        }

        public Form3(DataTable dt)
        {
            InitializeComponent();
            this.dt = dt;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string rezultat = null;
            foreach (DataRow row in dt.Rows)
            {
                string[] f = row["GateName"].ToString().Split('|');
                if (f[0].Length>=5 && f[0].Substring(0, 6) == "OUTPUT")
                {
                    rezultat = row["OutputResult"].ToString();
                    break;
                }
            }
            rezLabel.Text = "Rezultatul este: " + rezultat;
            dataGridView1.DataSource = dt;
        }
    }
}
