using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBKey
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        public int gettime = 0;
        public int Day = 0;
        public int Hour = 0;
        public int Minute = 0;
        public int Second = 0;
        public int Second2 = 0;
        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.time = Global.Encrypt(Convert.ToString(Second2));
            Properties.Settings.Default.Save();
            File.WriteAllText(Application.StartupPath + @"\license.lic", Global.Encrypt(Convert.ToString(Second2)));
            Application.Exit();
        }
        //DateTime endTime = new DateTime(2019, 06, 23, 0, 0, 0);
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!backgroundWorker1.CancellationPending)
            {

                label2.Text = Convert.ToString(Day + " Day " + Hour + " Hour " +  Minute +" Minute " + Second + " Second");
                Second2--;
                Second--;
                if (Second <= 0)
                {
                    Minute--;
                    Second = 59;
                }
                if (Minute <= 0)
                {
                    Hour -= 1;
                    Minute = 59;
                }
                if (Hour < 0)
                {
                    Day -= 1;
                    Hour = 23;
                }
                if(Day <= 0)
                {
                    Day = 0;
                }
                Thread.Sleep(1000);

            }
            
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Application.StartupPath + @"\license.lic"))
            {
                if(Properties.Settings.Default.time != "0")
                {
                    try
                    {
                        gettime = Convert.ToInt32(Global.Decrypt(Properties.Settings.Default.time));
                        Second2 = Convert.ToInt32(gettime);
                        label4.Text = Environment.MachineName;
                        Second = Convert.ToInt32(gettime);
                        for (int i = 0; gettime != 0;)
                        {
                            if (Second > 59)
                            {
                                Minute++;
                                Second -= 60;
                                if (Minute > 59)
                                {
                                    Hour++;
                                    Minute -= 60;
                                    if (Hour > 23)
                                    {
                                        Day++;
                                        Hour -= 24;
                                    }
                                }
                            }
                            gettime--;
                            // i++;
                        }
                        label2.Text = Convert.ToString(Day + " Day " + Hour + " Hour " + Minute + " Minute " + Second + " Second");
                        backgroundWorker1.RunWorkerAsync();
                    }
                    catch
                    {
                        MessageBox.Show("Can't find the key");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Key not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string text = File.ReadAllText(Application.StartupPath + @"\license.lic");
                gettime = Convert.ToInt32(Global.Decrypt(text));
                Second2 = Convert.ToInt32(gettime);
                label4.Text = Environment.MachineName;
                Second = Convert.ToInt32(gettime);
                for (int i = 0; gettime != 0;)
                {
                    if (Second > 59)
                    {
                        Minute++;
                        Second -= 60;
                        if (Minute > 59)
                        {
                            Hour++;
                            Minute -= 60;
                            if (Hour > 23)
                            {
                                Day++;
                                Hour -= 24;
                            }
                        }
                    }
                    gettime--;
                    // i++;
                }
                //label2.Text = Convert.ToString(Day + " Day " + Hour + " Hour " + Minute + " Minute " + Second + " Second");
                backgroundWorker1.RunWorkerAsync();
            }
           
        }

    }
}
