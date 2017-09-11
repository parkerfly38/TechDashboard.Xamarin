using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using TechDashboard.Models;
using TechDashboard.ViewModels;

namespace TechDashboard.Views
{
	public class TicketDetailsPage : ContentPage
	{
        protected TicketDetailsPageViewModel _vm;
       
        public TicketDetailsPage(App_ScheduledAppointment scheduledAppointment)
        {
            _vm = new TicketDetailsPageViewModel(scheduledAppointment);
            InitializePage();
        }

        public TicketDetailsPage()
        {
            _vm = new TicketDetailsPageViewModel();
            InitializePage();
        }

        protected void InitializePage()
        {
            Title = "Ticket Details";

			//brian refactor
			//add technician no header
			// Create our screen objects
			//  Create a label for the technician list
			Color asbestos = Color.FromHex("#7f8c8d");

            // if no work ticket at all, one must be provided.  Tell user such.
            if (_vm.WorkTicket == null)
            {
                Content = new StackLayout()
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Children =
                    {
                        new Xamarin.Forms.Label()
                        {
                            Text = "SELECT A TICKET",
                            FontSize = 16,
                            FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                            TextColor = Color.Red
                        }
                    }
                };
                return;
            }

            //  Create a label for the technician list
            Label _labelTitle = new Xamarin.Forms.Label();
            _labelTitle.Text = "SERVICE TICKET DETAIL";
            _labelTitle.FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null);
            _labelTitle.FontSize = 22;
            _labelTitle.TextColor = Color.White;
            _labelTitle.HorizontalTextAlignment = TextAlignment.Center;
            _labelTitle.VerticalTextAlignment = TextAlignment.Center;


