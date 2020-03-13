using Autofac;
using Xamarin.Forms;

using planets.ViewModels;

namespace planets.Views
{
    /// <summary>
    /// The planets page.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class PlanetsPage : ContentPage
    {
        /// <summary>
        /// The view model
        /// </summary>
        PlanetsViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetsPage"/> class.
        /// </summary>
        public PlanetsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = App.Container.Resolve<PlanetsViewModel>();
        }

        ///<inheritdoc/>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Planets.Count == 0)
            {
                viewModel.LoadPlanetsCommand.Execute(null);
            }
        }
    }
}