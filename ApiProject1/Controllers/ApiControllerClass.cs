using Microsoft.AspNetCore.Mvc;
using ModelProject1;
using BusinessProject1;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ApiProject1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerClass : ControllerBase
    {
        private readonly IBusinessLayerClass _businessClass;
        public ApiControllerClass(IBusinessLayerClass businessClass)
        {
            _businessClass = businessClass;
        }


        [HttpPost("NewUser")]
        public string NewUser(string username, string password)
        {
            return _businessClass.NewUser(username, password);
        }

        [HttpPost("AuthUserLogin")]
        public string AuthUserLogin(string username, string password)
        {
            return _businessClass.AuthUserLogin(username, password);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user, admin")]
        [HttpPost("ReimbursementRequest")]
        public string ReimbursementRequest(string ticketType, double reimbursementAmount)
        {
            string LogedInUserName = ($"{this.User.FindFirst(ClaimTypes.NameIdentifier).Value}");
            return _businessClass.ReimbursementRequest(ticketType, reimbursementAmount, LogedInUserName);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user, admin")]
        [HttpGet("GetUserReimbursements")]
        public List<ReimbursementDataClass> GetUserReimbursements()
        {
            string currentUser = ($"{this.User.FindFirst(ClaimTypes.NameIdentifier).Value}");
            return _businessClass.GetUserReimbursements(currentUser);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user, admin")]
        [HttpPost("UpdateUserInformation")]
        public string UpdateUserInformation(string newUserName, string newUserPass)
        {
            string currentUser = ($"{this.User.FindFirst(ClaimTypes.NameIdentifier).Value}");
            return _businessClass.UpdateUserInformation(newUserName, newUserPass, currentUser);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpGet("ManagerGetAllReimbursements")]
        public List<ReimbursementDataClass> ManagerGetAllReimbursements()
        {
            List<ReimbursementDataClass> result = _businessClass.ManagerGetAllReimbursements();
            return result;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost("ManagerUpdateReimbursement")]
        public string ManagerUpdateReimbursement(int reimbursementID, bool reimbursementApproved)
        {
            return _businessClass.ManagerUpdateReimbursement(reimbursementID, reimbursementApproved);
        }
    }
}