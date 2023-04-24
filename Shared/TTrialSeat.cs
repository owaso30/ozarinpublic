using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ozarin.Shared
{
    [Table("t_trialseat")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public class TTrialSeat
    {
        [Key]
        [Column("TrialSeatID", TypeName = "int(11)")]
        public int TrialSeatId { get; set; }

        [Column("TrialID", TypeName = "int(11)")]
        public int TrialId { get; set; }

        [Column("SeatID", TypeName = "int(11)")]
        public int SeatId { get; set; }

        [Column("UserID", TypeName = "int(11)")]
        public int UserId { get; set; }

        [Column("ProgressMidway", TypeName = "int(11)")]
        public int? ProgressMidway { get; set; }

        [Column("LoginUserID")]
        public string LoginUserId { get; set; }
    }
}
