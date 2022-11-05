using System;
namespace BudgetTracker.Models
{
    public class Item
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public double Amount { get; set; }

        public string? Category { get; set; }

        public DateOnly DateAdded;

    }
}

