using InvestorsAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Numerics;

namespace InvestorsAPI.Database
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new InvestorDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<InvestorDbContext>>()))
            {
                if (context.Investors.Any())
                {
                    return; //Data was already added
                }

                context.Funds.AddRange(
                        new Fund
                        {
                            FundName = "Mauris LLP, Nullam Velit Fund",
                        },
                        new Fund
                        {
                            FundName = "Nullam Velit Fund",
                        },
                        new Fund
                        {
                            FundName = "Mauris LLP, Ligula Aenean Fund, Mauris Sit Amet Fund",
                        },
                        new Fund
                        {
                            FundName = "Ullamcorper Viverra Fund"
                        }
                    );

                context.Investors.AddRange(
                     new Investor
                     {
                         Name = "Keely Newman",
                         Phone = "1-786-738-4711",
                         Email = "in.magna@yahoo.com",
                         Country = "USA"
                     },
                     new Investor
                     {
                         Name = "Kimerly",
                         Phone = "(684) 842-2371",
                         Email = "non.lacina:outlook.org",
                         Country = "Spain"
                     },
                     new Investor
                     {
                         Name = "Sean Massey",
                         Phone = "(548) 250-4693",
                         Email = "pharetra.quisque.ac@outlook.edu",
                         Country = "Ireland"
                     },
                     new Investor
                     {
                         Name = "Nyssa Barr",
                         Phone = "(673) 581-3597",
                         Email = "odio@aol.couk",
                         Country = "Canada"
                     }
                 );

                context.SaveChanges();

                context.InvestorFunds.AddRange(
                    new InvestorFund { InvestorId = 1, FundId = 1 },
                    new InvestorFund { InvestorId = 2, FundId = 2 },
                    new InvestorFund { InvestorId = 3, FundId = 3 },
                    new InvestorFund { InvestorId = 4, FundId = 4 }
                    );

                context.SaveChanges();
            }
        }
    }
}

 
