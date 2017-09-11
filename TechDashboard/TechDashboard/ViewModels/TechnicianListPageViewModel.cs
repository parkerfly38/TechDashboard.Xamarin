using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Runtime.CompilerServices;

using Sage.SData.Client;
using TechDashboard.Models;
using TechDashboard.Data;
using TechDashboard.Services;
using System.Threading;

namespace TechDashboard.ViewModels
{
    public class TechnicianListPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<App_Technician> TechnicianList;

        private bool _isSignedIn;
        public bool IsSignedIn
        {
            get
            {
                return _isSignedIn;
            }
            private set
            {
                _isSignedIn = value;
                NotifyPropertyChanged();
            }
        }

		private bool _isLoading;
		public bool IsLoading {
			get {
				return _isLoading;
			}
			private set {
				_isLoading = value;
				NotifyPropertyChanged();
			}
		}

        public TechnicianListPageViewModel()
        {
            IsSignedIn = false;
			IsLoading = false;
            var listOfTechnicians = App.Database.GetTechniciansFromDB();

            TechnicianList = new ObservableCollection<App_Technician>(listOfTechnicians.ToList());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public async void SignIn(JT_Technician technician)
        {
			IsLoading = true;
			await Task.Run(() => App.Database.SaveTechnicianAsCurrent(technician));
			await Task.Run(() => App.Database.CreateDependentTables(technician));

            App_Settings appSettings = App.Database.GetApplicationSettings();
            appSettings.LoggedInTechnicianNo = technician.TechnicianNo;
            appSettings.LoggedInTechnicianDeptNo = technician.TechnicianDeptNo;
            App.Database.SaveAppSettings(appSettings);

            IsLoading = false;
            IsSignedIn = true;
        }

        public async void SignIn(App_Technician technician)
        {
            if (IsLoading) {
                return;
            } else
            {
                IsLoading = true;
                await Task.Run(() => App.Database.SaveTechnicianAsCurrent(technician));
                //Thread.Sleep(5000);
                await Task.Run(() => App.Database.CreateDependentTables(technician));
                //Thread.Sleep(5000);

                App_Settings appSettings = App.Database.GetApplicationSettings();
                appSettings.LoggedInTechnicianNo = technician.TechnicianNo;
                appSettings.LoggedInTechnicianDeptNo = technician.TechnicianDeptNo;
                App.Database.SaveAppSettings(appSettings);

			IsLoading = false;
            IsSignedIn = true;
            }
        }

    }
}

