using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoProject1
{
    public interface IRepoClassAuthUserLogin
    {
    string AuthUserLogin(string username, string password);
    }
}