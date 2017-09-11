using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace TechDashboard.Views
{
	public class AppMasterDetailPage : MasterDetailPage
    {
		public AppMasterDetailPage ()
		{
			this.BackgroundColor = Color.White;
        //    this.Detail = new NavigationPage(new TicketDetailsPage(MainApp));
        //    this.Master = new MainMenu(MainApp, this);

        //    //this.IsPresented = true;

        //    masterPage.ListView.ItemSelected += OnItemSelected;

        //    if (Device.OS == TargetPlatform.Windows)
        //    {
        //        Master.Icon = "swap.png";
        //    }
        //}

        //void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var item = e.SelectedItem as MasterPageItem;
        //    if (item != null)
        //    {
        //        Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
        //        masterPage.ListView.SelectedItem = null;
        //        IsPresented = false;
        //    }
        }
    }
}
