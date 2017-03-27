using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            List<int> r = new List<int>();
            r.Add(50);
            r.Add(500);
           
            chart1.Series.Add("New").YValueMembers = "New";
            Series s = chart1.Series["New"];
            s.ChartType = SeriesChartType.Pie;
       
            for (int i = 0; i < r.Count; i++)
            {
                s.Points.AddXY(i + "*", r[i]);
            }
           
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
