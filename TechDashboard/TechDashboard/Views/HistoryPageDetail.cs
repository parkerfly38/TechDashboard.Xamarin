using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechDashboard.ViewModels;

using Xamarin.Forms;
using TechDashboard.Models;

namespace TechDashboard.Views
{
    public class HistoryPageDetail : ContentPage
    {

        HistoryPageViewModel _vm;

        static Color asbestos = Color.FromHex("#7F8C8D");
        Color emerald = Color.FromHex("#2ECC71");
        Color alizarin = Color.FromHex("#E74C3C");
        Color peterriver = Color.FromHex("#3498DB");

        public HistoryPageDetail(string selectedTicketNumber)
        {
            _vm = new HistoryPageViewModel(selectedTicketNumber);
            Initialize();
        }

        protected void Initialize()
        {

            // Set the page title.
            Title = "History Detail";

            BackgroundColor = Color.White;
            Label labelItemCodeTitle = new Label() {
                Text = "ITEM CODE",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            };

            Label labelItemCode = new Label() {
                //Text = _vm.History[0].ItemCode,
                FontFamily = Device.OnPlatform("OpenSans-Regular", "sans-serif", null),
                TextColor = asbestos
            };

            Label labelItemCodeDesc = new Label() {
                //Text = _vm.History[0].ItemDesc,
                FontFamily = Device.OnPlatform("OpenSans-Regular", "sans-serif", null),
                TextColor = asbestos
            };

            Label mfgSerNoTitle = new Label() {
                Text = "MFG SER NO",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            };

            Label mfgSerNo = new Label() {
                //Text = _vm.History[0].MfgSerialNo,
                FontFamily = Device.OnPlatform("OpenSans-Regular", "sans-serif", null),
                TextColor = asbestos
            };

            Label mfgSerNoDesc = new Label() {
                //Text = _vm.History[0].EADesc,
                FontFamily = Device.OnPlatform("OpenSans-Regular", "sans-serif", null),
                TextColor = asbestos
            };

            Label intSerNoTitle = new Label() {
                Text = "INT SER NO",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            };
            Label intSerNo = new Label() {
                //Text = _vm.History[0].IntSerialNo,
                FontFamily = Device.OnPlatform("OpenSans-Regular", "sans-serif", null),
                TextColor = asbestos
            };
            Label modelNoTitle = new Label() {
                Text = "MODEL NO",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = asbestos
            };
            Label modelNo = new Label() {
                //Text = _vm.History[0].ModelNo,
                FontFamily = Device.OnPlatform("OpenSans-Regular", "sans-serif", null),
                TextColor = asbestos
            };

            labelItemCode.Text = _vm.WorkTicket.DtlRepairItemCode;
            labelItemCodeDesc.Text = _vm.Item.ItemCodeDesc;
            mfgSerNo.Text = (_vm.History != null && _vm.History.Count > 0) ? _vm.History[0].MfgSerialNo : "";
            mfgSerNoDesc.Text = (_vm.History != null && _vm.History.Count > 0) ? _vm.History[0].EADesc : "";
            intSerNo.Text = (_vm.History != null && _vm.History.Count > 0) ? _vm.History[0].IntSerialNo : "";
            modelNo.Text = (_vm.History != null && _vm.History.Count > 0) ? _vm.History[0].ModelNo : "";

            //history 
            ListView historyList = new ListView() {
                ItemsSource = _vm.History,
                SeparatorVisibility = SeparatorVisibility.None,
                ItemTemplate = new DataTemplate(typeof(HistoryCell)),
                Header = new Xamarin.Forms.Label
                {
                    Text = "Details",
                    FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                    TextColor =  Color.FromHex("#FFFFFF"),
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.Center,
                    BackgroundColor= peterriver
                }
            };

            Grid topGrid = new Grid();
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200, GridUnitType.Absolute) });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200, GridUnitType.Absolute) });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200, GridUnitType.Absolute) });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200, GridUnitType.Absolute) });

            topGrid.Children.Add(labelItemCodeTitle, 0, 0);
            topGrid.Children.Add(labelItemCode, 1, 0);
            topGrid.Children.Add(labelItemCodeDesc, 2, 0);
            Grid.SetColumnSpan(labelItemCodeDesc, 2);

            topGrid.Children.Add(mfgSerNoTitle, 0, 1);
            topGrid.Children.Add(mfgSerNo, 1, 1);
            topGrid.Children.Add(mfgSerNoDesc, 2, 1);
            Grid.SetColumnSpan(mfgSerNoDesc, 2);

            topGrid.Children.Add(intSerNoTitle, 0, 2);
            topGrid.Children.Add(intSerNo, 1, 2);
            topGrid.Children.Add(modelNoTitle, 2, 2);
            topGrid.Children.Add(modelNo, 3, 2);

            Content = new StackLayout {
				Padding = 30,
				Children = {
					new StackLayout {
						BackgroundColor = peterriver,
						Padding = 50,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						VerticalOptions = LayoutOptions.Start,
						Children = {
							new Xamarin.Forms.Label { 
								Text = "HISTORY",
								FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
								TextColor = Color.White,
								HorizontalOptions = LayoutOptions.CenterAndExpand,
								VerticalOptions = LayoutOptions.Center
							}
						}
					},
                    topGrid,
					historyList
				}
			};

		}

		class HistoryCell : ViewCell
		{
			public HistoryCell() 
			{
				int rowindex = 1;
				if (!Application.Current.Properties.ContainsKey("historyrowindex"))
				{
					Application.Current.Properties["historyrowindex"] = rowindex;
				}
				rowindex = Convert.ToInt32(Application.Current.Properties["historyrowindex"]);
				Color rowcolor = Color.FromHex("#FFFFFF");
				if (rowindex % 2 == 0)
				{
                    rowcolor = Color.FromHex("#ECF0F1");
                }
                else {
                    rowcolor = Color.FromHex("#FFFFFF");
                }
                rowindex = rowindex + 1;
				Application.Current.Properties["historyrowindex"] = rowindex;

				Label labelDate = new Label();
				labelDate.TextColor = asbestos;
				labelDate.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
				labelDate.SetBinding(Label.TextProperty, "TransactionDate");
                labelDate.VerticalTextAlignment = TextAlignment.Center;

				Label labelTrx = new Label();
				labelTrx.TextColor = asbestos;
				labelTrx.FontFamily = Device.OnPlatform("OpenSans-Regular" ,null, null);
				labelTrx.SetBinding(Label.TextProperty, "Trx");
                labelTrx.VerticalTextAlignment = TextAlignment.Center;

                Label labelServiceTicketNo = new Label();
				labelServiceTicketNo.TextColor = asbestos;
				labelServiceTicketNo.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
				labelServiceTicketNo.SetBinding(Label.TextProperty, "ServiceTicketNo");
                labelServiceTicketNo.VerticalTextAlignment = TextAlignment.Center;

                Label labelItemEmployee = new Label();
				labelItemEmployee.TextColor = asbestos;
				labelItemEmployee.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
				labelItemEmployee.SetBinding(Label.TextProperty, "ItemEmployee");
                labelItemEmployee.VerticalTextAlignment = TextAlignment.Center;

                Label labelDescription = new Label();
				labelDescription.TextColor = asbestos;
				labelDescription.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
				labelDescription.SetBinding(Label.TextProperty, "Description");
                labelDescription.VerticalTextAlignment = TextAlignment.Center;

                Label labelQuantity = new Label();
				labelQuantity.TextColor = asbestos;
				labelQuantity.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
				labelQuantity.SetBinding(Label.TextProperty, "Quantity");
                labelQuantity.VerticalTextAlignment = TextAlignment.Center;

                StackLayout rowLayout = new StackLayout()
				{
					BackgroundColor = rowcolor,
					Spacing = 10,
					Orientation = StackOrientation.Horizontal,
					Children = {
						labelDate,
						labelTrx,
						labelServiceTicketNo,
						labelItemEmployee,
						labelDescription,
						labelQuantity
					}
				};
				View = rowLayout;
			}

		}
	}
}
