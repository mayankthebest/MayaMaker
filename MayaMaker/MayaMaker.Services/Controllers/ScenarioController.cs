using System.Collections.Generic;
using System.Threading.Tasks;
using MayaMaker.Services.Managers;
using MayaMaker.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace MayaMaker.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScenarioController : ControllerBase
    {
        private readonly IScenarioManager _scenarioManager;

        public ScenarioController(IScenarioManager scenarioManager)
        {
            _scenarioManager = scenarioManager;
        }

        [HttpGet("All")]
        public async Task<List<Scenario>> GetAll()
        {
            return await _scenarioManager.GetAllScenarios();
        }
    }
}