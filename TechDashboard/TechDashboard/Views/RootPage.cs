using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using TechDashboard.ViewModels;

namespace TechDashboard.Views
{
    public class RootPage : TicketDetailsPage
	{
        public RootPage () : base()
		{
           // empty
        }

        protected override void OnAppearing()
        {
            InitializePage();

            base.OnAppearing();

        }

        protected new void InitializePage()
        {
            base._vm = new TicketDetailsPageViewModel();
            base.InitializePage();

			BackgroundColor = Color.White;
        }
    }
}
