namespace ABPS_WEB.Domain.Reports;

// Ported verbatim from ABPS_WPF/Helper/IndianNumberHelper.cs.
public static class IndianNumberHelper
{
    private static readonly string[] UnitsMap =
    { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine",
      "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

    private static readonly string[] TensMap =
    { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

    public static string NumberToWords(int number)
    {
        if (number == 0)
            return "Zero";

        if (number < 0)
            return "Minus " + NumberToWords(Math.Abs(number));

        string words = "";

        if ((number / 10000000) > 0)
        {
            words += NumberToWords(number / 10000000) + " Crore ";
            number %= 10000000;
        }

        if ((number / 100000) > 0)
        {
            words += NumberToWords(number / 100000) + " Lakh ";
            number %= 100000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " Thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " Hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "and ";

            if (number < 20)
                words += UnitsMap[number];
            else
            {
                words += TensMap[number / 10];
                if ((number % 10) > 0)
                    words += " " + UnitsMap[number % 10];
            }
        }

        return words.Trim();
    }

    public static string NumberToCurrencyWords(decimal amount)
    {
        int rupees = (int)Math.Floor(amount);
        int paise = (int)((amount - rupees) * 100);

        string result = NumberToWords(rupees) + " Rupees";
        if (paise > 0)
            result += " and " + NumberToWords(paise) + " Paise";

        return result;
    }
}
