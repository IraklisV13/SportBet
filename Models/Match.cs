using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using static SportBet.Models.Enum;

namespace SportBet.Models
{
    [PrimaryKey(nameof(Id))]
    public class Match
    {
        #region Properties

        [NotNull]
        [Required]
        [StringLength(64)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateOnly MatchDate { get; set; }

        public TimeOnly MatchTime { get; set; }

        public string? TeamA { get; set; }

        public string? TeamB { get; set; }

        public Sport Sport { get; set; }

        #endregion
    }
}
