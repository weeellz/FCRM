using FCRM.Web.Results;
using FCRM.Web.Services;
using System;
using System.Web.Http;

namespace FCRM.Web.Controllers
{
    public class UserController : ApiController
    {
        private PerformerService _performerService = null;
        private ManagerService _managerService = null;

        public UserController() : this(new PerformerService(), new ManagerService()) { }

        public UserController(PerformerService performerService, ManagerService managerService)
        {
            _performerService = performerService;
            _managerService = managerService;
        }

        [HttpGet]
        [Route("api/specifications/{id:guid}")]
        public SpecificationResult GetSpecification(Guid id)
        {
            return _performerService.GetSpecification(id);
        }

        [HttpGet]
        [Route("api/orders/{id:guid}")]
        public OrderResult GetOrders(Guid id)
        {
            return _performerService.GetOrder(id);
        }

        //[HttpPost]
        //[Route("api/orders/{id:guid}/assign")]
        //public Result AssignToOrder(Guid id)
        //{
        //    return _managerService.AssignToOrder(id);
        //}

        [HttpPost]
        [Route("api/orders/{id:guid}/addperformer/{userid:guid}")]
        public Result AddPerformer(Guid id, Guid userid)
        {
            return _managerService.AddPerformer(id, userid);
        }
    }
}
