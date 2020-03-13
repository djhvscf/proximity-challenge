using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using planets.Models;
using planets.Services;
using Xamarin.Forms;

namespace planets.ViewModels
{
    /// <summary>
    /// The planets view model.
    /// </summary>
    /// <seealso cref="planets.ViewModels.BaseViewModel" />
    public class PlanetsViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the planets.
        /// </summary>
        /// <value>
        /// The planets.
        /// </value>
        public ObservableCollection<Planet> Planets { get; set; }

        /// <summary>
        /// Gets or sets the load planets command.
        /// </summary>
        /// <value>
        /// The load planets command.
        /// </value>
        public Command LoadPlanetsCommand { get; set; }

        /// <summary>
        /// The service
        /// </summary>
        private readonly IPlanetsRestService _service;

        /// <summary>
        /// Gets the refresh command.
        /// </summary>
        /// <value>
        /// The refresh command.
        /// </value>
        public Command RefreshCommand => new Command(async () => await RefreshPlanetsAsync());

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetsViewModel"/> class.
        /// </summary>
        /// <param name="_service">The planets rest service.</param>
        public PlanetsViewModel(IPlanetsRestService _service)
        {
            Title = "Planets";
            Planets = new ObservableCollection<Planet>();
            LoadPlanetsCommand = new Command(async () => await ExecuteLoadPlanetsCommand());
            this._service = _service;
        }

        /// <summary>
        /// Executes the load planets command.
        /// </summary>
        async Task ExecuteLoadPlanetsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Planets.Clear();
                var planets = await this._service.GetAllPlanets().ConfigureAwait(false);

                Device.BeginInvokeOnMainThread(() =>
                {
                    foreach (var planet in planets)
                    {
                        Planets.Add(planet);
                    }
                });
            }
            catch (Exception ex)
            {
                // Proper handling of exception
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Refreshes the planets asynchronous.
        /// </summary>
        async Task RefreshPlanetsAsync()
        {
            IsRefreshing = true;
            await Task.Delay(TimeSpan.FromSeconds(3));
            IsRefreshing = false;
        }
    }
}
