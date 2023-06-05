using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UnitTestingIntro
{
    public class Mail
    {
        public bool IsValidEmail(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new Exception("Email address is empty");
            }
            Regex regex = new Regex(@"^['\w0-9._%+-]+@[\w0-9.-]+\.[\w]{2,6}$");
            return regex.IsMatch(emailAddress);
        }
        public string IsSpam(string emailAddress) 
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new Exception("Email address is empty");
            }
            List<string> spammyDomains = new List<string>() { "spam.com", "dodgy.com", "donttrust.com"};
            return spammyDomains.Any(d => emailAddress.Contains(d)) ? "SPAM" : "OK";

        }
    }
    
}
