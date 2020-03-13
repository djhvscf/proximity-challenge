using Newtonsoft.Json;
using planets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace planets.Services
{
    /// <summary>
    /// The planets rest service.
    /// </summary>
    /// <seealso cref="planets.Services.BaseRestService" />
    /// <seealso cref="planets.Services.IPlanetsRestService" />
    public class PlanetsRestService : BaseRestService, IPlanetsRestService
    {
        /// <summary>
        /// Gets all planets.
        /// </summary>
        /// <returns>A list of planets.</returns>
        public async Task<IEnumerable<Planet>> GetAllPlanets()
        {
            var planets = new List<Planet>();
            var current = Connectivity.NetworkAccess;
            try
            {
                if (current == NetworkAccess.Internet)
                {
                    var uri = new Uri("http://code-challenge.proximitycr.com/planets");

                    try
                    {
                        var response = await this.SendRequest(HttpMethod.Get, uri).ConfigureAwait(false);
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                            planets = JsonConvert.DeserializeObject<List<Planet>>(content);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Proper exception handling
                    }
                }
            }
            catch (Exception ex)
            {
                // Proper exception handling
            }

            return planets.OrderBy(p => p.Name);
        }
    }
}
