using System.Text;

namespace Commons
{
    public static class CommonTools
    {
        static public string Trush(int n)
        {
            char[] answer = new char[n];
            int m = 'Z' - 'A' + 1;
            for (int i = 0; i < n; i++)
            {
                answer[i] = (char)('A' + i % m);
            }
            return new string(answer);
        }

        public static string SubstringMax(this string str, int len)
        {
            return str.Substring(0, Math.Min(len,str.Length)).Replace("\n","");
        }

        public static string GetParam(string command, int paramNumber, bool removeEndLines = true)
        {
            try
            {
                if (removeEndLines)
                    command = command.Replace("\n", "");

                int firstIdx = -1;
                int secondIdx = 0;
                for (int i = 0; i < paramNumber; i++)
                {
                    firstIdx = command.IndexOf(" ", firstIdx + 1);
                }
                for (int i = 0; i <= paramNumber; i++)
                {
                    secondIdx = command.IndexOf(" ", secondIdx + 1);
                    if (secondIdx == -1)
                        secondIdx = command.Length;
                }
                return command.Substring(firstIdx + 1, secondIdx - firstIdx - 1);
            }
            catch
            {
                return "";
            }
        }

        public static string FromSpecifiedDelimeterToEnd(string text, int startDelimeterNumber, char delimeter=' ',bool removeEndLines = true)
        {
            if (removeEndLines)
                text = text.Replace("\n", "");

            int startIdx = -1;
            for (int i = 0; i < startDelimeterNumber; i++)
            {
                startIdx = text.IndexOf(delimeter, startIdx+1);
            }
            if (startIdx == -1)
            {
                return "";
            }
            string result = text.Substring(startIdx + 1);

            return result;
        }

        public static string CreateFilePath(string directoryPath, string fileName)
        {
            return directoryPath + "\\" + fileName;
        }

        public static string EncodeToBase64(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            string output = Convert.ToBase64String(bytes);
            return output;
        }

        public static string DecodeFromBase64(string input)
        {
            byte[] bytes = Convert.FromBase64String(input);
            string output = Encoding.UTF8.GetString(bytes);
            return output;
        }


    }
}
