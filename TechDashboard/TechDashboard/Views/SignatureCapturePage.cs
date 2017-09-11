using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Acr.XamForms.SignaturePad;

namespace TechDashboard.Views
{
    public class SignatureCapturePage : ContentPage
    {
        public SignaturePadView signaturePad;
        public StackLayout SL_root;

        public SignatureCapturePage()
        {
            InitializePage();
        }


        protected void InitializePage()
        {
            Title = "Signature Capture";

            SL_root = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical
            };

            AddSignaturePadLayout(SL_root);

            Content = SL_root;
        }

        void AddSignaturePadLayout(StackLayout root)
        {
            signaturePad = new SignaturePadView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 320,
                BackgroundColor = Color.FromHex("#ecf0f1"),
                CaptionText = "SIGN HERE",
                CaptionTextColor = Color.Black,
                ClearText = "X",
                ClearTextColor = Color.Red,
                SignatureLineColor = Color.Black,
                StrokeColor = Color.FromHex("#2c3e50"),
                StrokeWidth = 2,
                VerticalOptions = LayoutOptions.Start
            };

            SignaturePadConfiguration aaas = new SignaturePadConfiguration()
            {
                SaveText = "Salvame"
            };
            Button saveButton = new Button
            {
                Text = "SAVE",
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                BackgroundColor = Color.FromHex("#2ECC71"),
                TextColor = Color.White
            };
            root.Children.Add(signaturePad);
            root.Children.Add(saveButton);
        }
    }
}
