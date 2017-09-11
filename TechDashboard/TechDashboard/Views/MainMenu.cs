using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace TechDashboard.Views
{
    class MainMenuItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }
    }

	class MainMenuCell : ViewCell
	{
		public MainMenuCell()
		{
			Color forecolor = Color.White;
			Image cellImage = new Image();
			cellImage.VerticalOptions = LayoutOptions.Center;
			cellImage.SetBinding(Image.SourceProperty, "IconSource");

			Label cellLabel = new Label() {
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
				TextColor = forecolor,
				VerticalOptions = LayoutOptions.Center
			};
			cellLabel.SetBinding(Label.TextProperty, "Title");

			View = new StackLayout() {
				BackgroundColor = Color.FromHex("#2980B9"),
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness(0,0,0,20),
				Children = {
					cellImage, 
					cellLabel
				}
			};
		}
	}

    public class MainMenu : ContentPage
    {
        ListView _listView;
        public ListView ListView
        { 
            get
            {
                return _listView;
            }
        }

        MainMenuItem _menuItemTechnician;
        MainMenuItem _menuItemSchedule;
        //MainMenuItem _menuItemDepart;
        //MainMenuItem _menuItemPartsList;
        //MainMenuItem _menuItemNotes;
        MainMenuItem _menuItemExpenses;
        //MainMenuItem _menuItemSmsMessages;
        MainMenuItem _menuItemExit;
		MainMenuItem _menuItemMisc;
		MainMenuItem _menuHistory;
        MainMenuItem _menuItemAppSettings;
		MainMenuItem _menuItemSync;
        //MainMenuItem _menuItemSignatureCapture;

        List<MainMenuItem> _masterPageItems;

        public MainMenu()
        {
            Title = "Menu";

            _masterPageItems = new List<MainMenuItem>();

            _menuItemTechnician = new MainMenuItem
            {
                Title = "TECHNICIAN",
                IconSource = "worker.png",
                TargetType = typeof(TechnicianPage)
            };
            
			_menuItemSchedule = new MainMenuItem
            {
                Title = "SCHEDULE",
                IconSource = "calendar.png",
                TargetType = typeof(SchedulePage)
            };

			_menuItemMisc = new MainMenuItem {
				Title = "MISC TIME",
				IconSource = "time.png",
				TargetType = typeof(MiscellaneousTimePage)
			};

			_menuHistory = new MainMenuItem {
				Title = "HISTORY",
				IconSource = "fullfolder.png",
				TargetType = typeof(HistoryPage)
			};
            
			_menuItemExpenses = new MainMenuItem
            {
                Title = "EXPENSES",
                IconSource = "banknotes.png",
                TargetType = typeof(ExpensesListPage)
            };
            
			/*_menuItemSmsMessages = new MainMenuItem
            {
                Title = "SMS MESSAGE",
                IconSource = "message.png",
                TargetType = typeof(SmsMessagePage)
            };*/

			_menuItemSync = new MainMenuItem {
				Title = "SYNC",
				IconSource = "sync.png",
				TargetType = typeof(SyncPage)
			};

            _menuItemAppSettings = new MainMenuItem
            {
                Title = "SETTINGS",
                IconSource = "cog.png",
                TargetType = typeof(AppSettingsPage)
            };

            /*_menuItemSignatureCapture = new MainMenuItem
            {
                Title = "SIGNATURE CAPTURE",
                IconSource = "cog.png",
                TargetType = typeof(SignatureCapturePage)
            };*/

            _menuItemExit = new MainMenuItem
            {
                Title = "LOG OUT",
                IconSource = "exit.png",
                TargetType = typeof(TechnicianListPage)
            };

            _masterPageItems.Add(_menuItemTechnician);
            _masterPageItems.Add(_menuItemSchedule);
			_masterPageItems.Add(_menuItemMisc);
			_masterPageItems.Add(_menuHistory);
            _masterPageItems.Add(_menuItemExpenses);
            //_masterPageItems.Add(_menuItemSmsMessages);
			_masterPageItems.Add(_menuItemSync);
            _masterPageItems.Add(_menuItemAppSettings);
            //_masterPageItems.Add(_menuItemSignatureCapture);
            _masterPageItems.Add(_menuItemExit);

			var dataTemplate = new DataTemplate(typeof(MainMenuCell));

            _listView = new ListView
            {
                ItemsSource = _masterPageItems,
                /*ItemTemplate = new DataTemplate(() => {
                    var imageCell = new ImageCell();
                    imageCell.SetBinding(TextCell.TextProperty, "Title");
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");
                    return imageCell;
                }),*/
				HasUnevenRows = true,
				ItemTemplate = dataTemplate,
				SeparatorVisibility = SeparatorVisibility.None,
				VerticalOptions = LayoutOptions.StartAndExpand,
				BackgroundColor = Color.FromHex("#2980b9")
            };

			StackLayout containerStackLayout = new StackLayout() {
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.FromHex("#2980b9"),
				Children = { 
					_listView
				}
			};

            Padding = new Thickness(0, 40, 0, 0);
            Content = new StackLayout
            {
				BackgroundColor = Color.FromHex("#2980B9"),
				Padding = 20,
				VerticalOptions = LayoutOptions.Fill,
                Children = {
                    containerStackLayout
                }
            };
			BackgroundColor = Color.FromHex("#2980B9");
        }
    }
}
