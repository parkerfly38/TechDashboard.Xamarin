using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using TechDashboard.ViewModels;

namespace TechDashboard.Views
{
	public class TicketListPage : ContentPage
	{
        #region Helper Classes        

        /// <summary>
        /// Class used for the layout of Work Ticket data.
        /// </summary>
        class WorkTicketDataCell : ViewCell
        {
            public WorkTicketDataCell()
            {
                // need a spot for the work ticket number
                Xamarin.Forms.Label labelWTNumber = new Xamarin.Forms.Label();
                labelWTNumber.FontSize = 10;
                labelWTNumber.SetBinding(Xamarin.Forms.Label.TextProperty, "FormattedTicketNo");

                // need a spot for the description
                Xamarin.Forms.Label labelDescription = new Xamarin.Forms.Label();
                labelDescription.FontSize = 14;
                labelDescription.FontAttributes = FontAttributes.Bold;
                labelDescription.SetBinding(Xamarin.Forms.Label.TextProperty, "Description");

                View = new StackLayout()
                {
                    Padding = 10,
                    Children =
                    {
                        labelWTNumber,
                        labelDescription
                    }
                };
            }
        }

        #endregion

        TicketListPageViewModel _vm;

        Xamarin.Forms.Label _labelTitle;
        ListView _listViewWorkTickets;

        public TicketListPage ()
        {
            // Set the page title.
            Title = "Ticket List";

			BackgroundColor = Color.White;
            _vm = new TicketListPageViewModel();

            BindingContext = _vm.TicketList;

            _labelTitle = new Xamarin.Forms.Label();
            _labelTitle.Text = "Select Work Ticket: ";
            _labelTitle.FontSize = 20;

            var dataTemplateItem = new DataTemplate(typeof(WorkTicketDataCell));

            _listViewWorkTickets = new ListView()
            {
                HasUnevenRows = true,
                ItemsSource = _vm.TicketList,
                ItemTemplate = dataTemplateItem
            };
            _listViewWorkTickets.ItemTapped += ListViewWorkTickets_ItemTapped;


            Content = new StackLayout
            {
                Children = {
                    _labelTitle,
                    _listViewWorkTickets
                }
            };
        }

        private void ListViewWorkTickets_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //JT_WorkTciket
            //MainApp.WorkTicket = e.Item as Models.JT_WorkTicket;
            //_vm.SetWorkTicketForApplication(e.Item as Models.JT_WorkTicket);
            ////MainApp.MoveNext();
        }
    }
}
