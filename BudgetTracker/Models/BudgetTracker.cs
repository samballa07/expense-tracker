using System;
namespace BudgetTracker.Models
{
    public class BudgetTracker
    {
        private Dictionary<string, Item> itemsByMonth;

        public BudgetTracker()
        {
            this.itemsByMonth = new Dictionary<string, Item>();
        }
    }
}

