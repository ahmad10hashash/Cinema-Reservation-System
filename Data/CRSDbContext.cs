using CRS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRS.Data
{
    public class CRSDbContext : IdentityDbContext
    {
        public CRSDbContext(DbContextOptions<CRSDbContext> options) : base(options) { }
        public DbSet <Hall> Halls { get; set; }
        public DbSet <HallReservation> HallsReservations { get; set; }
        public DbSet <Movie> Movies { get; set; }
        public DbSet <Reservation> Reservations { get; set; }
        public DbSet <ShowTime> ShowsTimes { get; set; }
        public DbSet<SeatsMap> SeatsMaps { get; set; }
    }
}
