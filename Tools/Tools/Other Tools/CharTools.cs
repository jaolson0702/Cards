using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public static class CharTools
    {
        public static char ToUpper(this char given) => given.ToString().ToUpper().ToChar();

        public static char ToLower(this char given) => given.ToString().ToLower().ToChar();

        public static bool EqualsIC(this char given, char other) => given.ToUpper() == other.ToUpper();

        public static bool IsNumber(this char given) => new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }.Contains(given);

        public static bool IsLetter(this char given)
        {
            char[] lowerLetters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            foreach (char letter in lowerLetters) if (given.EqualsIC(letter)) return true;
            return false;
        }

        public static bool IsUpper(this char given) => given == given.ToUpper();

        public static bool IsLower(this char given) => given == given.ToLower();
    }
}