using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SportStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            // this is going to help us on production
            context.Database.Migrate();
            if(context.Products.Any()) return;

            context.Products.AddRange(
                new Product { ProductId = 1, Name = "Kayak", Description = "A boat for one person", Category="Watersports", Price= 275},
                new Product { ProductId = 2, Name = "Lifejacket", Description = "Protective and fashionable", Category="Watersports", Price = 48.95m},
                new Product { ProductId = 3, Name = "Soccer ball", Description = "FIFA-approved size and weight", Category="Soccer", Price = 19.50m},
                new Product { ProductId = 4, Name = "Corner flags", Description = "Give your playing field a professional touch", Category="Soccer", Price = 19.50m},
                new Product { ProductId = 5, Name = "Stadium", Description = "Flat-packed, 35.000 seats stadium", Category="Soccer", Price = 79500},
                new Product { ProductId = 6, Name = "Thinking cap", Description = "Improve brain efficiency by 75%", Category="Chess", Price = 16},
                new Product { ProductId = 7, Name = "Unsteady chair", Description = "Secretly gives your opponnen a disadvantage", Category="Chess", Price = 29.95m},
                new Product { ProductId = 8, Name = "Human chess board", Description = "A fun game for the family", Category="Chess", Price = 75},
                new Product { ProductId = 9, Name = "Bling-Bling King", Description = "Gold-plated, diamond-studded King", Category="Chess", Price = 1200}
            );
            context.SaveChanges();
        }
    }
}