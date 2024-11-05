namespace CRS.Models
{
    public class Seat
    {
        public SeatState TheSeat { get; set; }
        public enum SeatState{
            Reserved,Empty,Unavaliable,NotExist
        }

    }
}
