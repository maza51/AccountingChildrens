using System;
using AccountingChildrens.WebApi.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AccountingChildrens.Application.Interfases;
using AccountingChildrens.Domain;
using AccountingChildrens.Application.Services;

namespace AccountingChildrens.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChildrenGroupController : Controller
    {
        private IChildrenGroupService _childrenGroupService;
        private readonly ILoggerManager _logger;

        public ChildrenGroupController(IChildrenGroupService childrenGroupService, ILoggerManager logger)
        {
            _childrenGroupService = childrenGroupService ?? throw new ArgumentNullException(nameof(childrenGroupService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task CreateChildrenGroup([FromBody] SetChildrenGroupVM setChildrenGroupVM)
        {
            _logger.LogInfo($"CreateChildrenGroup children id {setChildrenGroupVM.ChildrenId}, group id {setChildrenGroupVM.GroupId}");

            await _childrenGroupService.CreateChildrenGroupAsync(setChildrenGroupVM.ChildrenId, setChildrenGroupVM.GroupId);
        }

        [HttpDelete]
        public async Task DeleteChildrenGroup([FromBody] SetChildrenGroupVM setChildrenGroupVM)
        {
            _logger.LogInfo($"DeleteChildrenGroup children id {setChildrenGroupVM.ChildrenId}, group id {setChildrenGroupVM.GroupId}");

            await _childrenGroupService.DeleteChildrenGroupAsync(setChildrenGroupVM.ChildrenId, setChildrenGroupVM.GroupId);
        }
    }
}

