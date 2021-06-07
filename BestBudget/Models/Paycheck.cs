using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BestBudget.Models
{
    public class Paycheck
    {
        public int PaycheckAmount { get; set; }
        public int PaycheckOccurance { get; set; }
    }
}
