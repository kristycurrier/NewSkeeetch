using Skeeetch.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace Skeeetch.Data
{
    public class SkeeetchDbInitializer : CreateDatabaseIfNotExists<SkeeetchContext>
    {
        protected override void Seed(SkeeetchContext context)
        {
            var categories = new List<Category>
            {
                new Category{ID = 1, DisplayTerm = "Drinks", SearchTerm = "booze"},
                new Category{ID = 2, DisplayTerm = "Food", SearchTerm = "hungry"},
                new Category{ID = 3, DisplayTerm = "Art", SearchTerm = "creative"},
                new Category{ID = 4, DisplayTerm = "Chilling", SearchTerm = "relaxing"},
                new Category{ID = 5, DisplayTerm = "Active", SearchTerm = "excitement"}
            };

            categories.ForEach(k => context.Categories.Add(k));
            context.SaveChanges();
        }
    }
}