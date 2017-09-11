using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using TechDashboard.Models;
using TechDashboard.Tools;
using TechDashboard.ViewModels;
using Xamarin.Forms;

namespace TechDashboard.Views
{
	public class ExpensesListPage : ContentPage
	{
        #region Helper Classes

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
                    Padding = 10,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.Blue,
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
        class ExpenseDataCell : ViewCell
        {
            public ExpenseDataCell()
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

                // charge code
                //Xamarin.Forms.Label labelChargeCodeHdg = new Xamarin.Forms.Label();
                //labelChargeCodeHdg.FontSize = 14;
                //labelChargeCodeHdg.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
                //labelChargeCodeHdg.TextColor = forecolor;
                //labelChargeCodeHdg.Text = "Charge Code";

                Xamarin.Forms.Label labelChargeCode = new Xamarin.Forms.Label();
                labelChargeCode.FontSize = 14;
				labelChargeCode.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
				labelChargeCode.TextColor = forecolor;
                labelChargeCode.SetBinding(Xamarin.Forms.Label.TextProperty, "ChargeCode");

                // item code
                //Xamarin.Forms.Label labelItemCodeHdg = new Xamarin.Forms.Label();
                //labelItemCodeHdg.FontSize = 14;
                //labelItemCodeHdg.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
                //labelItemCodeHdg.TextColor = forecolor;
                //labelItemCodeHdg.Text = "Item Code";

                Xamarin.Forms.Label labelItemCode = new Xamarin.Forms.Label();
                labelItemCode.FontSize = 14;
                labelItemCode.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
                labelItemCode.TextColor = forecolor;
                labelItemCode.SetBinding(Xamarin.Forms.Label.TextProperty, "ItemCode");

                // quantity
                Xamarin.Forms.Label labelQuantityHdg = new Xamarin.Forms.Label();
                labelQuantityHdg.FontSize = 14;
                labelQuantityHdg.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
                labelQuantityHdg.TextColor = forecolor;
                labelQuantityHdg.Text = "Qty";

                Xamarin.Forms.Label labelQuantity = new Xamarin.Forms.Label();
                labelQuantity.FontSize = 14;
				labelQuantity.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
				labelQuantity.TextColor = forecolor;
                labelQuantity.SetBinding(Xamarin.Forms.Label.TextProperty, "Quantity");

                // unit of measure
                Xamarin.Forms.Label labelUnitOfMeasure = new Xamarin.Forms.Label();
                labelUnitOfMeasure.FontSize = 14;
				labelUnitOfMeasure.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
				labelUnitOfMeasure.TextColor = forecolor;
                labelUnitOfMeasure.SetBinding(Xamarin.Forms.Label.TextProperty, "UnitOfMeasure");

                // unit cost
                Xamarin.Forms.Label labelUnitCostHdg = new Xamarin.Forms.Label();
                labelUnitCostHdg.FontSize = 14;
                labelUnitCostHdg.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
                labelUnitCostHdg.TextColor = forecolor;
                labelUnitCostHdg.Text = "Unit Cost";

                Xamarin.Forms.Label labelUnitCost = new Xamarin.Forms.Label();
                labelUnitCost.FontSize = 14;
                labelUnitCost.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
                labelUnitCost.TextColor = forecolor;
                labelUnitCost.SetBinding(Xamarin.Forms.Label.TextProperty, "UnitCost");

                // unit price
                Xamarin.Forms.Label labelUnitPriceHdg = new Xamarin.Forms.Label();
                labelUnitPriceHdg.FontSize = 14;
                labelUnitPriceHdg.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
                labelUnitPriceHdg.TextColor = forecolor;
                labelUnitPriceHdg.Text = "Unit Price";

                Xamarin.Forms.Label labelUnitPrice = new Xamarin.Forms.Label();
                labelUnitPrice.FontSize = 14;
                labelUnitPrice.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
                labelUnitPrice.TextColor = forecolor;
                labelUnitPrice.SetBinding(Xamarin.Forms.Label.TextProperty, "UnitPrice");

                // extended price    
                Xamarin.Forms.Label labelExtdPriceHdg = new Xamarin.Forms.Label();
                labelExtdPriceHdg.FontSize = 14;
                labelExtdPriceHdg.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
                labelExtdPriceHdg.TextColor = forecolor;
                labelExtdPriceHdg.Text = "Extd Price";

                Xamarin.Forms.Label labelExtdPrice = new Xamarin.Forms.Label();
                labelExtdPrice.FontSize = 14;
                labelExtdPrice.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
                labelExtdPrice.TextColor = forecolor;
                labelExtdPrice.SetBinding(Xamarin.Forms.Label.TextProperty, "ExtdPrice");


                // description
                Xamarin.Forms.Label labelDescription = new Xamarin.Forms.Label();
				labelDescription.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
				labelDescription.TextColor = forecolor;
                labelDescription.SetBinding(Xamarin.Forms.Label.TextProperty, "BillingDescription");

