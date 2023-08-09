using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TempSensorWithLCD
{
    public partial class TempSensorLCD : Form
    {
        public TempSensorLCD()
        {
            InitializeComponent();
        }

        private void TempSensorLCD_Load(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening serial port: " + ex.Message);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            progressBar1.Value = trackBar1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = int.Parse(textBox1.Text);
            trackBar1.Value = int.Parse(textBox1.Text);
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string data = sp.ReadExisting();
           // string data = serialPort1.ReadLine();
           // ;
           UpdateTextBox(data);
        }

        private void UpdateTextBox(string data)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new Action<string>(UpdateTextBox), data);
            }
            else
            {
                string extractedString;
                //string fisult;
                string billu = data;
                int kk = billu.Length;
                int index1 = billu.IndexOf('#', 0);
                int index2= billu.IndexOf('@', kk-1);
                if (index1 != -1 || index2 != -1)
                {
                    try
                    {
                        extractedString = billu.Substring(index1 + 1, index2 - index1 - 1);
                        int result = (int.Parse(extractedString));
                        string fisult = result.ToString();
                        progressBar1.Value = int.Parse(fisult)-2;
                        textBox1.Text = "Temperature: " + fisult;

                    }
                    catch {; }

                }
                
            }
        }
    }
}
