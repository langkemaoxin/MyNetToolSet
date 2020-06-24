using System.Runtime.InteropServices;

namespace MyToolSet
{
    /// <summary>
    /// 生成分布式ID
    /// </summary>
    public class SequentialGuidUtils
    {
        [System.Runtime.InteropServices.DllImport("Rpcrt4", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern long UuidCreateSequential(ref System.Guid ptrGuid);

        /// <summary>
        /// 生成连续GUID
        /// </summary>
        /// <returns></returns>
        public static System.Guid CreateGuid()
        {
            System.Guid id = System.Guid.Empty;
            long num = SequentialGuidUtils.UuidCreateSequential(ref id);
            if (0L != num)
            {
                return System.Guid.NewGuid();
            }
            return id;
        }
    }
}