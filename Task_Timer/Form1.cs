using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Task_Timer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static List<MyTask> tasks = new List<MyTask>();// all our tasks
        public static List<CompTaskcs> comptasks = new List<CompTaskcs>();
        public static int ind = 0;
        private void button1_Click(object sender, EventArgs e)// button "add new task"
        {
            if (tasks.Count < 3)
            {
                tasks.Add(new MyTask());   
                tasks[tasks.Count - 1].Top = 50;     
                tabcontrol.TabPages[0].Controls.Add(tasks[tasks.Count - 1]);// add tasks to first page
                if (tasks.Count > 1) tasks[tasks.Count - 1].Top = tasks[tasks.Count - 2].Top + 75;
                
            }
            else
            { MessageBox.Show("There is a limit for tasks"); }
           

        }

        private void tabPage2_Click(object sender, EventArgs e)// 0000000000000000 page to hold programs' timers
        {
            //Dictionary<string, int> myTasks = new Dictionary<string, int>();
            //for (int i = 0; i < tasks.Count; i++)
            //{
            //    myTasks.Add(tasks[0].taskName, tasks[0].time); ;
            //}

            //chart1.DataSource = myTasks;
            
        }

        public static List<string> myPrograms = new List<string>();// Program names tracked by threads

        public static Thread progs;// thread to look at program names
        public  void TaskAdder(string s)
        {
            MessageBox.Show(s);
            comptasks.Add(new CompTaskcs());
            comptasks[comptasks.Count - 1].Top = 50;         
            tabcontrol.TabPages[1].Controls.Add(comptasks[comptasks.Count - 1]);// add tasks to second page
            if (comptasks.Count > 1) comptasks[comptasks.Count - 1].Top = comptasks[comptasks.Count - 2].Top + 75;
        }
        public void getPrograms()//00000000000000000000000
        {

            while (true)
            {
                string s="";
                var processes = Process.GetProcesses();
                foreach (var process in processes)
                    s += process.ProcessName;
              
                for (int i = 0; i < 6; i++)
                {            
                    if (s.Contains(myPrograms[i]))
                    {
                        TaskAdder(myPrograms[i]); }// add task for this program
                }
             Thread.Sleep(1000 * 10);// sleep for 10 sec         
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            myPrograms.Add("chrome");  //HW tasks
            myPrograms.Add("calc");
            myPrograms.Add("WINWORD");

            myPrograms.Add("eclipse");// programming tasks
            myPrograms.Add("devenv");       
            myPrograms.Add("sublime");

            progs = new Thread(getPrograms);
            progs.IsBackground = false;
            progs.Start();
        }
 
        private void button4_Click(object sender, EventArgs e)
        {

            timer1.Start();
        }
        public bool stoptime()
        {
            int count = 0;
            for (int i = 0; i < tasks.Count(); i++)
            {
                if (tasks[i].finished == false)
                { return false;  }
                else
                    count++;
            }
            if (count == tasks.Count)
                return true;
            return false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (stoptime() == true)
                timer1.Stop();
            else
            {
                chart1.Series.Clear();
                if (tasks[tasks.Count - 1].taskName != "")
                {
                    chart1.Series.Add(tasks[tasks.Count - 1].taskName).YValueMembers = tasks[tasks.Count - 1].taskName;
                    Series s = chart1.Series[tasks[tasks.Count - 1].taskName];

                    s.ChartType = SeriesChartType.Pie;

                    for (int i = 0; i < tasks.Count; i++)
                    {
                        s.Points.AddXY(tasks[i].taskName, tasks[i].time);
                    }
                }
                else

                {
                    chart1.Series.Add(tasks[tasks.Count - 2].taskName).YValueMembers = tasks[tasks.Count - 2].taskName;
                    Series s = chart1.Series[tasks[tasks.Count - 2].taskName];

                    s.ChartType = SeriesChartType.Pie;

                    for (int i = 0; i < tasks.Count; i++)
                    {
                        s.Points.AddXY(tasks[i].taskName, tasks[i].time);
                    }
                }
            }
        }
    }
}
