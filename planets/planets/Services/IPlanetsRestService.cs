using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using planets.Models;

namespace planets.Services
{
    public interface IPlanetsRestService
    {
        Task<IEnumerable<Planet>> GetAllPlanets();
    }
}
