using Microsoft.AspNetCore.Mvc;
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
            //return matches == null ? NotFound() : new OkObjectResult(matches);
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




//[HttpGet]
//public async Task<IActionResult> GetMatches()
//{
//    var matches = await _context.Matches.ToListAsync();
//    return Ok(matches);
//}

////[HttpGet("{id}")]
////public async Task<IActionResult> GetMatch(string id)
////{
////    var match = await _context.Matches.FindAsync(id);

////    if (match == null)
////    {
////        return NotFound();
////    }

////    return Ok(match);
////}
////[HttpPost]
////public async Task<IActionResult> CreateMatch([FromBody] Match match)
////{
////    if (match == null)
////    {
////        return BadRequest();
////    }

////    _context.Matches.Add(match);
////    await _context.SaveChangesAsync();

////    return CreatedAtAction("GetMatch", new { id = match.Id }, match);
////}

////[HttpPut("{id}")]
////public async Task<IActionResult> UpdateMatch(string id, [FromBody] Match match)
////{
////    if (id != match.Id)
////    {
////        return BadRequest();
////    }

////    _context.Entry(match).State = EntityState.Modified;

////    try
////    {
////        await _context.SaveChangesAsync();
////    }
////    catch (DbUpdateConcurrencyException)
////    {
////        if (!_context.Matches.Any(e => e.Id == id))
////        {
////            return NotFound();
////        }
////        else
////        {
////            throw;
////        }
////    }

////    return NoContent();
////}
////[HttpDelete("{id}")]
////public async Task<IActionResult> DeleteMatch(string id)
////{
////    var match = await _context.Matches.FindAsync(id);

////    if (match == null)
////    {
////        return NotFound();
////    }

////    _context.Matches.Remove(match);
////    await _context.SaveChangesAsync();

////    return NoContent();
////}