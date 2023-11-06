using Microsoft.AspNetCore.Mvc;
using SportBet.Models;

namespace SportBet.Repository
{
    public interface IMatchRepository
    {
        public IEnumerable<Match> GetMatches();

        public Match GetMatch(string id);

        public void CreateMatch(Match match);

        public void UpdateMatch([FromBody] Match match);

        public void DeleteMatch(string id);

        public void Save();
    }
}
