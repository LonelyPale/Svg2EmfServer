using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColinChang.RedisHelper;

namespace SNWDSafeMonitor
{
    public class RedisContext
    {
        //ip: 101.132.163.137 端口6379 密码   nfxzzf98765#$%$..aab123
        public static RedisHelper DB =
            new RedisHelper("101.132.163.137:6379,defaultDatabase=1,password=nfxzzf98765#$%$..aab123");
    }
}
