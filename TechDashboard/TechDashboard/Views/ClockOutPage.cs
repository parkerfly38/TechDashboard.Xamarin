using System;

using Xamarin.Forms;
using TechDashboard.Models;
using TechDashboard.ViewModels;
using TechDashboard.Tools;

namespace TechDashboard.Views
{
    public class ClockOutPage : ContentPage
    {

        ClockOutPageViewModel _vm;
        DatePicker _pickerStartDate;
        TimePicker _pickerStartTime;
        DatePicker _pickerDepartDate;
        TimePicker _pickerDepartTime;
        Editor _editorHoursBilled;
        BindablePicker _pickerTechnicianStatus;
        BindablePicker _pickerTicketStatus;
        BindablePicker _pickerEarningsCode;
        BindablePicker _pickerActivityCode;
        BindablePicker _pickerBillable;
        BindablePicker _pickerDepartment;
        Editor _editorMeterReading;
        Editor _editorWorkPerformed;
        Editor _editorHoursWorked;
        Editor _editorRefRate;
        Editor _editorBillableRate;
        Editor _editorBillableAmount;
        Switch switchLaborCoveredSvcAgreement;
        Switch switchLaborCoveredWarranty;

        string _captureTimeInTimeTracker;
        decimal _MinHourlyCostIncrement;
        CI_Options _ciOptions;

        Xamarin.Forms.Label _labelTitle;

        public ClockOutPage(App_WorkTicket workTicket)
        {
            // Set the page title.
            //Title = "Clock Out";

            _vm = new ClockOutPageViewModel(workTicket);


            Color asbestos = Color.FromHex("#7f8c8d");

            BackgroundColor = Color.White;
            _labelTitle = new Xamarin.Forms.Label();
            _labelTitle.Text = "CLOCK OUT";
            _labelTitle.FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null);
            _labelTitle.FontSize = 22;
            _labelTitle.TextColor = Color.White;
            _labelTitle.HorizontalTextAlignment = TextAlignment.Center;
            _labelTitle.VerticalTextAlignment = TextAlignment.Center;

