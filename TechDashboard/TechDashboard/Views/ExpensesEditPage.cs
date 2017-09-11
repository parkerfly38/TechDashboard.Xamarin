using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TechDashboard.Models;
using TechDashboard.Tools;
using TechDashboard.ViewModels;

using Xamarin.Forms;

namespace TechDashboard.Views
{
	public class ExpensesEditPage : ContentPage
	{
        ExpensesEditPageViewModel _vm;

        //Xamarin.Forms.Label _labelHeading;
        //Xamarin.Forms.Entry _entryCategory;
        DatePicker _datePickerExpenseDate;
        BindablePicker _pickerCategory;
        BindablePicker _pickerChargeCode;
        Entry _entryQuantity;
        Entry _entryUnitOfMeasure;
        Entry _entryUnitCost;
        Entry _entryTotalCost;
        Entry _entryUnitPrice;
        Entry _entryMarkupPercentage;
        Entry _entryTotalPrice;
        Switch _switchIsReimbursable;
        Switch _switchChargeCustomer;
        Editor _editorDescription;
        Button _buttonAddEditExpense;
        Button _buttonDelete;         // dch rkl 10/14/2016 When updating, add option to delete expense, only if it is in the JT_TransactionImportDetail table
        Button _buttonCancel;

        Xamarin.Forms.Label _labelTitle;

        //      public ExpensesEditPage (JT_TransactionImportDetail expense)
        //      {
        //          //_expense = expense;
        //          _vm = new ExpensesEditPageViewModel(expense);
        //          Initialize();
        //}

        // dch rkl 10/12/2016
        string workticket = "";

        public ExpensesEditPage(App_Expense expense)
        {
            //_expense = expense;

            // dch rkl 10/12/2016 
            workticket = expense.WorkTicket.FormattedTicketNumber;

            _vm = new ExpensesEditPageViewModel(expense); 
            Initialize();
        }

        public ExpensesEditPage(App_WorkTicket workTicket)
        {
            // dch rkl 10/12/2016 
            workticket = workTicket.FormattedTicketNumber;

            //_expense = new App_Expense();
            _vm = new ExpensesEditPageViewModel(workTicket);
            Initialize();
        }

        protected void Initialize()
        {
            // Set the page title.
            //Title = "Add Expense";
			Color asbestos = Color.FromHex("#7f8C8d");
            //_labelHeading = new Xamarin.Forms.Label();

			BackgroundColor = Color.White;
            //  Create a label for the expense page
            _labelTitle = new Xamarin.Forms.Label();
            if (_vm.ExpenseId > 0)
            {
                _labelTitle.Text = "EDIT EXPENSE";
            }
            else
            {
                _labelTitle.Text = "ADD EXPENSE";
            }

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

            _datePickerExpenseDate = new DatePicker();
            _datePickerExpenseDate.Date = _vm.ExpenseDate;

            _pickerCategory = new BindablePicker { Title = "CATEGORY", ItemsSource = _vm.ExpenseCategories };
            _pickerCategory.SetBinding(BindablePicker.DisplayPropertyProperty, "FullDescription");
            // dch rkl 10/14/2016 Show Code + Description in Expense Category Dropd
            // eek - don't do it this way in the future!  check out clock out page for example
            //List<JT_MiscellaneousCodes> lsMiscCodes = App.Database.GetExpenseCategoriesWithDesc();
            //foreach (JT_MiscellaneousCodes code in lsMiscCodes)
            //{
            //    _pickerCategory.Items.Add(string.Format("{0} - {1}", code.MiscellaneousCode, code.Description));
            //}
            //foreach(string s in _vm.ExpenseCategories)
            //{
            //    _pickerCategory.Items.Add(s);
            //}
            // dch rkl 10/14/2016 Show Code + Description in Expense Category Dropdown END
            //_pickerCategory.BindingContext = _vm.ExpenseCategory; // where does category go?

            _pickerCategory.SelectedIndexChanged += PickerCategory_SelectedIndexChanged;

            _pickerChargeCode = new BindablePicker() { Title = "CHARGE CODE", ItemsSource = _vm.ExpenseChargeCodes };

            // dch rkl 10/14/2016 Show Code + Description in Charge Code Dropdown BEGIN
            //List<string> lsChgCodes = App.Database.GetExpenseChargeCodesWithDesc();
            //foreach (string chgCode in lsChgCodes)
            //{
            //    _pickerChargeCode.Items.Add(chgCode);
            //}
            //foreach (string s in _vm.ExpenseChargeCodes)
            //{
            //    _pickerChargeCode.Items.Add(s);
            //}
            // dch rkl 10/14/2016 Show Code + Description in Charge Code Dropdown END
            //_pickerChargeCode.BindingContext = _vm.ExpenseChargeCode;
            _pickerChargeCode.SelectedIndexChanged += PickerChargeCode_SelectedIndexChanged;

            //         _labelHeading.Text = "ADD EXPENSE";
            //_labelHeading.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
            //_labelHeading.TextColor = asbestos;

            _entryQuantity = new Entry();
            _entryQuantity.Placeholder = "Qty";
            _entryQuantity.Keyboard = Keyboard.Numeric;
            _entryQuantity.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
            _entryQuantity.Text = "";
            _entryQuantity.TextChanged += EntryQuantity_TextChanged;

            _entryUnitOfMeasure = new Entry();
            _entryUnitOfMeasure.Placeholder = "U/M";
            _entryUnitOfMeasure.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);

