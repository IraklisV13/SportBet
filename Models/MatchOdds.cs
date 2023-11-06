using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportBet.Models
{
    [PrimaryKey(nameof(Id))]
    public class MatchOdds          // rename to MatchOdd ???
    {
        #region Properties

        public string Id { get; set; } = string.Empty;

        [ForeignKey(nameof(MatchId))]
        public string MatchId { get; set; } = string.Empty;

        public string Specifier { get; set; } = string.Empty;

        public double Odd { get; set; }

        #endregion
    }
}
