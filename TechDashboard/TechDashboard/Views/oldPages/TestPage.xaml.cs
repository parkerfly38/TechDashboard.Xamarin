﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using TechDashboard.ViewModels;

namespace TechDashboard.Views
{
    public partial class TestPage : BaseContentPage
    {
        TestPageViewModel _vm;

        public TestPage(Application application) : base(application)
        {
            InitializeComponent();

            _vm = new TestPageViewModel();
            BindingContext = _vm.Customer;

            _vm.PropertyChanged += CustomerChanged;
        }

        public void btnSearchSData_OnClick(object sender, EventArgs e)
        {
            //AR_Customer customer;

            //this.BindingContext = null;

            //string customerNumber = CustomerNo.Text;
            //customer = _vm.GetCustomerFromSData(customerNumber);

            //if (customer != null)
            //{
            //    this.BindingContext = customer;
            //}
            _vm.GetCustomerFromSData(CustomerNo.Text);
        }

        public void btnSearchDB_OnClick(object sender, EventArgs e)
        {
            _vm.GetCustomerFromDB(CustomerNo.Text);
        }

        public void btnFillDB_OnClick(object sender, EventArgs e)
        {
            _vm.FillDbFromSdata();
        }

        private void CustomerChanged(object sender, PropertyChangedEventArgs e)
        {
            BindingContext = _vm.Customer;
        }
    }
}
