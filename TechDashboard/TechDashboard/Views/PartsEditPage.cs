using System;
using System.Windows;

using Xamarin.Forms;
using TechDashboard.Models;
using TechDashboard.ViewModels;
using TechDashboard.Data;

namespace TechDashboard.Views
{
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is decimal)
                return value.ToString();
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal dec;
            if (decimal.TryParse(value as string, out dec))
                return dec;
            return value;
        }
    }

    public class PartsEditPage : ContentPage
	{
        public enum PageMode
        {
            Add,
            Edit
        };

        PartsEditPageViewModel _vm;

        App_ScheduledAppointment _scheduledAppointment;
        PageMode _pageMode;
        Xamarin.Forms.Label _labelTitle;

        Label _labelPartNumber;
        Entry _labelPartDescription;
        Picker _pickerSerialNumber;
        Picker _pickerWarehouse;
        Entry _entryQuantity;
        Picker _entryUnitOfMeasure;
        Entry _entryUnitCost;
        Entry _entryUnitPrice;
        Label _labelExtensionPrice;
        Button _buttonAddPart;
        Button _buttonCancel;
        Button _buttonDeletePart;
        Entry _entryComments;
        Switch _switchIsChargeable;
        Switch _switchIsPrintable;
        Switch _switchIsPurchased;
        Switch _switchIsOverhead;        

        public PartsEditPage(App_WorkTicket workTicket, CI_Item part, PageMode pageMode, App_ScheduledAppointment scheduledAppointment)
        {
            _pageMode = pageMode;
            _vm = new PartsEditPageViewModel(workTicket, part);
            _scheduledAppointment = scheduledAppointment;

            SetPageLayout();
        }

        public PartsEditPage(App_WorkTicket workTicket, App_RepairPart part, PageMode pageMode, App_ScheduledAppointment scheduledAppointment)
        {
            _pageMode = pageMode;
            _vm = new PartsEditPageViewModel(workTicket, part);
            _scheduledAppointment = scheduledAppointment;
            BindingContext = _vm.PartToEdit;
            SetPageLayout();
        }

        protected void SetPageLayout()
        {
            //  Create a label for the technician list
            _labelTitle = new Xamarin.Forms.Label();

			BackgroundColor = Color.White;
            // Set the page title.
            switch (_pageMode)
            {
                case PageMode.Add:
                    Title = "Add Part";
                    _labelTitle.Text = "ADD PART";
                    break;
                case PageMode.Edit:
                    Title = "Edit Part";
                    _labelTitle.Text = "EDIT PART";
                    break;
                default:
                    Title = "Add/Edit Part";
                    _labelTitle.Text = "ADD/EDIT PART";
                    break;
            }

            //BindingContext = _vm.PartToEdit;

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

            Color asbestos = Color.FromHex("#7f8C8d");

            _labelPartNumber = new Label();
            _labelPartNumber.SetBinding(Label.TextProperty, "PartItemCode");
            _labelPartNumber.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
            _labelPartNumber.TextColor = asbestos;
            _labelPartNumber.HorizontalOptions = LayoutOptions.FillAndExpand;

            _labelPartDescription = new Entry();
            _labelPartDescription.SetBinding(Entry.TextProperty, "PartItemCodeDescription");
            _labelPartDescription.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
            _labelPartDescription.TextColor = asbestos;
            _labelPartDescription.HorizontalOptions = LayoutOptions.FillAndExpand;

            Button btnExtdDesc = new Button() {
                BackgroundColor = Color.FromHex("#7FeC8D"),
                TextColor = Color.White,
                Text = "Extended Desc"
            };
            btnExtdDesc.Clicked += BtnExtdDesc_Clicked;

            //_pickerSerialNumber = new Picker();

            // dch rkl 10/13/2016
            //_pickerSerialNumber.Title = "MFG. Serial";
            //_pickerSerialNumber.Title = "Lot No";

            /*foreach (LotQavl serialNumber in _vm.GetMfgSerialNumbersForPart())
            {
                _pickerSerialNumber.Items.Add(serialNumber);
            }
            if ((_pickerSerialNumber.Items == null) || (_pickerSerialNumber.Items.Count == 0))
            {
                _pickerSerialNumber.IsVisible = false;
            }
            else
            {
                _pickerSerialNumber.IsVisible = true;
                _pickerSerialNumber.SelectedIndex = 0;
            }*/
            Label labelWarehouse = new Label();
            labelWarehouse.FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null);
            labelWarehouse.TextColor = asbestos;
            labelWarehouse.HorizontalOptions = LayoutOptions.FillAndExpand;
            labelWarehouse.Text = "Warehouse";

            _pickerWarehouse = new Picker();
            _pickerWarehouse.Title = "Warehouse";
            _pickerWarehouse.WidthRequest = 100;
            foreach (string warehouse in _vm.WarehouseList)
            {
                _pickerWarehouse.Items.Add(warehouse);
            }
            try { _pickerWarehouse.SelectedIndex = _pickerWarehouse.Items.IndexOf(_vm.PartToEdit.Warehouse); } catch { }
            //_pickerWarehouse.SelectedIndex = 0;

            _entryQuantity = new Entry();            
            _entryQuantity.Placeholder = "Qty";
            _entryQuantity.Keyboard = Keyboard.Numeric;
            _entryQuantity.SetBinding(Entry.TextProperty, new Binding("Quantity", converter: new DecimalConverter()));
            _entryQuantity.TextChanged += EntryQuantity_TextChanged;
			_entryQuantity.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
			_entryQuantity.TextColor = asbestos;

            // dch rkl 10/14/2016 If editing, the quantity should come from the existing entry
            if (_pageMode == PageMode.Edit) { _entryQuantity.Text = _vm.PartToEdit.Quantity.ToString(); }
            else { _entryQuantity.Text = "1"; }
            //_entryQuantity.Text = "1";

            _entryUnitOfMeasure = new Picker();
            //_entryUnitOfMeasure.Placeholder = "U/M";           
            //_entryUnitOfMeasure.SetBinding(Entry.TextProperty, "UnitOfMeasure");
            //entryUnitOfMeasure.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
            foreach (CI_UnitOfMeasure uom in _vm.UnitOfMeasureList) {
                _entryUnitOfMeasure.Items.Add(uom.UnitOfMeasure);
            }
            try { _entryUnitOfMeasure.SelectedIndex = _entryUnitOfMeasure.Items.IndexOf(_vm.PartToEdit.UnitOfMeasure); } catch { }
			_entryUnitOfMeasure.TextColor = asbestos;

            _entryUnitCost = new Entry();
            _entryUnitCost.Placeholder = "Cost";
            _entryUnitCost.Keyboard = Keyboard.Numeric;
            _entryUnitCost.SetBinding(Entry.TextProperty, new Binding("UnitCost", converter: new DecimalConverter()));
            _entryUnitCost.IsEnabled = false;
			_entryUnitCost.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
			_entryUnitCost.TextColor = asbestos;

            _entryUnitPrice = new Entry();
            _entryUnitPrice.Placeholder = "Price";
            _entryUnitPrice.Keyboard = Keyboard.Numeric;
            _entryUnitPrice.SetBinding(Entry.TextProperty, new Binding("UnitPrice", converter: new DecimalConverter()));
            _entryUnitPrice.IsEnabled = false;
			_entryUnitPrice.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
			_entryUnitPrice.TextColor = asbestos;

            _labelExtensionPrice = new Label();

            // dch rkl 10/13/2016 format price, since qty defaults to 1, use 1 as the quantity
            if (_pageMode == PageMode.Edit) { _labelExtensionPrice.Text = (_vm.PartToEdit.UnitPrice * _vm.PartToEdit.Quantity).ToString(); }
            else { _labelExtensionPrice.Text = (_vm.PartToEdit.UnitPrice * 1).ToString(); }
            //_labelExtensionPrice.Text = (_vm.PartToEdit.UnitPrice * _vm.PartToEdit.Quantity).ToString();

			_labelExtensionPrice.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
			_labelExtensionPrice.TextColor = asbestos;

            _entryComments = new Entry();
            _entryComments.Placeholder = "Comments";
            _entryComments.SetBinding(Entry.TextProperty, "Comment");
			_entryComments.TextColor = asbestos;
			_entryComments.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);

            _switchIsChargeable = new Switch();
            _switchIsChargeable.SetBinding(Switch.IsToggledProperty, "IsChargeable");
			_switchIsChargeable.HorizontalOptions = LayoutOptions.End;
            _switchIsChargeable.Toggled += switchIsChargeable_Toggled;

            _switchIsPrintable = new Switch();
            _switchIsPrintable.SetBinding(Switch.IsToggledProperty, "IsPrintable");
			_switchIsPrintable.HorizontalOptions = LayoutOptions.End;

            _switchIsPurchased = new Switch();
            _switchIsPurchased.SetBinding(Switch.IsToggledProperty, "IsPurchased");
			_switchIsPurchased.HorizontalOptions = LayoutOptions.End;

            _switchIsOverhead = new Switch();
            _switchIsOverhead.SetBinding(Switch.IsToggledProperty, "IsOverhead");
			_switchIsOverhead.HorizontalOptions = LayoutOptions.End;

            // dch rkl 11/23/2016 if misc part, hide warehouse dropdown
            bool bShowWhse = true;
            if (_vm.PartToEdit.PartItemCode.Trim().Substring(0, 1) == "*" || _vm.PartToEdit.ItemType == "4" || _vm.PartToEdit.ItemType == "5") {
                bShowWhse = false;
            }
            if (bShowWhse) {
                _pickerWarehouse.IsVisible = true;
                labelWarehouse.IsVisible = true;
            } else {
                _pickerWarehouse.IsVisible = false;
                labelWarehouse.IsVisible = false;
            }

            Grid topGrid = new Grid();
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150, GridUnitType.Absolute) });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            topGrid.Children.Add(new Label {
                Text = "Part",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }
                                 , 0, 0);
            topGrid.Children.Add(_labelPartNumber, 1, 0);
            topGrid.Children.Add(
                new Label {
                    Text = "Part Desc",
                    FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                    TextColor = asbestos
                }, 0, 1);
            topGrid.Children.Add(_labelPartDescription, 1, 1);

            topGrid.Children.Add(labelWarehouse, 0, 2);
            topGrid.Children.Add(_pickerWarehouse, 1, 2);

            Grid amtsGrid = new Grid();
            amtsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            amtsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            amtsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            amtsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            amtsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150, GridUnitType.Absolute) });
            amtsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) });
            amtsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) });

            amtsGrid.Children.Add(new Label { Text = "Qty", FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null), TextColor = asbestos }, 0, 0);
            amtsGrid.Children.Add(_entryQuantity, 1, 0);
            Grid.SetColumnSpan(_entryQuantity, 2);
            amtsGrid.Children.Add(_entryUnitOfMeasure, 3, 0);

            amtsGrid.Children.Add(new Label { Text = "Unit Cost", FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null), TextColor = asbestos }, 0, 1);
            amtsGrid.Children.Add(_entryUnitCost, 1, 1);
            Grid.SetColumnSpan(_entryUnitCost, 3);

            amtsGrid.Children.Add(new Label { Text = "Unit Price", FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null), TextColor = asbestos }, 0, 2);
            amtsGrid.Children.Add(_entryUnitPrice, 1, 2);
            Grid.SetColumnSpan(_entryUnitPrice, 3);

            amtsGrid.Children.Add(new Label { Text = "Ext Price", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null), TextColor = asbestos }, 0, 3);
            amtsGrid.Children.Add(_labelExtensionPrice, 1, 3);
            Grid.SetColumnSpan(_labelExtensionPrice, 3);

            _buttonAddPart = new Button();

            // dch rkl 01/13/2017 if extended description exists, display button to view/edit it BEGIN
            if (_vm.ExtendedDescriptionKey > 0 && _vm.ExtendedDescriptionText.Trim().Length > 0) {
                btnExtdDesc.IsVisible  = true;
            } else {
                btnExtdDesc.IsVisible = false;
            }
            // dch rkl 01/13/2017 if extended description exists, display button to view/edit it END

            switch(_pageMode)
            {
                case PageMode.Add:
                    _buttonAddPart.Text = "ADD";
                    break;
                case PageMode.Edit:
                    _buttonAddPart.Text = "UPDATE";
                    break;
            }            
            _buttonAddPart.Clicked += ButtonAddPart_Click;

			_buttonAddPart.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
			_buttonAddPart.TextColor = Color.White;
			_buttonAddPart.BackgroundColor = Color.FromHex("#2ECC71");
			_buttonAddPart.HorizontalOptions = LayoutOptions.FillAndExpand;

            _buttonDeletePart = new Button();
            _buttonDeletePart.Text = "DELETE";
            _buttonDeletePart.FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null);
            _buttonDeletePart.TextColor = Color.White;
            _buttonDeletePart.BackgroundColor = Color.FromHex("#2ECC71");
            _buttonDeletePart.HorizontalOptions = LayoutOptions.FillAndExpand;
            _buttonDeletePart.IsVisible = (_pageMode == PageMode.Edit);
            _buttonDeletePart.Clicked += ButtonDeletePart_Clicked;

            _buttonCancel = new Xamarin.Forms.Button();
            _buttonCancel.Text = "CANCEL";
			_buttonCancel.TextColor = Color.White;
			_buttonCancel.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
			_buttonCancel.BackgroundColor = Color.FromHex("#E74C3C");
            _buttonCancel.Clicked += ButtonCancel_Click;
			_buttonCancel.HorizontalOptions = LayoutOptions.FillAndExpand;

            Grid gridYesNo = new Grid();
            gridYesNo.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            gridYesNo.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            gridYesNo.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            gridYesNo.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            gridYesNo.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) });
            gridYesNo.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) });

            gridYesNo.Children.Add(
                new Xamarin.Forms.Label { Text = "Charge", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null), TextColor = asbestos },
                0, 0);
            gridYesNo.Children.Add(_switchIsChargeable, 1, 0);
            gridYesNo.Children.Add(
                new Xamarin.Forms.Label { Text = "Print", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null), TextColor = asbestos },
                0, 1);
            gridYesNo.Children.Add(_switchIsPrintable, 1, 1);
            gridYesNo.Children.Add(
                new Xamarin.Forms.Label { Text = "Purchase", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null), TextColor = asbestos },
                0, 2);
            gridYesNo.Children.Add(_switchIsPurchased, 1, 2);
            gridYesNo.Children.Add(
                new Xamarin.Forms.Label { Text = "Overhead", FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null), TextColor = asbestos },
                0, 3);
            gridYesNo.Children.Add(_switchIsOverhead, 1, 3);

            Content = new StackLayout
            {
				Padding = 30,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children =
                {
                    titleLayout,
                    topGrid,
                    amtsGrid,
                    _entryComments,
                    gridYesNo,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
						HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        {
                            _buttonAddPart,
                            _buttonDeletePart,
                            _buttonCancel
                        }
                    },
                }
			};
		}

        void switchIsChargeable_Toggled(object sender, ToggledEventArgs e)
        {
            if (_switchIsChargeable.IsToggled == true) {
                _switchIsPrintable.IsToggled = true;
                _switchIsPrintable.IsEnabled = false;
            } else {
                _switchIsPrintable.IsEnabled = true;
                _entryUnitPrice.Text = "0.00";
                _labelExtensionPrice.Text = "0.00";
            }
                
        }

        private void EntryQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double qty;
                double.TryParse(_entryQuantity.Text, out qty);
                double unitPrice;
                double.TryParse(_entryUnitPrice.Text, out unitPrice);
                double newExtensionPrice = qty * unitPrice;
                _labelExtensionPrice.Text = newExtensionPrice.ToString("C2");
                if (qty == 0) {
                    _entryQuantity.Text = "";
                    _switchIsPurchased.IsEnabled = false;
                    _switchIsPurchased.IsToggled = false;
                } else {
                    _switchIsPurchased.IsEnabled = true;
                }
            }
            catch
            {
                // don't do anything
            }                
        }

        protected async void ButtonAddPart_Click(object sender, EventArgs e)
        {
            _vm.PartToEdit.Warehouse = _pickerWarehouse.Items[_pickerWarehouse.SelectedIndex];

            if (_entryUnitOfMeasure.SelectedIndex < 0)
            {
                await DisplayAlert("Unit of Measure", "You haven't selected a unit of measure.", "OK");
                return;
            }

            // puke
            switch(_pageMode)
            {
                case PageMode.Add:
                    _vm.AddPartToPartsList();
                    //await Navigation.PopAsync();
                    break;
                case PageMode.Edit:
                    _vm.UpdatePartOnPartsList();
                    break;
            }
            
            await Navigation.PopAsync();
                  
        }

        private async void ButtonDeletePart_Clicked(object sender, EventArgs e)
        {
            _vm.DeletePartFromPartsList();
            await Navigation.PopAsync();
        }

        protected async void ButtonCancel_Click(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        
        protected async void BtnExtdDesc_Clicked (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PartEditExtdDescPage(_vm.WorkTicket, _vm.PartToEdit, PartsEditPage.PageMode.Edit, _scheduledAppointment));
        }
    }
}
