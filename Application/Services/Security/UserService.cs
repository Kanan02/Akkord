using Application.Constants;
using Application.Entities;
using Application.Entities.Security;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces.IServices.Security;
using Application.Interfaces.IUoW;
using Application.Models.Request;
using Application.Models.Request.Base;
using Application.Models.Request.Security;
using Application.Models.Response;
using Application.Services.Base;
using Application.Spesifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Security
{
    public class UserService : BaseService<User>, IUserService
    {
        private ISecurityService _securityService { get; }
        public UserService(IUoW unitOfWork, ISecurityService securityService) : base(unitOfWork)
        {
            _securityService = securityService;
        }

        public async Task<LoginRes> SignIn(UserReq req)
        {
            req.IncludeRole = true;
            //req.IncludeManager = true;s
            ValidateLoginInput(req);
            var user = (await GetListByFilter(req)).FirstOrDefault();
            if (user == null)
                throw new AkkordException("user_not_found");
            var manager = await GetSaleManager(user);

            ValidateActivation(user, manager);

            var token = _securityService.GenerateToken(user);
            var roles = user.UserRoles.Select(x => x.Role.Name).ToList();
            return new LoginRes(token, roles,user, manager);
        }


        private void ValidateLoginInput(UserReq req)
        {
            if (req.Value == null 
                || string.IsNullOrEmpty(req.Value.Username) 
                || string.IsNullOrEmpty(req.Value.Password))
                throw new AkkordException("username_and_password_reuquired");
        }


        private void ValidateActivation(User user, SaleManager saleManager)
        {
            if (user.UserRoles.Any(ur=>ur.Role.Name == RoleConstant.Admin))
                return;

            //var spec = new BaseSpecification<SaleManager>()
            //{
            //    Filters = new List<Expression<Func<SaleManager, bool>>>() {
            //     (x) => x.Id == user.Id
            //    }
            //};
            //var saleManager =(await _unitOfWork.Repository<SaleManager>().GetAsync(spec)).FirstOrDefault();
            // var saleManager =(await GetSaleManager(user));
            if (saleManager.Status == ActivationStatus.Deactive)
                throw new AkkordException("deactive_user");
        }

        private Task<SaleManager> GetSaleManager(User user) => _unitOfWork.Repository<SaleManager>().GetByIdAsync(user.Id);

        protected override ISpecification<User> FilterList(BaseReq<User> request, ISpecification<User> spec)
        {
            var req = (UserReq)request;
            var value = req.Value;

            if (req.IncludeRole)
            {
                var roleInclude = $"{nameof(User.UserRoles)}.{nameof(UserRole.Role)}";
                spec.IncludeStrings.Add(roleInclude);
            }

            //if (req.IncludeManager)
            //    spec.Includes.Add(x => x.SaleManager);

            if (value!=null)
            {
                if (!string.IsNullOrEmpty(value.Username))
                    spec.Filters.Add(f => f.Username == value.Username.Trim());

                if (!string.IsNullOrEmpty(value.Password))
                    spec.Filters.Add(f => f.Password == value.Password.Trim());
            }

            return spec;
        }
    }
}
