using System;

namespace Model
{
    public class Booking : BaseEntity
    {
        private User user;
        private Room room;
        private DateTime createdAt;
        private DateTime startDate;
        private DateTime endDate;
        private int adultCount;
        private int childCount;
        private string status;

        public User User { get => user; set => user = value; }
        public Room Room { get => room; set => room = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public int AdultCount { get => adultCount; set => adultCount = value; }
        public int ChildCount { get => childCount; set => childCount = value; }
        public string Status { get => status; set => status = value; }

        public override string ToString()
        {
            return base.ToString() + " | " +
                   $"Room = {Room?.RoomName}, " +
                   $"User = {User?.FullName}, " +
                   $"Start = {StartDate:dd/MM/yyyy}, " +
                   $"End = {EndDate:dd/MM/yyyy}, " +
                   $"Adults = {AdultCount}, Kids = {ChildCount}, " +
                   $"Status = {Status}";
        }
    }
}
