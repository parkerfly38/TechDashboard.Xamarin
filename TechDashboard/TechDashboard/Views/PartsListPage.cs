﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using TechDashboard.ViewModels;
using TechDashboard.Models;
using Xamarin.Forms;
using DevExpress.Mobile;

namespace TechDashboard.Views
{
	public class PartsListPage : ContentPage
	{
        #region Helper Classes

        class PartsListDataCell : ViewCell
        {
            public PartsListDataCell()
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
                // need a spot for the work ticket number
                Xamarin.Forms.Label labelPartItemCode = new Xamarin.Forms.Label();
                labelPartItemCode.FontSize = 14;
				labelPartItemCode.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
				labelPartItemCode.TextColor = forecolor;
                labelPartItemCode.SetBinding(Xamarin.Forms.Label.TextProperty, "PartItemCode");

                // need a spot for the description
                Xamarin.Forms.Label labelItemCode = new Xamarin.Forms.Label();
                labelItemCode.FontSize = 14;
				labelItemCode.TextColor = forecolor;
				labelItemCode.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
				//labelItemCode.FontAttributes = FontAttributes.Bold;
                labelItemCode.SetBinding(Xamarin.Forms.Label.TextProperty, "PartItemCodeDescription");

                // warehouse
                Xamarin.Forms.Label labelWarehouseHdg = new Xamarin.Forms.Label();
                labelWarehouseHdg.FontSize = 14;
                labelWarehouseHdg.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
                labelWarehouseHdg.TextColor = forecolor;
                labelWarehouseHdg.Text = "Warehouse";

                Xamarin.Forms.Label labelWarehouse = new Xamarin.Forms.Label();
                labelWarehouse.FontSize = 14;
                labelWarehouse.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
                labelWarehouse.TextColor = forecolor;
                labelWarehouse.SetBinding(Xamarin.Forms.Label.TextProperty, "Warehouse");

                // quantity
                Xamarin.Forms.Label labelQuantityHdg = new Xamarin.Forms.Label();
                labelQuantityHdg.FontSize = 14;
                labelQuantityHdg.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
                labelQuantityHdg.TextColor = forecolor;
                labelQuantityHdg.Text = "Quantity";

                Xamarin.Forms.Label labelQuantity = new Xamarin.Forms.Label();
                labelQuantity.FontSize = 14;
                labelQuantity.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
                labelQuantity.TextColor = forecolor;
                labelQuantity.SetBinding(Xamarin.Forms.Label.TextProperty, "Quantity");

                // unit cost
                Xamarin.Forms.Label labelCostHdg = new Xamarin.Forms.Label();
                labelCostHdg.FontSize = 14;
                labelCostHdg.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
                labelCostHdg.TextColor = forecolor;
                labelCostHdg.Text = "Unit Cost";

                Xamarin.Forms.Label labelCost = new Xamarin.Forms.Label();
                labelCost.FontSize = 14;
                labelCost.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
                labelCost.TextColor = forecolor;
                labelCost.SetBinding(Xamarin.Forms.Label.TextProperty, new Binding("UnitCost", stringFormat: "{0:C}"));

                // unit price
                Xamarin.Forms.Label labelPriceHdg = new Xamarin.Forms.Label();
                labelPriceHdg.FontSize = 14;
                labelPriceHdg.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
                labelPriceHdg.TextColor = forecolor;
                labelPriceHdg.Text = "Unit Price";

                Xamarin.Forms.Label labelPrice = new Xamarin.Forms.Label();
                labelPrice.FontSize = 14;
                labelPrice.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
                labelPrice.TextColor = forecolor;
                labelPrice.SetBinding(Xamarin.Forms.Label.TextProperty, new Binding("UnitPrice",stringFormat:"{0:C}"));

                // extd price
                Xamarin.Forms.Label labelExtdPriceHdg = new Xamarin.Forms.Label();
                labelExtdPriceHdg.FontSize = 14;
                labelExtdPriceHdg.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
                labelExtdPriceHdg.TextColor = forecolor;
                labelExtdPriceHdg.Text = "Extended Price";

