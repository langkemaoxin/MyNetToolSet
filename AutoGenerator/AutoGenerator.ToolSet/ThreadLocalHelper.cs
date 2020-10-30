using System.Threading;

namespace MyToolSet
{
    public class ThreadLocalHelper
    {
        private static readonly ThreadLocal<string> CurrentInfo = new ThreadLocal<string>();

        /// <summary>
        ///     设置当前 请求IP
        /// </summary>
        /// <param name="ip"></param>
        public static void SetCurrentRequestIp(string ip)
        {
            CurrentInfo.Value = ip;
        }

        /// <summary>
        ///     获取当前IP
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentRequestIp()
        {
            return CurrentInfo.Value;
        }
    }
}