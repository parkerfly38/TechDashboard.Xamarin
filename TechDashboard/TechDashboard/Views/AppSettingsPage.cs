using System;
using Xamarin.Forms;

using TechDashboard.Data;
using TechDashboard.Tools;
using TechDashboard.ViewModels;
using System.Collections.Generic;
using Rkl.Erp.Sage.Sage100.TableObjects;
using System.Net;

namespace TechDashboard.Views
{
    public class AppSettingsPage : ContentPage
    {
        void _entryScheduleDaysAfter_Unfocused(object sender, EventArgs e)
        {
            int value;
            if (!int.TryParse(_entryScheduleDaysAfter.Text, out value)) {
                DisplayAlert("Invalid entry", "This field accepts integer values only", "OK");
                _entryScheduleDaysAfter.Text = "";
            }
        }

        void _entryScheduleDaysBefore_Unfocused(object sender, EventArgs e)
        {
            int value;
            if (!int.TryParse(_entryScheduleDaysBefore.Text, out value)) {
                DisplayAlert("Invalid entry", "This field accepts integer values only", "OK");
                _entryScheduleDaysBefore.Text = "";
            }
        }

        IHud hud = DependencyService.Get<IHud>();
        
        public event EventHandler SettingsSaved;
        public void OnSettingsSaved(object sender, EventArgs e)
        {
            if(SettingsSaved != null)
            {
                SettingsSaved(sender, e);
            }
        }


        AppSettingsPageViewModel _vm;
        Label _labelHeading;
        BindablePicker _pickerErpConnectionType;
        Switch _switchIsUsingHttps;
        Label _labelProtocol;
        Entry _entrySDataUrl;
        Entry _entryRestServiceUrl;
        Entry _entrySDataUserId;
        Entry _entrySDataPassword;
        //Entry _entrySDataPasswordConfirm;
        Entry _entryScheduleDaysBefore;
        Entry _entryScheduleDaysAfter;
        Button _buttonSave;
        Switch _24hourTime;
        Entry _entryLoggedInTechnicianNo;
        Entry _entryLoggedInTechnicianDeptNo;


        public AppSettingsPage()
        {
            InitializePage();
        }

        protected void InitializePage()
        {
            _vm = new AppSettingsPageViewModel();

            BindingContext = _vm;

            Color asbestos = Color.FromHex("#7f8C8d");
            BackgroundColor = Color.White;

            _labelHeading = new Xamarin.Forms.Label();
            _labelHeading.Text = "APP SETTINGS";
            _labelHeading.FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null);
            _labelHeading.FontSize = 22;
            _labelHeading.TextColor = Color.White;
            _labelHeading.HorizontalTextAlignment = TextAlignment.Center;
            _labelHeading.VerticalTextAlignment = TextAlignment.Center;

            Grid titleLayout = new Grid() {
                BackgroundColor = Color.FromHex("#2980b9"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 80
            };
            titleLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            titleLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            titleLayout.Children.Add(_labelHeading, 0, 0);

            _pickerErpConnectionType = new BindablePicker();
            _pickerErpConnectionType.Title = "Connection Type";
            _pickerErpConnectionType.ItemsSource = Enum.GetNames(typeof(ConnectionType));
            _pickerErpConnectionType.SetBinding(BindablePicker.SelectedItemProperty, "ErpConnectionTypeAsString");
            _pickerErpConnectionType.SelectedIndexChanged += PickerErpConnectionType_SelectedIndexChanged;
            _pickerErpConnectionType.IsVisible = false;

            _switchIsUsingHttps = new Switch();
            _switchIsUsingHttps.Toggled += SwitchIsUsingHttps_Toggled;
            _switchIsUsingHttps.SetBinding(Switch.IsToggledProperty, "IsUsingHttps");

            _labelProtocol = new Label();
            _labelProtocol.Text = (_vm.IsUsingHttps ? @"https://" : @"http://");
            _labelProtocol.FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null);
            _labelProtocol.TextColor = asbestos;

