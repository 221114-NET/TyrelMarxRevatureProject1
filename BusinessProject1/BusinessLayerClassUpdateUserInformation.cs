using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RepoProject1;

namespace BusinessProject1
{
    public class BusinessLayerClassUpdateUserInformation : IBusinessLayerClassUpdateUserInformation
    {
        private readonly IRepoClassUpdateUserInformation _IRepoClassUpdateUserInformation;

        public BusinessLayerClassUpdateUserInformation(IRepoClassUpdateUserInformation irepoClassUpdateUserInformation)
        {
            _IRepoClassUpdateUserInformation = irepoClassUpdateUserInformation;
        }

        public string UpdateUserInformation(string newUserName, string newUserPass, string currentUser)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            Regex re = new Regex(strRegex);
            if (re.IsMatch(newUserName) && newUserPass.Length > 1 && newUserPass != null)
            {
                return _IRepoClassUpdateUserInformation.UpdateUserInformation(newUserName, newUserPass, currentUser);
            }
            else
            {
                //if email is not valid tell user to enter valid email
                return "Invalid email or password";
            }
        }
    }
}