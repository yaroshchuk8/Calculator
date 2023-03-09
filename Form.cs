namespace Calculator;

public partial class Form : System.Windows.Forms.Form
{
    public Form()
    {
        InitializeComponent();
    }

    private const float MaxFontSize = 26.0f;
    private const float MinFontSize = 11.0f;

    private bool numberClicked;
    private bool operatorClicked;
    private bool commaClicked;
    private bool equalsClicked;

    private string[] priority = { "*/", "+-" };

    private void NumberClick(object sender, EventArgs e)
    {
        numberClicked = true;
        operatorClicked = false;

        // basic replacement of the first digit (+ case after buttonEquals_Click to clear the result)
        if (TextBox.Text == "0" || equalsClicked)
        {
            TextBox.Text = ((Button)sender).Text;
            equalsClicked = false;
            return;
        }

        TextBox.Text += ((Button)sender).Text;
    }

    private void OperatorClick(object sender, EventArgs e)
    {
        // issue #1 & #4 fix (|| ((Button)sender).Text == textBox.Text[^2] effective?)
        if (Supportive.LastElIsCommaOrMinus(TextBox.Text)) return; 

        // sign addition
        if (!operatorClicked) TextBox.Text += " " + ((Button)sender).Text + " "; 
        
        // sign replacement
        else TextBox.Text = TextBox.Text.Remove(TextBox.Text.Length - 2, 2) + ((Button)sender).Text + " ";

        numberClicked = false;
        operatorClicked = true;
        commaClicked = false;
        equalsClicked = false;
    }

    private void ButtonEquals_Click(object sender, EventArgs e)
    {
        // the following code should be executed only if the string has such format "number operation number ... number" (required to end with a number)
        if (!numberClicked) return;

        List<string> operators = new(TextBox.Text.Split(" "));
        List<decimal> numbers = new();

        // operators(list) is empty after .Calculate(), numbers(list) contains answer as its first element and nothing else
        try
        {
            Supportive.SortLists(ref operators, ref numbers);
            Supportive.Calculate(ref operators, ref numbers, priority);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error");
            ButtonClearAll.PerformClick();
            return;
        }

        // issue #3 (trimming unnecessary zeros)
        TextBox.Text = Supportive.TrimZerosAndComma(numbers[0].ToString());
        TextBoxResult.Text = "Ans = " + TextBox.Text;

        // issue #8 (commaClicked state if result is fractional number)
        commaClicked = TextBox.Text.Contains(',') ? true : false;

        equalsClicked = true;
        numberClicked = false;
    }

    private void ButtonComma_Click(object sender, EventArgs e)
    {
        // comma can be placed only once in a single number
        if (commaClicked) return;

        TextBox.Text += ",";

        commaClicked = true;
        operatorClicked = false;
        equalsClicked = false;
    }

    private void ButtonClear_Click(object sender, EventArgs e)
    {
        // logic for single-digit input
        // {
        if (TextBox.Text == "0") return;

        else if (TextBox.Text.Length == 1)
        {
            TextBox.Text = "0";
            return;
        }
        // }

        equalsClicked = false;

        // deleting beforeSpace, sign and afterSpace | defining bools' state in case lastEl = operator
        if (TextBox.Text[^1] == ' ')
        {
            TextBox.Text = TextBox.Text[..^3];

            if (Supportive.LastElement(TextBox.Text).Contains(',')) commaClicked = true;
            
            numberClicked = true;
            operatorClicked = false;

            return;
        }

        // deleting last digit or comma and defining bools' state in case lastEl = number | comma
        // {
        if (TextBox.Text[^1] == ',') commaClicked = false;

        if (TextBox.Text[^2] == ' ')
        {
            numberClicked = false;
            operatorClicked = true;
        }

        TextBox.Text = TextBox.Text[..^1];
        // }
    }

    private void ButtonClearAll_Click(object sender, EventArgs e)
    {
        if (TextBox.Text == "0") return;

        TextBoxResult.Text = string.Empty;
        TextBox.Text = "0";

        numberClicked = false;
        operatorClicked = false;
        commaClicked = false;
        equalsClicked = false;
    }

    private void TextBox_TextChanged(object sender, EventArgs e)
    {
        // issue #7 (auto resizable text)
        bool decreased = false;

        // decreasing font size
        while (TextBox.Font.Size > MinFontSize)
        {
            SizeF size = TextRenderer.MeasureText(TextBox.Text, TextBox.Font);
            if (TextBox.Width >= size.Width) break;

            TextBox.Font = new Font(TextBox.Font.FontFamily, TextBox.Font.Size - 1.25f);
            decreased = true;
        }

        // if font size was decreased, there's no need in increasing
        if (decreased) return;

        // increasing font size
        while (TextBox.Font.Size < MaxFontSize)
        {
            Font font = new(TextBox.Font.FontFamily, TextBox.Font.Size + 1.25f);
            SizeF sizeNext = TextRenderer.MeasureText(TextBox.Text, font);

            if (sizeNext.Width > TextBox.Width) return;

            TextBox.Font = font;
        }
    }

    /*
    private void Form1_Load(object sender, EventArgs e)
    {

    }
    */
}