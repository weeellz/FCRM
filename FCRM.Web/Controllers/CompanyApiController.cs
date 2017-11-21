using FCRM.Web.Requests;
using FCRM.Web.Results;
using FCRM.Web.Services;
using System;
using System.Web.Http;

namespace FCRM.Web.Controllers
{
    public class CompanyApiController : ApiController
    {
        private CompanyService _companyService = null;
        private ManagerService _managerService = null;

        public CompanyApiController() : this(new CompanyService(), new ManagerService()) { }

        public CompanyApiController(CompanyService companyService, ManagerService managerService)
        {
            _companyService = companyService;
            _managerService = managerService;
        }

        [HttpGet]
        [Authorize]
        [Route("api/company_info")]
        public CompanyResult GetCompanyInfo()
        {
            return _companyService.GetCompanyInfo();
        }

        [HttpGet]
        [Authorize]
        [Route("api/company_orders/my")]
        public OrderListResult GetMyOrders()
        {
            return _companyService.GetMyOrders();
        }
        [HttpGet]
        [Authorize]
        [Route("api/company_orders/unassigned")]
        public OrderListResult GetUnassignedOrders()
        {
            return _companyService.GetUnassignedOrders();
        }
        [HttpGet]
        [Authorize]
        [Route("api/company_orders")]
        public OrderListResult GetOrders()
        {
            return _companyService.GetOrders();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/{key:guid}/add_order")]
        public Result AddOrder(Guid key, OrderRequest model)
        {
            return _companyService.AddOrder(key, model);
        }

        [HttpGet]
        [Authorize(Roles = "owner")]
        [Route("api/users")]
        public UserListResult GetUsers()
        {
            return _companyService.GetUsers();
        }

        [HttpPost]
        [Authorize(Roles = "owner")]
        [Route("api/users/{UserId}/add_role/{RoleName}")]
        public Result AddRole(string UserId, string RoleName)
        {
            return _companyService.AddRole(UserId, RoleName);
        }

        [HttpPost]
        [Authorize(Roles = "owner")]
        [Route("api/users/{UserId}/remove_role/{RoleName}")]
        public Result RemoveAtRole(string UserId, string RoleName)
        {
            return _companyService.RemoveRole(UserId, RoleName);
        }
        [HttpGet]
        [Authorize]
        [Route("api/company_id")]
        public GuidResult GetCompanyId()
        {
            return _companyService.GetCompanyGuid();
        }
        [HttpPost]
        [Authorize(Roles = "manager")]
        [Route("api/assign_to_order/{OrderId}")]
        public Result AssignToOrder(Guid OrderId)
        {
            return _companyService.AssignToOrder(OrderId);
        }
        [HttpGet]
        [Authorize]
        [Route("api/performers")]
        public UserListResult GetPerformers()
        {
            return _companyService.GetPerformersList();
        }
        [HttpPost]
        [Authorize(Roles = "manager")]
        [Route("api/order/{OrderId}/add_performer/{PerformerId}")]
        public Result AddPerformer(Guid OrderId, Guid PerformerId)
        {
            return _managerService.AddPerformer(OrderId, PerformerId);
        }
        [HttpGet]
        [Authorize]
        [Route("api/performers/count")]
        public Result<int> GetPerformersCount()
        {
            return _companyService.GetPerformersCount();
        }
        [HttpGet]
        [Authorize]
        [Route("api/managers/count")]
        public Result<int> GetManagersCount()
        {
            return _companyService.GetManagersCount();
        }
        [HttpGet]
        [Authorize]
        [Route("api/company_name")]
        public StringResult GetCompanyName()
        {
            return _companyService.GetCompanyName();
        }
        [HttpPost]
        [Authorize(Roles = "manager")]
        [Route("api/orders/{OrderId}/set_price/{Price}")]
        public Result SetOrderRrice(Guid OrderId, double Price)
        {
            return _managerService.SetOrderPrice(Price, OrderId);
        }
    }
}