            _entrySDataUrl = new Entry();
            _entrySDataUrl.Keyboard = Keyboard.Url;
            _entrySDataUrl.Placeholder = "restserver/webservice.svc";
            _entrySDataUrl.SetBinding(Entry.TextProperty, "RestServiceUrl");
            _entrySDataUrl.Unfocused += _entrySDataUrl_Unfocused;     // dch rkl 10/17/2016

            _labelProtocol.TextColor = asbestos;
            _entryRestServiceUrl = new Entry();
            _entryRestServiceUrl.Keyboard = Keyboard.Url;
            _entryRestServiceUrl.Placeholder = @"http://restserver/TdWs/TdWs.svc";
            _entryRestServiceUrl.SetBinding(Entry.TextProperty, "RestServiceUrl");
            _entryRestServiceUrl.IsVisible = false;

            _entrySDataUserId = new Entry();
            _entrySDataUserId.Keyboard = Keyboard.Text;
            //_entrySDataUserId.Text = _vm.SDataUserId;
            _entrySDataUserId.SetBinding(Entry.TextProperty, "SDataUserId");

            _entrySDataPassword = new Entry();
            _entrySDataPassword.IsPassword = true;
            _entrySDataPassword.Keyboard = Keyboard.Text;
            //_entrySDataPassword.Text = _vm.SDataPassword;
            _entrySDataPassword.SetBinding(Entry.TextProperty, "SDataPassword");

            _entryScheduleDaysBefore = new Entry();
            _entryScheduleDaysBefore.Keyboard = Keyboard.Numeric;
            _entryScheduleDaysBefore.SetBinding(Entry.TextProperty, "ScheduleDaysBefore");
            //_entryScheduleDaysBefore.TextChanged += _entryScheduleDaysBefore_TextChanged;
            _entryScheduleDaysBefore.Unfocused += _entryScheduleDaysBefore_Unfocused; 
            _entryScheduleDaysBefore.Text = "1";          // dch rkl 10/17/2016 Default to 1

            _entryScheduleDaysAfter = new Entry();
            _entryScheduleDaysAfter.Keyboard = Keyboard.Numeric;
            _entryScheduleDaysAfter.SetBinding(Entry.TextProperty, "ScheduleDaysAfter");
            //_entryScheduleDaysAfter.TextChanged += _entryScheduleDaysAfter_TextChanged;
            _entryScheduleDaysAfter.Unfocused += _entryScheduleDaysAfter_Unfocused;
            _entryScheduleDaysAfter.Text = "1";          // dch rkl 10/17/2016 Default to 1

            Label label24HourTime = new Label();
            label24HourTime.Text = "Use 24 Hour Time";
            label24HourTime.FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null);
            label24HourTime.TextColor = asbestos;