            _entryUnitCost = new Entry();
            _entryUnitCost.Text = "";
            _entryUnitCost.Keyboard = Keyboard.Numeric;
            _entryUnitCost.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
            _entryUnitCost.TextChanged += EntryUnitCost_TextChanged;

            _entryTotalCost = new Entry();
            _entryTotalCost.Text = "";
            _entryTotalCost.IsEnabled = false;

            _entryUnitPrice = new Entry();
            _entryUnitPrice.Text = "";
            _entryUnitPrice.Keyboard = Keyboard.Numeric;
			_entryUnitPrice.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
 			_entryUnitPrice.TextChanged += EntryUnitPrice_TextChanged;

			_entryTotalPrice = new Entry();
            _entryTotalPrice.Text = "";
            _entryTotalPrice.IsEnabled = false;

            _entryMarkupPercentage = new Entry();
            _entryMarkupPercentage.Text = "0.00";
            _entryMarkupPercentage.Keyboard = Keyboard.Numeric;
            _entryMarkupPercentage.TextChanged += EntryMarkupPercentage_TextChanged;

            _switchIsReimbursable = new Switch();
            _switchIsReimbursable.IsToggled = _vm.ExpenseIsReimbursable;

            _switchChargeCustomer = new Switch();
            _switchChargeCustomer.IsToggled = _vm.ExpenseIsChargeableToCustomer;

            _editorDescription = new Editor();
            //_editorDescription.Text = _vm.ExpenseBillingDescription;

            _buttonAddEditExpense = new Button();
            _buttonAddEditExpense.Clicked += ButtonAddEditExpense_Click;
			_buttonAddEditExpense.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
            _buttonAddEditExpense.Text = "ADD";
			_buttonAddEditExpense.BackgroundColor = Color.FromHex("#2ECC71");
			_buttonAddEditExpense.TextColor = Color.White;
			_buttonAddEditExpense.HorizontalOptions = LayoutOptions.FillAndExpand;

            _buttonCancel = new Button();
            _buttonCancel.Clicked += ButtonCancel_Click;
            _buttonCancel.Text = "CANCEL";
			_buttonCancel.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
			_buttonCancel.TextColor = Color.White;
			_buttonCancel.BackgroundColor = Color.FromHex("#E74C3C");
			_buttonCancel.HorizontalOptions = LayoutOptions.FillAndExpand;

