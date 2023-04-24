using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ozarin.Shared
{
    [Table("t_table")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public class TTable
    {
        [Key]
        [Column("TableID", TypeName = "int(11)")]
        public int TableId { get; set; }

        [Column("TrialSeatID", TypeName = "int(11)")]
        public int TrialSeatId { get; set; }

        [Column("TableCountID", TypeName = "int(11)")]
        public int TableCountId { get; set; }

        [Column("Rank", TypeName = "int(11)")]
        public int? Rank { get; set; }

        [Column("Score", TypeName = "int(11)")]
        public int? Score { get; set; }

		[Column("ChipsChange", TypeName = "int(11)")]
		public int? ChipsChange { get; set; }

		[Column("YakitoriState", TypeName = "int(11)")]
		public int? YakitoriState { get; set; }

		[Column("TobiState", TypeName = "int(11)")]
		public int? TobiState { get; set; }

		[Column("YakumanState", TypeName = "int(11)")]
		public int? YakumanState { get; set; }

		[Column("YakumanBonus", TypeName = "int(11)")]
        public int? YakumanBonus { get; set; }

        [Column("LoginUserID")]
        public string LoginUserId { get; set; }
    }
}