                View = new StackLayout()
                {
                    Padding = 10,
                    BackgroundColor = rowcolor,
                    Children =
                    {
                        new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                labelChargeCode,
                                labelItemCode
                    }
                        },
                        new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                labelQuantityHdg,
                                labelQuantity,
                                labelUnitOfMeasure
                            }
                        },
                        new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                labelUnitPriceHdg,
                                labelUnitPrice,
                                labelExtdPriceHdg,
                                labelExtdPrice
                            }
                        },
                        new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                labelUnitCostHdg,
                                labelUnitCost,
                            }
                        },
                        labelDescription
                    }
                };
            }
        }

        #endregion


        ExpensesListPageViewModel _vm;
        Xamarin.Forms.Label _labelTitle;
        ListView _listViewExpenses;
        BindablePicker _pickerScheduledAppointment;

        public ExpensesListPage()
        {
            _vm = new ExpensesListPageViewModel();
        }

        public ExpensesListPage(App_WorkTicket workTicket)
        {
            _vm = new ExpensesListPageViewModel(workTicket);
        }

        public void SetPageDisplay()
        { 
            // Set the page title.
            //Title = "Expenses";
			Color asbestos = Color.FromHex("#7f8C8d");

			BackgroundColor = Color.White;
            //  Create a label for the expenses list
            _labelTitle = new Xamarin.Forms.Label();
            _labelTitle.Text = "EXPENSES";
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

            _pickerScheduledAppointment = new BindablePicker();
            _pickerScheduledAppointment.Title = "WORK TICKET";
            _pickerScheduledAppointment.ItemsSource = _vm.GetScheduledAppointments();
            _pickerScheduledAppointment.SetBinding(BindablePicker.DisplayPropertyProperty, "FormattedTicketNumber");
            _pickerScheduledAppointment.SelectedIndexChanged += PickerScheduledAppointmentt_SelectedIndexChanged;
            for (int i = 0; i < _pickerScheduledAppointment.Items.Count; i++)
            {
                if (_vm.WorkTicket != null && _pickerScheduledAppointment.Items[i] == _vm.WorkTicket.FormattedTicketNumber)
                {
                    _pickerScheduledAppointment.SelectedIndex = i;
                    break;
                }
            }

            // Create our screen objects
            //  Create a label for the technician list
   //         _labelTitle = new Xamarin.Forms.Label();
   //         _labelTitle.Text = "EXPENSES";
   //         _labelTitle.FontSize = 20;
			//_labelTitle.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
			//_labelTitle.TextColor = asbestos;

            Button buttonAddExpense = new Button();
            
            buttonAddExpense.Text = "ADD";
			buttonAddExpense.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
			buttonAddExpense.TextColor = Color.White;
			buttonAddExpense.BackgroundColor = Color.FromHex("#2EcC71");
			buttonAddExpense.HorizontalOptions = LayoutOptions.FillAndExpand;
            buttonAddExpense.Clicked += ButtonAddEditExpense_Clicked;

            // Create a template to display each technician in the list
            var dataTemplateExpense = new DataTemplate(typeof(ExpenseDataCell));

            // Create the actual list
            _listViewExpenses = new ListView()
            {
                HasUnevenRows = true,
                ItemsSource = _vm.ExpensesList,
                ItemTemplate = dataTemplateExpense,
                SeparatorVisibility = SeparatorVisibility.None
            };
            _listViewExpenses.BindingContext = _vm.ExpensesList;
            _listViewExpenses.ItemTapped += ListViewExpenses_ItemTapped;


            Content = new StackLayout
            {
				Padding = 30,
                Children = {
                    //_labelTitle,
                    titleLayout,
                    _pickerScheduledAppointment,
                    _listViewExpenses,
                    buttonAddExpense
                }
            };
        }

        protected void PickerScheduledAppointmentt_SelectedIndexChanged(object sender, EventArgs e)
        {
            App_ScheduledAppointment selectedAppointment = null;   

            // I've found that the picker object will remove duplicate entries when displaying on-screen data, 
            //  so I need to do a little more work to find the proper object selected.  Will revisit code for 
            //  future improvements.
            for(int i = 0; i < _pickerScheduledAppointment.ItemsSource.Count; i++)
            {
                selectedAppointment = _pickerScheduledAppointment.ItemsSource[i] as App_ScheduledAppointment;

                if (selectedAppointment.ServiceTicketNumber == _pickerScheduledAppointment.Items[_pickerScheduledAppointment.SelectedIndex])
                {
                    _vm.SetWorkTicket(selectedAppointment.ServiceTicketNumber);
                    break;
                }
            }            
        }

        private async void ListViewExpenses_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Get the selected item and type it properly.
            App_Expense expense = e.Item as App_Expense;

            // Unset the selection so nothing is highlighted in the list anymore
            _listViewExpenses.SelectedItem = null;

            // Go to the edit page and pass the selected expense item.
            await Navigation.PushAsync(new ExpensesEditPage(expense));
        }

        private async void ButtonAddEditExpense_Clicked(object sender, EventArgs e)
        {
            // Make sure that some sort of work ticket was selected!  If not, we can't move 
            //  forward.
            if (_pickerScheduledAppointment.SelectedIndex < 0)
            {
                await DisplayAlert("Select Work Ticket", "Please select a work ticket for your expenses.", "OK");
                return;
            }

            // Let's add a new expense item
            await Navigation.PushAsync(new ExpensesEditPage(_vm.WorkTicket));
        }

        protected override void OnAppearing()
        {
            // ensure the expenses list is updated, and re-display the page
            _vm.RefreshExpensesList();
            SetPageDisplay();            

            base.OnAppearing();
        }
    }
}
