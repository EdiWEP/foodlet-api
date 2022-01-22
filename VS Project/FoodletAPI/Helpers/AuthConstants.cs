using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Helpers
{
    public static class AuthConstants
    {
        public const int OK = 0;

        public const int PWD_NODIGIT = 1001;
        public const int SHORT_PWD = 1002;

        public const int NULL_USERNAME = 2001;
        public const int BAD_USERNAME = 2002;
        public const int TAKEN_USERNAME = 2003;

        public const int NULL_EMAIL = 3001;
        public const int BAD_EMAIL = 3002;
        public const int TAKEN_EMAIL = 3003;

        public static readonly char[] INPUT_TRIM_CHARS = { ' ', '>', '<', '\\', '/', ')', '(', '$', '@', '.', '\t', '\n', '!', '\'', '\"', '?', ',', ':', ';', '=', '+', '-', '#', '%', '^', '&', '*' };
        public const string VALID_USER_CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
    }
}
