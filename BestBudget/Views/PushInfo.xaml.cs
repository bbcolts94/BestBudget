using BestBudget.Models;
using SQLite;
using System;
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
using System.Windows.Shapes;

namespace BestBudget.Views
{
    /// <summary>
    /// Interaction logic for PushInfo.xaml
    /// </summary>
    public partial class PushInfo : Window
    {
        private bool isCompleteButton;
        private int idNumber;
        public PushInfo(Budget budget)
        {
            InitializeComponent();
            if (budget != null)
            {
                idNumber = budget.Id;
                LenderName.Text = budget.Lender;
                Amount.Text = budget.Payment.ToString();
                Occurance.Text = budget.Occurance.ToString();
                completebutton.Content = "DELETE DEBT RECORD";
                isCompleteButton = false;
            } else
            {
                isCompleteButton = true;
                completebutton.Content = "COMPLETE";
            }
        }


        private void back_Click(object sender, RoutedEventArgs e)
        {
            closeWindow();
        }

        private void completebutton_Click(object sender, RoutedEventArgs e)
        {
            if(!isCompleteButton)
            {
                DeleteData();
            }
            else
            {
                InsertNewData();
            }
            
            closeWindow();
        }

        private void DeleteData()
        {
            string db = (Application.Current as BestBudget.App).databasePath;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(db))
            {
                try
                {
                    string query = $"DELETE FROM Budget WHERE Id = {idNumber}";
                    sQLiteConnection.Execute(query);
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        }

        private void InsertNewData()
        {
            string db = (Application.Current as BestBudget.App).databasePath;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(db))
            {
                try
                {
                    var insertData = new Budget
                    {
                        Lender = LenderName.Text,
                        Occurance = Int32.Parse(Occurance.Text),
                        Payment = Int32.Parse(Amount.Text),
                        PaymentDate = PaymentDate.Text
                    };
                    sQLiteConnection.Insert(insertData);
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        }
        private void closeWindow()
        {
            BudgetMain objNameChange = new BudgetMain();
            this.Visibility = Visibility.Hidden;
            objNameChange.Show();
        }
    }
}
