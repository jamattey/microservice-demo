using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDB
    {
        //This class is used to generate migrations in our SQLServer. For testing purposes only

        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var ServiceScope = app.ApplicationServices.CreateScope())
            {
                SeedData (ServiceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            //Pass raw data. Edit when integrated with SQLServer

            if (!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding Data.....");
                context.Platforms.AddRange(
                    new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Postgres", Publisher = "Open Source", Cost = "Free" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }


    }
}