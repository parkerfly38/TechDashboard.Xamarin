using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sage.SData.Client;
using TechDashboard.Models;
using TechDashboard.Data;
using TechDashboard.Services;

namespace TechDashboard.ViewModels
{
    public class CustomerDetailsPageViewModel
    {
        protected App_Customer _customer;
        protected List<App_CustomerContact> _customerContacts;
        protected App_SalesOrder _salesOrder;

        public App_Customer Customer 
        {
            get { return _customer; }
        }

        public string CustomerName
        {
            get { return _customer.CustomerName; }
        }

        public string FormattedCustomerNumber
        {
            get { return _customer.FormattedCustomerNumber; }
        }

        public List<App_CustomerContact> CustomerContacts
        {
            get { return _customerContacts; }
        }

        public string AddressLine1
        {
            get
            {
                if(_salesOrder != null && _salesOrder.ShipToAddress1 != null)
                {
                    return _salesOrder.ShipToAddress1;
                }
                else
                {
                    return _customer.AddressLine1;
                }
            }
        }

        public string AddressLine2
        {
            get
            {
                if (_salesOrder != null)
                {
                    return _salesOrder.ShipToAddress2;
                }
                else
                {
                    return _customer.AddressLine2;
                }
            }
        }

        public string AddressLine3
        {
            get
            {
                if (_salesOrder != null)
                {
                    return _salesOrder.ShipToAddress3;
                }
                else
                {
                    return _customer.AddressLine3;
                }
            }
        }

        public string City
        {
            get
            {
                if (_salesOrder != null)
                {
                    return _salesOrder.ShipToCity;
                }
                else
                {
                    return _customer.City;
                }
            }
        }

        public string State
        {
            get
            {
                if (_salesOrder != null)
                {
                    return _salesOrder.ShipToState;
                }
                else
                {
                    return _customer.State;
                }
            }
        }

        public string ZipCode
        {
            get
            {
                if (_salesOrder != null)
                {
                    return _salesOrder.ShipToZipCode;
                }
                else
                {
                    return _customer.ZipCode;
                }
            }
        }

        public string TelephoneNo
        {
            get { return _customer.TelephoneNo; }
        }

        public string TelephoneExt
        {
            get { return _customer.TelephoneExt; }
        }


        public CustomerDetailsPageViewModel(App_WorkTicket workTicket)
        {
            _customer = App.Database.GetCustomerFromCurrentWorkTicket();
            _customerContacts = App.Database.GetAppCustomerContacts(_customer.CustomerNo);
            //_salesOrder = App.Database.GetSalesOrderForCurrentWorkTicket(_customer);
            _salesOrder = App.Database.GetSalesOrder(workTicket, _customer);
        }

        public CustomerDetailsPageViewModel(App_Customer customer, App_WorkTicket workTicket)
        {
            _customer = customer;
            _customerContacts = App.Database.GetAppCustomerContacts(_customer.CustomerNo);
            //_salesOrder = App.Database.GetSalesOrderForCurrentWorkTicket(_customer);

            _salesOrder = App.Database.GetSalesOrder(workTicket, _customer);
        }

        public CustomerDetailsPageViewModel(App_Customer customer, App_ScheduledAppointment scheduledAppointment)
        {
            _customer = customer;
            _customerContacts = App.Database.GetAppCustomerContacts(_customer.CustomerNo);            
            _salesOrder = App.Database.GetSalesOrder(scheduledAppointment, _customer);
        }
    }
}
