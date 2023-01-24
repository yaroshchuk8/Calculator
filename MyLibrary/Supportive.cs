namespace MyLibrary;

public class Supportive
{
    public static void SortLists(ref List<string> operators, ref List<double> numbers)
    {
        for (int i = 0; i < operators.Count; i++)
        {
            numbers.Add(Double.Parse(operators[i]));
            operators.RemoveAt(i);
        }
    }

    public static void Calculate(ref List<string> operators, ref List<double> numbers, string[] priority)
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
}
