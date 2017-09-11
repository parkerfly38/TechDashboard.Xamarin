using System;

using Xamarin.Forms;

using TechDashboard.Data;
using TechDashboard.Models;
using TechDashboard.Views;
using TechDashboard.ViewModels;
using System.Threading.Tasks;
using System.Threading;

namespace TechDashboard
{
	public class App : Application
	{
        protected static TechDashboardDatabase _database;
        protected static App_Technician _currentTechnician;
        protected static App_WorkTicket _currentWorkTicket;

        public static TechDashboardDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new TechDashboardDatabase();
                }
                return _database;
            }
        }

        public static App_Technician CurrentTechnician
        {
            get
            {
                if (_currentTechnician == null)
                {
                    _currentTechnician = new App_Technician(Database.GetCurrentTechnicianFromDb());
                }

                return _currentTechnician;
            }
        }

        public static App_WorkTicket CurrentWorkTicket
        {
            get
            {
                if (_currentWorkTicket == null)
                {
                    _currentWorkTicket = Database.GetCurrentWorkTicket();
                }
                return _currentWorkTicket;
            }
        }

        public void LogOff()
        {
            if(CurrentTechnician != null)
            {
                // puke... log off current tech                
            }

            TechnicianListPageViewModel vm = new TechnicianListPageViewModel();
            MainPage = new TechnicianListPage(vm);
        }

        public void TechnicianListPageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            TechnicianListPageViewModel vm = sender as TechnicianListPageViewModel;
            switch (e.PropertyName)
            {
				case "IsLoading":
				IHud hud = DependencyService.Get<IHud>();
				if(vm.IsLoading) {
					//XHUD.HUD.Show("Loading data...", -1, XHUD.MaskType.Black);

					hud.Show("Loading data...");
				} else {
					hud.Dismiss();
				}
				break;
                case "IsSignedIn":                
                    if (vm.IsSignedIn)
                    {
					Device.BeginInvokeOnMainThread(() =>
					                               MainPage = new MainDashboard());
                        //MainPage = new MainDashboard(this);
                    }
                    else
                    {
                        if (!(MainPage is TechnicianListPage))
                        {
                            MainPage = new TechnicianListPage(vm);
                        }
                    }
                    break;

                default:
                    break;
                
            }
        }

        private void AppSettingsPage_SettingsSaved(object sender, EventArgs e)
        {
            Database.CreateGlobalTables();

            TechnicianListPageViewModel viewModel = new TechnicianListPageViewModel();
            viewModel.PropertyChanged += TechnicianListPageViewModel_PropertyChanged;

            MainPage = new TechnicianListPage(viewModel); 
        }

        public App()
        {
			// Gotta have something set as main page...
			//  maybe we should make some sort of loading page???
			//AppSettingsPage settingsPage = new AppSettingsPage();
			//settingsPage.SettingsSaved += AppSettingsPage_SettingsSaved;

			LoadingPage loadingPage = new LoadingPage();

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomain_UnhandledException;


            MainPage = loadingPage;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        public static void sendException(Exception exception, string thread)
        {
            // dch rkl 12/07/2016 only log error if there is a connection
            // TODO - may want to log errors locally, and send over in Sync when connected
            TechDashboardDatabase oDB = new TechDashboardDatabase();
            bool bHasConnection = oDB.HasDataConnection();

            if (bHasConnection)
            {
                ApplicationLog appLog = new ApplicationLog();
                appLog.log_date = DateTime.Now.ToLongTimeString();
                appLog.log_level = "Error";
                appLog.log_logger = "REST SERVICE";
                appLog.log_message = exception.Message;
                appLog.log_machine_name = System.Environment.MachineName;
                appLog.log_user_name = "WPF TechDashboard";
                appLog.log_call_site = App.Database.GetApplicationSettings().SDataUrl;     // we're going to use their SDATA root

                // dch rkl 12/07/2016 add the call/sub/proc to log
                //appLog.log_thread = "";
                appLog.log_thread = thread;

                appLog.log_stacktrace = exception.StackTrace;

                // dch rkl 12/07/2016 change to new API
                //var client = new RestSharp.RestClient("http://50.200.65.158/tdwsnew/tdwsnew.svc/i/ApplicationLog"); //hard coded to get it back to us
                var client = new RestSharp.RestClient("http://50.200.65.158/techdashboardapi/v1-6/i/ApplicationLog"); //hard coded to get it back to us

                var request = new RestSharp.RestRequest(RestSharp.Method.POST);
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddBody(appLog);

                try
                {
                    var response = client.Execute(request) as RestSharp.RestResponse;
                }
                catch (Exception restException)
                {
                    // can't do much
                }
            }
        }

        public static void sendException(Exception exception)
        {
            ApplicationLog appLog = new ApplicationLog();
            appLog.log_date = DateTime.Now.ToLongTimeString();
            appLog.log_level = "Error";
            appLog.log_logger = "REST SERVICE";
            appLog.log_message = exception.Message;
            appLog.log_machine_name = Device.OS.ToString();
            appLog.log_user_name = "Mobile TechDashboard";
            appLog.log_call_site = App.Database.GetApplicationSettings().SDataUrl;     // we're going to use their SDATA root
            appLog.log_thread = "";
            appLog.log_stacktrace = exception.StackTrace;

            var client = new RestSharp.RestClient("http://50.200.65.158/tdwsnew/tdwsnew.svc/i/ApplicationLog"); //hard coded to get it back to us
            var request = new RestSharp.RestRequest(RestSharp.Method.POST);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddBody(appLog);

            try
            {
                var response = client.Execute(request) as RestSharp.RestResponse;
            }
            catch (Exception restException)
            {
                // can't do much
            }
        }

        protected async override void OnStart()
        {
            // Handle when your app starts
            bool hasValidSetup = false;
            App_Settings appSettings = App.Database.GetApplicationSettings();
            string loggiedintechnicianno;
            JT_Technician technician = null;
            bool tableExists = false;

            try
            {
                hasValidSetup = await Database.HasValidSetup(appSettings);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                await MainPage.DisplayAlert("Error!", ex.Message, "OK");
                throw;
            }

            tableExists = App.Database.TableExists<JT_Technician>();

            // Are the settings valid?
            if (hasValidSetup)
            {
                // Yes, so move on to the technician login
                //App_Settings appSettings = App.Database.GetApplicatioinSettings();
                loggiedintechnicianno = (appSettings.LoggedInTechnicianNo != null) ? appSettings.LoggedInTechnicianNo : "";

                if (tableExists && loggiedintechnicianno.Length > 0) // we've already established we do && Database.HasDataConnection())
                {
                    technician = App.Database.GetTechnician(appSettings.LoggedInTechnicianDeptNo, appSettings.LoggedInTechnicianNo);
                    if (technician != null) {
                        App.Database.SaveTechnicianAsCurrent(technician);
                        App.Database.CreateDependentTables(technician);

                        MainPage = new MainDashboard();
                    } else {
                        if (Database.HasDataConnection())
                            Database.CreateGlobalTables();

                        Thread.Sleep(5000);
                        TechnicianListPageViewModel viewModel = new TechnicianListPageViewModel();
                        viewModel.PropertyChanged += TechnicianListPageViewModel_PropertyChanged;

                        MainPage = new TechnicianListPage(viewModel);
                    }

                } else {
                    if (Database.HasDataConnection())  
                        Database.CreateGlobalTables();

                  Thread.Sleep(5000);
                  TechnicianListPageViewModel viewModel = new TechnicianListPageViewModel ();
                  viewModel.PropertyChanged += TechnicianListPageViewModel_PropertyChanged;

                  MainPage = new TechnicianListPage(viewModel);
                        
                }
            }
            else
            {
                // Invalid settings, so show the settings page.
                //  Otherwise, data calls will never work.
                AppSettingsPage settingsPage = new AppSettingsPage();
                settingsPage.SettingsSaved += AppSettingsPage_SettingsSaved;

                MainPage = settingsPage;

                return;
            }
        }

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
    }
}