            // dch rkl 10/14/2016 When updating, add option to delete expense, only if it is in the JT_TransactionImportDetail table
            _buttonDelete = new Button();
            _buttonDelete.Clicked += ButtonCancel_Click;
            _buttonDelete.Text = "DELETE";
            _buttonDelete.FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null);
            _buttonDelete.TextColor = Color.White;
            _buttonDelete.BackgroundColor = Color.FromHex("#E74C3C");
            _buttonDelete.HorizontalOptions = LayoutOptions.FillAndExpand;
            if (_vm.ExpenseId == 0) { _buttonDelete.IsVisible = false; }

            if (_vm.ExpenseId > 0)
            {
                Title = "EDIT EXPENSE";

                //BindingContext = _vm.ExpenseItem; puke

                //_labelHeading.Text = "EDIT EXPENSE";

                _buttonAddEditExpense.Text = "UPDATE";

                for(int i = 0; i < _pickerCategory.Items.Count; i++)
                {
                    // dch rkl 10/14/2016 Show Code + Description in Expense Category Dropdown BEGIN
                    string pickerCategory = "";
                    try { pickerCategory = _pickerCategory.Items[i].ToString().Split(' ')[0].ToString().Trim(); }
                    catch (Exception ex) { }
                    //if ((string)_pickerCategory.Items[i] == _vm.ExpenseCategory)
                    if (pickerCategory == _vm.ExpenseCategory)
                    // dch rkl 10/14/2016 Show Code + Description in Expense Category Dropdown END
                    {
                        _pickerCategory.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < _pickerChargeCode.Items.Count; i++)
                {
                    // dch rkl 10/14/2016 Show Code + Description in Charge Code Dropdown BEGIN
                    string pickerChargeCode = "";
                    try { pickerChargeCode = _pickerChargeCode.Items[i].ToString().Split(' ')[0].ToString().Trim(); }
                    catch (Exception ex) { }
                    //if ((string)_pickerChargeCode.Items[i] == _vm.ExpenseChargeCode)
                    if (pickerChargeCode == _vm.ExpenseChargeCode)
                    // dch rkl 10/14/2016 Show Code + Description in Charge Code Dropdown END
                    {
                        _pickerChargeCode.SelectedIndex = i;
                        break;
                    }
                }

                _entryQuantity.Text = _vm.ExpenseQuantity.ToString();
                _entryUnitOfMeasure.Text = _vm.ExpenseUnitOfMeasure;
                _entryUnitCost.Text = _vm.ExpenseUnitCost.ToString();
                _entryUnitPrice.Text = _vm.ExpenseUnitPrice.ToString();
                _switchIsReimbursable.IsToggled = _vm.ExpenseIsReimbursable;
                _editorDescription.Text = _vm.ExpenseBillingDescription;
            }

            Grid topGrid = new Grid();
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // dch rkl 10/12/2016 service ticket row 0
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = 150 });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200, GridUnitType.Absolute) });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150, GridUnitType.Absolute) });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) });

            // dch rkl 10/12/2016 display Service Ticket at top. BEGIN
            Label labelSvcTicketData = new Label
            {
                Text = workticket,
                TextColor = asbestos
            };
            topGrid.Children.Add(new Label
            {
                Text = "Service Ticket",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            }, 0, 0);
            topGrid.Children.Add(labelSvcTicketData, 1, 0);
            Grid.SetColumnSpan(labelSvcTicketData, 3);
            // dch rkl 10/12/2016 display Service Ticket at top. END

            topGrid.Children.Add(new Label
            {
                Text = "Date",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 0, 1);
            topGrid.Children.Add(_datePickerExpenseDate, 1, 1);
            Grid.SetColumnSpan(_datePickerExpenseDate, 3);

            topGrid.Children.Add(new Xamarin.Forms.Label
            {
                Text = "Expense Category",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 0, 2);
            topGrid.Children.Add(_pickerCategory, 1, 2);
            Grid.SetColumnSpan(_pickerCategory, 3);

            topGrid.Children.Add(new Label
            {
                Text = "Charge Code",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 0, 3);
            topGrid.Children.Add(_pickerChargeCode, 1, 3);
            Grid.SetColumnSpan(_pickerChargeCode, 3);

            topGrid.Children.Add(new Label
            {
                Text = "Quantity",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 0, 4);
            topGrid.Children.Add(_entryQuantity, 1, 4);
            topGrid.Children.Add(new Label
            {
                Text = "U/M",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 2, 4);
            topGrid.Children.Add(_entryUnitOfMeasure, 3, 4);

            topGrid.Children.Add(new Label
            {
                Text = "Unit Cost",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 0, 5);
            topGrid.Children.Add(_entryUnitCost, 1, 5);

            topGrid.Children.Add(new Label
            {
                Text = "Total Cost",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 2, 5);
            topGrid.Children.Add(_entryTotalCost, 3, 5);

            topGrid.Children.Add(new Label
            {
                Text = "Unit Price",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 0, 6);
            topGrid.Children.Add(_entryUnitPrice, 1, 6);

            topGrid.Children.Add(new Label
            {
                Text = "Total Price",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 2, 6);
            topGrid.Children.Add(_entryTotalPrice, 3, 6);

            topGrid.Children.Add(new Label
            {
                Text = "Markup",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 0, 7);
            topGrid.Children.Add(_entryMarkupPercentage, 1, 7);
            topGrid.Children.Add(new Label
            {
                Text = "%",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 2, 7);

            Label labelReimbursableHdg = new Label
            {
                Text = "Reimbursable Employee",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            };
            topGrid.Children.Add(labelReimbursableHdg, 0, 8);
            Grid.SetColumnSpan(labelReimbursableHdg, 2);
            topGrid.Children.Add(_switchIsReimbursable, 2, 8);

            Label labelChargeCustomer = new Label {
                Text = "Charge Customer",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            };
            topGrid.Children.Add(labelChargeCustomer, 0, 9);
            Grid.SetColumnSpan(labelChargeCustomer, 2);
            topGrid.Children.Add(_switchChargeCustomer, 2, 9);

            Label labelDescription = new Label
            {
                Text = "Billing Description",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            };
            topGrid.Children.Add(labelDescription, 0, 10);
            Grid.SetColumnSpan(labelDescription, 4);

            topGrid.Children.Add(_editorDescription, 0, 11);
            Grid.SetColumnSpan(_editorDescription, 4);

            Content = new StackLayout
            {
				Padding = 30,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    titleLayout,
                    topGrid,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        {
                            _buttonAddEditExpense,
                            _buttonCancel,
                            _buttonDelete           // dch rkl 10/14/2016
                        }
                    }
                    
                }
            };
        }

        

        //private void EntryUnitCost_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    UpdateTotalCost();
        //}

        //private void EntryQuantity_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    UpdateTotalCost();
        //}

        //private void UpdateTotalCost()
        //{
        //    double totalCost = 0;
        //    double unitCost = 0;
        //    try
        //    {
        //        unitCost = double.Parse(_entryUnitCost.Text);
        //        totalCost = double.Parse(_entryQuantity.Text) * unitCost;
        //    }
        //    catch
        //    {
        //        totalCost = 0;
        //    }

        //    _entryTotalCost.Text = totalCost.ToString("#.00");
        //}

        //private void EntryMarkupPercentage_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    UpdateTotalPrice();
        //}

        //private void EntryUnitPrice_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    UpdateTotalPrice();
        //}

        //private void UpdateTotalPrice()
        //{
        //    double totalPrice = 0;
        //    double markupPercentage = 0;
        //    try
        //    {
        //        markupPercentage = double.Parse(_entryMarkupPercentage.Text) / 100.0;
        //        if (markupPercentage == 0)
        //        {
        //            markupPercentage = 1.0;
        //        }
        //        else
        //        {
        //            markupPercentage += 1.0;
        //        }
        //    }
        //    catch
        //    {
        //        markupPercentage = 1.0;
        //    }
        //    try
        //    {
        //        totalPrice = 
        //            (double.Parse(_entryQuantity.Text) * 
        //             double.Parse(_entryUnitPrice.Text) * 
        //             (markupPercentage)
        //            );
        //    }
        //    catch
        //    {
        //        totalPrice = 0;
        //    }

        //    _entryTotalPrice.Text = totalPrice.ToString("#.00");
        //}

        private void PickerChargeCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            // dch rkl 10/14/2016 Show Code + Description in Charge Code Dropdown BEGIN
            string pickerChargeCode = "";
            try { pickerChargeCode = _pickerChargeCode.Items[_pickerChargeCode.SelectedIndex].ToString().Split(' ')[0].ToString().Trim(); }
            catch (Exception ex) { }
            //_vm.ExpenseChargeCode = _pickerChargeCode.Items[_pickerChargeCode.SelectedIndex];
            _vm.ExpenseChargeCode = pickerChargeCode;
            // dch rkl 10/14/2016 Show Code + Description in Charge Code Dropdown END

            // update other display items
            _entryUnitOfMeasure.Text = _vm.ExpenseUnitOfMeasure;
            if (_vm.ExpenseUnitPrice > 0)
            {
                _entryUnitCost.Text = _vm.ExpenseUnitPrice.ToString();
                _entryUnitPrice.Text = _vm.ExpenseUnitPrice.ToString();
            }
            else
            {
                _entryUnitCost.Text = string.Empty;
                _entryUnitPrice.Text = string.Empty;
            }
        }

        private void PickerCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // dch rkl 10/14/2016 Show Code + Description in Expense Category Dropdown BEGIN
            string pickerCategory = "";
            //try { pickerCategory = _pickerCategory.Items[_pickerCategory.SelectedIndex].ToString().Split(' ')[0].ToString().Trim(); }
            //catch (Exception ex) { }
            pickerCategory = ((JT_MiscellaneousCodes)_pickerCategory.SelectedItem).MiscellaneousCode;
            _vm.ExpenseCategory = pickerCategory;
            //_vm.ExpenseCategory = _pickerCategory.Items[_pickerCategory.SelectedIndex];
            // dch rkl 10/14/2016 Show Code + Description in Expense Category Dropdown END

            // scroll charge code
            for (int i = 0; i < _pickerChargeCode.Items.Count; i++)
            {
                // dch rkl 10/14/2016 Show Code + Description in Charge Code Dropdown BEGIN
                string pickerChargeCode = "";
                try { pickerChargeCode = _pickerChargeCode.Items[i].ToString().Split(' ')[0].ToString().Trim(); }
                catch (Exception ex) { }
                //if (_pickerChargeCode.Items[i] == _vm.ExpenseChargeCode)
                if (pickerChargeCode == _vm.ExpenseChargeCode)
                // dch rkl 10/14/2016 Show Code + Description in Charge Code Dropdown END
                {
                    _pickerChargeCode.SelectedIndex = i;
                    break;
                }
            }
            _entryUnitOfMeasure.Text = _vm.ExpenseUnitOfMeasure;
            _entryUnitPrice.Text = _vm.ExpenseUnitPrice.ToString();
        }

        protected async void ButtonAddEditExpense_Click(object sender, EventArgs e)
        {
            double quantity;
            double unitPrice;
            double unitCost;

            string pickerCategory = "";
            //if(_pickerCategory.SelectedIndex < 0)
            //{
                //await DisplayAlert("Category", "Please enter a valid Category.", "OK");
                //return;
            //}

            try
            {
                quantity = double.Parse(_entryQuantity.Text);
            }
            catch
            {
                await DisplayAlert("Quantity", "Please enter a valid Quantity.", "OK");
                return;
            }
            try
            {
                unitPrice = double.Parse(_entryUnitPrice.Text);
            }
            catch
            {
                await DisplayAlert("Unit Price", "Please enter a valid Unit Price.", "OK");
                return;
            }
            try
            {
                unitCost = double.Parse(_entryUnitCost.Text);
            }
            catch
            {
                await DisplayAlert("Unit Cost", "Please enter a valid Unit Cost.", "OK");
                return;
            }


            _vm.ExpenseDate = _datePickerExpenseDate.Date;

            // dch rkl 10/14/2016 Show Code + Description in Expense Category Dropdown BEGIN

            try { pickerCategory = _pickerCategory.Items[_pickerCategory.SelectedIndex].ToString().Split(' ')[0].ToString().Trim(); }
            catch (Exception ex) { }
            _vm.ExpenseCategory = pickerCategory;
            //_vm.ExpenseCategory = (_pickerCategory.Items[_pickerCategory.SelectedIndex]);
            // dch rkl 10/14/2016 Show Code + Description in Expense Category Dropdown END

            // dch rkl 10/14/2016 Show Code + Description in Charge Code Dropdown BEGIN
            string pickerChargeCode = "";
            try { pickerChargeCode = _pickerChargeCode.Items[_pickerChargeCode.SelectedIndex].ToString().Split(' ')[0].ToString().Trim(); }
            catch (Exception ex) { }
            //_vm.ExpenseChargeCode = (_pickerChargeCode.Items[_pickerChargeCode.SelectedIndex]);
            _vm.ExpenseChargeCode = pickerChargeCode;
            // dch rkl 10/14/2016 Show Code + Description in Charge Code Dropdown END

            _vm.ExpenseQuantity = quantity;
            _vm.ExpenseUnitOfMeasure = _entryUnitOfMeasure.Text;
            _vm.ExpenseUnitCost = unitCost;
            _vm.ExpenseUnitPrice = unitPrice;
            _vm.ExpenseIsReimbursable = _switchIsReimbursable.IsToggled;
            _vm.ExpenseIsChargeableToCustomer = _switchChargeCustomer.IsToggled;
            _vm.ExpenseBillingDescription = _editorDescription.Text;

            _vm.SaveExpenseItem();

            await Navigation.PopAsync();
        }

        protected async void ButtonCancel_Click(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        // dch rkl 10/14/2016 Delete - Remove Transaction from JT_TransactionImportDetail Table
        protected async void ButtonDelete_Click(object sender, EventArgs e)
        {
            // Delete Expense
            _vm.DeleteExpenseItem();

            // Return to Expense List Page
            await Navigation.PopAsync();
        }
















        private void EntryUnitCost_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTotalCost();
        }

        private void EntryQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTotalCost();
            UpdateTotalPrice();
        }

        private void UpdateTotalCost()
        {
            double quantity = 0;
            double unitCost = 0;
            double totalCost = 0;
            try
            {
                quantity = double.Parse(_entryQuantity.Text);
            }
            catch
            {
                quantity = 0;
            }
            try
            {
                unitCost = double.Parse(_entryUnitCost.Text);
            }
            catch
            {
                unitCost = 0;
            }

            totalCost = quantity * unitCost;
            _entryTotalCost.Text = totalCost.ToString("#.00");
        }

        private void EntryMarkupPercentage_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTotalPrice();
        }

        private void EntryUnitPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
            double quantity = 0;
            double unitPrice = 0;
            double markupPercentage = 0;
            double totalPrice = 0;

            try
            {
                quantity = double.Parse(_entryQuantity.Text);
            }
            catch
            {
                quantity = 0;
            }
            try
            {
                unitPrice = double.Parse(_entryUnitPrice.Text);
            }
            catch
            {
                unitPrice = 0;
            }
            try
            {
                markupPercentage = double.Parse(_entryMarkupPercentage.Text) / 100.0;
                if (markupPercentage == 0)
                {
                    markupPercentage = 1.0;
                }
                else
                {
                    markupPercentage += 1.0;
                }
            }
            catch
            {
                markupPercentage = 1.0;
            }

            totalPrice = (quantity * unitPrice * markupPercentage);
            _entryTotalPrice.Text = totalPrice.ToString("#.00");
        }
    }
}
