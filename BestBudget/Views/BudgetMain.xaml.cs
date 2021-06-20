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
                    int MonthlyIncome;
                    int MonthlyOut = 0;
                    debt = new ObservableCollection<Budget>();

                    string query = "SELECT * FROM Budget";
                    List<Budget> budgetReturnedList = new List<Budget>();
                    budgetReturnedList = sQLiteConnection.Query<Budget>(query);
                    
                    query = "SELECT Name FROM NameGiven";
                    List<NameGiven> nameReturned = new List<NameGiven>();
                    nameReturned = sQLiteConnection.Query<NameGiven>(query);
                    NameDisplay.Text = nameReturned[0].Name;
                    if(budgetReturnedList.Count != 0)
                    {

                        foreach(var item in budgetReturnedList) {
                            debt.Add(new Budget() { Id = item.Id, Lender = item.Lender, PaymentAmount = "$ " + item.Payment.ToString(), Occurance = item.Occurance }) ;

                            MonthlyOut = item.Payment * item.Occurance + MonthlyOut;

                        }
                        FancyDataGrid.ItemsSource = debt;

                        query = "SELECT * FROM Paycheck";
                        List<Paycheck> incomeReturned = new List<Paycheck>();
                        incomeReturned = sQLiteConnection.Query<Paycheck>(query);

                        MonthlyIncome = incomeReturned[0].PaycheckAmount * incomeReturned[0].PaycheckOccurance;
                        IncomeAmount.Text = "$ " + MonthlyIncome.ToString();






                        DateTime LastPaycheckDate = DateTime.Parse(incomeReturned[0].LastPayCheckDate);
                        DateTime nextPaycheckDate = LastPaycheckDate.AddDays(14);

                        int MoneyLeftThisPayperiod = MonthlyIncome / incomeReturned[0].PaycheckOccurance;

                        foreach(var item in budgetReturnedList)
                        {


                            DateTime paymentDate = DateTime.Parse($"{DateTime.Now.Month}/{Int32.Parse(item.PaymentDate)}/{DateTime.Now.Year}");

                            if(paymentDate < nextPaycheckDate && paymentDate > LastPaycheckDate)
                            {
                               int subtractedAmmt = MoneyLeftThisPayperiod - item.Payment;
                                MoneyLeftThisPayperiod = subtractedAmmt;
                            }
                        }

                        MoneyLeftAfterBills.Text = MoneyLeftThisPayperiod.ToString();



                        int monthlyNet = MonthlyIncome - MonthlyOut;
                        LeftOverCash.Text = monthlyNet.ToString();
                        if (monthlyNet > 0)
                        {
                            LeftOverCash.Foreground = new SolidColorBrush(Colors.Lime);

                        }
                        else
                        {
                            LeftOverCash.Foreground = new SolidColorBrush(Colors.Red);
                        }
                    }
                }
                catch (Exception ex)
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

        private void PaycheckButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            PaycheckView paycheckView = new PaycheckView();
            paycheckView.ShowDialog();
        }
    }
}
