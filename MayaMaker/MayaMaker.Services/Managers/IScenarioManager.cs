using MayaMaker.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MayaMaker.Services.Managers
{
    public interface IScenarioManager
    {
        Task<List<Scenario>> GetAllScenarios();
    }
}
