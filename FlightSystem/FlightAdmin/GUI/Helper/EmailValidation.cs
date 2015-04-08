using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightAdmin.GUI.Helper
{
    public class EmailValidation
    {

        static bool invalid = false;

        public static bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            try {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException) {
                return false;
            }

            if (invalid)
                return false;

 
            try {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException) {
                return false;
            }
        }

        private static string DomainMapper(Match match) {
          
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException) {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }


        public static bool IsEmailValid(TextBox txt, ErrorProvider errProvider, string objText) {

            string errText = "";
            if (txt.Text == string.Empty ) 
                errText = string.Format("{0} is requested!", objText);
            if (!IsValidEmail(txt.Text)) {
                errText = string.Format("is not in a valid email format");
                errProvider.SetError(txt, errText );
            }
            errProvider.SetError(txt, errText);
            return string.IsNullOrEmpty(errText);
        }
    }
}
