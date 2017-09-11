using System;
using TechDashboard.Models;
using TechDashboard.ViewModels;
using Xamarin.Forms;

namespace TechDashboard.Views
{
    public class MainDashboard : MasterDetailPage
    {
        MainMenu _masterPage;

        public MainDashboard()
        {
            this.Title = "Main Dashboard";

            _masterPage = new MainMenu();
            Master = _masterPage;
            //Master.Icon = "todo.png"; // puke

			BackgroundColor = Color.White;
            Detail = new NavigationPage(new RootPage());
            //Detail = new NavigationPage(new TicketDetailsPage());

            _masterPage.ListView.ItemSelected += OnItemSelected;

            if (Device.OS == TargetPlatform.Windows)
            {
                //Master.Icon = "swap.png"; We'll worry about this when we implement Windows Phone
            }

            // If we aren't logged into a ticket, display the main menu
            if(!App.Database.IsClockedIn())
            {
                IsPresented = true;
            }
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MainMenuItem item = e.SelectedItem as MainMenuItem;
            if (item != null)
            {
                if(item.Title.ToLower() == "exit") {
                    // puke... close app
                    await DisplayAlert("Log off", "Normally, this would log the user off.", "OK", "Cancel");
                    return;
                }

                if (item.Title.ToLower() == "log out") {

                    App_Settings appSettings = App.Database.GetApplicationSettings();
                    appSettings.LoggedInTechnicianNo = "";
                    appSettings.LoggedInTechnicianDeptNo = "";
                    App.Database.SaveAppSettings(appSettings);

                    TechnicianListPageViewModel viewModel = new TechnicianListPageViewModel();

                    viewModel.PropertyChanged += TechnicianListPageViewModel_PropertyChanged;
                    //await Navigation.PushAsync(new TechnicianListPage(viewModel));
                    //Detail = new NavigationPage(new TechnicianListPage(viewModel));
                    Application.Current.MainPage = new TechnicianListPage(viewModel); //new NavigationPage(new TechnicianListPage(viewModel));
                    return;
                }

                _masterPage.ListView.SelectedItem = null; // so the item doesn't stay highlighted

                if (item.Title.ToLower() == "settings")
                {
                    AppSettingsPage page = (AppSettingsPage)Activator.CreateInstance(item.TargetType);
                    page.SettingsSaved += AppSettingsPage_SettingsSaved;

                    await Detail.Navigation.PushAsync(page);
                }
                else
                {
                    await Detail.Navigation.PushAsync((Page)Activator.CreateInstance(item.TargetType));
                }

                IsPresented = false;
            }
        }

        public void TechnicianListPageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            TechnicianListPageViewModel vm = sender as TechnicianListPageViewModel;
            switch (e.PropertyName) {
                case "IsLoading":
                    IHud hud = DependencyService.Get<IHud>();
                    if (vm.IsLoading) {
                        //XHUD.HUD.Show("Loading data...", -1, XHUD.MaskType.Black);

                        hud.Show("Loading data...");
                    } else {
                        hud.Dismiss();
                    }
                    break;
                case "IsSignedIn":
                    if (vm.IsSignedIn) {
                        Device.BeginInvokeOnMainThread(() =>
                                                       Application.Current.MainPage = new MainDashboard());
                        //MainPage = new MainDashboard(this);
                    } else {
                        if (!(Application.Current.MainPage is TechnicianListPage)) {
                            Application.Current.MainPage = new TechnicianListPage(vm);
                        }
                    }
                    break;

                default:
                    break;

            }
        }

        private async void AppSettingsPage_SettingsSaved(object sender, EventArgs e)
        {
            await Detail.Navigation.PopAsync();
        }
    }
}
