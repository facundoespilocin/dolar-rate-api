using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace DollarInfo.Utils.Extensions
{
    public static class StringExtensions
    {
        public static int GetDeterministicHashCode(this string str)
        {
            unchecked
            {
                int hash1 = (5381 << 16) + 5381;
                int hash2 = hash1;

                for (int i = 0; i < str.Length; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];

                    if (i == str.Length - 1)
                    {
                        break;
                    }

                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }

        public static string Friendly(this string text)
        {
            if (String.IsNullOrEmpty(text)) return "";
            text = Regex.Replace(text, @"\s+", "-");
            text = Regex.Replace(text, @"\-{2,}", "-");

            text = text.ToLower();
            text = Regex.Replace(text, @"&\w+;", "");
            text = Regex.Replace(text, @"[^a-z0-9\-\s]", "");
            text = text.Replace(' ', '-');
            text = Regex.Replace(text, @"-{2,}", "-");
            text = text.TrimStart(new[] { '-' });
            if (text.Length > 80)
                text = text.Substring(0, 79);
            text = text.TrimEnd(new[] { '-' });
            return text;
        }

        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email) || email.Contains(".."))
            {
                return false;
            }

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                static string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

        }

        public static string ValidUrl(this string url)
        {
            string strRegex = @"((http|https)://)(www.)?" +
              "[a-zA-Z0-9@:%._\\+~#?&//=]" +
              "{2,256}\\.[a-z]" +
              "{2,6}\\b([-a-zA-Z0-9@:%" +
              "._\\+~#?&//=]*)";

            Regex re = new(strRegex);

            if (re.IsMatch(url))
            {
                return url;
            }

            return $"https://{url}";
        }

        public static string GenerateHash(this string value)
        {
            return GenerateHash(value, Encoding.UTF8);
        }

        public static string GenerateHash(this string value, Encoding textEncoding)
        {
            byte[] passBytes = textEncoding.GetBytes(value);
            byte[] hash;

            using (var sha1 = SHA1.Create())
            {
                hash = sha1.ComputeHash(passBytes);
                hash = sha1.ComputeHash(hash);
            }

            var sb = new StringBuilder();

            sb.Append('*');

            for (int i = 0; i < hash.Length; i++)
            {
                sb.AppendFormat("{0:X2}", hash[i]);
            }

            return sb.ToString();
        }

        public static string AppendToURL(this string baseURL, params string[] segments)
        {
            return string.Join("/", new[] { baseURL.TrimEnd('/') }
                .Concat(segments.Select(s => s.Trim('/'))));
        }

        public static string Capitalize(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            value = Regex.Replace(value.Replace("'", string.Empty), @"\s+", " ");

            var result = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());

            return result.Trim().Replace("'", "").Replace("′", "");
        }

        public static string ReplaceSpecialChars(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return String.Empty;
            }

            value = new Regex("[-+*'\",_&#%$().?~|{}!=:;<>^@]").Replace(value, string.Empty);

            var result = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());

            return result.Trim();
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string Get256HashedData(this string data)
        {
            StringBuilder hashed = new StringBuilder();
            try
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        hashed.Append(bytes[i].ToString("x2"));
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return hashed.ToString();
        }

        public static DateTime ParseDate(this string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime parsedDate))
            {
                return parsedDate;
            }
            else
            {
                return new DateTime();
            }
        }

        public static DateTime ParseDateExact(this string dateString, string format)
        {
            return DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
        }
    }
}