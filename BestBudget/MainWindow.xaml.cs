using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SQLite;
using BestBudget.Models;
namespace BestBudget
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        void Continue(object sender, RoutedEventArgs e)
        {
            string db = (Application.Current as BestBudget.App).databasePath;
            using(SQLiteConnection sQLiteConnection = new SQLiteConnection(db))
            {
                try
                {
                    var name = new NameGiven
                    {
                        Name = NameTextBox.Text
                    };
                    sQLiteConnection.Insert(name);

                } 
                    catch(Exception)
                {
                    sQLiteConnection.CreateTable<NameGiven>();
                    sQLiteConnection.CreateTable<Budget>();

                    var name = new NameGiven
                    {
                        Name = NameTextBox.Text
                    };
                    sQLiteConnection.Insert(name);
                }
            }

        }
    }
}
