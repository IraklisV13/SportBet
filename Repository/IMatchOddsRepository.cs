using Microsoft.AspNetCore.Mvc;
using SportBet.Models;

namespace SportBet.Repository
{
    public interface IMatchOddsRepository
    {
        public IEnumerable<MatchOdds> GetMatchesOdds();

        public MatchOdds GetMatchOdds(string id);

        public void CreateMatchOdds([FromBody] MatchOdds matchOdds);

        public void UpdateMatchOdds([FromBody] MatchOdds matchOdds);

        public void DeleteMatchOdds(string id);

        public void Save();
    }
}
