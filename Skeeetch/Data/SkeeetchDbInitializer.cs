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
                new Category{ID = 4, DisplayTerm = "Relaxation", SearchTerm = "relaxing"},
                new Category{ID = 5, DisplayTerm = "Active", SearchTerm = "excitement"},
                new Category{ID = 6, DisplayTerm = "Asian Food", SearchTerm = "asian"},
                new Category{ID = 7, DisplayTerm = "Mexican Food", SearchTerm = "mexican"},
                new Category{ID = 8, DisplayTerm = "Bar Food", SearchTerm = "bar"},
                new Category{ID = 9, DisplayTerm = "Upscale", SearchTerm = "upscale"},
                new Category{ID = 10, DisplayTerm = "Atmosphere", SearchTerm = "atmosphere"},
                new Category{ID = 11, DisplayTerm = "Modern", SearchTerm = "modern"},
                new Category{ID = 12, DisplayTerm = "Classic", SearchTerm = "classic"},
                new Category{ID = 13, DisplayTerm = "Late Night", SearchTerm = "late"},
                new Category{ID = 14, DisplayTerm = "Dancing", SearchTerm = "dancing"},
                new Category{ID = 15, DisplayTerm = "Karaoke", SearchTerm = "karaoke"},
                new Category{ID = 16, DisplayTerm = "Pool", SearchTerm = "pool"},
                new Category{ID = 17, DisplayTerm = "Kid Friendly", SearchTerm = "kids"},
                new Category{ID = 18, DisplayTerm = "Romactic", SearchTerm = "romantic"},
                new Category{ID = 19, DisplayTerm = "New", SearchTerm = "new"},
                new Category{ID = 20, DisplayTerm = "Italian Food", SearchTerm = "italian"},
                new Category{ID = 21, DisplayTerm = "Pizza", SearchTerm = "pizza"},
                new Category{ID = 22, DisplayTerm = "Indian Food", SearchTerm = "indian"},
                new Category{ID = 23, DisplayTerm = "Hookah Bar", SearchTerm = "hookah"},
                new Category{ID = 24, DisplayTerm = "Casino", SearchTerm = "casino"},
                new Category{ID = 25, DisplayTerm = "Mediterranean Food", SearchTerm = "mediterranean"},
            };

            categories.ForEach(k => context.Categories.Add(k));
            context.SaveChanges();
        }
    }
}