            Label labelTechnicianNo = new Label();
            labelTechnicianNo.Text = "Default Tech Number";
            labelTechnicianNo.FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null);
            labelTechnicianNo.TextColor = asbestos;

            _entryLoggedInTechnicianNo = new Entry();
            _entryLoggedInTechnicianNo.SetBinding(Entry.TextProperty, "LoggedInTechnicianNo");
            _entryLoggedInTechnicianNo.WidthRequest = 100;

            Label labelTechnicianDeptNo = new Label();
            labelTechnicianDeptNo.Text = "Default Tech Dept Number";
            labelTechnicianDeptNo.FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null);
            labelTechnicianDeptNo.TextColor = asbestos;

            _entryLoggedInTechnicianDeptNo = new Entry();
            _entryLoggedInTechnicianDeptNo.SetBinding(Entry.TextProperty, "LoggedInTechnicianDeptNo");
            _entryLoggedInTechnicianDeptNo.WidthRequest = 100;

            Label labelDeviceName = new Label();
            labelDeviceName.Text = "Device Name";
            labelDeviceName.FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null);
            labelDeviceName.TextColor = asbestos;

            Entry entryDeviceName = new Entry();
            entryDeviceName.SetBinding(Entry.TextProperty, "DeviceName");
            entryDeviceName.WidthRequest = 100;

            _24hourTime = new Switch();
            _24hourTime.SetBinding(Switch.IsToggledProperty, "Use24HourTime");

            _buttonSave = new Button();
            _buttonSave.Clicked += ButtonSave_Clicked;
            _buttonSave.FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null);
            _buttonSave.Text = "SAVE";
            _buttonSave.BackgroundColor = Color.FromHex("#2ECC71");
            _buttonSave.TextColor = Color.White;
            _buttonSave.HorizontalOptions = LayoutOptions.FillAndExpand;

            Grid topGrid = new Grid();
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(250, GridUnitType.Absolute) });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(250, GridUnitType.Absolute) });

            Label labelEncrypt = new Label {
                Text = "Use Encrypted Connection",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            };
            topGrid.Children.Add(labelEncrypt, 0, 1);
            topGrid.Children.Add(_switchIsUsingHttps, 1, 1);
            topGrid.Children.Add(_labelProtocol, 0, 2);
            Grid.SetColumnSpan(_labelProtocol, 2);

            Grid dtlGrid = new Grid();
            dtlGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            dtlGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            dtlGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            dtlGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            dtlGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            dtlGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            dtlGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            dtlGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            dtlGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            dtlGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            dtlGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            dtlGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(160, GridUnitType.Absolute) });
            dtlGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40, GridUnitType.Absolute) });
            dtlGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90, GridUnitType.Absolute) });
            dtlGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(350, GridUnitType.Absolute) });

            dtlGrid.Children.Add(new Label {
                Text = "URL",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 0);
            dtlGrid.Children.Add(_entrySDataUrl, 1, 0);
            Grid.SetColumnSpan(_entrySDataUrl, 3);

            dtlGrid.Children.Add(new Label {
                Text = "REST Service Url",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos,
                IsVisible = false
            }, 0, 1);
            dtlGrid.Children.Add(_entryRestServiceUrl, 1, 1);
            Grid.SetColumnSpan(_entryRestServiceUrl, 3);

            //dtlGrid.Children.Add(new Label
            /*{
                Text = "User ID",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 0, 2);
            dtlGrid.Children.Add(_entrySDataUserId, 1, 2);
            Grid.SetColumnSpan(_entrySDataUserId, 2);

            dtlGrid.Children.Add(new Label
            {
                Text = "Password",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 0, 3);
            dtlGrid.Children.Add(_entrySDataPassword, 1, 3);
            Grid.SetColumnSpan(_entrySDataPassword, 2);*/

            dtlGrid.Children.Add(new Label {
                Text = "Days Before",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 4);
            dtlGrid.Children.Add(_entryScheduleDaysBefore, 1, 4);
            Grid.SetColumnSpan(_entryScheduleDaysBefore, 3);

            dtlGrid.Children.Add(new Label {
                Text = "Days After",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 5);
            dtlGrid.Children.Add(_entryScheduleDaysAfter, 1, 5);
            Grid.SetColumnSpan(_entryScheduleDaysAfter, 3);

            dtlGrid.Children.Add(new Label {
                Text = "Conn. Type",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos,
                IsVisible = false
            }, 0, 6);
            dtlGrid.Children.Add(_pickerErpConnectionType, 1, 6);
            Grid.SetColumnSpan(_pickerErpConnectionType, 2);
            dtlGrid.Children.Add(labelTechnicianNo, 0, 7);
            dtlGrid.Children.Add(_entryLoggedInTechnicianNo, 1, 7);
            Grid.SetColumnSpan(_entryLoggedInTechnicianNo, 3);
            dtlGrid.Children.Add(labelTechnicianDeptNo, 0, 8);
            dtlGrid.Children.Add(_entryLoggedInTechnicianDeptNo, 1, 8);
            Grid.SetColumnSpan(_entryLoggedInTechnicianDeptNo, 3);
            dtlGrid.Children.Add(labelDeviceName, 0, 9);
            dtlGrid.Children.Add(entryDeviceName, 1, 9);
            Grid.SetColumnSpan(entryDeviceName, 3);
            dtlGrid.Children.Add(label24HourTime, 0, 10);
            dtlGrid.Children.Add(_24hourTime, 1, 10);
            Grid.SetColumnSpan(_24hourTime, 3);

            // dch rkl 10/12/2016 Do not enable save button until a valid url is entered
            TestIfValidURL(_vm.RestServiceUrl, _vm.IsUsingHttps);

            Content =
                new StackLayout {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        titleLayout,
                        new StackLayout
                        {
                            Padding = 30,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Children =
                            {
                                topGrid,
                                dtlGrid,
                                _buttonSave
                            }
                        }
                    }
                };
        }


        private void PickerErpConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // empty
        }

        private void ButtonSave_Clicked(object sender, System.EventArgs e)
        {
            // dch rkl 10/17/2016 Device ID Required
            if (_vm.DeviceName == null)
            {
                DisplayAlert("Invalid Device Name", "A device name must be provided.", "OK");
                return;
            }

            _vm.SaveAppSettings();
            string techno = (_vm.LoggedInTechnicianNo != null) ? _vm.LoggedInTechnicianNo : "";
            string techdeptno = (_vm.LoggedInTechnicianDeptNo != null) ? _vm.LoggedInTechnicianDeptNo : "";
            JT_Technician technician = App.Database.GetTechnician(_vm.LoggedInTechnicianDeptNo, _vm.LoggedInTechnicianNo);

            // dch rkl 11/02/2016 If this is the initial setup, the technician table will not be loaded yet
            if (technician == null)
            {
                List<JT_Technician> technicians = App.Database.GetErpData<JT_Technician>("where",
                    String.Format("TechnicianNo eq '{0}' and TechnicianDeptNo eq '{1}'", techno, techdeptno));
                if (technicians.Count > 0) { technician = technicians[0]; }
            }

            if ((techdeptno.Length > 0 && techno.Length > 0) && technician == null) {
                DisplayAlert("Invalid Technician", "The technician you entered is not valid.  Please check your entries and try again.", "OK");
                return;
            }
            bool hasValidSetup = App.Database.HasValidSetup().Result;
            if (!hasValidSetup) {
                DisplayAlert("SETTINGS VERIFICATION FAILED", "These settings do not appear to work.  Please check your Internet connection or verify your settings.", "OK");
                return;
            }
			OnSettingsSaved(sender, e);
        }

        private void SwitchIsUsingHttps_Toggled(object sender, ToggledEventArgs e)
        {
            if(_switchIsUsingHttps.IsToggled)
            {
                _labelProtocol.Text = @"https://";
            }
            else
            {
                _labelProtocol.Text = @"http://";
            }
        }

        // dch rkl 10/12/2016 validate url
        private void TestIfValidURL(string sUrl, bool bIsHttps)
        {
            // If url is not blank, validate it
            // If valid, enable technician number and department

            bool bValid = false;

            if (sUrl == null) { sUrl = ""; }
            if (bIsHttps == null) { bIsHttps = false; }

            if (sUrl.Length > 0)
            {
                string sPrefix = @"http://";
                if (bIsHttps == true) { sPrefix = "https://"; }
                sUrl = String.Format("{0}{1}", sPrefix, sUrl);

                // set up the proper URL
                if (!sUrl.EndsWith(@"/"))
                {
                    sUrl += @"/";
                }
                sUrl += @"test";

                try
                {
                    HttpWebRequest request = WebRequest.Create(sUrl) as HttpWebRequest;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream());
                    var sData = sr.ReadToEnd();
                    bValid = true;
                }
                catch (Exception ex)
                {
                    bValid = false;
                }
            }

            if (bValid)
            {
                _entryScheduleDaysAfter.IsVisible = true;
                _entryLoggedInTechnicianNo.IsVisible = true;
                _entryLoggedInTechnicianDeptNo.IsVisible = true;
                _buttonSave.IsEnabled = true;
            }
            else
            {
                _entryScheduleDaysAfter.IsVisible = false;
                _entryLoggedInTechnicianNo.IsVisible = false;
                _entryLoggedInTechnicianDeptNo.IsVisible = false;
                _buttonSave.IsEnabled = false;
            }
        }

        private void _entrySDataUrl_Unfocused(object sender, EventArgs e)
        {
            string sUrl = "";
            if (_entrySDataUrl.Text != null) { sUrl = _entrySDataUrl.Text.Trim();  }

            TestIfValidURL(sUrl, (bool)_switchIsUsingHttps.IsToggled);
        }
    }
}