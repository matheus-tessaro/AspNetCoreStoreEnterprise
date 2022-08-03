using System.Text.RegularExpressions;

namespace SE.Core.DomainObjects
{
    public class Email
    {
        protected Email() { }

        public Email(string emailAddress)
        {
            if (!Validate(emailAddress)) 
                throw new DomainException("Invalid e-mail");

            EmailAddress = emailAddress;
        }

        public string EmailAddress { get; private set; }

        public const int MaxLength = 254;
        public const int MinLength = 5;

        public static bool Validate(string email) => 
            new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$").IsMatch(email);
    }
}
