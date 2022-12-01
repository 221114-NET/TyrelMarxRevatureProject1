using Microsoft.AspNetCore.Mvc;
using ModelProject1;
using BusinessProject1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
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

        //testing Authorize
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user, admin")]
        [HttpPost("ReimbursementRequest")]
        public ActionResult<ReimbursementDataClass> ReimbursementRequest(ReimbursementDataClass reimbursement)
        {
            string LogedInUserName = ($"{this.User.FindFirst(ClaimTypes.NameIdentifier).Value}");
            return Created("Request Created ", _businessClass.ReimbursementRequest(reimbursement, LogedInUserName));
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
        public List<ReimbursementDataClass> UpdateUserInformation()
        {
            string currentUser = ($"{this.User.FindFirst(ClaimTypes.NameIdentifier).Value}");
            List<ReimbursementDataClass> result = _businessClass.UpdateUserInformation(currentUser);
            return result;
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
        public ReimbursementDataClass ManagerUpdateReimbursement(ReimbursementDataClass reimbursement)
        {
            return _businessClass.ManagerUpdateReimbursement(reimbursement);
        }
    }
}