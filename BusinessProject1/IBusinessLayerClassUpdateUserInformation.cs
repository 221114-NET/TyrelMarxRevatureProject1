using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessProject1
{
    public interface IBusinessLayerClassUpdateUserInformation
    {
    string UpdateUserInformation(string newUserName, string newUserPass, string currentUser);
    }
}