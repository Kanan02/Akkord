using System.Threading.Tasks;
using Application.Interfaces.IServices.Security;
using Application.Models.Request.Security;
using Application.Models.Request.Ui;
using Application.Models.Response;
using Application.Models.Response.Base;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Security
{

    public class UserController : ControllerBase
    {
        private IUserService _userService { get; }

        public UserController(IUserService userService) => _userService = userService;

        public async Task<ApiValueResponse<LoginRes>> SignIn([FromBody] SignInUi request)
        {
            var req = new UserReq(request);
            return new ApiValueResponse<LoginRes>(await _userService.SignIn(req));
        }
    }
}
