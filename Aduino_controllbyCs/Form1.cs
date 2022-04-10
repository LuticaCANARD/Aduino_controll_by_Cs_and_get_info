using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Aduino_controllbyCs
{
    public partial class Form1 : Form
    {
        private SerialPort arduinoSerial;
        private string[] frequeny = { "9600","19200","4800" };
        public Form1()
        {
            InitializeComponent();
            arduinoSerial = new SerialPort();
            string[] sp = SerialPort.GetPortNames();
            comboBox1.DataSource = sp;
            comboBox2.DataSource = frequeny;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Activated(object sender, System.EventArgs e)
        {
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (arduinoSerial.IsOpen)
            {
                arduinoSerial.Close();
            }
            if(!arduinoSerial.IsOpen)
            {
                arduinoSerial.PortName = comboBox1.Text;
                arduinoSerial.BaudRate =int.Parse(comboBox2.Text);
                arduinoSerial.DataBits = 8;
                arduinoSerial.Parity = Parity.None;
                arduinoSerial.StopBits = StopBits.One;
                arduinoSerial.DataReceived += new SerialDataReceivedEventHandler(arduinoSerial_DataReceived);

                try
                {
                    arduinoSerial.Open();
                   
                }
                catch (Exception ex)
                {
                    getSignal.Text = ex.Message;
                }
            }
            else
            {
                getSignal.Text = "지정된" + comboBox1.Text + "는 이미 열려있음";
            }

        }
        private byte[] strToByte(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return bytes; 
        }
        private string byteToStr(byte[] bt)
        {
            string str = Encoding.UTF8.GetString(bt);
            return str;
        }

        private void sentBtn_Click(object sender, EventArgs e)
        {
            
            if (arduinoSerial.IsOpen == false)
            {
                return;
            }
            /* string mes = writeSignal.Text + "\n";
             byte[] datas = strToByte(mes); //'1' 전송 뒤에 개행문자 \n을 필수로 붙혀야 함
             arduinoSerial.Write(datas, 0, datas.Length);
             label5.Text = mes;*/
            arduinoSerial.WriteLine(writeSignal.Text + "\n");
        }
        private void get_byte()
        {
            getSignal.Text = arduinoSerial.ReadLine();
        }

        private void getSignal_TextChanged(object sender, EventArgs e)
        {

        }
        private void arduinoSerial_DataReceived(object o, EventArgs e)
        {
            if (arduinoSerial.IsOpen == false) { return; };

                string resive_datap = arduinoSerial.ReadLine();
                if (this.getSignal.InvokeRequired == true)
                {
                    this.getSignal.Invoke((MethodInvoker)delegate
                     {
                         getSignal.Text += string.Format("{0} ", resive_datap);
                     });
                }
                else
                {
                    getSignal.Text += string.Format("{0} ", resive_datap);
                }
           
        }
    }
}
