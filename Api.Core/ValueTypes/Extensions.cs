using System.Linq;
using System.Text.RegularExpressions;

namespace Api.Core.ValueTypes
{
    public static class Extensions
    {
        /// <summary>
        /// Checks if password is valid, based on rules: "Minimum eight characters,
        /// at least one letter and one number and special char, at least 1 upper case char"
        /// </summary>
        public static bool IsValid(this Password password)
        {
            return !string.IsNullOrEmpty(password)
                   && new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$",
                           RegexOptions.Compiled | RegexOptions.CultureInvariant)
                       .IsMatch(password);
        }

        public static bool IsValid(this Email email)
        {
            return !string.IsNullOrEmpty(email)
                   && new Regex(
                       @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                       @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                       RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant).IsMatch(email);
        }
    }
}