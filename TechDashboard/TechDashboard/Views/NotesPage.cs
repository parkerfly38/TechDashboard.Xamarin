using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using TechDashboard.Models;
using TechDashboard.ViewModels;

namespace TechDashboard.Views
{
	public class NotesPage : ContentPage
	{
        NotesPageViewModel _vm;
        Editor _editorNotes;
        Xamarin.Forms.Label _labelTitle;

        public NotesPage(App_WorkTicket workTicket)
        {
            _vm = new NotesPageViewModel(workTicket);
            Initialize();
        }

        public NotesPage()
        {
            _vm = new NotesPageViewModel();
            Initialize();
        }

        protected void Initialize()
        {
            // Set the page title.
            Title = "Notes";

			BackgroundColor = Color.White;
            //  Create a label for the notes page
            _labelTitle = new Xamarin.Forms.Label();
            _labelTitle.Text = "NOTES";
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

            _editorNotes = new Editor()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Text = (_vm.WorkTicketText.Text == null ? string.Empty : _vm.WorkTicketText.Text)
            };

            Xamarin.Forms.Button buttonOK = new Button() { 
				Text = "SAVE",
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
				HorizontalOptions = LayoutOptions.Fill,
				BackgroundColor = Color.FromHex("#2ECC71"),
				TextColor = Color.White
			};
            buttonOK.Clicked += ButtonOK_Clicked;

            Xamarin.Forms.Button buttonCancel = new Button() { 
				Text = "CANCEL",
				FontFamily = Device.OnPlatform("OpenSans-Bold",null,null),
				HorizontalOptions = LayoutOptions.Fill,
				BackgroundColor = Color.FromHex("#E74C3C"),
				TextColor = Color.White
			};
            buttonCancel.Clicked += ButtonCancel_Clicked;

			Frame frame = new Frame();
			frame.Content = _editorNotes;

            Content = new StackLayout {
				Padding = 30,
				    Children = {
                    titleLayout,
     //               new Xamarin.Forms.Label { 
					//	Text = "TICKET NOTES",
					//	FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null), 
					//	TextColor = Color.FromHex("#7F8C8D")
					//},
					frame,
					new StackLayout
                    {
						Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.Fill,
                        Children =
                        {
                            buttonOK,
                            buttonCancel
                        }
                    }
				}
			};
		}

        protected async void ButtonOK_Clicked(object sender, EventArgs e)
        {
            // Update the note text
            _vm.UpdateNotes(_editorNotes.Text);

            await Navigation.PopAsync();
        }

        protected async void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
