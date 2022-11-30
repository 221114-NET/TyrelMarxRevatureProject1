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
            ReimbursementDataClass result = _businessClass.ReimbursementRequest(reimbursement);
            return Created("data ", result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user, admin")]
        [HttpGet("GetUserReimbursements")]
        public ActionResult<List<ReimbursementDataClass>> GetUserReimbursements()
        {
            List<ReimbursementDataClass> result = _businessClass.GetUserReimbursements();
            return Ok(result);
        }
    }
}