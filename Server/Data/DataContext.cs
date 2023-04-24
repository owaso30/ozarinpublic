using System;
using Ozarin.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ozarin.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public virtual DbSet<TTable> TTables { get; set; }
        public virtual DbSet<TTrial> TTrials { get; set; }
        public virtual DbSet<TTrialSeat> TTrialSeats { get; set; }
        public virtual DbSet<TUser> TUsers { get; set; }
    }
}
