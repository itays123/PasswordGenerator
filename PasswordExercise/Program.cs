using System;

namespace PasswordExercise
{
    class Program
    {

        static string SYMBOLS = "#@%$";
        static string LOWER = "abcdefghijklmnopqrstuvwxyz";
        static string UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static string DIGITS = "0123456789";

        static void Main(string[] args)
        {
            string currentPassword;
            bool passwordConfirm;
            Console.WriteLine("Please enter a password: ");
            currentPassword = Console.ReadLine();
            passwordConfirm = IsStrongPassword(currentPassword);
            if (!passwordConfirm)
                Console.WriteLine("Weak password. Generating new one...");
            while (!passwordConfirm)
            {
                currentPassword = GeneratePassword();
                Console.WriteLine("How about {0}? (y/n)", currentPassword);
                passwordConfirm = char.Parse(Console.ReadLine()) == 'y';
            }
            Console.WriteLine("Your Password Is Strong. Well Done!");
        }

        static bool IsStrongPassword(string charSequence)
        {
            bool pwSymbolExists = false, lowerExists = false, upperExists = false, digitExists = false;
            char current;

            if (charSequence.Length < 6)
                return false;

            for (int i = 0; i < charSequence.Length; i++)
            {
                current = charSequence[i];
                if (IsPasswordSymbol(current))
                    pwSymbolExists = true;
                else if (Char.IsLower(current))
                    lowerExists = true;
                else if (Char.IsUpper(current))
                    upperExists = true;
                else if (Char.IsDigit(current))
                    digitExists = true;
            }
            return pwSymbolExists && lowerExists && upperExists && digitExists;
        }

        static bool IsPasswordSymbol(char ch)
        {
            return ch == '$' || ch == '#' || ch == '%' || ch == '%';
        }


        static string GeneratePassword()
        {
            Random rand = new Random();
            int pwLength = rand.Next(6, 16);
            char[] seq = new char[pwLength];

            // 4 symbols must be defined according to definition
            seq[0] = GeneratePasswordSymbol();
            seq[1] = GenerateLower();
            seq[2] = GenerateUpper();
            seq[3] = GenerateDigit();

            // pick random chars for rest of the symbols
            for (int i = 4; i < pwLength; i++)
                seq[i] = GenerateChar();

            // shoufle order of characters
            Array.Sort(seq, (x, y) => x == y ? 0 : rand.Next(0, 2) == 1 ? 1 : -1);
            return new string(seq);
        }

        static char GeneratePasswordSymbol()
        {
            return PickRandomChar(SYMBOLS);
        }

        static char GenerateLower()
        {
            return PickRandomChar(LOWER);
        }

        static char GenerateUpper()
        {
            return PickRandomChar(UPPER);
        }

        static char GenerateDigit()
        {
            return PickRandomChar(DIGITS);
        }

        static char GenerateChar()
        {
            return PickRandomChar(SYMBOLS + LOWER + UPPER + DIGITS);
        }

        static char PickRandomChar(string charSequence)
        {
            Random rand = new Random();
            int length = charSequence.Length;
            int generatedIdx = rand.Next(length);
            return charSequence[generatedIdx];
        }

    }
}
