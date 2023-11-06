using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportBet.DBContexts;
using SportBet.Models;

namespace SportBet.Repository
{
    public class MatchRepository : IMatchRepository
    {
        private readonly MatchContext _dbContext;

        public MatchRepository(MatchContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateMatch(Match match)
        {
            _dbContext.Add(match);
            Save();
        }

        public void DeleteMatch(string id)
        {
            var match = _dbContext.Matches.Find(id);
            _dbContext.Matches.Remove(match);           // handle nulls ???
            Save();
        }

        public Match GetMatch(string id)
        {
            return _dbContext.Matches.Find(id);

            // handle nulls ???
        }

        public IEnumerable<Match> GetMatches()
        {
            return _dbContext.Matches.ToList();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateMatch([FromBody] Match match)
        {
            _dbContext.Entry(match).State = EntityState.Modified;
            Save();
        }
    }
}
