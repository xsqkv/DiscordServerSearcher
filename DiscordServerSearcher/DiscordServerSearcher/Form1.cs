using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace DiscordServerSearcher
{
    public partial class Form1 : Form
    {
        void Open(string URL)
        {
            const string PathToFile = @"C:\Program Files (x86)\Opera\launcher.exe";

            Process.Start(PathToFile, $"--ran-launcher --remote {URL}");
        }

        string GenerateString(int Length,bool Digits,bool L,bool U)
        {
            const string digits = "0123456789";

            const string l = "abcdefghijklmnopqrstuvwxyz";

            const string u = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string abc = "";

            if (Digits)
            {
                abc += digits;
            }

            if (L)
            {
                abc += l;
            }

            if (U)
            {
                abc += u;
            }

            Random random = new Random();

            string Result = "";

            for (int i = 0; i < Length; i++)
            {
                Result += abc[random.Next(abc.Length)];        
            }

            return Result;
        }

        bool HasServer(string URL)
        {
            WebClient client = new WebClient();
            string HTML = client.DownloadString(URL);
            return !HTML.Contains($@"<title>Discord</title>");
        }

        public Thread thread = new Thread(()=> { });

        public Form1()
        {
            InitializeComponent();
        }
        //Auto Search Start
        private void button1_Click(object sender, EventArgs e)
        {
            if (label10.Text == "Stoped")
            {
                label10.ForeColor = Color.Green;
                label10.Text = "Working";
                thread = new Thread(() =>
                {
                    bool Has = false;
                    while (Has != true)
                    {
                        if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                textBox1.Text = comboBox1.Text + GenerateString(int.Parse(textBox2.Text), checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);
                            });
                        }
                        else
                        {
                            MessageBox.Show("CheckBoxes can't be empty", "Err0r", MessageBoxButtons.OK);
                            break;
                        }
                        this.Invoke((MethodInvoker)delegate
                        {
                            label3.Text = "";
                        });
                        if (textBox1.Text.Length != 0)
                        {
                            Has = HasServer(textBox1.Text);
                            if (Has)
                            {
                                label3.ForeColor = Color.Green;
                                this.Invoke((MethodInvoker)delegate
                                {
                                    label3.Text = "True";
                                });
                            }
                            else
                            {
                                label3.ForeColor = Color.Red;
                                this.Invoke((MethodInvoker)delegate
                                {
                                    label3.Text = "False";
                                });
                            }
                        }
                        else
                        {
                            MessageBox.Show("Url can't be empty", "Err0r", MessageBoxButtons.OK);
                            break;
                        }
                        if (textBox3.Text != "0")
                        {
                            Thread.Sleep(int.Parse(textBox3.Text));
                        }
                    }
                    if (Has)
                    {
                        MessageBox.Show("Server is Find!", "Congratulations!", MessageBoxButtons.OK);
                        label10.ForeColor = Color.Red;
                        this.Invoke((MethodInvoker)delegate
                        {
                            label10.Text = "Stoped";
                        });
                    }
                    else
                    {
                        label10.ForeColor = Color.Red;
                        this.Invoke((MethodInvoker)delegate
                        {
                            label10.Text = "Stoped";
                        });
                    }
                });
                thread.Start();
            }
        }
        //Generate
        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked)
            {
                textBox1.Text = comboBox1.Text + GenerateString(int.Parse(textBox2.Text), checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);
            }
            else
            {
                MessageBox.Show("CheckBoxes can't be empty", "Err0r", MessageBoxButtons.OK);
            }
        }
        //Open
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0 && comboBox1.Text.Length != 0)
            {
                Open(textBox1.Text);
            }
            else
            {
                MessageBox.Show("Url or Type can't be empty", "Err0r", MessageBoxButtons.OK);
            }
        }
        //Check
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                label3.Text = "";
                bool Has = HasServer(textBox1.Text);
                if (Has)
                {
                    label3.ForeColor = Color.Green;
                    label3.Text = "True";
                }
                else
                {
                    label3.ForeColor = Color.Red;
                    label3.Text = "False";
                }
            }
            else
            {
                MessageBox.Show("Url can't be empty", "Err0r", MessageBoxButtons.OK);
            }
        }
        //Check BlackOut
        private void button5_Click(object sender, EventArgs e)
        {
            string ForeverURL = "https://discord.com/invite/lol";
            label6.Text = "";
            bool Has = HasServer(ForeverURL);
            if (!Has)
            {
                label6.ForeColor = Color.Red;
                label6.Text = "True";
            }
            else
            {
                label6.ForeColor = Color.Green;
                label6.Text = "False";
            }
        }
        //Auto Search Stop
        private void button6_Click(object sender, EventArgs e)
        {
            if (label10.Text == "Working")
            {
                label10.ForeColor = Color.Red;
                label10.Text = "Stoped";
                thread.Abort();
            }
        }
    }
}
