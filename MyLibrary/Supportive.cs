namespace MyLibrary;

public class Supportive
{
    public static void SortLists(ref List<string> operators, ref List<decimal> numbers)
    {
        for (int i = 0; i < operators.Count; i++)
        {
            numbers.Add(Decimal.Parse(operators[i]));
            operators.RemoveAt(i);
        }
    }

    public static void Calculate(ref List<string> operators, ref List<decimal> numbers, string[] priority)
    {
        for (int i = 0; i < priority.Length; i++)
            for (int j = 0; j < operators.Count; j++)
                if (priority[i].Contains(operators[j]))
                {
                    switch (operators[j])
                    {
                        case "+":
                            numbers[j] += numbers[j + 1];
                            break;
                        case "-":
                            numbers[j] -= numbers[j + 1];
                            break;
                        case "*":
                            numbers[j] *= numbers[j + 1];
                            break;
                        case "/":
                            numbers[j] /= numbers[j + 1];
                            break;
                    }
                    numbers.RemoveAt(j + 1);
                    operators.RemoveAt(j);
                    j--;
                }
    }

    // returns a number if lastEl == number and emptyString ("") if lastEl == operator 
    public static string LastElement(string str)
    {
        int i;
        for (i = str.Length - 1; i >= 0 && str[i] != ' '; i--) ;

        return str[(i + 1)..];
    }

    public static bool LastElIsCommaOrMinus(string str)
    {
        if ((str[^1] == ',' && str[^2] == ' ') || str[^1] == '-') return true;
        
        return false;
    }
    
    // removes all trailing zeros in fractional number, and if the last character after trimming is ',' than removes it too
    public static string TrimZerosAndComma(string str)
    {
        if (!str.Contains(',')) return str;

        str = str.TrimEnd('0');
        if (str[^1] == ',') str = str[..^1];
        
        return str;
    }
}
