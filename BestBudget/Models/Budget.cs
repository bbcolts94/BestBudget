using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BestBudget.Models
{
    public class Budget
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Lender { get; set; }

        public int Payment { get; set; }
        public string PaymentAmount { get; set; }


        public string displayDate { get; set; }

        public string PaymentDate { get; set; }
        public int Occurance { get; set; }
    }
}
