using Microsoft.AspNetCore.Mvc;
using SportBet.Models;

namespace SportBet.Repository
{
    public interface IMatchOddRepository
    {
        public IEnumerable<MatchOdd> GetMatchesOdds();

        public MatchOdd GetMatchOdds(string id);

        public void CreateMatchOdds([FromBody] MatchOdd matchOdds);

        public void UpdateMatchOdds([FromBody] MatchOdd matchOdds);

        public void DeleteMatchOdds(string id);

        public void Save();
    }
}
