using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RepoProject1;

namespace BusinessProject1
{
    public class BusinessLayerClassNewUser : IBusinessLayerClassNewUser
    {
        private readonly IRepoClassNewUser _IRepoClassNewUser;

        public BusinessLayerClassNewUser(IRepoClassNewUser irepoClassNewUser)
        {
            _IRepoClassNewUser = irepoClassNewUser;
        }
        public string NewUser(string username, string password)
    {
        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
     @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
     @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        Regex re = new Regex(strRegex);
        if (re.IsMatch(username) && password.Length > 1 && password != null)
        {
            return _IRepoClassNewUser.NewUser(username, password);
        }
        else
        {
            //if email is not valid tell user to enter valid email
            return "Invalid email or password";
        }
    }
    }

}