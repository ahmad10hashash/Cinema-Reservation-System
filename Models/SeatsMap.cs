using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CRS.Models
{
    
    public class SeatsMap : Seat 
    {
        [Key]
        public int Id { get; set; }
        public int ShowTimeID { get; set; }
        public string HallName { get; set; }
        public string MovieName { get; set; }
        public string HallType { get; set; }
        public DateTime DateAndTime { get; set; }
        
    }
}
