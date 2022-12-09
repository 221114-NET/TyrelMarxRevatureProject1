using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoProject1
{
    public interface IRepoClassUpdateUserInformation
    {
    string UpdateUserInformation(string newUserName, string newUserPass, string currentUser);
    }
}