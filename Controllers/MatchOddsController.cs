using Microsoft.AspNetCore.Mvc;
using SportBet.Models;
using SportBet.Repository;
using System.Transactions;

namespace SportBet.Controllers
{
    [Route("api/matchodds")]
    [ApiController]
    public class MatchOddsController : ControllerBase
    {
        private readonly IMatchOddsRepository _matchOddsRepository;

        public MatchOddsController(IMatchOddsRepository matchOddsRepository)
        {
            _matchOddsRepository = matchOddsRepository;
        }

        [HttpGet]
        public IActionResult GetMatchesOdds()
        {
            var matchesOdds = _matchOddsRepository.GetMatchesOdds();
            return new OkObjectResult(matchesOdds);
        }

        [HttpGet("{id}", Name = "GetMatchOdds")]
        public IActionResult GetMatchOdds(string id)
        {
            var matchOdds = _matchOddsRepository.GetMatchOdds(id);
            return new OkObjectResult(matchOdds);
        }

        [HttpPost]
        public IActionResult CreateMatchOdds([FromBody] MatchOdds matchOdds)
        {
            using (var scope = new TransactionScope())
            {
                _matchOddsRepository.CreateMatchOdds(matchOdds);
                scope.Complete();
                return CreatedAtAction(nameof(GetMatchOdds), new { id = matchOdds.Id }, matchOdds);
            }
        }

        [HttpPut]
        public IActionResult UpdateMatchOdds([FromBody] MatchOdds matchOdds)
        {
            if (matchOdds != null)
            {
                using (var scope = new TransactionScope())
                {
                    _matchOddsRepository.UpdateMatchOdds(matchOdds);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMatchOdds(string id)
        {
            _matchOddsRepository.DeleteMatchOdds(id);
            return new OkResult();
        }
    }
}