            Grid titleLayout = new Grid() {
                BackgroundColor = Color.FromHex("#2980b9"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 60
            };
            titleLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            titleLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            titleLayout.Children.Add(_labelTitle, 0, 0);

            // If we get here, we have a work ticket to display
            Label _labelTechnicianTitle = new Xamarin.Forms.Label();
			_labelTechnicianTitle.Text = "Technician";
			_labelTechnicianTitle.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
			_labelTechnicianTitle.TextColor = asbestos;
			_labelTechnicianTitle.HorizontalTextAlignment = TextAlignment.Start;
			_labelTechnicianTitle.VerticalTextAlignment = TextAlignment.Center;

			Label labelTechnicianNo = new Label();
			labelTechnicianNo.Text = _vm.AppTechnician.TechnicianNo;
			labelTechnicianNo.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
			labelTechnicianNo.TextColor = asbestos;

			Label labelTechnicianName = new Label();
			labelTechnicianName.Text = _vm.AppTechnician.FirstName + " " + _vm.AppTechnician.LastName;
			labelTechnicianName.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
			labelTechnicianName.TextColor = asbestos;			

			titleLayout.Padding = 30;

			StackLayout pageLayout = new StackLayout();

            // Create a stack layout to hold the page contents
            StackLayout layout = new StackLayout();
			layout.Padding = 30;

			Label labelServiceTicketTitle = new Label() {
				Text = "Service Ticket",
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
				TextColor = asbestos
			};
			Label labelServiceTicketNo = new Label() {
				Text = _vm.WorkTicket.FormattedTicketNumber,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
			Label labelServiceTicketDesc = new Label() {
				Text = _vm.WorkTicket.Description,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
			Label labelCustomerTitle = new Label() {
				Text = "Customer No",
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
				TextColor = asbestos
			};
			Label labelCustomerNo = new Label() {
				Text = _vm.Customer.CustomerNo,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
			Label labelCustomerName = new Label() {
				Text = _vm.Customer.CustomerName,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
			Label labelContactTitle = new Label() {
				Text = "Contact",
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
				TextColor = asbestos
			};
			Label labelContactCode = new Label() {
				Text = _vm.CustomerContact.ContactCode,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
			Label labelContactName = new Label() {
				Text = _vm.CustomerContact.ContactName,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
			Label labelAddressTitle = new Label() {
				Text = "Address",
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
				TextColor = asbestos
			};
			StackLayout customerStackLayout = new StackLayout();

			// label for address line 1
			if ((_vm.SalesOrder.ShipToAddress1 != null) &&
				(_vm.SalesOrder.ShipToAddress1.Trim().Length > 0))
			{
				Xamarin.Forms.Label labelAddressLine1 = new Xamarin.Forms.Label();
				labelAddressLine1.Text = _vm.SalesOrder.ShipToAddress1;
				labelAddressLine1.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
				labelAddressLine1.TextColor = asbestos;
				customerStackLayout.Children.Add(labelAddressLine1);
			}

			// label for address line 2
			if ((_vm.SalesOrder.ShipToAddress2 != null) &&
				(_vm.SalesOrder.ShipToAddress2.Trim().Length > 0))
			{
				Xamarin.Forms.Label labelAddressLine2 = new Xamarin.Forms.Label();
				labelAddressLine2.Text = _vm.SalesOrder.ShipToAddress2;
				labelAddressLine2.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
				labelAddressLine2.TextColor = asbestos;
				customerStackLayout.Children.Add(labelAddressLine2);
			}

			// label for address line 3
			if ((_vm.SalesOrder.ShipToAddress3 != null) &&
				(_vm.SalesOrder.ShipToAddress3.Trim().Length > 0))
			{
				Xamarin.Forms.Label labelAddressLine3 = new Xamarin.Forms.Label();
				labelAddressLine3.Text = _vm.SalesOrder.ShipToAddress3;
				labelAddressLine3.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
				labelAddressLine3.TextColor = asbestos;
				customerStackLayout.Children.Add(labelAddressLine3);
			}

			// label for city/state/zip
			Xamarin.Forms.Label labelCityStateZip = new Xamarin.Forms.Label();
			labelCityStateZip.Text = _vm.SalesOrder.ShipToCity + ", " + _vm.SalesOrder.ShipToState + " " + _vm.SalesOrder.ShipToZipCode;
			labelCityStateZip.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
			labelCityStateZip.TextColor = asbestos;
			customerStackLayout.Children.Add(labelCityStateZip);

			Label labelTelephoneTitle = new Label() {
				Text = "Main Telephone No",
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
				TextColor = asbestos
			};

			// label for the phone number
			Xamarin.Forms.Label labelTelephoneNo = new Xamarin.Forms.Label();
			labelTelephoneNo.Text = _vm.Customer.TelephoneNo;
			if((_vm.Customer.TelephoneExt != null) &&
				(_vm.Customer.TelephoneExt.Trim().Length > 0))
			{
				labelTelephoneNo.Text += " Ext. " + _vm.Customer.TelephoneExt;

			}
			labelTelephoneNo.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
			labelTelephoneNo.TextColor = asbestos;	//top grid

            Label labelShipToPhoneTitle = new Label()
            {
                Text = "Shipping Telephone No",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            };
            Label labelShipToPhoneNo = new Label();
            labelShipToPhoneNo.Text = _vm.SalesOrder.TelephoneNo;
            if(_vm.SalesOrder.TelephoneExt != null && _vm.SalesOrder.TelephoneExt.Trim().Length > 0)
            {
                labelShipToPhoneNo.Text += "Ext. " + _vm.SalesOrder.TelephoneExt;
            }
            labelShipToPhoneNo.FontFamily = Device.OnPlatform("OpenSans-Regular", "sans-serif", null);
            labelShipToPhoneNo.TextColor = asbestos;

            Label labelContactPhoneTitle = new Label()
            {
                Text = "Contact Telephone No",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            };
            Label labelContactPhone1 = new Label();
            labelContactPhone1.Text = _vm.CustomerContact.TelephoneNo1;
            if(_vm.CustomerContact.TelephoneExt1 != null && _vm.CustomerContact.TelephoneExt1.Trim().Length > 0)
            {
                labelContactPhone1.Text += "Ext. " + _vm.CustomerContact.TelephoneExt1; 
            }
            labelContactPhone1.FontFamily = Device.OnPlatform("OpenSans-Regular", "sans-serif", null);
            labelContactPhone1.TextColor = asbestos;

            Label labelContactPhone2 = new Label();
            if (_vm.CustomerContact.TelephoneNo2 != null && _vm.CustomerContact.TelephoneNo2.Trim().Length > 0)
            {
                labelContactPhone2.Text = _vm.CustomerContact.TelephoneNo2;
            }
            if (_vm.CustomerContact.TelephoneExt2 != null && _vm.CustomerContact.TelephoneExt2.Trim().Length > 0)
            {
                labelContactPhone2.Text += "Ext. " + _vm.CustomerContact.TelephoneExt2;
            }
            labelContactPhone2.FontFamily = Device.OnPlatform("OpenSans-Regular", "sans-serif", null);
            labelContactPhone2.TextColor = asbestos;

			Grid topGrid = new Grid();
			topGrid.RowDefinitions.Add(new RowDefinition { Height = 20 });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = 20 });
			topGrid.RowDefinitions.Add(new RowDefinition { Height = 20 });
			topGrid.RowDefinitions.Add(new RowDefinition { Height = 20 });
			topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			topGrid.RowDefinitions.Add(new RowDefinition { Height = 20 });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			Grid.SetColumnSpan(customerStackLayout, 2);
            topGrid.Children.Add(_labelTechnicianTitle, 0, 0);
            topGrid.Children.Add(labelTechnicianNo, 1, 0);
            topGrid.Children.Add(labelTechnicianName, 2, 0);
			topGrid.Children.Add(labelServiceTicketTitle, 0, 1);
			topGrid.Children.Add(labelServiceTicketNo, 1, 1);
			topGrid.Children.Add(labelServiceTicketDesc, 2, 1);
			topGrid.Children.Add(labelCustomerTitle, 0, 2);
			topGrid.Children.Add(labelCustomerNo, 1, 2);
			topGrid.Children.Add(labelCustomerName, 2, 2);

			topGrid.Children.Add(labelContactTitle, 0, 3);
			topGrid.Children.Add(labelContactCode, 1, 3);
			topGrid.Children.Add(labelContactName, 2, 3);

			topGrid.Children.Add(labelAddressTitle, 0, 4);
			topGrid.Children.Add(customerStackLayout, 1, 4);
			topGrid.Children.Add(labelTelephoneTitle, 0, 5);
			topGrid.Children.Add(labelTelephoneNo, 1, 5);
            topGrid.Children.Add(labelShipToPhoneTitle, 0, 6);
            topGrid.Children.Add(labelShipToPhoneNo, 1, 6);
            topGrid.Children.Add(labelContactPhoneTitle, 0, 7);
            topGrid.Children.Add(new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    labelContactPhone1,
                    labelContactPhone2
                }
            }, 1, 7);

			topGrid.Padding = 10;

            //ticket info
            /*Label labelWorkTicketClass = new Label()
            {
                Text = _vm.WorkTicket.WorkTicketClass,
                FontFamily = Device.OnPlatform("OpenSans-Regular", "sans-serif", null),
                TextColor = asbestos
            };*/
            Label labelStepNumber = new Label() {
				Text = _vm.WorkTicket.WTStep,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
			Label labelDescription = new Label() {
				Text = _vm.WorkTicket.Description,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
			Label labelIntSerNoTitle = new Label() {
				Text = "Int Serial No",
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
				TextColor = asbestos
			};
			Label labelIntSerNo = new Label() {
				Text = _vm.WorkTicket.InternalSerialNumber,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
            /*StackLayout ticketClassHorizontalLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 10,
                Children =
                {
                    labelWorkTicketClass
                }
            };*/
			StackLayout stepHorizontalLayout = new StackLayout() {
				Orientation = StackOrientation.Vertical,
				Spacing = 10,
				Children = 
				{
                    labelStepNumber,
					labelDescription,
					labelIntSerNoTitle,
					labelIntSerNo
				}
			};

			Label labelItemCode = new Label() {
				//Text = _vm.RepairItem.DtlRepairItemCode,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
			Label labelItemDesc = new Label() {
				//Text = _vm.RepairItem.ItemCodeDesc
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
			if (_vm.RepairItem != null) {
				labelItemCode.Text = _vm.RepairItem.ItemCode;
				labelItemDesc.Text = _vm.RepairItem.ItemCodeDesc;
			}
			StackLayout repairItemLayout = new StackLayout() {
				Orientation = StackOrientation.Vertical,
				Spacing = 10,
				Children = {
					labelItemCode,
					labelItemDesc
				}
			};

			Label labelMfgSerialNo = new Label() {
				Text = _vm.WorkTicket.DtlMfgSerialNo,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos,
                IsEnabled = false
			};
			/*labelMfgSerialNo.TextChanged += async delegate(object sender, TextChangedEventArgs e)
			{
				_vm.WorkTicket.DtlMfgSerialNo = ((Entry)sender).Text;
			};*/

			Label labelMfgSerialNoDesc = new Label() {
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
			if (_vm.RepairItem != null) {
				labelMfgSerialNoDesc.Text = _vm.RepairItem.ItemCodeDesc + _vm.RepairItem.StandardUnitOfMeasure;
			}
			StackLayout mfgSerLayout = new StackLayout() {
				Orientation = StackOrientation.Vertical,
				Spacing = 10,
				Children = {
					labelMfgSerialNo,
					labelMfgSerialNoDesc
				}
			};

			Label labelServiceAgreementNo = new Label() {
				Text = _vm.WorkTicket.ServiceAgreement.ServiceAgreementNumber,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
			Label labelServiceAgreementDesc = new Label() {
				Text = _vm.WorkTicket.ServiceAgreement.Description,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
			StackLayout serviceAgreementLayout = new StackLayout() {
				Orientation = StackOrientation.Vertical,
				Spacing = 10,
				Children = {
					labelServiceAgreementNo,
					labelServiceAgreementDesc
				}
			};

			Label labelProblemCode = new Label() {
				Text = _vm.WorkTicket.DtlProblemCode,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
			Label labelProblemCodeDesc = new Label() {
				Text = _vm.WorkTicket.DtlProblemCodeDescription,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
			StackLayout problemCodeLayout = new StackLayout() {
				Orientation = StackOrientation.Vertical,
				Spacing = 10,
				Children = {
					labelProblemCode,
					labelProblemCodeDesc
				}
			};


			Label labelExceptionCode = new Label() {
				Text = _vm.WorkTicket.DtlCoverageExceptionCode,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
			Label labelExceptionCodeDesc = new Label() {
				Text = _vm.WorkTicket.DtlaCoverageExceptionCodeDescription,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos
			};
			StackLayout exceptionCodeLayout = new StackLayout() {
				Orientation = StackOrientation.Vertical,
				Spacing = 10,
				Children = {
					labelExceptionCode,
					labelExceptionCodeDesc
				}
			};

			//service agreement grid
			Grid serviceAgreementGrid = new Grid();
			serviceAgreementGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			serviceAgreementGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto  });
			serviceAgreementGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto  });
			serviceAgreementGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			serviceAgreementGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			serviceAgreementGrid.Children.Add(
				new Label {
					Text = "Service Agreement",
					FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
					TextColor = asbestos,
					FontSize = 12
				}, 0, 0);
			serviceAgreementGrid.Children.Add(
				new Label {
					Text = "Preventative Maintenance",
					FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
					TextColor = asbestos,
					FontSize = 12
				}, 0, 1);
			serviceAgreementGrid.Children.Add(
				new Label {
					Text = "Warranty Repair",
					FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
					TextColor = asbestos,
					FontSize = 12
				}, 0, 2);

			Switch switchWarrantyRepair = new Switch();
			switchWarrantyRepair.IsEnabled = false;
			if ((_vm.WorkTicket.DtlWarrantyRepair != null) &&
			    (_vm.WorkTicket.DtlWarrantyRepair.ToUpper() == "Y")) {
				switchWarrantyRepair.IsToggled = true;
			} else {
				switchWarrantyRepair.IsToggled = false;
			}
			Switch switchServiceAgreement = new Switch();
			switchServiceAgreement.IsEnabled = false;
			if (_vm.WorkTicket.ServiceAgreement != null && _vm.WorkTicket.ServiceAgreement.ServiceAgreementNumber != null) {
				switchServiceAgreement.IsToggled = true;
			} else {
				switchServiceAgreement.IsToggled = false;
			}
			Switch switchPreventativeMaintenance = new Switch();
			switchPreventativeMaintenance.IsEnabled = false;
			if (_vm.WorkTicket.IsPreventativeMaintenance)
			{
				switchPreventativeMaintenance.IsToggled = true;
			} else {
				switchPreventativeMaintenance.IsToggled = false;
			}
			serviceAgreementGrid.Children.Add(switchServiceAgreement, 1, 0);
			serviceAgreementGrid.Children.Add(switchWarrantyRepair, 1, 2);
			serviceAgreementGrid.Children.Add(switchPreventativeMaintenance, 1, 1);
			serviceAgreementGrid.Padding = 10;
			Grid.SetRowSpan(serviceAgreementGrid, 6);

			Grid stepGrid = new Grid();
			stepGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto  });//puke
			stepGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto  });
			stepGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto  });
			stepGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto  });
			stepGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto  });
			stepGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto  });
            stepGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto  });
            stepGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			stepGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			stepGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            stepGrid.Children.Add(labelIntSerNoTitle, 0, 1);
            stepGrid.Children.Add(labelIntSerNo, 1, 1);
            stepGrid.Children.Add(
				new Label {
					Text = "Step No",
					FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
					TextColor = asbestos
				}, 0, 0);
			stepGrid.Children.Add(stepHorizontalLayout, 1, 0);
			stepGrid.Children.Add(
				new Label {
					Text = "Item Code",
					FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
					TextColor = asbestos
				}, 0, 2);
			stepGrid.Children.Add(repairItemLayout, 1, 2);
			stepGrid.Children.Add(
				new Label {
					Text = "Mfg Serial No",
					FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
					TextColor = asbestos
				}, 0, 3);
			stepGrid.Children.Add(mfgSerLayout, 1, 3);
			stepGrid.Children.Add(
				new Label {
					Text = "Svc Agreement",
					FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
					TextColor = asbestos
				}, 0, 4);
			stepGrid.Children.Add(serviceAgreementLayout, 1, 4);
			stepGrid.Children.Add(
				new Label { 
					Text = "Problem Code",
					FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
					TextColor = asbestos
				}, 0, 5);
			stepGrid.Children.Add(problemCodeLayout, 1, 5);
			stepGrid.Children.Add(
				new Label {
					Text = "Exception Code",
					FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
					TextColor = asbestos
				}, 0, 6);
			stepGrid.Children.Add(exceptionCodeLayout, 1, 6);
			stepGrid.Children.Add(serviceAgreementGrid, 3, 1);

			stepGrid.Padding = 10;
			           
			// button to clock in/out
			Xamarin.Forms.Button buttonClockInOut = new Button();
			Label labelClockInOut = new Label();
			labelClockInOut.TextColor = Color.White;
			labelClockInOut.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
			labelClockInOut.FontSize = 12;
			if (App.Database.IsClockedIn())
			{
				if (_vm.ScheduledAppointment.IsCurrent)
				{
					// Give user option to clock out of this current ticket
					labelClockInOut.Text = "CLOCK OUT";
					buttonClockInOut.BackgroundColor = asbestos;
					buttonClockInOut.BorderWidth = 0;
					buttonClockInOut.IsVisible = true;
					buttonClockInOut.Clicked += ButtonClockInOut_Clicked;
					buttonClockInOut.Image = "time.png";
				}
				else
				{
					buttonClockInOut.IsVisible = false;
					labelClockInOut.IsVisible = false;
				}
			}
			else
			{
				// Give user option to clock in
				labelClockInOut.Text = "CLOCK IN";
				buttonClockInOut.IsVisible = true;
				buttonClockInOut.Clicked += ButtonClockInOut_Clicked;
				buttonClockInOut.BackgroundColor = asbestos;
				buttonClockInOut.Image = "time.png";
			}
			buttonClockInOut.VerticalOptions = LayoutOptions.Center;
			buttonClockInOut.HorizontalOptions = LayoutOptions.Center;
			labelClockInOut.HorizontalOptions = LayoutOptions.Center;

			// button to show ticket notes page
			Xamarin.Forms.Button buttonShowNotes = new Xamarin.Forms.Button();
			Label labelShowNotes = new Label();
			labelShowNotes.Text = "NOTES";
			labelShowNotes.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
			labelShowNotes.FontSize = 12;
			buttonShowNotes.Image = "clipboard.png";
			buttonShowNotes.Clicked += ButtonShowNotes_Clicked;
			buttonShowNotes.BackgroundColor = asbestos;
			buttonShowNotes.VerticalOptions = LayoutOptions.Center;
			buttonShowNotes.HorizontalOptions = LayoutOptions.Center;
			labelShowNotes.HorizontalOptions = LayoutOptions.Center;
			labelShowNotes.TextColor = Color.White;

			Button buttonShowCustomerDetails = new Button();
			Label labelShowCustomerDetails = new Label();
			labelShowCustomerDetails.Text = "CUSTOMER";
			labelShowCustomerDetails.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
			labelShowCustomerDetails.FontSize = 12;
			labelShowCustomerDetails.TextColor = Color.White;
			labelShowCustomerDetails.HorizontalOptions = LayoutOptions.Center;
			buttonShowCustomerDetails.Image = "user.png";
			buttonShowCustomerDetails.BackgroundColor = asbestos;
			buttonShowCustomerDetails.VerticalOptions = LayoutOptions.Center;
			buttonShowCustomerDetails.HorizontalOptions = LayoutOptions.Center;
			buttonShowCustomerDetails.Clicked += ButtonShowCustomerDetails_Clicked;

			//button to show Schedule Details
			Button buttonShowSchedule = new Button();
			Label labelShowSchedule = new Label();
			labelShowSchedule.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
			labelShowSchedule.FontSize = 12;
			labelShowSchedule.TextColor = Color.White;
			labelShowSchedule.Text = "SCHEDULE";
			buttonShowSchedule.Image = "viewdetails.png";
			buttonShowSchedule.Clicked += ButtonShowSchedule_Clicked;
			buttonShowSchedule.VerticalOptions = LayoutOptions.Center;
			buttonShowSchedule.BackgroundColor = asbestos;
			buttonShowSchedule.HorizontalOptions = LayoutOptions.Center;
			labelShowSchedule.HorizontalOptions = LayoutOptions.Center;

			// button to show the map
			Button buttonShowMap = new Button();
			Label labelShowMap = new Label();
			labelShowMap.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
			labelShowMap.FontSize = 12;
			labelShowMap.TextColor = Color.White;
			labelShowMap.Text = "MAP";
			buttonShowMap.Image = "mapmarker.png";
			buttonShowMap.Clicked += ButtonShowMap_Clicked;
			buttonShowMap.VerticalOptions = LayoutOptions.Center;
			buttonShowMap.HorizontalOptions = LayoutOptions.Center;
			labelShowMap.HorizontalOptions = LayoutOptions.Center;
			buttonShowMap.BackgroundColor = asbestos;


			// button to show parts list page
			Button buttonShowPartsList = new Button();
			Label labelShowPartsList = new Label();
			labelShowPartsList.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
			labelShowPartsList.FontSize = 12;
			labelShowPartsList.TextColor = Color.White;
			labelShowPartsList.Text = "PARTS";
			buttonShowPartsList.Image = "cog.png";
			buttonShowPartsList.Clicked += ButtonShowPartsList_Clicked;
			buttonShowPartsList.VerticalOptions = LayoutOptions.Center;
			buttonShowPartsList.HorizontalOptions = LayoutOptions.Center;
			labelShowPartsList.HorizontalOptions = LayoutOptions.Center;
			buttonShowPartsList.BackgroundColor = asbestos;

			Grid buttonGrid = new Grid();
			buttonGrid.RowDefinitions.Add(new RowDefinition { Height = 110 } );
			buttonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 90 });
			buttonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 90 });
			buttonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 90 });
			buttonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 90 });
			buttonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 90 });
			buttonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 90 });

			buttonGrid.BackgroundColor = asbestos;
			StackLayout clockInLayout = new StackLayout() {
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				Children = {
					buttonClockInOut,
					labelClockInOut
				}
			};
			buttonGrid.Children.Add(clockInLayout, 0, 0);

			StackLayout notesLayout = new StackLayout() {
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				Children = {
					buttonShowNotes,
					labelShowNotes
				}
			};
			buttonGrid.Children.Add(notesLayout, 1, 0);

			StackLayout custDetailsLayout = new StackLayout() {
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				Children = {
					buttonShowCustomerDetails,
					labelShowCustomerDetails
				}
			};
			buttonGrid.Children.Add(custDetailsLayout, 2, 0);

			StackLayout showScheduleLayout = new StackLayout() {
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				Children = {
					buttonShowSchedule,
					labelShowSchedule
				}
			};
			buttonGrid.Children.Add(showScheduleLayout, 3, 0);

			StackLayout mapLayout = new StackLayout() {
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				Children = {
					buttonShowMap,
					labelShowMap
				}
			};
			buttonGrid.Children.Add(mapLayout, 4, 0);

			StackLayout partsLayout = new StackLayout() {
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				Children = {
					buttonShowPartsList,
					labelShowPartsList
				}
			};
			buttonGrid.Children.Add(partsLayout, 5, 0);

			buttonGrid.VerticalOptions = LayoutOptions.End;

            // set the page content to the stack layout we've created
			pageLayout.Children.Add(titleLayout);

			ScrollView scrollContent = new ScrollView() {
				Content = new StackLayout {
					Children = { topGrid, stepGrid }
				}
			};

			pageLayout.Children.Add(scrollContent);
			pageLayout.Children.Add(layout);
			pageLayout.Children.Add(buttonGrid);
            Content = pageLayout;
        }

        protected async void ButtonShowSchedule_Clicked (object sender, EventArgs e)
        {
			await Navigation.PushModalAsync (new ScheduleDetailPage (_vm.ScheduledAppointment));
        }

        protected async void ButtonShowNotes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotesPage(_vm.WorkTicket));
        }

        protected async void ButtonShowPartsList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PartsListPageNew(_vm.WorkTicket, _vm.ScheduledAppointment));
        }

        protected async void ButtonShowCustomerDetails_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CustomerDetailsPage(_vm.Customer, _vm.WorkTicket));
        }

        protected async void ButtonShowMap_Clicked(object sender, EventArgs e)
        {
            Geocoder geoCoder = new Geocoder();
            StringBuilder customerAddress = new StringBuilder();
            Pin pin = new Pin();

            customerAddress.Append(_vm.SalesOrder.ShipToAddress1);
            customerAddress.Append(", ");
            customerAddress.Append(_vm.SalesOrder.ShipToCity);
            customerAddress.Append(", ");
            customerAddress.Append(_vm.SalesOrder.ShipToState);
            customerAddress.Append(" ");
            customerAddress.Append(_vm.SalesOrder.ShipToZipCode);

            string addr = customerAddress.ToString();

            try
            {
                var location = await geoCoder.GetPositionsForAddressAsync(addr);
                List<Position> approximateLocation = location.ToList();
                pin.Position = approximateLocation[0];
                pin.Label = _vm.Customer.CustomerName;
                pin.Address = customerAddress.ToString();

                await Navigation.PushAsync(new CustomerMapPage(pin));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                await DisplayAlert("Invalid Location", "Address cannot be mapped.", "OK");
                return;
            }
        }

        protected async void ButtonClockInOut_Clicked(object sender, EventArgs e)
        {
            //Xamarin.Forms.Button theButton = sender as Xamarin.Forms.Button;
			if (!App.Database.IsClockedIn())
            {
                //puke
                await Navigation.PushAsync(new ClockInPage(_vm.ScheduledAppointment));
            }
            else
            {
                await Navigation.PushAsync(new ClockOutPage(_vm.WorkTicket));
            }
            
        }
	}
}
