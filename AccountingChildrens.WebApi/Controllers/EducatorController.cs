using System;
using AccountingChildrens.Application.DTOs;
using System.Threading.Tasks;
using AccountingChildrens.Application.Interfases;
using AccountingChildrens.Application.Services;
using AccountingChildrens.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AccountingChildrens.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EducatorController : Controller
    {
        private IEducatorService _educatorService;
        private readonly ILoggerManager _logger;

        public EducatorController(IEducatorService educatorService, ILoggerManager logger)
        {
            _educatorService = educatorService ?? throw new ArgumentNullException(nameof(educatorService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInfo("Fetching all the educators");

            var educators = await _educatorService.GetAllAsync();

            return Ok(educators);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInfo($"Fetching educator by id {id}");

            var educator = await _educatorService.GetByIdAsync(id);

            return Ok(educator);
        }

        [HttpPost]
        public async Task Create([FromBody] EducatorDTO educator)
        {
            _logger.LogInfo($"Creating educator");

            await _educatorService.CreateAsync(educator);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _logger.LogInfo($"Delete educator id {id}");

            await _educatorService.DeleteAsync(id);
        }
    }
}

