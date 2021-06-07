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
using BestBudget.Views;
using BestBudget.Models;
using SQLite;

namespace BestBudget.Views
{
    /// <summary>
    /// Interaction logic for PaycheckView.xaml
    /// </summary>
    public partial class PaycheckView : Window
    {
        public PaycheckView()
        {
            InitializeComponent();
            string db = (Application.Current as BestBudget.App).databasePath;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(db))
            {
                try
                {
                    List<Paycheck> paychecks = new List<Paycheck>();
                    string query = "SELECT * FROM Paycheck";
                    paychecks = sQLiteConnection.Query<Paycheck>(query);
                    paycheckAmount.Text = paychecks[0].PaycheckAmount.ToString();
                    paycheckOccurance.Text = paychecks[0].PaycheckOccurance.ToString();
                }
                catch (Exception)
                {
                    sQLiteConnection.CreateTable<Paycheck>();
                }
            }
        }

        private void addbutton_Click(object sender, RoutedEventArgs e)
        {
            BudgetMain budgetMain = new BudgetMain();

            string db = (Application.Current as BestBudget.App).databasePath;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(db))
            {
                try
                {
                    sQLiteConnection.DeleteAll<Paycheck>();
                    var insertData = new Paycheck
                    {
                       PaycheckAmount = Int32.Parse(paycheckAmount.Text),
                       PaycheckOccurance = Int32.Parse(paycheckOccurance.Text)
                    };
                    sQLiteConnection.InsertOrReplace(insertData);
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
            this.Visibility = Visibility.Hidden;
            budgetMain.ShowDialog();


        }
    }
}
