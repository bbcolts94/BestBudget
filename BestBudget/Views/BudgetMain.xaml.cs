﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BestBudget.Models;
using System.Windows.Shapes;
using SQLite;
using System.Collections.ObjectModel;

namespace BestBudget.Views
{
    /// <summary>
    /// Interaction logic for BudgetMain.xaml
    /// </summary>
    public partial class BudgetMain : Window
    {
        public ObservableCollection<Budget> debt { get; set; }
        public BudgetMain()
        {
            InitializeComponent();
            string db = (Application.Current as BestBudget.App).databasePath;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(db))
            {
                try
                {
                    debt = new ObservableCollection<Budget>();
                    string query = "SELECT * FROM Budget";
                    List<Budget> budgetReturnedList = new List<Budget>();
                    budgetReturnedList = sQLiteConnection.Query<Budget>(query);

                    foreach(var item in budgetReturnedList) {
                        debt.Add(new Budget() { Id = item.Id, Lender = item.Lender, Payment = item.Payment, Occurance = item.Occurance });
                    }
                    FancyDataGrid.ItemsSource = debt;
                    query = "SELECT Name FROM NameGiven";
                    List<NameGiven> nameReturned = new List<NameGiven>();
                    nameReturned = sQLiteConnection.Query<NameGiven>(query);
                    NameDisplay.Text = nameReturned[0].Name;



                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        }
        private void Account_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objNameChange = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objNameChange.Show();
        }

        private void addbutton_Click(object sender, RoutedEventArgs e)
        {
            PushInfo objNameChange = new PushInfo(null);
            this.Visibility = Visibility.Hidden;
            objNameChange.Show();
        }

        private void FancyDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = sender as DataGridRow;
            var budgetDataFromRow = row.Item as Budget;
            PushInfo dataEntryView = new PushInfo(budgetDataFromRow);
            this.Visibility = Visibility.Hidden;
            dataEntryView.ShowDialog();
        }
    }
}
