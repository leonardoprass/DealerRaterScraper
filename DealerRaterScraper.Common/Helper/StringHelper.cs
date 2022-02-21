namespace DealerRaterScraper.Common.Helper
{
    public static class StringHelper
    {
        public static string RemoveSpecialCharacters(string str)
        {
            char[] arr = str.Where(c =>
                char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)
            ).ToArray();

            return new string(arr);
        }

        public static IEnumerable<string> NormalizeText(string text)
        {
            //clean whitespaces and line breaks to only get data with actual text
            return text.Replace("\n", string.Empty).Replace("\r", string.Empty).Trim().Split("  ").Where(t => !string.IsNullOrEmpty(t));
        }

        public static IEnumerable<string> GetWords(string phrase)
        {
            //clean whitespaces and line breaks to only get data with actual text
            return phrase.Split(' ').Where(t => !string.IsNullOrEmpty(t));
        }
    }
}
