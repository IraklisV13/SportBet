using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using SportBet.DBContexts;
using SportBet.Models;
using System.Transactions;
using static SportBet.Models.Enum;

namespace SportBet.Data
{
    public class DbInitializer
    {
        public static void Initialize(MatchContext context)
        {
            if (context == null) 
            {
                throw new ArgumentNullException(nameof(context), "Database context is null. Cannot initialize the database.");
            }

            // Check if database already exists else create it and initialize with data
            if (!(context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                context.Database.EnsureCreated();

                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        var matches = new Match[]
                        {
                            new Match
                            {
                                Id="1",
                                Description = "First Game",
                                MatchDate = DateOnly.FromDateTime(DateTime.Now),
                                MatchTime = TimeOnly.FromDateTime(DateTime.Now),
                                TeamA = "PAO",
                                TeamB = "OSFP",
                                Sport = Sport.Football
                            },
                            new Match
                            {
                                Id="2",
                                Description = "Second Game",
                                MatchDate = DateOnly.FromDateTime(DateTime.Now),
                                MatchTime = TimeOnly.FromDateTime(DateTime.Now),
                                TeamA = "OSFP",
                                TeamB = "PAO",
                                Sport = Sport.Basketball
                            }
                        };

                        var matchesOdds = new MatchOdd[]
                        {
                            new MatchOdd
                            {
                                Id = "1",
                                MatchId = "1",
                                Specifier = "X",
                                Odd = 2
                            },
                            new MatchOdd
                            {
                                Id = "2",
                                MatchId = "2",
                                Specifier = "1",
                                Odd = 5
                            }
                        };

                        foreach (Match m in matches)
                        {
                            context.Matches.Add(m);
                        }

                        foreach (MatchOdd mo in matchesOdds)
                        {
                            context.MatchOdds.Add(mo);
                        }

                        context.SaveChanges();

                        transaction.Complete();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException(ex.Message);
                    }
                }
            }
        }
    }
}