            Grid titleLayout = new Grid() {
                BackgroundColor = Color.FromHex("#2980b9"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 80
            };
            titleLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            titleLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            titleLayout.Children.Add(_labelTitle, 0, 0);

            Label lblServiceTicket = new Label();
            lblServiceTicket.TextColor = asbestos;
            lblServiceTicket.Text = _vm.ScheduleDetail.ServiceTicketNumber;

            JT_Technician technician = App.Database.GetCurrentTechnicianFromDb();

            Label lblEmployeeNo = new Label();
            lblEmployeeNo.TextColor = asbestos;
            lblEmployeeNo.Text = technician.FormattedTechnicianNo; //_vm.CurrentTechnician.FormattedTechnicianNo;

            Label lblEmployeeName = new Label();
            lblEmployeeName.TextColor = asbestos;
            lblEmployeeName.Text = string.Format("{0} {1}", technician.FirstName, technician.LastName); //_vm.CurrentTechnician.FirstName + " " + _vm.CurrentTechnician.LastName;

            Grid clockoutLayout = new Grid();
            clockoutLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            clockoutLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // service ticket line
            clockoutLayout.Children.Add(new Label() {
                Text = "Service Ticket", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 0);
            Grid.SetColumnSpan(lblServiceTicket, 3);
            clockoutLayout.Children.Add(lblServiceTicket, 1, 0);
            // employee number
            clockoutLayout.Children.Add(new Label() {
                Text = "Employee Number", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 1);
            clockoutLayout.Children.Add(lblEmployeeNo, 1, 1);
            clockoutLayout.Children.Add(lblEmployeeName, 2, 1);
            //  checks switches
            Switch switchWarrantyRepair = new Switch() { IsEnabled = false };
            if (_vm.WorkTicket.DtlWarrantyRepair == "Y") switchWarrantyRepair.IsToggled = true;
            clockoutLayout.Children.Add(new Label() {
                Text = "Warranty Repair", TextColor = asbestos,
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null), FontSize = 12
            }, 0, 2);
            clockoutLayout.Children.Add(switchWarrantyRepair, 1, 2);
            bool isChkd = false;
            switchLaborCoveredWarranty = new Switch() { IsEnabled = false };
            if (_vm.WorkTicket.StatusDate != null && _vm.WorkTicket.RepairItem.MfgLaborWarrantyPeriod != null) {
                TimeSpan tsDateDiff = _vm.WorkTicket.RepairItem.MfgLaborWarrantyPeriod.Subtract(_vm.WorkTicket.StatusDate);
                if (tsDateDiff.TotalDays > 0 && _vm.WorkTicket.DtlWarrantyRepair == "Y") {
                    switchLaborCoveredWarranty.IsToggled = true;
                    isChkd = true;
                }
            }
            if (_vm.WorkTicket.StatusDate != null && _vm.WorkTicket.RepairItem.IntLaborWarrantyPeriod != null) {
                TimeSpan tsDateDiff = _vm.WorkTicket.RepairItem.IntLaborWarrantyPeriod.Subtract(_vm.WorkTicket.StatusDate);
                if (tsDateDiff.TotalDays > 0 && _vm.WorkTicket.DtlWarrantyRepair == "Y") {
                    switchLaborCoveredWarranty.IsToggled = true;
                    isChkd = true;
                }
            }
            clockoutLayout.Children.Add(new Label() {
                Text = "Labor Covered on Warranty", TextColor = asbestos, FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                FontSize = 12
            }, 2, 2);
            clockoutLayout.Children.Add(switchLaborCoveredWarranty, 3, 2);
            Switch switchServiceAgreementRepair = new Switch() { IsEnabled = false };
            if (_vm.WorkTicket.DtlCoveredOnContract == "Y") {
                switchServiceAgreementRepair.IsToggled = true;
            }
            clockoutLayout.Children.Add(new Label() {
                Text = "Svc Agmt Repair", TextColor = asbestos,
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null), FontSize = 12
            }, 0, 3);
            clockoutLayout.Children.Add(switchServiceAgreementRepair, 1, 3);
            switchLaborCoveredSvcAgreement = new Switch() { IsEnabled = false };
            if (_vm.WorkTicket.IsPreventativeMaintenance && _vm.WorkTicket.ServiceAgreement.IsPMLaborCovered) {
                switchLaborCoveredSvcAgreement.IsToggled = true;
            } else if (_vm.WorkTicket.IsPreventativeMaintenance == false && _vm.WorkTicket.IsServiceAgreementRepair && _vm.WorkTicket.ServiceAgreement.IsLaborCovered) {
                switchLaborCoveredSvcAgreement.IsToggled = true;
            }
            clockoutLayout.Children.Add(new Label() {
                Text = "Labor Covered on Svc Agmt", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                FontSize = 12, TextColor = asbestos
            }, 2, 3);
            clockoutLayout.Children.Add(switchLaborCoveredSvcAgreement, 3, 3);

            // date part of the grid
            _pickerBillable = new BindablePicker();
            _pickerBillable.ItemsSource = _vm.BillableList;
            _pickerBillable.SelectedIndexChanged += _pickerBillable_SelectedIndexChanged;

            DateTime dtStartDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) + _vm.StartTime;

            _pickerStartDate = new DatePicker();
            _pickerStartDate.Date = technician.CurrentStartDate; //_vm.CurrentTechnician.CurrentStartDate;

            _pickerStartTime = new TimePicker();
            _pickerStartTime.Time = _vm.StartTime;
            _pickerStartTime.IsEnabled = false;
            clockoutLayout.Children.Add(new Label() {
                Text = "Start Time", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 4);
            clockoutLayout.Children.Add(_pickerStartDate, 1, 4);
            clockoutLayout.Children.Add(_pickerStartTime, 2, 4);

            _pickerDepartDate = new DatePicker();
            _pickerDepartDate.Date = DateTime.Now;

            _pickerDepartTime = new TimePicker { Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second) };
            _pickerDepartTime.Unfocused += PickerDepartTime_Unfocused;

