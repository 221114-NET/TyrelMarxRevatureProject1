using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RepoProject1
{
    public class LoginLogger : ILoginLogger
    {
        public void LoginLog(string log)
        {
            File.AppendAllText("../Logs/LoginLog.txt", $"{log} Logged In At {DateTime.Now}\n");
        }
    }
}