using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportBet.DBContexts;
using SportBet.Models;

namespace SportBet.Repository
{
    public class MatchOddRepository : IMatchOddRepository
    {
        private readonly MatchContext _dbContext;

        public MatchOddRepository(MatchContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateMatchOdds(MatchOdd matchOdds)
        {
            _dbContext.Add(matchOdds);
            Save();
        }

        public void DeleteMatchOdds(string id)
        {
            var matchOdds = _dbContext.MatchOdds.Find(id);
            if (matchOdds != null)
            {
                _dbContext.MatchOdds.Remove(matchOdds);
                Save();
            }
        }

        public IEnumerable<MatchOdd> GetMatchesOdds()
        {
            return _dbContext.MatchOdds.ToList();
        }

        public MatchOdd GetMatchOdds(string id)
        {
            return _dbContext.MatchOdds.Find(id);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateMatchOdds([FromBody] MatchOdd matchOdds)
        {
            _dbContext.Entry(matchOdds).State = EntityState.Modified;
            Save();
        }
    }
}