            if (App.Database.GetApplicationSettings().TwentyFourHourTime) {
                _pickerStartTime.Format = "HH:mm";
                _pickerDepartTime.Format = "HH:mm";
            }
            //if (_pickerStartTime.Time != null && _pickerDepartTime.Time != null) {
            //    SetHoursBilled();
            //}
            clockoutLayout.Children.Add(new Label() {
                Text = "Depart Time", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 5);
            clockoutLayout.Children.Add(_pickerDepartDate, 1, 5);
            clockoutLayout.Children.Add(_pickerDepartTime, 2, 5);

            //hours worked
            clockoutLayout.Children.Add(new Label() {
                Text = "Hours Worked", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 6);
            _editorHoursWorked = new Editor();
            _editorHoursWorked.Text = Math.Round((_pickerDepartTime.Time - _pickerStartTime.Time).TotalHours, 2).ToString();
            _editorHoursWorked.Keyboard = Keyboard.Numeric;
            _editorHoursWorked.IsEnabled = false;
            clockoutLayout.Children.Add(_editorHoursWorked, 1, 6);
            Grid.SetColumnSpan(_pickerBillable, 2);
            _pickerBillable.SetBinding(BindablePicker.DisplayPropertyProperty, "BillableDesc");
            clockoutLayout.Children.Add(_pickerBillable, 2, 6);

            //hours billed
            clockoutLayout.Children.Add(new Label() {
                Text = "Billable Hours", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 7);
            _editorHoursBilled = new Editor();
            _editorHoursBilled.Text = Math.Round((_pickerDepartTime.Time - _pickerStartTime.Time).TotalHours, 2).ToString();
            _editorHoursBilled.Keyboard = Keyboard.Numeric;
            clockoutLayout.Children.Add(_editorHoursBilled, 1, 7);
            clockoutLayout.Children.Add(new Label() {
                Text = "Ref Rate", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 2, 7);
            _editorRefRate = new Editor() { IsEnabled = false };
            clockoutLayout.Children.Add(_editorRefRate, 3, 7);

            // rates
            clockoutLayout.Children.Add(new Label() {
                Text = "Billable Rate", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 8);
            _editorBillableRate = new Editor() { IsEnabled = false };
            clockoutLayout.Children.Add(_editorBillableRate, 1, 8);
            clockoutLayout.Children.Add(new Label() {
                Text = "Billable Amount", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 2, 8);
            _editorBillableAmount = new Editor() { IsEnabled = false };
            clockoutLayout.Children.Add(_editorBillableAmount, 3, 8);

            _pickerTechnicianStatus = new BindablePicker { Title = "Technician Status", ItemsSource = _vm.TechnicianStatusList };
            _pickerTechnicianStatus.SetBinding(BindablePicker.DisplayPropertyProperty, "StatusDescription");
            for (int i = 0; i < _pickerTechnicianStatus.Items.Count; i++) {
                if (_pickerTechnicianStatus.Items[i] == _vm.DefaultDepartStatusCodeDescription) {
                    _pickerTechnicianStatus.SelectedIndex = i;
                    break;
                }
            }
            clockoutLayout.Children.Add(new Label() {
                Text = "Technician Status", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 9);
            Grid.SetColumnSpan(_pickerTechnicianStatus, 3);
            clockoutLayout.Children.Add(_pickerTechnicianStatus, 1, 9);

            _pickerTicketStatus = new BindablePicker { Title = "Service Ticket Status", ItemsSource = _vm.ServiceTicketStatusList };
            _pickerTicketStatus.SetBinding(BindablePicker.DisplayPropertyProperty, "Description");
            /*for (int i = 0; i < _pickerTicketStatus.Items.Count; i++) {
                if (_pickerTicketStatus.Items[i].Substring(0, 3) == _vm.DefaultServiceTicketStatusCode) {
                    _pickerTicketStatus.SelectedIndex = i;
                    break;
                }
            }*/
            clockoutLayout.Children.Add(new Label() {
                Text = "Ticket Status", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 10);
            Grid.SetColumnSpan(_pickerTicketStatus, 3);
            clockoutLayout.Children.Add(_pickerTicketStatus, 1, 10);

            _pickerActivityCode = new BindablePicker { Title = "Activity Code", ItemsSource = _vm.ActivityCodeList };
            _pickerActivityCode.SetBinding(BindablePicker.DisplayPropertyProperty, "ActivityDescription");
            for (int i = 0; i < _pickerActivityCode.Items.Count; i++) {
                if (_pickerActivityCode.Items[i] == _vm.DefaultActivityCode) {
                    _pickerActivityCode.SelectedIndex = i;
                    break;
                }
            }
            clockoutLayout.Children.Add(new Label() {
                Text = "Activity Code", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 11);
            Grid.SetColumnSpan(_pickerActivityCode, 3);
            clockoutLayout.Children.Add(_pickerActivityCode, 1, 11);

            _pickerDepartment = new BindablePicker { Title = "Department", ItemsSource = _vm.DepartmentCodesList };
            _pickerDepartment.SetBinding(BindablePicker.DisplayPropertyProperty, "MiscellaneousCode");
            JT_ActivityCode dfltActCode = new JT_ActivityCode();
            if (_vm.DefaultActivityCode != null) {
                dfltActCode = App.Database.GetActivityCodeFromDB(_vm.DefaultActivityCode);
            }
            if (dfltActCode != null) {
                for (int i = 0; i < _pickerDepartment.Items.Count; i++) {
                    if (_pickerDepartment.Items[i] == dfltActCode.DeptWorkedIn ) {
                        _pickerDepartment.SelectedIndex = i;
                        break;
                    }
                }
            }
            clockoutLayout.Children.Add(new Label() {
                Text = "Department", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 12);
            Grid.SetColumnSpan(_pickerDepartment, 3);
            clockoutLayout.Children.Add(_pickerDepartment, 1, 12);

            _pickerEarningsCode = new BindablePicker { Title = "Earnings Code", ItemsSource = _vm.EarningsCodeList };
            _pickerEarningsCode.SetBinding(BindablePicker.DisplayPropertyProperty, "EarningsDeductionDesc");
            /*for (int i = 0; i < _pickerEarningsCode.Items.Count; i++) {
                if (_pickerEarningsCode.Items[i] == _vm.DefaultEarningCode) {
                    _pickerEarningsCode.SelectedIndex = i;
                    break;
                }
            }*/
            clockoutLayout.Children.Add(new Label() {
                Text = "Earnings Code", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 13);
            Grid.SetColumnSpan(_pickerEarningsCode, 3);
            clockoutLayout.Children.Add(_pickerEarningsCode, 1, 13);

            _editorMeterReading = new Editor();
            _editorMeterReading.Keyboard = Keyboard.Numeric;
            _editorMeterReading.HeightRequest = 100;
            _editorMeterReading.IsEnabled = _vm.IsRepairItemAnEquipmentAsset;
            clockoutLayout.Children.Add(new Label() {
                Text = "Meter Reading", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 14);
            Grid.SetColumnSpan(_editorMeterReading, 3);
            clockoutLayout.Children.Add(_editorMeterReading, 1, 14);

            _editorWorkPerformed = new Editor();
            _editorWorkPerformed.HeightRequest = 100;
            _editorWorkPerformed.TextChanged += EditorWorkPerformed_TextChanged;
            clockoutLayout.Children.Add(new Label() {
                Text = "Work Performed", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 15);
            Grid.SetColumnSpan(_editorWorkPerformed, 3);
            clockoutLayout.Children.Add(_editorWorkPerformed, 1, 15);

            // create a "clock out" button to go back
            Xamarin.Forms.Button buttonClockOut = new Button() {
                Text = "Clock Out",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = Color.White,
                BackgroundColor = asbestos,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            buttonClockOut.Clicked += ButtonClockOut_Clicked;

            // create a "cancel" button to go back
            Xamarin.Forms.Button buttonCancel = new Button() {
                Text = "Cancel",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#E74C3C"),
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            buttonCancel.Clicked += buttonCancel_Clicked;

            Content = new StackLayout {
                Children =
                {
                    titleLayout,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        Padding = 30,
                        Children =
                        {
                            clockoutLayout,
                            buttonClockOut,
                            buttonCancel
                        }
                    }
                }
            };
        }

        private void SetHoursBilled()
        {
            _editorHoursBilled.Text = Math.Round((_pickerDepartTime.Time - _pickerStartTime.Time).TotalHours, 2).ToString();
            _editorHoursWorked.Text = Math.Round((_pickerDepartTime.Time - _pickerStartTime.Time).TotalHours, 2).ToString();
        }

        private void SetRefRate()
        {
            decimal billingRate = 0;

            decimal billingRateMultiplier = 1;
            JT_ActivityCode activityCode = (JT_ActivityCode)_pickerActivityCode.SelectedItem;
            if (activityCode != null) {
                billingRateMultiplier = activityCode.BillingRateMultiplier;
                if (billingRateMultiplier == 0)
                    billingRateMultiplier = 1;
            }

            if (_vm.WorkTicket.DtlCoverageExceptionCode != null && _vm.WorkTicket.DtlCoverageExceptionCode.Trim().Length > 0) {
                billingRate = _vm.WorkTicket.DtlCoverageExceptionFixedRate;
            }
            if (_vm.WorkTicket.IsPreventativeMaintenance) {
                billingRate = Math.Round(_vm.WorkTicket.ServiceAgreement.PmDetail.Rate * billingRateMultiplier, 2, MidpointRounding.AwayFromZero);
            } else {
                if (_vm.WorkTicket.ServiceAgreement.DetailRate != 0) {
                    billingRate = Math.Round(_vm.WorkTicket.ServiceAgreement.DetailRate * billingRateMultiplier, 2, MidpointRounding.AwayFromZero);
                } else {
                    billingRate = Math.Round(_vm.WorkTicket.ServiceAgreement.StandardLaborRate * billingRateMultiplier, 2, MidpointRounding.AwayFromZero);
                }
            }

            if (activityCode != null) {
                App_Customer customer = App.Database.GetAppCustomer(_vm.WorkTicket);
                JT_CustomerBillingRates custBillRate = App.Database.GetJT_CustomerBillingRate(customer.ARDivisionNo, customer.CustomerNo, activityCode.ActivityCode);
                if (custBillRate != null) {
                    billingRate = Math.Round(custBillRate.BillRatePerHour * billingRateMultiplier, 2, MidpointRounding.AwayFromZero);
                }
            }

            if (billingRate == 0) {
                JT_Technician technician = App.Database.GetCurrentTechnicianFromDb();
                billingRate = Math.Round(technician.StandardBillingRate * billingRateMultiplier, 2, MidpointRounding.AwayFromZero);
            }

            _editorRefRate.Text = billingRate.ToString("C2");

            SetBillableRate();
        }

        private void SetBillableRate()
        {
            decimal billingRate = 0;
            string sBillable = "";

            ClockOutPageViewModel.App_Billable appBillable = (ClockOutPageViewModel.App_Billable)_pickerBillable.SelectedItem;

            sBillable = appBillable.BillableFlag;

            string sBillingMiscCode = "";
            JT_ActivityCode activityCode = (JT_ActivityCode)_pickerActivityCode.SelectedItem;

            if (activityCode != null) {
                if (activityCode.BillingMiscCode != null) {
                    sBillingMiscCode = activityCode.BillingMiscCode;
                }
            }

            if (sBillable != "B") 
            { 
                billingRate = 0; 
            } else if ((bool)switchLaborCoveredSvcAgreement.IsToggled 
                       && _vm.WorkTicket.ServiceAgreement.BillingType == "F"
                       && _vm.WorkTicket.DtlCoverageExceptionCode == null 
                       && sBillingMiscCode == "") 
            { 
                billingRate = 0; 
            } else if (_vm.WorkTicket.ServiceAgreement.IsLaborCovered == true 
                && _vm.WorkTicket.IsCoveredOnContract == true) 
            { 
                billingRate = 0; 
            } else if ((bool)switchLaborCoveredWarranty.IsToggled && _vm.WorkTicket.DtlCoverageExceptionCode == null
                && _vm.WorkTicket.SE_OverridePricing != "Y") 
            { 
                billingRate = 0; 
            } else { 
                billingRate = Decimal.Parse(_editorRefRate.Text.Replace("$", "")); 
            }

            // Set the value in the rate box
            _editorBillableRate.Text = billingRate.ToString("C2");

            // Set the value in the Billable Amount Box
            decimal hours;
            decimal.TryParse(_editorHoursBilled.Text.Replace("$", ""), out hours);
            _editorBillableAmount.Text = Math.Round(billingRate * hours, 2, MidpointRounding.AwayFromZero).ToString("C2");    
        }

        private void PickerDepartTime_Unfocused(object sender, FocusEventArgs e)
        {
            SetHoursBilled();
        }

        void _pickerBillable_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetRefRate();
        }

        private void EditorWorkPerformed_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((_editorWorkPerformed.Text != null) && (_editorWorkPerformed.Text.Length > 0))
            {
                _editorHoursBilled.IsEnabled = true;
            }
            else
            {
                _editorHoursBilled.Text = Math.Round((_pickerDepartTime.Time - _pickerStartTime.Time).TotalHours,2).ToString();
                _editorHoursBilled.IsEnabled = false;
            }
        }

        protected async void buttonCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        protected async void ButtonClockOut_Clicked(object sender, EventArgs e)
        {
            if (_pickerTechnicianStatus.SelectedIndex < 0)
            {
                await DisplayAlert("Status", "Select a technician status.", "OK");
                return;
            }
            if (_pickerTicketStatus.SelectedIndex < 0)
            {
                await DisplayAlert("Status", "Select a ticket status.", "OK");
                return;
            }
            if (_pickerEarningsCode.SelectedIndex < 0)
            {
                await DisplayAlert("Earnings Code", "Select an earnings code.", "OK");
                return;
            }
            if (_pickerActivityCode.SelectedIndex < 0)
            {
                await DisplayAlert("Activity Code", "Select an activity code.", "OK");
                return;
            }

            DateTime dtDepart = DateTime.Now;
            double hoursWorked = 0;

            if (_captureTimeInTimeTracker == "Y") {
                DateTime dtArrive;
                if (_pickerStartDate.Date == null) {
                    await DisplayAlert("Arrive Date", "Enter a valid arrival date.", "OK");
                    return;
                }

                if (_pickerDepartDate.Date == null) {
                    await DisplayAlert("Depart Date", "Enter a valid departure date.", "OK");
                    return;
                }

                // dch rkl 02/03/2017 Capture hours worked, for validation
                if (_editorHoursWorked.Text != null) { double.TryParse(_editorHoursWorked.Text, out hoursWorked); }

            } else {
                // dch rkl 01/23/2017 captureTimeInTimeTracker == "N", hours must be entered
                if (_editorHoursWorked.Text != null) { double.TryParse(_editorHoursWorked.Text, out hoursWorked); }
                if (hoursWorked == 0) {
                    await DisplayAlert("Hours Worked", "Enter valid hours worked.", "OK");
                    return;
                }
            }
            if (hoursWorked > 24) {
                await DisplayAlert("Hours Worked", "Time entry cannot exceed 24 hours.  Please adjust your Depart Date/Time and create a separate Clock In/Clock Out for additional hours.", "OK");
                return;
            }

            double meterReading = 0;
            double hoursBilled = 0;

            JT_TechnicianStatus selectedTechnicianStatus = _pickerTechnicianStatus.SelectedItem as JT_TechnicianStatus;
            JT_MiscellaneousCodes selectedTicketStatus = _pickerTicketStatus.SelectedItem as JT_MiscellaneousCodes;
            JT_EarningsCode selectedEarningsCode = _pickerEarningsCode.SelectedItem as JT_EarningsCode;
            JT_ActivityCode selectedActivityCode = _pickerActivityCode.SelectedItem as JT_ActivityCode;
            
            try
            {
                if (_vm.IsRepairItemAnEquipmentAsset)
                {
                    meterReading = double.Parse(_editorMeterReading.Text);
                }
                else
                {
                    meterReading = 0;
                }
            }
            catch
            {
                // empty
            }
            try
            {
                hoursBilled = double.Parse(_editorHoursBilled.Text);
            }
            catch
            {
                // empty
            }
            JT_MiscellaneousCodes deptCode = (JT_MiscellaneousCodes)_pickerDepartment.SelectedItem;
            ClockOutPageViewModel.App_Billable appBillable = (ClockOutPageViewModel.App_Billable)_pickerBillable.SelectedItem;

            if (_captureTimeInTimeTracker == "Y") {
                //_vm.ClockOut(_pickerDepartTime.Time, selectedTechnicianStatus, selectedTicketStatus, selectedActivityCode, string.Empty,
                //             selectedEarningsCode, hoursBilled, meterReading, _editorWorkPerformed.Text, _pickerDepartDate.Date.ToShortDateString());
                _vm.ClockOut(_pickerDepartTime.Time, selectedTechnicianStatus, selectedTicketStatus, selectedActivityCode, deptCode.MiscellaneousCode,
                             selectedEarningsCode, hoursBilled, meterReading, _editorWorkPerformed.Text, _pickerDepartDate.Date.ToShortDateString(), _captureTimeInTimeTracker,
                             hoursWorked, _vm.WorkTicket.ServiceAgreement.ServiceAgreementNumber, appBillable.BillableFlag);
            } else {
                _vm.ClockOut(new TimeSpan(), selectedTechnicianStatus, selectedTicketStatus, selectedActivityCode, deptCode.MiscellaneousCode,
                             selectedEarningsCode, hoursBilled, meterReading, _editorWorkPerformed.Text, "", _captureTimeInTimeTracker, hoursWorked,
                             _vm.WorkTicket.ServiceAgreement.ServiceAgreementNumber, appBillable.BillableFlag);
            }
            await Navigation.PopToRootAsync();
        }
    }
}
