using System;

namespace Model
{
    public class RoomAvailability : BaseEntity
    {
        private Room room;
        private DateTime startDate;
        private DateTime endDate;

        public Room Room { get => room; set => room = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }

        public override string ToString()
        {
            return base.ToString() + " | " +
                   $"Room = {Room?.RoomName}, From = {StartDate:dd/MM/yyyy}, To = {EndDate:dd/MM/yyyy}";
        }
    }
}
