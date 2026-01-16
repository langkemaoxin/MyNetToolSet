using System.Text;

namespace MyToolSet
{
    /// <summary>
    /// 进制转换
    /// </summary>
    public class NumberConvert
    {
        //36位 进制转换
        //private static readonly char[] Arrays = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        //10位 进制转换
        private static readonly char[] Arrays = "0123456789".ToCharArray();

        public static string GetNumber(int number, int paddingCount)
        {
            var myDec = Convert(number);
            var paddingMyDec = PaddingMyDec(myDec, paddingCount);
            return paddingMyDec;
        }

        private static string PaddingMyDec(string numberString, int paddingCount)
        {
            var paddingString = numberString.PadLeft(paddingCount, '0');
            return paddingString;
        }

        private static string Convert(int number)
        {
            var n = Arrays.Length;
            var result = new StringBuilder();

            while (number > 0)
            {
                result.Insert(0, Arrays[number % n]);
                number /= n;
            }

            return result.ToString();
        }
    }
}