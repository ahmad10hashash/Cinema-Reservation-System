using System.ComponentModel.DataAnnotations;

namespace CRS.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        
        public string MovieName { get; set; }
        public string ClientName { get; set; }
        [EmailAddress]
        public string ClientEmail { get; set; }
        public string PhoneNumber { get; set; }
        
        public int SeatNumber { get; set; }
        public DateTime DateAndTime { get; set; }
        public string HallName { get; set; }
        
        

    }
}
