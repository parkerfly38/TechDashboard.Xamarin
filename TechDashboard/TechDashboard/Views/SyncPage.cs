using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechDashboard.ViewModels;

using Xamarin.Forms;
using TechDashboard.Models;
using System.Threading;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Threading.Tasks;

namespace TechDashboard.Views
{

	public class SyncPage : ContentPage
	{
        SyncPageViewModel _vm;

        IHud hud = DependencyService.Get<IHud>();

		Color asbestos = Color.FromHex("#7F8C8D");
		Color emerald = Color.FromHex("#2ECC71");
		Color alizarin = Color.FromHex("#E74C3C");
		Color peterriver = Color.FromHex("#3498DB");

		public SyncPage ()
		{
            hud.Show();
            hud.Dismiss();



			Initialize();	
		}

		protected void Initialize()
		{
			Title = "Sync";

			_vm = new SyncPageViewModel();
			this.BindingContext = _vm;

			BackgroundColor = Color.White;
			Label labelUpdateAppData = new Label() {
				Text = "REFRESH APP DATA",
				TextColor = asbestos,
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null)
			};

			Label labelUpdateWarning = new Label() {
				Text = "This will refresh the data on your application, using the time frame that can be adjusted in Settings.  It requires either mobile data or WiFi connectivity.",
				TextColor = asbestos,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null)
			};

			Button buttonUpdateData = new Button() {
				TextColor = Color.White,
				BackgroundColor = emerald,
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null)
			};
			//later, add check for connectivity and make this presentation conditional
			buttonUpdateData.Text = "UPDATE APP DATA";
            buttonUpdateData.Clicked += ButtonUpdateData_Clicked;

			BoxView separator = new BoxView() {
				HeightRequest = 1,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = asbestos
			};

			StackLayout refreshLayout = new StackLayout {
				Padding = 30,
				Children = {
					labelUpdateAppData,
					labelUpdateWarning,
					buttonUpdateData
				}
			};

			Label labelSendDataTitle = new Label() {
				Text = "UPLOAD DATA",
				TextColor = asbestos,
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null)
			};

			Label labelSendWarning = new Label() {
				Text = "This will send data from Tech Dashboard to the central server.  This operations requires either mobile data or WiFi connections.",
				TextColor = asbestos,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null)
			};

			Label labelSendCount = new Label () {
				TextColor = Color.Red,
				FontFamily = Device.OnPlatform ("OpenSans-Regular", "sans-serif", null)
			};
			labelSendCount.SetBinding (Label.TextProperty, "UpdateCount");

			Label labelSendContent = new Label () {
				TextColor = Color.Red,
				FontFamily = Device.OnPlatform ("OpenSans-Regular", "sans-serif", null),
				Text = "records awaiting sync."
			};

			Button buttonSendData = new Button() {
				TextColor = Color.White,
				BackgroundColor = emerald,
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null)
			};
			// same as bove, add check for connectivity and make this enabled conditional
			buttonSendData.Text = "SYNC DATA";
			buttonSendData.Clicked += ButtonSendData_Clicked;

            Button buttonViewData = new Button() {
                TextColor = Color.White,
                BackgroundColor = alizarin,
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null)
            };
            buttonViewData.Text = "PREVIEW DATA";
            buttonViewData.Clicked += ButtonViewData_Clicked;

			StackLayout titleLayout = new StackLayout {
				BackgroundColor = peterriver,
				Padding = 50,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Start,
				Children = {
					new Xamarin.Forms.Label { 
						Text = "SYNC",
						FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
						TextColor = Color.White,
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.Center
					}
				}
			};

			StackLayout syncLayout = new StackLayout
			{
				Padding = 30,
				Children =
				{
					labelSendDataTitle,
					labelSendWarning,
					new StackLayout
					{
						Spacing = 5,
						Orientation = StackOrientation.Horizontal,
						Children =
						{
							labelSendCount,
							labelSendContent
						}
					},
                    buttonViewData,
					buttonSendData
				}
			};

			Content = new StackLayout {
				Spacing = 10,
				Children = {
					titleLayout,
					refreshLayout,
					separator,
					syncLayout
				}
			};
		}

		async void ButtonSendData_Clicked (object sender, EventArgs e)
		{
            //IHud hud = DependencyService.Get<IHud>();
            hud.Show("Syncing data...");
            int iSyncSuccess = 0;
            int iSyncFailed = 0;
            await Task.Run(() => _vm.syncWithServer(ref iSyncSuccess, ref iSyncFailed));
            string syncMessage = "";
            if (iSyncFailed > 0 && iSyncSuccess > 0)
            {
                syncMessage = string.Format("{0} transactions synced successfully; {1} transactions failed during sync.", iSyncSuccess, iSyncFailed);
            }
            else if (iSyncSuccess > 0 && iSyncFailed == 0)
            {
                syncMessage = string.Format("{0} transactions synced successfully.", iSyncSuccess);
            }
            else if (iSyncSuccess == 0 && iSyncFailed > 0)
            {
                syncMessage = string.Format("{0} transactions failed during sync.", iSyncFailed);
            }
            else
            {
                syncMessage = "Sync completed successfully.";
            }

            await DisplayAlert("Synchronization", syncMessage, "OK");
            hud.Dismiss();
		}

        async void ButtonUpdateData_Clicked (object sender, EventArgs e)
		{

            hud.Show("Loading data...");
            //App.Database.CreateDependentTables(currentTechnician);
            int awaier = await UpdateData();
            hud.Dismiss();
        }

        private async Task<int> UpdateData()
        {
            JT_Technician currentTechnician = App.Database.GetCurrentTechnicianFromDb();
            await Task.Run(() => App.Database.CreateGlobalTables());
            await Task.Run(() => App.Database.SaveTechnicianAsCurrent(currentTechnician));
            await Task.Run(() => App.Database.CreateDependentTables(currentTechnician));
            return 1;
        }

        void ButtonViewData_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SyncDetails());
        }
	}
}

