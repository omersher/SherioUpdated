using System;

namespace Model
{
    public class Review : BaseEntity
    {
        private User user;
        private Room room;
        private int rating;
        private string comment;
        private DateTime createdAt;

        public User User { get => user; set => user = value; }
        public Room Room { get => room; set => room = value; }
        public int Rating { get => rating; set => rating = value; }
        public string Comment { get => comment; set => comment = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }

        public override string ToString()
        {
            return base.ToString() + " | " +
                   $"Review: User = {User?.FullName}, Room = {Room?.RoomName}, " +
                   $"Rating = {Rating}, Comment = {Comment}";
        }
    }
}
