using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CreditCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                if (decimal.TryParse(textBox1.Text, out decimal creditAmount) &&
                    int.TryParse(textBox2.Text, out int term) &&
                    decimal.TryParse(textBox3.Text, out decimal rate))
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
                    // Расчет ежемесячного платежа
                    decimal monthlyPayment = CalculateMonthlyPayment(creditAmount, term, rate);
                    label7.Text = $"{monthlyPayment:C0}";

                    // Расчет общей переплаты
                    decimal totalOverpayment = CalculateTotalOverpayment(creditAmount, term, rate, monthlyPayment);
                    label8.Text = $"{totalOverpayment:C0}";
                }
                else
                {
                    MessageBox.Show("Проверьте правильность ввода данных.");
                }

                stopwatch.Stop();
                MessageBox.Show($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private decimal CalculateMonthlyPayment(decimal creditAmount, int term, decimal rate)
        {
            decimal monthlyRate = rate / 12 / 100;
            decimal factor = monthlyRate * (decimal)Math.Pow(1 + (double)monthlyRate, term) /
                             ((decimal)Math.Pow(1 + (double)monthlyRate, term) - 1);
            decimal monthlyPayment = factor * creditAmount;
            return monthlyPayment;
        }

        private decimal CalculateTotalOverpayment(decimal creditAmount, int term, decimal rate, decimal monthlyPayment)
        {
            decimal totalOverpayment = monthlyPayment * term - creditAmount;
            return totalOverpayment;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = termTrackBar.Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            termTrackBar.Minimum = 1;
            termTrackBar.Maximum = 12;
            termTrackBar.Value = 1;

            termTrackBar2.Minimum = 1;
            termTrackBar2.Maximum = 14;
            termTrackBar2.Value = 1;

            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;

            label7.Text = "0 ₽";
            label8.Text = "0 ₽";

            // Добавление обработчика события "MouseDown" для формы
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // Начало перемещения формы при нажатии на любую ее область
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ReleaseCapture();

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            textBox3.Text = termTrackBar2.Value.ToString();
        }
    }
}
