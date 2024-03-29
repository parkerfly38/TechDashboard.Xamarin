﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

using TechDashboard.Models;

namespace TechDashboard.ViewModels
{
    public class ExpensesListPageViewModel : INotifyPropertyChanged
    {
        protected App_WorkTicket _workTicket;
        public App_WorkTicket WorkTicket
        {
            get { return _workTicket; }
        }

        protected ObservableCollection<App_Expense> _expensesList;
        public ObservableCollection<App_Expense> ExpensesList
        {
            get { return _expensesList; }
        }

        public bool IsWorkTicketSelected
        {
            get { return (_workTicket != null); }
        }

        public ExpensesListPageViewModel()
        {
            _expensesList = new ObservableCollection<App_Expense>();
            SetWorkTicket(App.CurrentWorkTicket);
        }

        public ExpensesListPageViewModel(JT_WorkTicket workTicket)
        {
            // puke
            _expensesList = new ObservableCollection<App_Expense>();
            SetWorkTicket(workTicket);
        }

        public void SetWorkTicket(JT_WorkTicket workTicket)
        {
            //_workTicket = workTicket; puke
            SetExpensesList();
        }

        public ExpensesListPageViewModel(App_WorkTicket workTicket)
        {
            _expensesList = new ObservableCollection<App_Expense>();
            SetWorkTicket(workTicket);
        }

        public ExpensesListPageViewModel(App_ScheduledAppointment scheduledAppointment)
        {
            _expensesList = new ObservableCollection<App_Expense>();
            SetWorkTicket(App.Database.GetWorkTicket(scheduledAppointment));
        }

        public void SetWorkTicket(App_WorkTicket workTicket)
        {
            _workTicket = workTicket;
            SetExpensesList();
        }

        public void SetWorkTicket(string formattedWorkTicketNumber)
        {            
            _workTicket = App.Database.GetWorkTicket2(formattedWorkTicketNumber);
            OnPropertyChanged("WorkTicket");
            SetExpensesList();
        }

        public List<App_ScheduledAppointment> GetScheduledAppointments()
        {
            return App.Database.GetScheduledAppointments();
        }

        public void RefreshExpensesList()
        {
            SetExpensesList();
        }

        protected void SetExpensesList()
        {
            _expensesList.Clear();
            try
            {
                foreach (App_Expense expense in App.Database.GetExpensesForWorkTicket2(_workTicket))
                {
                    _expensesList.Add(expense);
                }
            }
            catch
            {
                // empty
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
