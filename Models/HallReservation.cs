using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CRS.Models
{
    public class HallReservation : Seat
    {
        [Key]
        public int HallResId { get; set; }
        public int HallId { get; set; }
        public string HallName { get; set; }

    }
}
