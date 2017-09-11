using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace TechDashboard.Views
{
    public class BaseMasterDetailPage : MasterDetailPage
    {
        App _application;
        protected App MainApp
        {
            get { return _application; }
        }

        public BaseMasterDetailPage(App application) : base()
		{
            _application = application;            
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    //MainApp.MoveBack();
        //    return true;
        //}
    }
}
