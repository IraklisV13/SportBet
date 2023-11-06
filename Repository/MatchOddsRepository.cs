using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportBet.DBContexts;
using SportBet.Models;

namespace SportBet.Repository
{
    public class MatchOddsRepository : IMatchOddsRepository
    {
        private readonly MatchContext _dbContext;

        public MatchOddsRepository(MatchContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateMatchOdds(MatchOdds matchOdds)
        {
            _dbContext.Add(matchOdds);
            Save();
        }

        public void DeleteMatchOdds(string id)
        {
            var matchOdds = _dbContext.MatchOdds.Find(id);
            _dbContext.MatchOdds.Remove(matchOdds);           // handle nulls ???
            Save();
        }

        public IEnumerable<MatchOdds> GetMatchesOdds()
        {
            return _dbContext.MatchOdds.ToList();
        }

        public MatchOdds GetMatchOdds(string id)
        {
            return _dbContext.MatchOdds.Find(id);

            // handle nulls ???
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateMatchOdds([FromBody] MatchOdds matchOdds)
        {
            _dbContext.Entry(matchOdds).State = EntityState.Modified;
            Save();
        }
    }
}
