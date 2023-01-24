using MyLibrary;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private bool numberClicked;
        private bool operatorClicked;
        private bool dotClicked;
        private bool equalsClicked;

        private string[] priority = { "*/", "+-" };

        List<string> operators;
        List<double> numbers;

        private void numberClick(object sender, EventArgs e)
        {
            numberClicked = true;
            operatorClicked = false;

            // basic replacement of the first number (+ case after buttonEquals_Click to clear the result)
            if (textBox.Text == "0" || equalsClicked)
            {
                textBox.Text = ((Button)sender).Text;
                equalsClicked = false;
                return;
            }

            textBox.Text += ((Button)sender).Text;
        }

        private void operatorClick(object sender, EventArgs e)
        {
            // (0 + , - 0) case (probably temporary) fix
            // || ((Button)sender).Text == textBox.Text[textBox.Text.Length - 1] effective?
            if (Supportive.lastElIsComma(textBox.Text)) return;

            // sign addition
            if (!operatorClicked) textBox.Text += " " + ((Button)sender).Text + " "; 
            
            // sign replacement
            else textBox.Text = textBox.Text.Remove(textBox.Text.Length - 2, 2) + ((Button)sender).Text + " ";

            numberClicked = false;
            operatorClicked = true;
            dotClicked = false;
            equalsClicked = false;
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            // the following code should be executed only if the string has such format "number operation number ... number" (required to end with a number)
            if (!numberClicked) return;

            operators = new List<string>(textBox.Text.Split(" "));
            numbers = new List<double>();

            // operators(list) is empty after .Calculate(), numbers(list) contains answer as its first element and nothing else
            try
            {
                Supportive.SortLists(ref operators, ref numbers);
                Supportive.Calculate(ref operators, ref numbers, priority);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                buttonClearAll.PerformClick();
                return;
            }

            label.Text = $"Answer = {numbers[0]}";
            textBox.Text = numbers[0].ToString();

            equalsClicked = true;
            numberClicked = false;
        }

        private void buttonDot_Click(object sender, EventArgs e)
        {
            // dot can be placed only once in a single number
            if (dotClicked) return;

            textBox.Text += ",";

            dotClicked = true;
            operatorClicked = false;
            equalsClicked = false;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // logic for single-digit input
            // {
            if (textBox.Text == "0") return;

            else if (textBox.Text.Length == 1)
            {
                textBox.Text = "0";
                return;
            }
            // }

            equalsClicked = false;

            // deleting beforeSpace, sign and afterSpace | defining bools' state in case lastEl = operator
            if (textBox.Text[textBox.Text.Length - 1] == ' ')
            {
                // deleting beforeSpace, a sign, and afterSpace
                textBox.Text = textBox.Text[..^3];

                // defining dotClicked value
                if (Supportive.LastElement(textBox.Text).Contains(',')) dotClicked = true;
                
                numberClicked = true;
                operatorClicked = false;

                return;
            }

            // deleting last digit or comma and defining bools' state in case lastEl = number | comma
            // {
            if (textBox.Text[textBox.Text.Length - 1] == ',') dotClicked = false;

            if (textBox.Text[textBox.Text.Length - 2] == ' ')
            {
                numberClicked = false;
                operatorClicked = true;
            }

            textBox.Text = textBox.Text[..^1];
            // }
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            if (textBox.Text == "0") return;

            textBox.Text = "0";
            numberClicked = false;
            operatorClicked = false;
            dotClicked = false;
            equalsClicked = false;
        }

        /*
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        */
    }
}