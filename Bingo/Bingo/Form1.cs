using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Speech.Synthesis;

namespace Bingo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = ".";
            label2.Text = ".";
            label3.Text = ".";
            label4.Text = ".";
            label5.Text = ".";
            label6.Text = ".";
            label7.Text = ".";
        }
        int currentNumber;
        List<int> pastNumber = new List<int>();
        SpeechSynthesizer syn = new SpeechSynthesizer();
        Task t;
        bool pause_ = true;

        void GenerateNumber()
            {
                Random rnd = new Random();
                do
                {
                    currentNumber = rnd.Next(0, 99);
                }
                while (pastNumber.Contains(currentNumber));
                pastNumber.Add(currentNumber);

                if(InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        label1.Refresh();
                        label1.Text = currentNumber.ToString();

                        if (pastNumber.Count == 2)
                        {
                            label7.Text = pastNumber[pastNumber.Count - 2].ToString();
                        }
                        else if (pastNumber.Count == 3)
                        {
                            label6.Text = pastNumber[pastNumber.Count - 3].ToString();
                            label7.Text = pastNumber[pastNumber.Count - 2].ToString();
                        }
                        else if (pastNumber.Count == 4)
                        {
                            label5.Text = pastNumber[pastNumber.Count - 4].ToString();
                            label6.Text = pastNumber[pastNumber.Count - 3].ToString();
                            label7.Text = pastNumber[pastNumber.Count - 2].ToString();

                        }
                        else if (pastNumber.Count == 5)
                        {
                            label4.Text = pastNumber[pastNumber.Count - 5].ToString();
                            label5.Text = pastNumber[pastNumber.Count - 4].ToString();
                            label6.Text = pastNumber[pastNumber.Count - 3].ToString();
                            label7.Text = pastNumber[pastNumber.Count - 2].ToString();
                        }
                        else if (pastNumber.Count == 6)
                        {
                            label3.Text = pastNumber[pastNumber.Count - 6].ToString();
                            label4.Text = pastNumber[pastNumber.Count - 5].ToString();
                            label5.Text = pastNumber[pastNumber.Count - 4].ToString();
                            label6.Text = pastNumber[pastNumber.Count - 3].ToString();
                            label7.Text = pastNumber[pastNumber.Count - 2].ToString();
                        }
                        else if (pastNumber.Count > 7 || pastNumber.Count == 7)
                        {
                            label2.Text = pastNumber[pastNumber.Count - 7].ToString();
                            label3.Text = pastNumber[pastNumber.Count - 6].ToString();
                            label4.Text = pastNumber[pastNumber.Count - 5].ToString();
                            label5.Text = pastNumber[pastNumber.Count - 4].ToString();
                            label6.Text = pastNumber[pastNumber.Count - 3].ToString();
                            label7.Text = pastNumber[pastNumber.Count - 2].ToString();
                        }

                        if (pastNumber.Count == 99)
                        {
                            MessageBox.Show("Finished");

                        }

                    }));
                }

                syn.Speak("The current number is" + currentNumber.ToString());

            }
            private void button1_Click(object sender, EventArgs e)
        {
            t = Task.Factory.StartNew(() => {
            
                for (int i=0;i<100;i++)
                {
                    Thread.Sleep(1000);
                    if(pause_==false)
                    {
                        pause_ = true;
                        t.Wait();
                    }
                    GenerateNumber();

                }
            
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pause_ = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //clear pastnumber list
            pastNumber.Clear();
            pause_ = false;
            button2.Text = "Start_manual";
            label1.Text = ".";
            label2.Text = ".";
            label3.Text = ".";
            label4.Text = ".";
            label5.Text = ".";
            label6.Text = ".";
            label7.Text = ".";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Text = "NEXT";
            Task.Factory.StartNew(() =>
            {
                GenerateNumber();
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
