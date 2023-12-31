﻿using Microsoft.AspNetCore.Mvc;
using SportBet.Models;
using SportBet.Repository;
using System.Transactions;

namespace SportBet.Controllers
{
    [Route("api/matches")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchRepository _matchRepository;

        public MatchController(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        [HttpGet]
        public IActionResult GetMatches()
        {
            var matches = _matchRepository.GetMatches();
            return new OkObjectResult(matches);
        }

        [HttpGet("{id}", Name = "GetMatch")]
        public IActionResult GetMatch(string id)
        {
            var match = _matchRepository.GetMatch(id);
            return new OkObjectResult(match);
        }

        [HttpPost]
        public IActionResult CreateMatch([FromBody] Match match)
        {
            using (var scope = new TransactionScope())
            {
                _matchRepository.CreateMatch(match);
                scope.Complete();
                return CreatedAtAction(nameof(GetMatch), new { id = match.Id }, match);
            }
        }

        [HttpPut]
        public IActionResult UpdateMatch([FromBody] Match match)
        {
            if (match != null)
            {
                using (var scope = new TransactionScope())
                {
                    _matchRepository.UpdateMatch(match);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMatch(string id)
        {
            _matchRepository.DeleteMatch(id);
            return new OkResult();
        }
    }
}
