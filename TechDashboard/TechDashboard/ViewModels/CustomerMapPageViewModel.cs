﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sage.SData.Client;
using TechDashboard.Models;
using TechDashboard.Data;
using TechDashboard.Services;
using Xamarin.Forms.Maps;

namespace TechDashboard.ViewModels
{
    public class CustomerMapPageViewModel
    {
        Xamarin.Forms.Maps.Pin _customerLocationPin;
        public Xamarin.Forms.Maps.Pin CustomerLocationPin
        {
            get { return _customerLocationPin; }
        }

        //AR_Customer _customer;
        //public AR_Customer Customer
        //{
        //    get { return _customer; }
        //}

        // puke
        public CustomerMapPageViewModel()
        {
            // puke
            //_customer = App.Database.RetrieveCustomerFromCurrentWorkTicket();
            SetCustomerLocationPin();
        }

        public CustomerMapPageViewModel(Pin customerLocationPin)
        {
            // puke
            _customerLocationPin = customerLocationPin;
        }

        protected  void SetCustomerLocationPin()
        {
            Geocoder geoCoder = new Geocoder();

            string xamarinAddress = "91 Oak Glen Dr, Pequea, PA 17565";
            //Task<IEnumerable<Position>> geoCoderTask = (geoCoder.GetPositionsForAddressAsync(xamarinAddress));
            //geoCoderTask.Wait();

            //List<Position> approximateLocations = geoCoderTask.Result.ToList();
            //if (approximateLocations.Count < 1)
            //{
            //    return;
            //}
            

            _customerLocationPin = new Pin();
            _customerLocationPin.Position = new Position(39.909630, -76.299061); //approximateLocations[0];
            _customerLocationPin.Label = "Greg Hripto"; // _customer.CustomerName;
            _customerLocationPin.Address = xamarinAddress;
            
        }
    }
}
