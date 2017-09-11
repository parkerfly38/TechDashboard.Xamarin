using System;

using Xamarin.Forms;
using TechDashboard.ViewModels;
using TechDashboard.Models;
using System.Collections.ObjectModel;

namespace TechDashboard.Views
{
    public class PartsAddPage : ContentPage
    {
        #region Helper Classes

        class PartsListDataCell : ViewCell
        {
            public PartsListDataCell()
            {
                // Alternating row colors in list
                int rowindex = 1;
                if (!Application.Current.Properties.ContainsKey("rowindex"))
                {
                    Application.Current.Properties["rowindex"] = rowindex;
                }
                rowindex = Convert.ToInt32(Application.Current.Properties["rowindex"]);
                Color rowcolor = Color.FromHex("#FFFFFF");
                if (rowindex % 2 == 0)
                {
                    rowcolor = Color.FromHex("#ECF0F1");
                }
                else {
                    rowcolor = Color.FromHex("#FFFFFF");
                }
                rowindex = rowindex + 1;
                Application.Current.Properties["rowindex"] = rowindex;
                Color forecolor = Color.FromHex("#95A5A6");

                Color asbestos = Color.FromHex("#7f8C8d");

                // Item code
                Label labelItemCode = new Label();
                labelItemCode.FontSize = 14;
                labelItemCode.TextColor = forecolor;
                labelItemCode.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
                labelItemCode.TextColor = asbestos;
                labelItemCode.SetBinding(Label.TextProperty, "ItemCode");

                // Description
                Label labelItemCodeDesc = new Label();
                labelItemCodeDesc.FontSize = 14;
                labelItemCodeDesc.TextColor = forecolor;
                labelItemCodeDesc.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
                labelItemCodeDesc.TextColor = asbestos;
                labelItemCodeDesc.SetBinding(Label.TextProperty, "ItemCodeDesc");

                // Whse/QOH/Qty Avl
                Label labelWhseQOHAvl = new Label();
                labelWhseQOHAvl.FontSize = 14;
                labelWhseQOHAvl.TextColor = forecolor;
                labelWhseQOHAvl.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
                labelWhseQOHAvl.TextColor = asbestos;
                labelWhseQOHAvl.SetBinding(Label.TextProperty, "WhseOnHandAvail");

                View = new StackLayout()
                {
                    Padding = 10,
                    BackgroundColor = rowcolor,
                    Children =
                    {
                        labelItemCode,
                        labelItemCodeDesc,
                        labelWhseQOHAvl
                    }
                };
            }
        }

        #endregion

        PartsAddPageViewModel _vm;
        App_ScheduledAppointment _scheduledAppointment;
        ListView _itemsList;
        SearchBar _searchBarItems;
        Xamarin.Forms.Label _labelTitle;

        public PartsAddPage(App_WorkTicket workTicket, App_ScheduledAppointment scheduledAppointment)
        {
            // instantiate the view model
            _vm = new PartsAddPageViewModel(workTicket);
            _scheduledAppointment = scheduledAppointment;

			BackgroundColor = Color.White;
            // Apply initial filter that will return empty list
            _vm.FilterItemList("ZXZXZXZXZX128391");

            // Set the page title.
            //Title = "Add Part";

            //  Create a label for the technician list
            _labelTitle = new Xamarin.Forms.Label();
            _labelTitle.Text = "SELECT PART";
            _labelTitle.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
            _labelTitle.FontSize = 22;
            _labelTitle.TextColor = Color.White;
            _labelTitle.HorizontalTextAlignment = TextAlignment.Center;
            _labelTitle.VerticalTextAlignment = TextAlignment.Center;

            Grid titleLayout = new Grid()
            {
                BackgroundColor = Color.FromHex("#2980b9"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 80
            };
            titleLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            titleLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            titleLayout.Children.Add(_labelTitle, 0, 0);

            // create a list to hold our items
            _itemsList = new ListView()
            {
                HasUnevenRows = true,
                SeparatorVisibility = SeparatorVisibility.None,
                ItemsSource = _vm.ItemList,
                ItemTemplate = new DataTemplate(typeof(PartsListDataCell))
            };
            _itemsList.ItemTapped += ListViewItemsList_ItemTapped;

            // put a search bar on the page to filter the items list
            _searchBarItems = new SearchBar();
            _searchBarItems.Placeholder = "Search Item Code or Desc";
            _searchBarItems.TextChanged += SearchBarItems_TextChanged;
            _searchBarItems.SearchCommand = new Command(() => { _vm.FilterItemList(_searchBarItems.Text); });

            // create a "cancel" button to go back
            Xamarin.Forms.Button buttonAddPart = new Button()
            {
                Text = "CANCEL",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#E74C3C"),
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            buttonAddPart.Clicked += buttonAddPart_Click;

            // put it all together on the page
            Content = new StackLayout
            {
                Padding = 30,
                Children = {
                    titleLayout,
                    _searchBarItems,
                    _itemsList,
                    buttonAddPart
                }
            };
        }

        /// <summary>
        /// Handles the TextChanged event of the search bar
        /// </summary>
        /// <param name="sender">Sending object (search bar)</param>
        /// <param name="e">Event args</param>
        private void SearchBarItems_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((_searchBarItems.Text == null) || (_searchBarItems.Text.Length == 0))
            {
                _vm.FilterItemList(null);
            }
        }

        public async void buttonAddPart_Click(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void ListViewItemsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            App_RepairPart part = new App_RepairPart(e.Item as App_Item, _vm.WorkTicket);
            Navigation.PushAsync(new PartsEditPage(_vm.WorkTicket, part, PartsEditPage.PageMode.Add, _scheduledAppointment));
        }
    }
}
