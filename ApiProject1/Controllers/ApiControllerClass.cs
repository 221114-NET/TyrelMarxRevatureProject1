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
        private readonly IBusinessLayerClassAuthUserLogin _iBusinessLayerClassAuthUserLogin;
        private readonly IBusinessLayerClassGetUserReimbursements _iBusinessLayerClassGetUserReimbursements;
        private readonly IBusinessLayerClassManagerGetAllReimbursements _iBusinessLayerClassManagerGetAllReimbursements;
        private readonly IBusinessLayerClassManagerUpdateReimbursement _iBusinessLayerClassManagerUpdateReimbursement;
        private readonly IBusinessLayerClassNewUser _iBusinessLayerClassNewUser;
        private readonly IBusinessLayerClassReimbursementRequest _iBusinessLayerClassReimbursementRequest;
        private readonly IBusinessLayerClassUpdateUserInformation _iBusinessLayerClassUpdateUserInformation;

        public ApiControllerClass(IBusinessLayerClassAuthUserLogin ibusinessLayerClassAuthUserLogin, IBusinessLayerClassGetUserReimbursements ibusinessLayerClassGetUserReimbursements, IBusinessLayerClassManagerGetAllReimbursements ibusinessLayerClassManagerGetAllReimbursements, IBusinessLayerClassManagerUpdateReimbursement ibusinessLayerClassManagerUpdateReimbursement, IBusinessLayerClassNewUser ibusinessLayerClassNewUser, IBusinessLayerClassReimbursementRequest ibusinessLayerClassReimbursementRequest, IBusinessLayerClassUpdateUserInformation ibusinessLayerClassUpdateUserInformation)
        {
            _iBusinessLayerClassAuthUserLogin = ibusinessLayerClassAuthUserLogin;
            _iBusinessLayerClassGetUserReimbursements = ibusinessLayerClassGetUserReimbursements;
            _iBusinessLayerClassManagerGetAllReimbursements = ibusinessLayerClassManagerGetAllReimbursements;
            _iBusinessLayerClassManagerUpdateReimbursement = ibusinessLayerClassManagerUpdateReimbursement;
            _iBusinessLayerClassNewUser = ibusinessLayerClassNewUser;
            _iBusinessLayerClassReimbursementRequest = ibusinessLayerClassReimbursementRequest;
            _iBusinessLayerClassUpdateUserInformation = ibusinessLayerClassUpdateUserInformation;
        }


        [HttpPost("NewUser")]
        public string NewUser(string username, string password)
        {
            return _iBusinessLayerClassNewUser.NewUser(username, password);
        }

        [HttpPost("UserLogin")]
        public string AuthUserLogin(string username, string password)
        {
            return _iBusinessLayerClassAuthUserLogin.AuthUserLogin(username, password);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user, admin")]
        [HttpPost("ReimbursementRequest")]
        public string ReimbursementRequest(string ticketType, double reimbursementAmount, string description)
        {
            string LogedInUserName = ($"{this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value}");
            return _iBusinessLayerClassReimbursementRequest.ReimbursementRequest(ticketType, reimbursementAmount, LogedInUserName, description);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user, admin")]
        [HttpGet("MyReimbursements")]
        public List<ReimbursementDataClass> GetUserReimbursements(TicketFilter filter)
        {
            string currentUser = ($"{this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value}");
            return _iBusinessLayerClassGetUserReimbursements.GetUserReimbursements(currentUser, filter);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user, admin")]
        [HttpPost("UpdateUserInformation")]
        public string UpdateUserInformation(string newUserName, string newUserPass)
        {
            string currentUser = ($"{this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value}");
            return _iBusinessLayerClassUpdateUserInformation.UpdateUserInformation(newUserName, newUserPass, currentUser);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpGet("ManagerPendingReimbursements")]
        public List<ReimbursementDataClass> ManagerGetAllReimbursements()
        {
            List<ReimbursementDataClass> result = _iBusinessLayerClassManagerGetAllReimbursements.ManagerGetAllReimbursements();
            return result;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost("ManagerUpdateReimbursement")]
        public string ManagerUpdateReimbursement(int reimbursementID, bool reimbursementApproved)
        {
            return _iBusinessLayerClassManagerUpdateReimbursement.ManagerUpdateReimbursement(reimbursementID, reimbursementApproved);
        }
    }
}