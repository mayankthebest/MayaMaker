using MayaMaker.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MayaMaker.Services.Managers
{
    public class ScenarioManager : IScenarioManager
    {
        readonly MayaMakerContext _dbContext = null;

        public ScenarioManager(MayaMakerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Scenario>> GetAllScenarios()
        {
            return _dbContext.Scenarios.ToList();
        }
    }
}
