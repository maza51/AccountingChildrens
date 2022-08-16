using System;
using AccountingChildrens.Application.DTOs;
using AccountingChildrens.Application.Interfases;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AccountingChildrens.WebApi.Models;
using AccountingChildrens.Domain;
using AccountingChildrens.Domain.Entities;

namespace AccountingChildrens.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChildrenController : Controller
    {
        private IChildrenService _childrenService;
        private readonly ILoggerManager _logger;

        public ChildrenController(IChildrenService childrenService, ILoggerManager logger)
        {
            _childrenService = childrenService ?? throw new ArgumentNullException(nameof(childrenService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInfo("Fetching all the childrens");

            var childrens = await _childrenService.GetAllAsync();

            return Ok(childrens);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInfo($"Fetching children by id {id}");

            var children = await _childrenService.GetByIdAsync(id);

            return Ok(children);
        }

        [HttpPost]
        public async Task Create([FromBody] ChildrenDTO children)
        {
            _logger.LogInfo($"Creating children");

            await _childrenService.CreateAsync(children);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _logger.LogInfo($"Delete children id {id}");

            await _childrenService.DeleteAsync(id);
        }
    }
}

