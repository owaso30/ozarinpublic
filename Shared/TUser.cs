using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ozarin.Shared
{
    [Table("t_user")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public class TUser
    {
        [Key]
        [Column("UserID", TypeName = "int(11)")]
        public int UserId { get; set; }

        [Column("UserName")]
        [StringLength(5)]
        public string UserName { get; set; }

        [Column("LoginUserID")]
        public string LoginUserId { get; set; }
    }
}
