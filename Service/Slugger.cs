using System.Text.RegularExpressions;

namespace pspbe.Service 
{
    public class Slugger
    {
        public string Sluggify (string title)
        {
            string replaced = title
                .ToLower()
                .Replace("а", "a")
                .Replace("б", "b")
                .Replace("б", "v")
                .Replace("г", "g")
                .Replace("д", "d")
                .Replace("е", "e")
                .Replace("ё", "yo")
                .Replace("ж", "zh")
                .Replace("з", "z")
                .Replace("и", "i")
                .Replace("й", "y")
                .Replace("к", "k")
                .Replace("л", "l")
                .Replace("м", "m")
                .Replace("н", "n")
                .Replace("о", "o")
                .Replace("п", "p")
                .Replace("р", "r")
                .Replace("с", "s")
                .Replace("т", "t")
                .Replace("у", "u")
                .Replace("ф", "f")
                .Replace("х", "h")
                .Replace("ц", "ts")
                .Replace("ч", "ch")
                .Replace("ш", "sh")
                .Replace("щ", "sch")
                .Replace("ь", "")
                .Replace("ы", "yi")
                .Replace("ъ", "")
                .Replace("э", "e")
                .Replace("ю", "yu")
                .Replace("я", "ya")
                .Replace(" ", "-")
                .Replace(".", "")
                .Replace(",", "")
                .Replace(":", "")
                .Replace("(", "-")
                .Replace(")", "-")
                .Replace("\"", "")
                .Replace("'", "");
            return Regex.Replace(replaced, @"-{1,}", "-");
        }
    }
}