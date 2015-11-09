namespace Agile.Ksp.Extensions
{
    public static class StringExtensions
    {
        public static string Color(this string input, string color)
        {
            string c = color.TrimStart('#');
            if (c.Length == 6)
                c += "ff";

            return "<color=#" + c + ">" + input + "</color>";
        }

        public static string Bold(this string input)
        {
            return "<b>" + input + "</b>";
        }

        public static string Italic(this string input)
        {
            return "<i>" + input + "</i>";
        }

        public static string Fill(this string input, int totalLenght, char character)
        {
            string ret = input;
            while (ret.Length < totalLenght)
                ret = character + ret;

            return ret;
        }
    }
}