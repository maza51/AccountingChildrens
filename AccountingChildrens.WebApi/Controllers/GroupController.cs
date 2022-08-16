using System;
using AccountingChildrens.Application.DTOs;
using System.Threading.Tasks;
using AccountingChildrens.Application.Interfases;
using AccountingChildrens.Application.Services;
using Microsoft.AspNetCore.Mvc;
using AccountingChildrens.Domain;
using AccountingChildrens.Domain.Entities;

namespace AccountingChildrens.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : Controller
    {
        private IGroupService _groupservice;
        private readonly ILoggerManager _logger;

        public GroupController(IGroupService groupservice, ILoggerManager logger)
        {
            _groupservice = groupservice ?? throw new ArgumentNullException(nameof(groupservice));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInfo("Fetching all the groups");

            var groups = await _groupservice.GetAllAsync();

            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInfo($"Fetching group by id {id}");

            var group = await _groupservice.GetByIdAsync(id);

            return Ok(group);
        }

        [HttpPost]
        public async Task Create([FromBody] GroupDTO group)
        {
            _logger.LogInfo($"Creating group");

            await _groupservice.CreateAsync(group);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _logger.LogInfo($"Delete group id {id}");

            await _groupservice.DeleteAsync(id);
        }
    }
}

