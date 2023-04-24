using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ozarin.Shared
{
    [Table("t_trial")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public class TTrial
    {
        [Key]
        [Column("TrialID", TypeName = "int(11)")]
        public int TrialId { get; set; }
        
        [Column("TrialDateTime")]
        public DateTime? TrialDateTime { get; set; }

		[Column("StartReturn")]
		public string StartReturn { get; set; }

		[Column("BonusByRanking")]
		public string BonusByRanking { get; set; }

		[Column("ChipsPrice", TypeName = "int(11)")]
		public int? ChipsPrice { get; set; }

		[Column("YakitoriPrice", TypeName = "int(11)")]
		public int? YakitoriPrice { get; set; }

		[Column("TobiPrice", TypeName = "int(11)")]
		public int? TobiPrice { get; set; }

		[Column("YakumanPrice", TypeName = "int(11)")]
		public int? YakumanPrice { get; set; }

		[Column("LoginUserID")]
        public string LoginUserId { get; set; }
    }
}
