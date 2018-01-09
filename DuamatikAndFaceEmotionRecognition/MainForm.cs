using System;
using System.Windows.Forms;
using Inversion;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuamatikAndFaceEmotionRecognition
{
    public partial class MainForm : Form
    {
        private LogicalOperator processing;
        private WebCam webcam;
        DBConnect mydatabase = new DBConnect();
        List<String>[] list = new List<string>[1];
        public int counter = 0;
        public static int IdCounter = 0;
        string query = "SELECT Count(*) FROM dua_db";

        public MainForm()
        {
            InitializeComponent();
            processing = new LogicalOperator();
            Load += new EventHandler(MainForm_Load);
            button3.Visible = true;
            label1.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;
            counter = mydatabase.Count(query);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            /*enable timers*/
            timer1.Enabled = true;
            timer2.Enabled = true;
            /*create Webcam obj*/
            webcam = new WebCam();
            webcam.InitializeWebCam(ref pictureBox1);
            /*Start webcam*/
            webcam.Start();
        }
        /*Kayan yazi*/
        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = label2.Text.Substring(1) + label2.Text.Substring(0, 1);
            label2.Text = label2.Text.Substring(1) + label2.Text.Substring(0, 1);
        }
        /*Tarih ve saat*/
        private void timer2_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToLongTimeString();
            label5.Text = DateTime.Now.ToLongDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            webcam.Stop();
            button3.ForeColor = Color.Green;
           
            blink_lbl(5);
            //Application.Idle -= new EventHandler(MainForm_Load);
            //this.Visible = false; /* invisible main form but didnt destruct.*/

        }

        private void button3_Click(object sender, EventArgs e)
        {

            webcam.Start();
        }
        public void blink_lbl(int count)
        {
            int ms = 100;
            for (int i = 0; i < count; i++)
            {

                if (button3.ForeColor == Color.Green)
                    button3.ForeColor = Color.Red;
                else
                    button3.ForeColor = Color.Green;

                Application.DoEvents();
                Thread.Sleep(ms);

            }
        }
        /*now show prayers..*/
        private void button2_Click(object sender, EventArgs e)
        {

            list = mydatabase.SelectPrayers(counter);
            if (counter == IdCounter)
                IdCounter = 0;
            MessageBox.Show(list[IdCounter].ElementAt(0));
            IdCounter++;
        }

        
    }

}
