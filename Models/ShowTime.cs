using System.ComponentModel.DataAnnotations;

namespace CRS.Models
{
    public class ShowTime
    {
        [Key]
        public int Id { get; set; }
        public string MovieName { get; set; }
        public DateTime DateAndTime { get; set; }
        public string HallName { get; set; }
        public string HallType { get; set; }
        



    }
}
