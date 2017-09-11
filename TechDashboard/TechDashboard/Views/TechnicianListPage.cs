using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;
using TechDashboard.ViewModels;
using System.Threading.Tasks;

namespace TechDashboard.Views
{
	public class TechnicianListPage : ContentPage
    {
		#region Helper Classes

		IHud hud = DependencyService.Get<IHud>();

		class GroupHeaderCell : ViewCell
        {
			public GroupHeaderCell()
            {
                // need a spot to hold the group key for display
                Xamarin.Forms.Label labelKey = new Xamarin.Forms.Label();
                labelKey.TextColor = Color.White;
                labelKey.VerticalOptions = LayoutOptions.Center;
                labelKey.SetBinding(Xamarin.Forms.Label.TextProperty, "Key");

                View = new StackLayout()
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
					BackgroundColor = Color.FromHex("#2980b9"),
                    Children =
                    {
                        labelKey
                    }
                };           
            }
        }

        /// <summary>
        /// Class to display technician data in a cell for a list.
        /// </summary>
        class TechnicialDataCell : ViewCell
        {			
            public TechnicialDataCell()
            {
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
				} else {
					rowcolor = Color.FromHex("#FFFFFF");
				}
				rowindex = rowindex + 1;
				Application.Current.Properties["rowindex"] = rowindex;
				Color forecolor = Color.FromHex("#95A5A6");
                // need a spot for the technician number
                Xamarin.Forms.Label labelTechnicianNo = new Xamarin.Forms.Label();
                labelTechnicianNo.FontSize = 22;
				labelTechnicianNo.TextColor = forecolor;
				labelTechnicianNo.FontFamily = Device.OnPlatform("OpenSans-Bold",null,null);
                labelTechnicianNo.SetBinding(Xamarin.Forms.Label.TextProperty, "FormattedTechnicianNumber");

                // need a spot for the last name
                Xamarin.Forms.Label labelLastName = new Xamarin.Forms.Label();
                labelLastName.FontSize = 22;
				labelLastName.TextColor = forecolor;
				labelLastName.FontFamily = Device.OnPlatform("OpenSans-Regular",null,null);
                labelLastName.SetBinding(Xamarin.Forms.Label.TextProperty, "LastName");

                // need a spot for the first name
                Xamarin.Forms.Label labelFirstName = new Xamarin.Forms.Label();
                labelFirstName.FontSize = 22;
				labelFirstName.TextColor = forecolor;
				labelFirstName.FontFamily = Device.OnPlatform("OpenSans-Regular", null,null);
                labelFirstName.SetBinding(Xamarin.Forms.Label.TextProperty, "FirstName");

                View = new StackLayout()
                {
					Padding = 30,
					BackgroundColor = rowcolor,
                    Children =
                    {
                        labelTechnicianNo,
                        new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                labelFirstName,
                                labelLastName
                            }
                        }                       
                    }
                };
            }
        }

        #endregion

       	TechnicianListPageViewModel _vm;
        Xamarin.Forms.Label _labelTitle;
        ListView _listViewTechnicians;
		ActivityIndicator _activityIndicator;

		public TechnicianListPage(TechnicianListPageViewModel viewModel)
		{
			hud.Show();
			hud.Dismiss();
            // Set the page title.
			NavigationPage.SetHasNavigationBar(this, true);
			Title = "Technican Dashboard";
			_activityIndicator = new ActivityIndicator() {
				Color = Color.Red
			};
			_activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsLoading");
			_activityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsLoading");
			_activityIndicator.BindingContext = _vm;
			BackgroundColor = Color.White;
            // Create the view model for this page
            _vm = viewModel;   //new TechnicianListPageViewModel();
          
            // Set the binding context for this page
            BindingContext = _vm.TechnicianList;

            // Create our screen objects
            //  Create a label for the technician list
            _labelTitle = new Xamarin.Forms.Label();
            _labelTitle.Text = "SELECT TECHNICIAN";
			_labelTitle.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
            _labelTitle.FontSize = 22;
			_labelTitle.TextColor = Color.White;
			_labelTitle.HorizontalTextAlignment = TextAlignment.Center;
			_labelTitle.VerticalTextAlignment = TextAlignment.Center;


			Grid titleLayout = new Grid() {
				BackgroundColor = Color.FromHex("#2980b9"),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				HeightRequest = 80
			};
			titleLayout.RowDefinitions.Add (new RowDefinition { Height = new GridLength (1, GridUnitType.Star) });
			titleLayout.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) });
			titleLayout.Children.Add(_labelTitle, 0, 0);

            // Create a template to display each technician in the list
            var dataTemplateItem = new DataTemplate(typeof(TechnicialDataCell));

            // Create the actual list
            _listViewTechnicians = new ListView()
            {
                HasUnevenRows = true,
				HorizontalOptions = LayoutOptions.Fill,
				SeparatorVisibility = SeparatorVisibility.None,
				BackgroundColor = Color.White,

                ItemsSource = _vm.TechnicianList,
                ItemTemplate = dataTemplateItem
            };

           
			_listViewTechnicians.ItemTapped += ListViewTechnicians_ItemTapped;

            StackLayout layout = new StackLayout
            {
				BackgroundColor = Color.FromHex("#2980b9"),
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = {
					titleLayout,
					_activityIndicator,
					_listViewTechnicians
				}
			};
            if (Device.OS == TargetPlatform.iOS)
            {
                // move layout under the status bar
                layout.Padding = new Thickness(0, 40, 0, 0);
            }
            Content = layout;
        }

        private async void ListViewTechnicians_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //_vm.SignIn(e.Item as Models.JT_Technician);
            //_vm.IsSignedIn = false;
			await Task.Run(() =>_vm.SignIn(e.Item as Models.App_Technician)); // puke
														 //MainApp.Technician = e.Item as Models.JT_Technician;
			//App.Database.CreateDependentTables(App.CurrentTechnician);

            //MainApp.MoveNext();
        }
    }
}
