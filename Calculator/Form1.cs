using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        string decRes = "";
        double result = 0;
        string hexaResult = "";
        string operation = "";
        bool decim, binary, hexa, operatorClicked;
        public Form1()
        {
            InitializeComponent();
        }
        private void buttonClick(object sender, EventArgs e)
        {
            if ((textBox.Text == "0") || (operatorClicked))
                textBox.Clear();

            operatorClicked = false;
            Button button = (Button)sender;
            Number number = new Number(button.Text);
            if (number.value() == ".")
            {
                if (!textBox.Text.Contains("."))
                {
                    textBox.Text += number.value();
                }
            } else textBox.Text += number.value();
        }

        private void operatorClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Operator oper = new Operator(button.Text);
            if (!hexa)
            {
                if (result != 0)
                {
                    buttonEqual.PerformClick();
                    operation = oper.value();
                    history.Text = result + " " + operation;
                    operatorClicked = true;
                }
                else
                {
                    operation = oper.value();
                    result = double.Parse(textBox.Text);
                    operatorClicked = true;
                }
                textBox.Text = "";
            }
            else
            {
                if (hexaResult != "0")
                {
                    buttonEqual.PerformClick();
                    operation = oper.value();
                    history.Text = hexaResult + " " + operation;
                    operatorClicked = true;
                }
                else
                {
                    operation = oper.value();
                    hexaResult = textBox.Text;
                    operatorClicked = true;
                }
                textBox.Text = "";
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBox.Text = "0";
            hexaResult = "";
            result = 0;
            operation = "";
            operatorClicked = false;
            history.Text = "";
            label1.Text = "0";
            label2.Text = "0";
            label3.Text = "0";
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Operator oper = new Operator(textBox.Text);
            if (oper.value() != "")
            {
                textBox.Text = oper.delete();
                if (oper.value().Length == 0) result = 0;
                else result = double.Parse(oper.value());
            }
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            if (decim)
            {
                switch (operation)
                {
                    case "+":
                        textBox.Text = (result + double.Parse(textBox.Text)).ToString();
                        break;
                    case "-":
                        textBox.Text = (result - double.Parse(textBox.Text)).ToString();
                        break;
                    case "*":
                        textBox.Text = (result * double.Parse(textBox.Text)).ToString();
                        break;
                    case "/":
                        textBox.Text = (result / double.Parse(textBox.Text)).ToString();
                        break;
                    default:
                        break;
                }
                label1.Text = textBox.Text;
                label2.Text = Convert.ToString((int)double.Parse(textBox.Text), 2);
                label3.Text = Convert.ToString((int)double.Parse(textBox.Text), 16);
                result = double.Parse(textBox.Text);
                history.Text = "";
            }
            if (binary)
            {
                switch (operation)
                {
                    case "+":
                        decRes = (Convert.ToInt32(result.ToString(), 2) + Convert.ToInt32(textBox.Text, 2)).ToString();
                        textBox.Text = Convert.ToString((int)double.Parse(decRes), 2);
                        break;
                    case "-":
                        decRes = (Convert.ToInt32(result.ToString(), 2) - Convert.ToInt32(textBox.Text, 2)).ToString();
                        textBox.Text = Convert.ToString((int)double.Parse(decRes), 2);
                        break;
                    case "*":
                        decRes = (Convert.ToInt32(result.ToString(), 2) * Convert.ToInt32(textBox.Text, 2)).ToString();
                        textBox.Text = Convert.ToString((int)double.Parse(decRes), 2);
                        break;
                    case "/":
                        decRes = (Convert.ToInt32(result.ToString(), 2) / Convert.ToInt32(textBox.Text, 2)).ToString();
                        textBox.Text = Convert.ToString((int)double.Parse(decRes), 2);
                        break;
                    default:
                        break;
                }
                label1.Text = decRes;
                label2.Text = textBox.Text;
                label3.Text = Convert.ToString((int)double.Parse(textBox.Text), 16);
                result = double.Parse(textBox.Text);
                history.Text = "";
            }
            if (hexa)
            {
                switch (operation)
                {
                    case "+":
                        decRes = (Convert.ToInt32(hexaResult, 16) + Convert.ToInt32(textBox.Text, 16)).ToString();
                        textBox.Text = Convert.ToString((int)double.Parse(decRes), 16);
                        break;
                    case "-":
                        decRes = (Convert.ToInt32(hexaResult, 16) - Convert.ToInt32(textBox.Text, 16)).ToString();
                        textBox.Text = Convert.ToString((int)double.Parse(decRes), 16);
                        break;
                    case "*":
                        decRes = (Convert.ToInt32(hexaResult, 16) * Convert.ToInt32(textBox.Text, 16)).ToString();
                        textBox.Text = Convert.ToString((int)double.Parse(decRes), 16);
                        break;
                    case "/":
                        decRes = (Convert.ToInt32(hexaResult, 16) / Convert.ToInt32(textBox.Text, 16)).ToString();
                        textBox.Text = Convert.ToString((int)double.Parse(decRes), 16);
                        break;
                    default:
                        break;
                }
                label1.Text = decRes;
                //label2.Text = Convert.ToString((int)double.Parse(decRes), 2);
                label3.Text = textBox.Text;
                hexaResult = textBox.Text;
                history.Text = "";
            }
        }

        private void buttonInverse_Click(object sender, EventArgs e)
        {
            textBox.Text = (1 / double.Parse(textBox.Text)).ToString();
            result = 1 / double.Parse(textBox.Text);
        }

        private void buttonStatus_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length != 0 && textBox.Text != "0")
            {
                textBox.Text = (double.Parse(textBox.Text) * (-1)).ToString();
            }
        }

        private void buttonDecimal_Click(object sender, EventArgs e)
        {
            buttonClear.PerformClick();
            decim = true;
            binary = false;
            hexa = false;
            buttonA.Hide();
            buttonB.Hide();
            buttonC.Hide();
            buttonD.Hide();
            buttonE.Hide();
            buttonF.Hide();
            buttonInverse.Show();
            buttonStatus.Show();
            button2.Show();
            button3.Show();
            button4.Show();
            button5.Show();
            button6.Show();
            button7.Show();
            button8.Show();
            button9.Show();
            buttonDot.Show();
        }

        private void buttonBinary_Click(object sender, EventArgs e)
        {
            buttonClear.PerformClick();
            binary = true;
            decim = false;
            hexa = false;
            buttonA.Hide();
            buttonB.Hide();
            buttonC.Hide();
            buttonD.Hide();
            buttonE.Hide();
            buttonF.Hide();
            buttonInverse.Hide();
            buttonStatus.Hide();
            button2.Hide();
            button3.Hide();
            button4.Hide();
            button5.Hide();
            button6.Hide();
            button7.Hide();
            button8.Hide();
            button9.Hide();
            buttonDot.Hide();
        }

        private void buttonHexadecimal_Click(object sender, EventArgs e)
        {
            buttonClear.PerformClick();
            hexa = true;
            decim = false;
            binary = false;
            buttonA.Show();
            buttonB.Show();
            buttonC.Show();
            buttonD.Show();
            buttonE.Show();
            buttonF.Show();
            buttonInverse.Hide();
            buttonStatus.Hide();
            button2.Show();
            button3.Show();
            button4.Show();
            button5.Show();
            button6.Show();
            button7.Show();
            button8.Show();
            button9.Show();
            buttonDot.Hide();
        }
    }
}
