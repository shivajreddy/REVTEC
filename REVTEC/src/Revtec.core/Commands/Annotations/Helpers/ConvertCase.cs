using System.Globalization;

namespace Revtec.core.Commands.Annotations
{
    public class ConvertCase
    {
        public static string ToAllCaps(string inputString)
        {
            return inputString.ToUpper();
        }

        public static string ToCamelCase(string inputString)
        {
            //string inputString = "TWIN I.L.O DOUBLE HUNG";
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string titleCaseString = textInfo.ToTitleCase(inputString.ToLower());
            //Console.WriteLine(titleCaseString); // Output: Twin I.L.O Double Hung

            return titleCaseString;
        }

        public static string ToLower(string inputString)
        {
            return inputString.ToLower();
        }

    }
}