                Xamarin.Forms.Label labelExtdPrice = new Xamarin.Forms.Label();
                labelExtdPrice.FontSize = 14;
                labelExtdPrice.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
                labelExtdPrice.TextColor = forecolor;
                labelExtdPrice.SetBinding(Xamarin.Forms.Label.TextProperty, new Binding("ExtdPrice",stringFormat:"{0:C}"));

                View = new StackLayout()
                {
                    Padding = 5,
                    Orientation = StackOrientation.Vertical,
                    BackgroundColor = rowcolor,
                    Children =
                    {
                        new StackLayout()
                        {
                            Padding = 1,
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                labelPartItemCode,
                                labelItemCode
                            }
                        },
                        new StackLayout()
                        {
                            Padding = 1,
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                labelWarehouseHdg,
                                labelWarehouse
                            }
                        },
                        new StackLayout()
                        {
                            Padding = 1,
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                labelCostHdg,
                                labelCost
                            }
                        },
                        new StackLayout()
                        {
                            Padding = 1,
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                labelQuantityHdg,
                                labelQuantity,
                                labelPriceHdg,
                                labelPrice,
                                labelExtdPriceHdg,
                                labelExtdPrice                            }
                        }
                    }
                };
            }
        }

        #endregion

        PartsListPageViewModel _vm;
        App_ScheduledAppointment _scheduledAppointment;
        Xamarin.Forms.Label _labelTitle;
        ListView _listViewParts;

        public PartsListPage(App_WorkTicket workTicket, App_ScheduledAppointment scheduledAppointment)
        {
            _vm = new PartsListPageViewModel(workTicket);
            _scheduledAppointment = scheduledAppointment;
            SetPageDisplay();
        }

        //public PartsListPage()
        //{
        //    _vm = new PartsListPageViewModel();
        //    SetPageDisplay();
        //}

        protected void SetPageDisplay()
        {
     
            // Set the page title.
            //Title = "Parts List";

			Color asbestos = Color.FromHex("#7f8C8d");
            BindingContext = _vm.ObservablePartsList;

			BackgroundColor = Color.White;
            //  Create a label for the technician list
            _labelTitle = new Xamarin.Forms.Label();
            _labelTitle.Text = "PARTS LIST";
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

            //var dataTemplateItem = new DataTemplate(typeof(PartsListDataCell));

            /*_listViewParts = new ListView()
            {
                HasUnevenRows = true,
                SeparatorVisibility = SeparatorVisibility.None,
                ItemsSource = _vm.ObservablePartsList,
                ItemTemplate = dataTemplateItem
            };*/

            Button buttonAddEditPart = new Button()
            {
                Text = "ADD NEW PART",
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
				TextColor = Color.White,
				BackgroundColor = Color.FromHex("#2ECC71")
            };
            
            buttonAddEditPart.Clicked += ButtonAddEditPart_Clicked; // puke... only do if clocked in on ticket
            
            //_listViewParts.ItemTapped += ListViewWorkTickets_ItemTapped;  // puke... only do if clocked in on ticket


            Content = new StackLayout
            {
				Padding = 30,
                Children = {
                    titleLayout,
                    //_listViewParts,
                    buttonAddEditPart
                }
            };
        }

        private async void ListViewWorkTickets_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            App_RepairPart part = e.Item as App_RepairPart;
            CI_Item partAsItem = App.Database.GetItemFromDB(part.PartItemCode);
           
            await Navigation.PushAsync(new PartsEditPage(_vm.WorkTicket, part, PartsEditPage.PageMode.Edit, _scheduledAppointment));
        }

        private async void ButtonAddEditPart_Clicked(object sender, EventArgs e)
        {
            // puke... what if not clocked into a ticket?  How do we know which parts list to add to?
            await Navigation.PushAsync(new PartsAddPage(_vm.WorkTicket, _scheduledAppointment));
        }

        protected override void OnAppearing()
        {
            App_WorkTicket workTicket = _vm.WorkTicket;
            _vm = new PartsListPageViewModel(workTicket);

            SetPageDisplay();

            base.OnAppearing();

        }
    }
}
