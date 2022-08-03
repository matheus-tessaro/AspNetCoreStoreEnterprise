using SE.Core.Utils;

namespace SE.Core.DomainObjects
{
    public class SocialSecurityNumber
    {
        protected SocialSecurityNumber() { }

        public SocialSecurityNumber(string number)
        {
            if (!Validate(number))
                throw new DomainException("Invalid social security number");

            Number = number;
        }

        public const int MaxLength = 11;
        public string Number { get; private set; }

        public static bool Validate(string number)
        {
            number = number.NumbersOnly(number);

            if (number.Length > 11)
                return false;

            while (number.Length != 11)
                number = '0' + number;

            var equals = true;

            for (var i = 1; i < 11 && equals; i++)
                if (number[i] != number[0])
                    equals = false;

            if (equals || number == "12345678909")
                return false;

            var arrNumbers = new int[11];

            for (var i = 0; i < 11; i++)
                arrNumbers[i] = int.Parse(number[i].ToString());

            var sum = 0;

            for (var i = 0; i < 9; i++)
                sum += (10 - i) * arrNumbers[i];

            var result = sum % 11;

            if (result == 1 || result == 0)
            {
                if (arrNumbers[9] != 0)
                    return false;
            }
            else if (arrNumbers[9] != 11 - result)
                return false;

            sum = 0;

            for (var i = 0; i < 10; i++)
                sum += (11 - i) * arrNumbers[i];

            result = sum % 11;

            if (result == 1 || result == 0)
            {
                if (arrNumbers[10] != 0)
                    return false;
            }
            else if (arrNumbers[10] != 11 - result)
                return false;

            return true;
        }
    }
}
