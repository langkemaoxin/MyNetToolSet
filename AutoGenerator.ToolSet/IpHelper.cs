using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace MyToolSet
{
    public class IpHelper
    {
        /// <summary>
        /// 获取本地IP等信息
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {
            //本机IP地址 
            string strLocalIP = "";
            //得到计算机名 
            string strPcName = Dns.GetHostName();
            //得到本机IP地址数组 
            IPHostEntry ipEntry = Dns.GetHostEntry(strPcName);
            //遍历数组 
            foreach (var IPadd in ipEntry.AddressList)
            {
                //判断当前字符串是否为正确IP地址 
                if (IsRightIP(IPadd.ToString()))
                {
                    //得到本地IP地址 
                    strLocalIP = IPadd.ToString();
                    //结束循环 
                    break;
                }
            }

            //返回本地IP地址 
            return strLocalIP;
        }

        /// <summary>
        /// 判断是否为正确的IP地址 
        /// </summary>
        /// <param name="strIPadd">IP</param>
        /// <returns></returns>
        public static bool IsRightIP(string strIPadd)
        {
            //利用正则表达式判断字符串是否符合IPv4格式 
            if (Regex.IsMatch(strIPadd, @"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$"))
            {
                //根据小数点分拆字符串 
                string[] ips = strIPadd.Split('.');
                if (ips.Length == 4 || ips.Length == 6)
                {
                    //如果符合IPv4规则 
                    if (System.Int32.Parse(ips[0]) < 256 && System.Int32.Parse(ips[1]) < 256 & System.Int32.Parse(ips[2]) < 256 & System.Int32.Parse(ips[3]) < 256)
                    {
                        if (IsPingIP(strIPadd))
                            //正确 
                            return true;
                        else
                            //错误 
                            return false;
                    }

                    //如果不符合 
                    else
                        //错误 
                        return false;
                }
                else
                    //错误 
                    return false;
            }
            else
                //错误 
                return false;
        }

        /// <summary>
        /// 尝试Ping指定IP 是否能够Ping通 
        /// </summary>
        /// <param name="strIP">IP</param>
        /// <returns></returns>
        public static bool IsPingIP(string strIP)
        {
            try
            {
                //创建Ping对象 
                Ping ping = new Ping();
                //接受Ping返回值 
                PingReply reply = ping.Send(strIP, 1000);
                //Ping通 
                return true;
            }
            catch
            {
                //Ping失败 
                return false;
            }
        }
    }
}