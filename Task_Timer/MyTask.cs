using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_Timer
{
    public partial class MyTask : UserControl
    {
        
        public MyTask()
        {
            InitializeComponent();
        }
        public string taskName = "";
        public int time = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);//deletion
            int index = Form1.tasks.IndexOf(this);
            Form1.tasks.Remove(this);//remove from list

            if (Form1.tasks.Count > 1)
            {
                for (int i = index; i < Form1.tasks.Count; i++)
                    Form1.tasks[i].Top -= 75;//move upper
            }
            else if (Form1.tasks.Count == 1)//move to top
                Form1.tasks[0].Top = 50;
            timer1.Stop();
        }
        public int TimeConvert(string h,string m,string s)// get seconds from numericupdowns
        {
            return int.Parse(h) * 60 * 60 + int.Parse(m) * 60 + int.Parse(s);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Save")
            {
                if (textBox1.Text == "")
                    MessageBox.Show("Name is required!");
                else if ((TimeConvert(numericUpDown1.Text, numericUpDown2.Text, numericUpDown3.Text) == 0) && (checkBox1.Checked == false))
                    MessageBox.Show("Set time");
                else
                {

                    textBox1.Enabled = false;//title
                    textBox2.Enabled = false;// info
                    numericUpDown1.Enabled = false;//goal
                    numericUpDown2.Enabled = false;
                    numericUpDown3.Enabled = false;
                    numericUpDown4.Enabled = false;//spend time
                    numericUpDown5.Enabled = false;
                    numericUpDown6.Enabled = false;
                    button1.Enabled = true;// start
                    button2.Enabled = true;//restart
                    checkBox1.Enabled = false;//indefinite
                    time = TimeConvert(numericUpDown4.Text, numericUpDown5.Text, numericUpDown6.Text);// zero or firstly set time
                    if (checkBox1.Checked == false)// we have defined goal
                    {
                        progressBar1.Maximum = TimeConvert(numericUpDown1.Text, numericUpDown2.Text, numericUpDown3.Text);//get goal for progressbar and for check
                        goal = progressBar1.Maximum;
                        time = TimeConvert(numericUpDown4.Text, numericUpDown5.Text, numericUpDown6.Text);// starting point
                        progressBar1.Value = time;
                    }
                    else//goal is not defined
                    {
                        numericUpDown1.Text = "00";
                        numericUpDown2.Text = "00";
                        numericUpDown3.Text = "00";

                    }
                    taskName = textBox1.Text;//get name for piechart
                    button3.Text = "Edit";
                }
            }
            else if (button3.Text == "Edit")//start editing
            {
                timer1.Stop();// stop timer and progressbar
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = true;
                numericUpDown4.Enabled = true;
                numericUpDown5.Enabled = true;
                numericUpDown6.Enabled = true;
                checkBox1.Enabled = true;
                button1.Enabled = false;//cannot start without goal and title
                button2.Enabled = false;// and also restart
                button3.Text = "Save";// their presence are checked after "save" is set
                progressBar1.Style = ProgressBarStyle.Blocks;// stop progressbar for indef. case

            }
        }
        public void changeTime()//update timer values, max is 23.59.59
        {
            int seconds = int.Parse(numericUpDown6.Text);
            int minutes = int.Parse(numericUpDown5.Text);
            int hours = int.Parse(numericUpDown4.Text);
            if (seconds + 1 < 59)
                seconds++;
            else
            {
                seconds = 0;
                if (minutes + 1 < 59)
                    minutes++;
                else
                {
                    minutes = 0;
                    if (hours + 1 < 23)
                        hours++;
                    else
                        MessageBox.Show("24 hours");

                }
            }
            numericUpDown4.Text = hours.ToString();
            numericUpDown5.Text = minutes.ToString();
            numericUpDown6.Text = seconds.ToString();
        }
        public int goal;
        public bool finished = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
           
            if (checkBox1.Checked == false)// we've a defined time
            {
                
               time= TimeConvert(numericUpDown4.Text,numericUpDown5.Text,numericUpDown6.Text);
             
                if (time == goal)// check if we reached the goal
                {
                    timer1.Stop();
                    finished = true;
                    MessageBox.Show(this.taskName+" is completed!");
                }
                else
                {
                    changeTime();//add one second
                    progressBar1.Value++;// change progressbar
                }

            }
            else// we don't have defined  time
            {
                time = TimeConvert(numericUpDown4.Text, numericUpDown5.Text, numericUpDown6.Text);
                progressBar1.Style = ProgressBarStyle.Marquee;// progressbar which runs always
                changeTime();
                
            }
        }

        private void button2_Click(object sender, EventArgs e)// reset spend time to zero
        {
            progressBar1.Value = 0;
            numericUpDown4.Text = "00";
            numericUpDown5.Text = "00";
            numericUpDown6.Text = "00";
            time = 0;
        }

        private void button1_Click(object sender, EventArgs e)// start, activate timer
        {
            timer1.Interval = 1000;
            timer1.Start();
        }
    }
}
