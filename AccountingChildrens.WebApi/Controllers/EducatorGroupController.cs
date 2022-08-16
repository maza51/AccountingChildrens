using System;
using System.Threading.Tasks;
using AccountingChildrens.Application.Interfases;
using AccountingChildrens.Application.Services;
using AccountingChildrens.Domain;
using AccountingChildrens.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountingChildrens.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EducatorGroupController : Controller
    {
        private IEducatorGroupService _educatorGroupService;
        private readonly ILoggerManager _logger;

        public EducatorGroupController(IEducatorGroupService educatorGroupService, ILoggerManager logger)
        {
            _educatorGroupService = educatorGroupService ?? throw new ArgumentNullException(nameof(educatorGroupService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task CreateEducatorGroup([FromBody] SetEducatorGroupVM setEducatorGroupVM)
        {
            _logger.LogInfo($"CreateEducatorGroup children id {setEducatorGroupVM.EducatorId}, group id {setEducatorGroupVM.GroupId}");

            await _educatorGroupService.CreateEducatorGroupAsync(setEducatorGroupVM.EducatorId, setEducatorGroupVM.GroupId);
        }

        [HttpDelete]
        public async Task DeleteEducatorGroup([FromBody] SetEducatorGroupVM setEducatorGroupVM)
        {
            _logger.LogInfo($"DeleteEducatorGroup children id {setEducatorGroupVM.EducatorId}, group id {setEducatorGroupVM.GroupId}");

            await _educatorGroupService.DeleteEducatorGroupAsync(setEducatorGroupVM.EducatorId, setEducatorGroupVM.GroupId);
        }
    }
}

